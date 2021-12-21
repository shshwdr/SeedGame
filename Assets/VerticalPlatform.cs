using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    PlatformEffector2D effector;
    float waitTime = 0.3f;
    float currentWaitTimer = 0f;
    GameObject playerCollider;
    Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        playerCollider = GameObject.Find("seed");
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S))
        {
            collider.enabled = false;
            //playerCollider.layer = LayerMask.NameToLayer("PlayerIgnoreBranch");
            Invoke("changeLayerBack", 0.5f);
            //effector.rotationalOffset = 180f;
            //currentWaitTimer = waitTime;
        }
        //if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S))
        //{
        //    if (currentWaitTimer <= 0)
        //    {

        //    }
        //}
    }
    void changeLayerBack()
    {

        collider.enabled = true;
        //playerCollider.layer = LayerMask.NameToLayer("Player");
    }
}
