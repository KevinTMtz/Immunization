using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class BulletController : MonoBehaviourPunCallbacks
{
    public Transform shootPoint;
    public int userId;
    void Start()
    {
        StartCoroutine(DestroyIn(4f));
    }

    IEnumerator DestroyIn(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);

        }

    }

}
