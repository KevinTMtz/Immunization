using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class BulletController : MonoBehaviourPunCallbacks
{
    public Transform shootPoint;
    public int userId;
    private PhotonView pv;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        StartCoroutine(DestroyIn(4f));
    }

    IEnumerator DestroyIn(float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PhotonNetwork.Destroy(gameObject);

        }

    }

}
