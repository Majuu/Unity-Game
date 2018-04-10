using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace S3
{
    public class ApteczkaSpawner : NetworkBehaviour

    {
        public GameObject apteczkaPrefab;
        public int numberOfEnemies;

        public override void OnStartServer()
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-20.0f, 20.0f), -0.55f, Random.Range(-20.0f, 20.0f));
                Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 180.0f), 0);

                GameObject enemy = (GameObject)Instantiate(apteczkaPrefab, spawnPosition, spawnRotation);
                NetworkServer.Spawn(enemy);
            }
        }

    }
}