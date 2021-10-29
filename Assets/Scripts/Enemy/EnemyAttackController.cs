using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class EnemyAttackController : MonoBehaviourPunCallbacks
{
    GameObject player;

    public GameObject bullet;
    public Transform shootPoint;

    private float shootWaitTime;

    private bool ableToShoot;
    private float startTime;
    private float endTime;
    private PhotonView pv;
    private float playerDistance;
    private float closerPlayerDistance = 100000;
    private int closerPlayerId;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        shootWaitTime = 1f;
        player = GameObject.Find("Player(Clone)");
    }

    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (playerDistance < 10)
        {
            if (closerPlayerDistance != 100000 && playerDistance < closerPlayerDistance)
            {
                pv.RPC("RPC_SelectPlayer", RpcTarget.AllBuffered, playerDistance, player.GetComponent<PhotonView>().ViewID);
            }
        }

        if (pv.IsMine)
        {
            pv.RPC("RPC_AimPlayer", RpcTarget.AllBuffered);
            if (ableToShoot)
            {
                pv.RPC("RPC_ShootPlayer", RpcTarget.AllBuffered);
            }

        }


    }

    [PunRPC]
    void RPC_ShootPlayer()
    {
        GameObject bulletInstantiated = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        Rigidbody bulletRB = bulletInstantiated.GetComponent<Rigidbody>();
        bulletRB.AddForce(shootPoint.right * 10, ForceMode.Impulse);

        startTime = Time.time;
        endTime = startTime + shootWaitTime;
        ableToShoot = false;
    }

    [PunRPC]
    void RPC_SelectPlayer(float distance, int playerId)
    {
        closerPlayerDistance = distance;
        closerPlayerId = playerId;
    }

    [PunRPC]
    void RPC_AimPlayer()
    {
        Transform playerTransform = PhotonView.Find(closerPlayerId).gameObject.GetComponent<Transform>();
        gameObject.transform.LookAt(playerTransform.position);
        if (Time.time > endTime && ableToShoot == false)
            ableToShoot = true;
    }
}
