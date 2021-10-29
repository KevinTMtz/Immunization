using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public GameObject[] targets;
    public GameObject playerPrefab;
    // TODO: Add component Photon Transform View and Photon View to player prefab 
    public GameObject pauseCanvas;
    public bool isPaused = false;

    public GameObject spawnPoint;

    public Transform[] enemySpawnPoints;
    public Spawner spawner;
    public int spawnLimit;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint");

        PhotonNetwork.Instantiate("Prefabs/" + playerPrefab.name, spawnPoint.transform.position, Quaternion.identity);

        pauseCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (!PhotonNetwork.IsMasterClient) return;

        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            StartCoroutine(SpawnEnemy(enemySpawnPoints[i]));

        }


    }

    IEnumerator SpawnEnemy(Transform spawnPoint)
    {
        for (int i = 0; i < spawnLimit; i++)
        {
            yield return new WaitForSeconds(Random.Range(10f, 20f));
            GameObject enemy = spawner.spawnAt(spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    void Update()
    {
        // TODO: Change to proper pause button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Pause();
        }
    }

    void Pause()
    {
        pauseCanvas.SetActive(isPaused);
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }
}
