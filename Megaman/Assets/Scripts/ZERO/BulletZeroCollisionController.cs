using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletZeroCollisionController : MonoBehaviour
{
    private float damage = 150;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().TakenDamage(damage);
        }
    }
}