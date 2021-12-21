using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveRadius;
    public float moveSpeed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //var playerLayer = LayerMask.NameToLayer("Player");
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, moveRadius, playerLayer);
        if (collision.GetComponent<PlayerMovement>())
        {
            var targetPosition = collision.transform.position;
            var dir = (targetPosition - transform.position).normalized;
            rb.MovePosition(transform.position + dir * Time.deltaTime * moveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            AudioManager.Instance.playInsect();
        }
    }
}
