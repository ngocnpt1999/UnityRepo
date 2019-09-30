using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectEvent : MonoBehaviour
{
    public static string characterTag;

    public void OnButtonClick()
    {
        characterTag = gameObject.tag;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}