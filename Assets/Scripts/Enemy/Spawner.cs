using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
public class Spawner : MonoBehaviourPunCallbacks
{
    public GameObject enemyPrefab;

    public GameObject spawnAt(Vector3 pos, Quaternion rot)
    {
        GameObject enemy = PhotonNetwork.Instantiate("Prefabs/" + enemyPrefab.name, pos, rot, 0);
        return enemy;
    }
}
