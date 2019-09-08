using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    public GameObject character;

    private bool holding;

    private void Start()
    {
        holding = false;
    }

    private void Update()
    {
        if (holding)
        {
            character.GetComponent<CharacterController>().Charge();
        }
    }

    public void OnAttackButtonPressed()
    {
        holding = true;
    }

    public void OnAttackButtonReleased()
    {
        if (character.GetComponent<CharacterController>().CanAction())
        {
            character.GetComponent<CharacterController>().Attack();
        }
        holding = false;
    }

    public void OnJumpButtonClick()
    {
        if (character.GetComponent<CharacterController>().CanAction())
        {
            character.GetComponent<CharacterController>().Jump();
        }
    }

    public void OnSurfButtonClick()
    {
        if (character.GetComponent<CharacterController>().CanAction())
        {
            character.GetComponent<CharacterController>().Surf();
        }
    }
}