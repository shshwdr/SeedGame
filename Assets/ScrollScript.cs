using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public float scrollSpeed = 5f;
    Vector2 startPosition;
    float length;
    float newPos;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        length = 18.92f;
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Time.deltaTime * scrollSpeed;
        float newValue = transform.position.x + dist;
        transform.position = new Vector3(newValue, transform.position.y, transform.position.z);
        if (newValue > startPosition.x + length) 
            transform.position = new Vector3(newValue - length, transform.position.y, transform.position.z);
        else if (newValue < startPosition.x - length) 
            transform.position = new Vector3(newValue + length, transform.position.y, transform.position.z);
    }
}
