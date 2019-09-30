using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = character.transform.position;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    public void Slash()
    {
        float range;
        if (character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("combo1"))
        {
            range = 1.8f;
        }
        else
        {
            range = 2.2f;
        }

        if (character.GetComponent<SpriteRenderer>().flipX == false)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + range,
                                                        gameObject.transform.position.y,
                                                        gameObject.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - range,
                                                        gameObject.transform.position.y,
                                                        gameObject.transform.position.z);
        }
    }

    public void ResetPosition()
    {
        gameObject.transform.position = character.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
        }
    }
}