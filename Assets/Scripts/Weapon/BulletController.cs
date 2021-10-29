using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class BulletController : MonoBehaviourPunCallbacks, IPunObservable
{
    public PhotonView pv;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        if (pv.IsMine)
        {
            pv.RPC("RPC_syncHealth", RpcTarget.AllBuffered, transform);
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
    void RPC_syncTransform(Transform new_transform)
    {
        transform.position = new_transform.position;
        transform.rotation = new_transform.rotation;
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
