using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    private bool canAttack;
    private float activeTime;
    private float delayTime;

    private const int dead = -1;
    private const int idle = 0;
    private const int fire = 20;

    public GameObject weapon;
    public float range;

    private float healthPoint = 100;

    // Start is called before the first frame update
    void Start()
    {
        if (CharacterSelectEvent.characterTag == "X")
        {
            target = GameObject.Find("Megaman_X").transform;
        }
        else
        {
            target = GameObject.Find("ZERO").transform;
        }
        canAttack = true;
        activeTime = 0;
        delayTime = 0;
        weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon.activeSelf)
        {
            if (gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                weapon.transform.Translate(Vector3.right * range * Time.deltaTime);
            }
            else
            {
                weapon.transform.Translate(Vector3.left * range * Time.deltaTime);
            }
            activeTime += Time.deltaTime;
            if (activeTime >= 1)
            {
                weapon.SetActive(false);
            }
        }
        else if (!weapon.activeSelf && !canAttack)
        {
            gameObject.GetComponent<Animator>().SetInteger("status", idle);
            delayTime += Time.deltaTime;
            if (delayTime >= 3)
            {
                delayTime = 0;
                canAttack = true;
            }
        }

        if (gameObject.transform.position.x - target.position.x >= -range &&
            gameObject.transform.position.x - target.position.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            weapon.GetComponent<SpriteRenderer>().flipX = true;
            if (canAttack)
            {
                gameObject.GetComponent<Animator>().SetInteger("status", fire);
            }
        }
        else if (gameObject.transform.position.x - target.position.x <= range &&
                 gameObject.transform.position.x - target.position.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            weapon.GetComponent<SpriteRenderer>().flipX = false;
            if (canAttack)
            {
                gameObject.GetComponent<Animator>().SetInteger("status", fire);
            }
        }
    }

    void Fire()
    {
        canAttack = false;
        activeTime = 0;
        weapon.transform.position = gameObject.transform.position;
        weapon.SetActive(true);
    }

    public void TakenDamage(float damage)
    {
        if (healthPoint > damage)
        {
            healthPoint -= damage;
        }
        else
        {
            healthPoint = 0;
            gameObject.SetActive(false);
        }
    }
}