using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletXController : MonoBehaviour
{
    private float distance;

    public int speedOfBullet;

    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                gameObject.transform.Translate(Vector3.left * speedOfBullet * Time.deltaTime);
            }
            else
            {
                gameObject.transform.Translate(Vector3.right * speedOfBullet * Time.deltaTime);
            }
            distance += speedOfBullet * Time.deltaTime;
            if (distance >= speedOfBullet - 1)
            {
                distance = 0;
                gameObject.SetActive(false);
            }
        }
    }

    public void ActivateBullet()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (gameObject.tag == "plasma")
            {
                gameObject.SetActive(false);
            }
            collision.gameObject.SetActive(false);
        }
    }
}