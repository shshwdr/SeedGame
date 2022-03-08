using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithCamera : MonoBehaviour
{
    Camera camera;
    float originalScale;
    float originalCameraSize;
    float currentScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        originalScale = transform.localScale.x;
        originalCameraSize = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scale =  camera.orthographicSize / originalCameraSize;
        //targetScale = scale;
        currentScale = scale;// Mathf.Lerp(currentScale, scale, 0.9f);
        transform.localScale = new Vector3(originalScale* currentScale, originalScale * currentScale, 1);
    }
}
