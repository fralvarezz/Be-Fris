using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FrisbieJuice_Shadow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale == Vector3.zero)
            Destroy(this.gameObject);
    }
}
