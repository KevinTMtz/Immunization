using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    GameObject player;

    public GameObject bullet;
    public Transform shootPoint;

    private float shootWaitTime;

    private bool ableToShoot;
    private float startTime;
    private float endTime;
    
    void Start()
    {
        shootWaitTime = 1f;
        player = GameObject.Find("Player(Clone)");
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 10)
        {
            gameObject.transform.LookAt(player.transform.position);

            if (Time.time > endTime && ableToShoot == false)
                ableToShoot = true;

            if (ableToShoot) {
                GameObject bulletInstantiated = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

                Rigidbody bulletRB = bulletInstantiated.GetComponent<Rigidbody>();
                bulletRB.AddForce(shootPoint.right * 10, ForceMode.Impulse);

                startTime = Time.time;
                endTime = startTime + shootWaitTime;
                ableToShoot = false;
            }
        }
    }
}
