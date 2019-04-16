using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollisionKnockback : MonoBehaviour
{

    public PlayerController myController;
    public GameObject collisionExplosion;

    private void Awake()
    {
        myController = GetComponentInParent<PlayerController>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("armour"))
        {
            myController.applyKnockback = true;
            myController.knockbackCollisionObject = collision.gameObject.GetComponentInParent<PlayerController>().gameObject;
            collision.gameObject.GetComponentInParent<PlayerController>().applyKnockback = true;
            collision.gameObject.GetComponentInParent<PlayerController>().knockbackCollisionObject = this.GetComponentInParent<PlayerController>().gameObject;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("player"))){
            //StartCoroutine(slowMo(0.06f, 0.3f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.gameObject.layer == LayerMask.NameToLayer("armour"))&& collision.gameObject != this.gameObject)
        {
            //StartCoroutine(slowMo(0.06f, 0.3f));
            //myController.isLethal = false;
            Instantiate(collisionExplosion, transform.position, Quaternion.identity);
        }
    }
    

    IEnumerator slowMo(float multiplier, float duration)
    {
        float timer = 0f;
        Time.timeScale = multiplier;
        Time.fixedDeltaTime = 1f / 60f * Time.timeScale;
        while (timer < duration * multiplier)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        if (timer >= duration * multiplier)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 1f / 60f * Time.timeScale;
        }
    }
}
