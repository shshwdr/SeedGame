using Cinemachine;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : SerializableObject
{
    CharacterController2D controller;

    public Animator animator;
    public float runSpeed = 4f;
    public float undergroundSpeed = 4f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    Vector2 movement;
    bool jump = false;
    bool crouch = false;
    //public GameObject gameOverUI;
    public bool isDead;
    public bool isFullyDead;
    public bool isUnderground;
    Rigidbody2D rb;
    public Collider2D collider;
    public Collider2D colliderChild;
    public Collider2D colliderTopdown;

    public float footStepMakeSoundMinDistance;

    List<GameObject> activePositions;
    public CinemachineVirtualCamera cineCam;
    public GameObject spawnPositions;
    int currentSpawnPoint = 0;
    bool isSelectingSpawnPoint = false;
    bool isCheat;
    public bool gameFinished;
    public bool usingJoyStick = true;
    public VariableJoystick variableJoystick;
    public GameObject tutorialPanel;
    bool isTutorialHidden = false;
    bool hasMoved = false;
    bool hasJumped = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            usingJoyStick = true;
        }
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        EventPool.OptIn("clickGameOver", GameoverRespawn);
    }



    public override void Save(SerializedGame save)
    {
        if (isDead)
        {
            
            prepareSpawnSelection();
            save.playerPosition = activePositions[currentSpawnPoint].transform.position;
        }
        else
        {
            save.playerPosition = new SerializedVector(transform.position);
        }
        save.isPlayerUnderground = isUnderground;
        save.isTutorialHidden = isTutorialHidden;
    }

    public override void Load(SerializedGame save)
    {
        transform.position = save.playerPosition.GetPos();
        isUnderground = save.isPlayerUnderground;
        colliderTopdown.enabled = isUnderground;
        animator.SetBool("underground", isUnderground);
        if (isUnderground)
        {
            FModSoundManager.Instance.SetParam("Underground", 0.96f);
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        isTutorialHidden = save.isTutorialHidden;
        if (isTutorialHidden)
        {
            hideTutorial(0);
            hideTutorial(1);
        }
    }

    void GameoverRespawn()
    {
        SelectSpawnPoint();
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    isCheat = true;
        //}
        if (isDead)
        {
            if (isFullyDead)
            {
                //Respawn();
                if (Input.GetKeyDown(KeyCode.R) && !isSelectingSpawnPoint)
                {
                    SelectSpawnPoint();
                }
                if (isSelectingSpawnPoint)
                {
                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        currentSpawnPoint++;
                        updateCamera();
                    }
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        currentSpawnPoint--;
                        updateCamera();
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Respawn();
                    }
                }
            }
            
            return;
        }

        //if (isCheat || gameFinished)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Die();
                FullyDie();
                SelectSpawnPoint();
            }
        }



        //android
        if (usingJoyStick)
        {
            horizontalMove = variableJoystick.Horizontal; 
            verticalMove = variableJoystick.Vertical;
           // Debug.Log("horizontal " + horizontalMove + " " + verticalMove);
        }
        else
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            //Debug.Log("horizontal move " + horizontalMove);
            verticalMove = Input.GetAxisRaw("Vertical");
        }


        float speed = Mathf.Abs(horizontalMove);
        if (isUnderground)
        {
            speed = Mathf.Abs(horizontalMove) + Mathf.Abs(verticalMove);
            movement.x = horizontalMove;
            movement.y = verticalMove;

            movement = Vector2.ClampMagnitude(movement, 1);
        }
        if (speed >= 0.01)
        {
            hideTutorial(0);
        }
        animator.SetFloat("speed", speed);
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        };
        //if (Input.GetButtonDown("Crouch"))
        //{
        //    crouch = true;
        //}
        //else if (Input.GetButtonUp("Crouch"))
        //{
        //    crouch = false;
        //};
    }

    public void suicide()
    {

        Die();
        FullyDie();
        prepareSpawnSelection();
        Respawn();
    }

    public void hideTutorial(int i)
    {
        if(i == 0)
        {
            hasMoved = true;
        }
        if(i == 1)
        {

            hasJumped = true;
        }
        if(hasMoved && hasJumped)
        {
            isTutorialHidden = true;
            tutorialPanel.SetActive(false);
        }
    }
    public void Jump()
    {

        jump = true;
        hideTutorial(1);
    }
    public void OnLanding()
    {
        animator.SetBool("jump", false);
    }
    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        if (isUnderground)
        {

            //animator.SetBool("underground", true);
            var moveDis = movement * undergroundSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveDis);
            var characterController = GetComponent<CharacterController2D>();
            if (moveDis.magnitude > footStepMakeSoundMinDistance && !GetComponent<PlayerMovement>().isDead)
            {
                if (characterController.footstepTimer > characterController.footstepTime)
                {
                    characterController.OnStepEvent.Invoke();
                    characterController.footStepEmitter.Play();
                    characterController.footstepTimer = 0;
                }

                characterController.footstepTimer += Time.deltaTime;
            }
            //flip
        }
        else
        {

            controller.Move(horizontalMove*runSpeed * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }

    public void FullyDie()
    {
        isFullyDead = true;

        controller.footStepParamChanger.TriggerParameters();
    }



    public void Die(bool destoryPlayerCollider = true)
    {
        AudioManager.Instance.playDie();
        isDead = true;
        animator.SetTrigger("die");
        animator.SetBool("jump",false);
        if (destoryPlayerCollider)
        {
            rb.velocity = Vector2.zero;
            //collider.enabled = false;
            rb.gravityScale = 0;
            //rb.simulated = false;
        }
    }

    public void updateCamera()
    {
        if (currentSpawnPoint>= activePositions.Count)
        {
            currentSpawnPoint = 0;
        }
        if (currentSpawnPoint <0)
        {
            currentSpawnPoint = activePositions.Count-1;
        }
        cineCam.Follow = activePositions[currentSpawnPoint].transform;
    }

    public void SelectSpawnPoint()
    {
        Dialogues.Instance.hideGameOverText();

        Dialogues.Instance.showActionText("selectSpawn");
        isSelectingSpawnPoint = true;
        prepareSpawnSelection();
        updateCamera();
    }

    void prepareSpawnSelection()
    {
        activePositions = new List<GameObject>();
        currentSpawnPoint = 0;
        float closestDistance = 100000000;
        for (int i = 0; i < spawnPositions.transform.childCount; i++)
        {
            var go = spawnPositions.transform.GetChild(i).gameObject;
            if (go.active || isCheat)
            {
                activePositions.Add(go);
                if ((go.transform.position - transform.position).magnitude < closestDistance)
                {
                    closestDistance = (go.transform.position - transform.position).magnitude;
                    currentSpawnPoint = activePositions.Count - 1;
                }

            }
        }
    }
    public void Respawn()
    {
        AudioManager.Instance.playSeedRespawn();
        transform.position = activePositions[currentSpawnPoint].transform.position;
        isDead = false;
        isFullyDead = false;
        collider.enabled = true;
        rb.simulated = true;
        rb.gravityScale = 1;
        Dialogues.Instance.hideActionText();
        isSelectingSpawnPoint = false;
        cineCam.Follow = transform;
        isUnderground = false;
        FModSoundManager.Instance.SetParam("Underground", 0);

        animator.SetBool("underground", false);
        collider.enabled = true;
        colliderChild.enabled = true;
        colliderTopdown.enabled = false;
        animator.Rebind();
        animator.Update(0f);
    }
}
