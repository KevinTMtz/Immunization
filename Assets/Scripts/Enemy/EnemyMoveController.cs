using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    void Start()
    {
        GameObject pointToGo = GameObject.Find("PointToGo");
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(pointToGo.transform.position);
    }
}
