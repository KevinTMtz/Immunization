using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject bullet;

    private float shootWaitTime;

    private bool ableToShoot;
    private float startTime;
    private float endTime;

    void Start()
    {
        shootWaitTime = 0.1f;
    }

    void Update()
    {   
        if (Time.time > endTime && ableToShoot == false)
            ableToShoot = true;

        if (Input.GetAxis("Fire3") == 1 && ableToShoot)
        {
            GameObject bulletInstantiated = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

            Rigidbody bulletRB = bulletInstantiated.GetComponent<Rigidbody>();
            bulletRB.AddForce(shootPoint.right * 40, ForceMode.Impulse);

            startTime = Time.time;
            endTime = startTime + shootWaitTime;
            ableToShoot = false;
        }
    }
}
