using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class comboshaker : MonoBehaviour
{
    Vector3 orgPos;
    void Start()
    {
        orgPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.current.comboCounter > 0)
        {
            if (!DOTween.IsTweening(transform))
            {
                transform.DORotate(new Vector3(0, 0, Random.Range(-17, 17)), 0.045f);
            }
        }
        else
        {
            if ((!DOTween.IsTweening(transform)))
                transform.DORotate(Vector3.zero, 0.1f);
        }
    }
}
