using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    public GameObject character;
    public GameObject ultimateButton;

    private bool holding;

    private void Start()
    {
        holding = false;
    }

    private void Update()
    {
        if (holding)
        {
            character.GetComponent<MainCharacterController>().Charge();
        }
    }

    public void OnFireButtonClick()
    {
        if (character.GetComponent<MainCharacterController>().CanAction())
        {
            character.GetComponent<MainCharacterController>().Fire();
        }
    }

    public void OnAttackButtonPressed()
    {
        holding = true;
    }

    public void OnAttackButtonReleased()
    {
        if (character.GetComponent<MainCharacterController>().CanAction())
        {
            character.GetComponent<MainCharacterController>().MeleeAttack();
        }
        holding = false;
    }

    public void OnUltimateAttackButtonClick()
    {
        if (character.GetComponent<MainCharacterController>().CanAction())
        {
            if (!character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("surf") &&
                !character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("jump") &&
                !character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("falldown") &&
                !character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("skyattack"))
            {
                character.GetComponent<MainCharacterController>().UltimateAttack();
                ultimateButton.GetComponent<ButtonController>().SetDelay(15);
            }
        }
    }

    public void OnUltimateFireButtonClick()
    {
        if (character.GetComponent<MainCharacterController>().CanAction())
        {
            if (!character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("surf"))
            {
                character.GetComponent<MainCharacterController>().UltimateFire();
                ultimateButton.GetComponent<ButtonController>().SetDelay(5);
            }
        }
    }

    public void OnJumpButtonClick()
    {
        if (character.GetComponent<MainCharacterController>().CanAction())
        {
            character.GetComponent<MainCharacterController>().Jump();
        }
    }

    public void OnSurfButtonClick()
    {
        if (character.GetComponent<MainCharacterController>().CanAction())
        {
            character.GetComponent<MainCharacterController>().Surf();
        }
    }
}