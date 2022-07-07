using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKill : MonoBehaviour
{
    public SoundController sounds;
    void Start()
    {
        int rng = Random.Range(1,3);
        sounds.PlayRandomised("boom" + rng);
        Invoke("Kill", 0.5f);
    }

    void Kill()
    {
        Destroy(this.gameObject);
    }
}
