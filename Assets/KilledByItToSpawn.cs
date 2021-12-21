using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilledByItToSpawn : GameTrigger
{
    public GameObject[] spawnObject;
    public GameObject[] destroyObject;
    public string[] showDeathString;
    public bool shouldDestroyself = true;
    public int progressAmount = 0;
    public bool useCollider;
    public bool destoryPlayerCollider = true;
    bool triggered;
    PlayerMovement player;
    public bool shouldGameEnd;
    public GameObject gameoverHint;
    public bool makeSoundWhenKill;
    public GameObject imageToShow;
    public string plantName;



    // Update is called once per frame
    void Update()
    {
        
    }

    public override void trigger()
    {
        base.trigger();
        if (TriggersManager.Instance.IsTriggered(this))
        {
            return;
        }
        TriggersManager.Instance.trigger(this);
        PlantManager.Instance.unlockPlant(plantName);
        if (shouldDestroyself)
        {
            Destroy(gameObject);

        }
        else
        {
        }
        foreach (var go in destroyObject)
        {
            if (go && go.active)
            {
                go.SetActive(false);
            }
        }

        foreach (var ob in spawnObject)
        {

            if (ob && !ob.active)
            {
                ob.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!useCollider)
        {

            colliderPlayer(collision);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (useCollider)
        {

            colliderPlayer(collision.collider);
        }
    }

    IEnumerator fullyKill()
    {
        yield return new WaitForSeconds(1.5f);
        bool isSettingActive = false;
        foreach (var ob in spawnObject)
        {

            if (ob && !ob.active)
            {
                isSettingActive = true;
            }
        }
        if (isSettingActive)
        {

            PlantManager.Instance.addProgress(progressAmount);

        }
        if (!isSettingActive)
        {
            foreach (var dd in showDeathString)
            {
                if (dd == "increaseProgress")
                {
                    var newStrings = new string[showDeathString.Length - 1];
                    int i = 0;
                    foreach (var d in showDeathString)
                    {
                        if (d != "increaseProgress")
                        {
                            newStrings[i] = d;
                            i++;
                        }
                    }
                    showDeathString = newStrings;
                    break;

                }
            }
            
        }

        Dialogues.Instance.showGameOverText(showDeathString, imageToShow);
        trigger();


        SaveLoadManager.Instance.saveGame();

        player.FullyDie();
        if (shouldGameEnd)
        {
            player.gameFinished = true;
            gameoverHint.SetActive(true);
        }
    }

    void colliderPlayer(Collider2D collision)
    {
        PlayerMovement pm= collision.GetComponentInChildren<PlayerMovement>();
        if (!pm)
        {
            pm = collision.transform.parent.GetComponentInChildren<PlayerMovement>();
        }
        if (collision.GetComponent<PlayerMovement>() && !collision.GetComponent<PlayerMovement>().isDead)
        {
            collision.GetComponent<PlayerMovement>().Die(destoryPlayerCollider);
            player = collision.GetComponent<PlayerMovement>();
            if (makeSoundWhenKill)
            {
                AudioManager.Instance.playWater();
            }
            StartCoroutine(fullyKill());
        }
    }
}
