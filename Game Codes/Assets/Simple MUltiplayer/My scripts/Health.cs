using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace S3
{
    public class Health : NetworkBehaviour
    {
        public const int maxHealth = 100;
        [SyncVar(hook = "OnChangeHealth")] public int currentHealth = maxHealth;
        public RectTransform healthbar;
        public bool destroyOnDeath;
        private NetworkStartPosition[] spawnPoints;

        void Start()
        {
            if(isLocalPlayer)
            {
                spawnPoints = FindObjectsOfType<NetworkStartPosition>();

                    Camera.main.transform.position = this.transform.position;
                    Camera.main.transform.LookAt(this.transform.position);
                    Camera.main.transform.parent = this.transform;

            }
        }

        public void TakeDamage(int amount)
        {
            if (!isServer)
            {
                return;
            }

            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                if (destroyOnDeath)
                {
                    Destroy(gameObject);
                }
                else
                {
                    currentHealth = maxHealth;
                    RpcRespawn();

                }

            }
            if (currentHealth > 100)
            {

                currentHealth = 100;
            }
        }
        void OnChangeHealth(int Health)
        {

            healthbar.sizeDelta = new Vector2(currentHealth * 2, healthbar.sizeDelta.y);
        }

        [ClientRpc]
        void RpcRespawn()
        {
            if(isLocalPlayer)
            {
                Vector3 spawnPoint = Vector3.zero;

                if (spawnPoints != null && spawnPoints.Length > 0)
                {
                    spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
                }
                transform.position = spawnPoint;
            }
        }

    }
}