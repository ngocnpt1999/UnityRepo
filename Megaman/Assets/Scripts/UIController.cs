using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button jumpButton;
    public Button attackButton;
    public GameObject target;

    private bool holding;

    private void Start()
    {
        holding = false;
    }

    public bool AttackButtonIsHolding()
    {
        return holding;
    }

    public void OnAttackButtonPressed()
    {
        holding = true;
    }

    public void OnAttackButtonReleased()
    {
        if (target.GetComponent<CharacterController>().CanAction())
        {
            target.GetComponent<CharacterController>().Attack();
        }
        holding = false;
    }

    public void OnJumpButtonClick()
    {
        if (target.GetComponent<CharacterController>().CanAction())
        {
            target.GetComponent<CharacterController>().Jump();
        }
    }
}