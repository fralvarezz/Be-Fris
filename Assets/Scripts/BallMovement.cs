using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public SineWave wave;
    float orignal;
    private void Start()
    {
        orignal = transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, orignal + wave.sine, transform.position.z);
    }
}
