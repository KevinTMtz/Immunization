using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class BulletController : MonoBehaviourPunCallbacks, IPunObservable
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
    void Update()
    {
        if (pv.IsMine)
        {
            pv.RPC("RPC_syncTransform", RpcTarget.AllBuffered, pv.ViewID);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (pv.IsMine)
        {
            if (other.CompareTag("Enemy"))
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }

    [PunRPC]
    void RPC_syncTransform(int bulletId)
    {
        PhotonView bullet_pv = PhotonView.Find(bulletId);
        if (bullet_pv)
        {
            Transform new_transform = bullet_pv.gameObject.GetComponent<Transform>();
            transform.position = new_transform.position;
            transform.rotation = new_transform.rotation;

            Rigidbody bulletRB = bullet_pv.gameObject.GetComponent<Rigidbody>();
            bulletRB.AddForce(shootPoint.right * 40, ForceMode.Impulse);
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
