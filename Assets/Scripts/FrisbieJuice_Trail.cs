using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FrisbieJuice_Trail : MonoBehaviour
{
    public float timeTo0 = 0.2f;
    public Ease scaleEase;
    public GameObject frisbieShadow;
    Transform daddyFrisbie;
    public Rigidbody rb;

    [Header("Timer")]
    public float currentTime;
    public float spwnTime;

    void Start()
    {
        daddyFrisbie = GetComponentInParent<Transform>();
    }

    void Update()
    {
        if (rb.velocity.magnitude > 2)
        {
            currentTime += Time.deltaTime;
            if (currentTime > spwnTime)
            {
                SpawnFrisbie();
                currentTime = 0;
            }
        }
    }

    void SpawnFrisbie()
    {
        GameObject shadow = Instantiate(frisbieShadow, daddyFrisbie.position, Quaternion.Euler(daddyFrisbie.rotation.eulerAngles));
        shadow.transform.DOScale(Vector3.zero, timeTo0).SetEase(scaleEase);
    }
}
