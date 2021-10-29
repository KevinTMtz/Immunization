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

    void Start()
    {
        pv = GetComponent<PhotonView>();
        shootWaitTime = 1f;
        player = GameObject.Find("Player(Clone)");
    }

    void Update()
    {
        if (pv.IsMine)
        {
            if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 10)
            {
                gameObject.transform.LookAt(player.transform.position);

                if (Time.time > endTime && ableToShoot == false)
                    ableToShoot = true;

                if (ableToShoot)
                {
                    photonView.RPC("RPC_ShootPlayer", RpcTarget.All);
                }
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
}
