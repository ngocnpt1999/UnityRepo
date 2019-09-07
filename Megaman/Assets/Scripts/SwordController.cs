﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = parent.transform.position;
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    public void Slash()
    {
        double speed;
        if (parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("combo1"))
        {
            speed = 1.5 / parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
        else
        {
            speed = 1.9 / parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }

        if (parent.GetComponent<SpriteRenderer>().flipX == false)
        {
            gameObject.transform.Translate(new Vector3((float)(speed * Time.deltaTime), 0, 0));
        }
        else
        {
            gameObject.transform.Translate(new Vector3((float)(-speed * Time.deltaTime), 0, 0));
        }
    }

    public void ResetPosition()
    {
        gameObject.transform.position = parent.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}