using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnCooldown;
    private float nextSpawnTime;

    public GameObject enemy;

    public Transform[] spawnPoints;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time>nextSpawnTime)
        {
            nextSpawnTime=Time.time+spawnCooldown;


            Transform randomSpawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];

            Instantiate(enemy, randomSpawnPoint.position+new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0), Quaternion.identity);
        }
    }
}
