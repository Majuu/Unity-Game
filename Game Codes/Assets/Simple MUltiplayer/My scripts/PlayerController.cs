using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace S3
{
    public class PlayerController : NetworkBehaviour
    {

        public GameObject bulletPrefab;
        public Transform bulletSpawn;
        private Camera m_Camera;
        private Rigidbody rbody;

        private void Start()
        {
            if (isLocalPlayer)
            {
                Camera.main.transform.position = this.transform.position;
                Camera.main.transform.LookAt(this.transform.position);
                Camera.main.transform.parent = this.transform;
            }

        }



        /*void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "sciana_lewa" )  // or if(gameObject.CompareTag("YourWallTag"))
            {
                rbody.velocity = Vector3.zero;
                rbody.angularVelocity = Vector3.zero;
            }
        }
        */
        void OnCollisionEnter()
        {
            rbody.isKinematic = true;
        }
    // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CmdFire();
            }

        }

        [Command]


        void CmdFire()
        {
            //bullet from the prefab
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

            //velocity
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12.0f;

            //spawn bullet (client)

            NetworkServer.Spawn(bullet);

            //2s 
            Destroy(bullet, 2);

        }
        public override void OnStartLocalPlayer()
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
