using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private float damage = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ZERO" || collision.gameObject.tag == "X")
        {
            gameObject.SetActive(false);
            collision.gameObject.GetComponent<MainCharacterController>().TakenDamage(damage);
        }
    }
}