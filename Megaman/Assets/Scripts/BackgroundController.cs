using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject character;
    public Vector3 minPosition;
    public Vector3 maxPosition;

    private float offset;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (character.GetComponent<CharacterController>().IsMoveRight())
        {
            offset = character.transform.position.x - gameObject.transform.position.x;
            if (offset > 0)
            {
                float posX = gameObject.transform.position.x + offset;
                if (posX > maxPosition.x)
                {
                    posX = maxPosition.x;
                }
                gameObject.transform.position = new Vector3(posX, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
        else if (character.GetComponent<CharacterController>().IsMoveLeft())
        {
            offset = character.transform.position.x - gameObject.transform.position.x;
            if (offset < 0)
            {
                float posX = gameObject.transform.position.x + offset;
                if (posX < minPosition.x)
                {
                    posX = minPosition.x;
                }
                gameObject.transform.position = new Vector3(posX, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
    }
}
