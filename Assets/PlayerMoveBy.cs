using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;

public class PlayerMoveBy : MonoBehaviour
{
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
        if (collision.GetComponent<PlayerMovement>()) { 
            GetComponent<RandomWind>().interact();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.GetComponent<PlayerMovement>())
        {
            GetComponent<RandomWind>().interact();
        }
    }
}
