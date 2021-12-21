using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerToShowAction : MonoBehaviour
{
    public string actionTitle;
    public bool isGameover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            if (isGameover)
            {

                Dialogues.Instance.showGameOverText( actionTitle);
            }
            else
            {
                Dialogues.Instance.showActionText(actionTitle);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            if (isGameover)
            {

                Dialogues.Instance.hideGameOverText();
            }
            else
            {
                Dialogues.Instance.hideActionText();
            }
        }
    }
}
