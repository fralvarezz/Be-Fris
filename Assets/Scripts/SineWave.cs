using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    public float sine;
    [SerializeField] float frequency = 1f;
    [SerializeField] float magnitude = 50f;
    private void FixedUpdate()
    {
        sine = Mathf.Sin(Time.time * frequency) * magnitude;
    }
}
