using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundGenerator : MonoBehaviour
{
    public GameObject undergroundObject;
    public GameObject uppergroundObject;
    public Transform spawnPosition;
    public bool toUnderground;
    bool isTriggering;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        bool isGetDownKey = false;
        bool isGetUpKey = false;
        if (!player)
        {
            return;
        }
        if (player.GetComponent<PlayerMovement>().usingJoyStick)
        {
            isGetDownKey = player.GetComponent<PlayerMovement>().variableJoystick.Vertical < -0.5f;
        }
        else
        {
            isGetDownKey = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        }

        if (player.GetComponent<PlayerMovement>().usingJoyStick)
        {
            isGetUpKey = player.GetComponent<PlayerMovement>().variableJoystick.Vertical >0.5f;
        }
        else
        {
            isGetUpKey = (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow));
        }

        if (toUnderground && isTriggering && isGetDownKey)
        {
            player.transform.position = spawnPosition.position;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<PlayerMovement>().isUnderground = true;

            FModSoundManager.Instance.SetParam("Underground", 0.96f);
            player.GetComponent<PlayerMovement>().animator.SetBool("underground", true);
            player.GetComponent<PlayerMovement>().animator.SetBool("jump", false);
            player.GetComponent<PlayerMovement>().collider.enabled = true;
            player.GetComponent<PlayerMovement>().colliderChild.enabled = true;
            player.GetComponent<PlayerMovement>().colliderTopdown.enabled = false;
        }
        else if (!toUnderground && isTriggering && isGetUpKey)
        {

            player.transform.position = spawnPosition.position;
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
            player.GetComponent<PlayerMovement>().isUnderground = false;

            FModSoundManager.Instance.SetParam("Underground", 0);
            player.GetComponent<PlayerMovement>().animator.SetBool("underground", false);
            player.GetComponent<PlayerMovement>().collider.enabled = true;
            player.GetComponent<PlayerMovement>().colliderChild.enabled = true;
            player.GetComponent<PlayerMovement>().colliderTopdown.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement pm = collision.GetComponent<PlayerMovement>();
        if (!pm)
        {
            pm = collision.transform.parent.GetComponent<PlayerMovement>();
        }
        if (pm)
        {
            isTriggering = true;
            player = pm.gameObject;
            if (toUnderground)
            {

                Dialogues.Instance.showActionText("toUnderground");
            }
            else
            {

                Dialogues.Instance.showActionText("toUpperground");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            isTriggering = false;
            Dialogues.Instance.hideActionText();
        }
    }

}
