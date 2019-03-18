using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTarget : MonoBehaviour
{
    public Spawner mySpawner;
    public GameObject explosion;
    bool canExplode = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pointy1") || collision.CompareTag("pointy2"))
        {
            mySpawner.newTarget();
        }
    }

    public void TriggerExplosion()
    {
        if (canExplode)
        {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, -.5f), Quaternion.identity);
            canExplode = false;
        }

    }
}
