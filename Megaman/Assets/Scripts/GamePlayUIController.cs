using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    public GameObject character;
    public GameObject ultimateAttackButton;

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

    public void OnUltimateAttackButtonClick()
    {
        if (character.GetComponent<CharacterController>().CanAction())
        {
            if (!character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("surf") &&
                !character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("jump") &&
                !character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("falldown") &&
                !character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("skyattack"))
            {
                character.GetComponent<CharacterController>().UltimateAttack();
                ultimateAttackButton.GetComponent<ButtonController>().SetDelay(15);
            }
        }
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