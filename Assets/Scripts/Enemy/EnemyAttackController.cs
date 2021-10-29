using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player(Clone)");
    }

    void Update()
    {
        
    }
}
