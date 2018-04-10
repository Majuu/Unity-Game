using System.Collections;
using UnityEngine;

namespace S3
{
    public class Bullet : MonoBehaviour
    {

        // Update is called once per frame
        void OnCollisionEnter(Collision collision)
        {
            GameObject hit = collision.gameObject;
            Health health = hit.GetComponent<Health>();

            if(health !=null)
            {
                health.TakeDamage(10);

            }
            Destroy(gameObject);
        }
    }
}