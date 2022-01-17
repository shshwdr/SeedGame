using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogues : Singleton<Dialogues>
{
    static public Dictionary<string,string> dialogues = new Dictionary<string, string>()
    {

        {"start","The seed wants to spread throughout the world. Help it reach its goal.\n Use arrow key to move." },

        {"killedByChest","The seed has been pierced by a chestnut." },
        {"killedByInsect","The seed has been eaten by an insect." },
        {"killedByWater","The seed has drowned in the water." },
        {"killedByHoe","The seed has been cut by a hoe." },
        {"killedByBird","The seed has been eaten by a bird." },
        {"winGame","The seed found a place next to the large tree. \nSoon the sprout will grow up and produce more and more seeds." },

        {"spawnNormalTree","\nIts death is not in vain, as it will grow into grass. Take advantage of that to make a journey throughout this world" },
        {"spawnBlossom","\nMore energy bursts from the seed. This time it's more than just grass." },
        {"spawnFlower","\nThe golden flower will shine into every dark corner of the world." },
        {"spawnRoot","The seed has fertilized the ground. You can now explore underground." },
        {"spawnFern","\nWith the limited amount of oxygen in this cave, the seed has grown into a fern." },
        {"spawnOnCliff","\nIts death is not in vain. The bird takes it to the cliff and drops it there, the seed starts growing into a vine on cliff." },
        {"spawnLotus","\nIts death is not in vain, as it has grown into various lotus leaves." },
        {"spawnWheat","\nIts death is not in vain, as it has grown into wheat and is now being farmed by humans." },

        {"increaseProgress","\nThe progress of spreading throughout the world has increased." },
        {"toRestart","\nPress R to respawn." },
        {"keepPlaying","\nThanks for playing. You may keep exploring the game." },
        {"toUnderground","Down arrow\n\nGo underground" },
        {"toUpperground","Up arrow\n\nGo to the surface" },
        {"selectSpawn","Left or Right to select spawn point.\nSpace to respawn" },


        {"hintForRoot","The ground here looks barren..." },

        {"dieUnderground","The seed can not sprout because it is too deep." },
        {"fertilize","The soil is fertilized and a root can now grow better... There might be more space to explore" },



        {"progressFull","The seed has now completed its journey, as it has spread throughout the world. Thanks for helping it!" },



        {"hintDialog","Do you want to watch ads to get a hint?" },
        {"thanksDialog","Thanks For supporting us!" },
        {"supportDialog","Do you want to watch ads to support us?" },
        {"credits","Programmer: Flavedo\nArtist:Sealcat\nComposer:Dieck" },
        {"restart","Do you want to clear your previous data and restart the game?" },

    };

    public Text actionText;
    public Text gameoverText;
    public Text hintText;

    GameObject imageToShowBefore;

    public Button gameOverButton;

    public GameObject joystick;
    public GameObject jumpButton;

    public void showText(Text te, string[] dialogTitles)
    {
        string dialogTitle = "";
        int i = 0;
        foreach(var d in dialogTitles)
        {

            if (!dialogues.ContainsKey(d))
            {
                te.gameObject.SetActive(true);
                te.text = "THIS IS A BUG! action title " + d + "DOES NOT EXISTTTTTT!!!";
                return;
            }
            dialogTitle += dialogues[d];
            if(i != dialogTitles.Length - 1)
            {

                dialogTitle += "\n";
            }
            i++;
        }
        if (te.transform.parent. gameObject.active)
        {
            te.text = "THIS IS A BUG! Action text " + dialogTitle + " was active!!!";
        }
        te.transform.parent.gameObject.SetActive(true);
        te.text = dialogTitle;
    }

    public void hideText(Text te)
    {
        if (!te.transform.parent.gameObject.active)
        {
            te.transform.parent.gameObject.SetActive(true);
            te.text = "THIS IS A BUG! Action text was not active.";
        }
        te.transform.parent.gameObject.SetActive(false);
    }
    public void showActionText(string dialogTitle)
    {
        showText(actionText, new string[] { dialogTitle });
    }

    public void hideActionText()
    {
        hideText(actionText);
    }
    public void showGameOverText(string dialogTitle)
    {
        showGameOverText(new string[] { dialogTitle });
    }
    public void showGameOverText(string[] dialogTitle, GameObject imageToShow = null)
    {
        showText(gameoverText, dialogTitle);
        joystick.SetActive(false);
        jumpButton.SetActive(false);
        if (imageToShow)
        {
            imageToShow.SetActive(true);
            imageToShowBefore = imageToShow;
        }
    }

    public void hideGameOverText()
    {
        AudioManager.Instance.playEndMessage();
        hideText(gameoverText);
        joystick.SetActive(true);
        jumpButton.SetActive(true);
        if (imageToShowBefore)
        {
            imageToShowBefore.SetActive(false);

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameOverButton.onClick.AddListener(delegate {
            EventPool.Trigger("clickGameOver");
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
