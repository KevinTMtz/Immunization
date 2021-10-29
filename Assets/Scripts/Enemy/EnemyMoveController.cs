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
    public int health = 10;
    public PhotonView pv;

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
        CheckHealth();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            health--;
            if (health < 0)
            {
                health = 0;
            }
            pv.RPC("RPC_syncHealth", RpcTarget.AllBuffered, health);
        }
    }

    void CheckHealth()
    {
        if (isDead || health <= 0)
        {
            isDead = true;
            // Set collider enable to false
            // Set dead animation
            pv.RPC("RPC_isDead", RpcTarget.AllBuffered);
            PhotonNetwork.Destroy(gameObject);
        }
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
            stream.SendNext(health);
        }
        else
        {
            syncPos = (Vector3)stream.ReceiveNext();
            syncRot = (Quaternion)stream.ReceiveNext();
            health = (int)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void RPC_isDead()
    {
        isDead = true;
        // Change collide
        // Set animation
    }

    [PunRPC]
    void RPC_syncHealth(int newValue)
    {
        health = newValue;
    }
}
