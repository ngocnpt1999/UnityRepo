using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int rotation;
    private float distance;

    public int speedOfBullet;

    // Start is called before the first frame update
    void Start()
    {
        rotation = 0;
        distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            foreach (Transform bullet in transform)
            {
                Vector3 direction = Quaternion.AngleAxis(rotation, Vector3.forward) * Vector3.right;
                bullet.Translate(direction * speedOfBullet * Time.deltaTime);
                rotation += 15;
            }
            rotation = 0;
            distance += speedOfBullet * Time.deltaTime;
            if (distance >= speedOfBullet + 1)
            {
                distance = 0;
                gameObject.transform.position = new Vector3(0, 0, gameObject.transform.position.z);
                foreach (Transform bullet in transform)
                {
                    bullet.position = new Vector3(0, 0, bullet.position.z);
                }
                gameObject.SetActive(false);
            }
        }
    }

    public void ActivateBullet()
    {
        gameObject.SetActive(true);
    }
}