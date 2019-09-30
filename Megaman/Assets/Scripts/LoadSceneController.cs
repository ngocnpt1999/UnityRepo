using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objects;
        if (CharacterSelectEvent.characterTag == "X")
        {
            objects = GameObject.FindGameObjectsWithTag("ZERO");
        }
        else
        {
            objects = GameObject.FindGameObjectsWithTag("X");
        }
        foreach (var it in objects)
        {
            it.SetActive(false);
        }
    }
}