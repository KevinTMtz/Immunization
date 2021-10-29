using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class WeaponController : MonoBehaviourPunCallbacks
{
    public Transform shootPoint;
    public GameObject bullet;

    private float shootWaitTime;

    private bool ableToShoot;
    private float startTime;
    private float endTime;
    private PhotonView pv;

    void Start()
    {
        shootWaitTime = 0.1f;
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pv.IsMine)
        {
            if (Time.time > endTime && ableToShoot == false)
                ableToShoot = true;

            if (Input.GetAxis("Fire3") == 1 && ableToShoot)
            {
                photonView.RPC("RPC_Shoot", RpcTarget.All);
            }
        }

    }

    [PunRPC]
    void RPC_Shoot()
    {
        GameObject bulletInstantiated = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        Rigidbody bulletRB = bulletInstantiated.GetComponent<Rigidbody>();
        bulletRB.AddForce(shootPoint.right * 30, ForceMode.Impulse);
        bulletInstantiated.GetComponent<BulletController>().shootPoint = shootPoint;

        startTime = Time.time;
        endTime = startTime + shootWaitTime;
        ableToShoot = false;
    }
}
