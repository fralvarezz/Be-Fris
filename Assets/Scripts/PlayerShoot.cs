using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private GameObject _frisbee;
    private Frisbee _frisbeeScript;
    private bool _firstTime;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _frisbee = GameObject.FindGameObjectWithTag("Frisbee");
        _frisbeeScript = _frisbee.GetComponent<Frisbee>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_firstTime)
            {
                _firstTime = true;
                GameManager.current.StartGame();
            }
            _frisbeeScript.Action();
        }
    }
}
