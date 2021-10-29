using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class EnemyMoveController : MonoBehaviourPunCallbacks, IPunObservable
{
    private GameObject target;
    public bool isAttacking = false;
    public bool isDead = false;

    private Vector3 syncPos = Vector3.zero;
    private Quaternion syncRot = Quaternion.identity;

    private NavMeshAgent agent;

    void Awake()
    {
        syncPos = transform.position;
        syncRot = transform.rotation;
    }

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        if (PhotonNetwork.IsMasterClient)
        {

            GameObject pointToGo = GameObject.Find("PointToGo");
            agent.SetDestination(pointToGo.transform.position);
        }
        else
        {
            agent.enabled = false;
        }
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SyncTransform();
            return;
        }

        // TODO: Check if target is still available
    }


    void SyncTransform()
    {
        transform.position = Vector3.Lerp(transform.position, syncPos, 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, syncRot, 0.1f);
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
            syncPos = (Vector3)stream.ReceiveNext();
            syncRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
