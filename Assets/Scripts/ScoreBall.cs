using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreBall : MonoBehaviour
{
    public bool spawned = true;
    public int activeStage = 1;
    [Header("Juice")]
    public float killStr = 1f;
    public float killtime = 0.2f;
    public Ease killEase;
    public GameObject particle;

    private void OnCollisionEnter(Collision other)
    {
        if (spawned)
        {
            if (other.gameObject.CompareTag("Frisbee"))
            {
                //Debug.Log("I've been hit");
                StartCoroutine(KillFrisbie());
                GameManager.current.BallHit();
            }
        }
    }

    IEnumerator KillFrisbie()
    {
        Camera.main.DOShakeRotation(0.1f, 20f, 5, 10, true);
        Instantiate(particle, transform.position, transform.rotation);
        DOTween.Sequence()
            .Append(transform.DOPunchScale(Vector3.one * killStr, killtime, 4, 0)).SetEase(killEase);
        //.Append(transform.DOScale(Vector3.zero, 0.1f));
        yield return new WaitForSeconds(killtime * 1.001f);
        Destroy(this.gameObject);
    }

    

}
