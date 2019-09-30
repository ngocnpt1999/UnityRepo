using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ZERO" || collision.gameObject.tag == "X")
        {
            collision.gameObject.GetComponent<MainCharacterController>().TakenDamage(10);
            gameObject.SetActive(false);
        }
    }
}