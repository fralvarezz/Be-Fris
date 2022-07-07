using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicController : MonoBehaviour
{
    public static MusicController current;

    private SoundController _soundController;

    // Start is called before the first frame update
    void Awake()
    {
        if (current == null)
            current = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        _soundController = GetComponent<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartPlaying()
    {
        _soundController.Play("Music");
    }

}
