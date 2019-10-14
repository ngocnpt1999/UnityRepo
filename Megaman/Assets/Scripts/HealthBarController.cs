using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBar;

    public MainCharacterController mainCharacter;

    // Update is called once per frame
    void Update()
    {
        float hp = mainCharacter.GetHP();
        float amount = (hp / 100) * 0.5f;
        healthBar.fillAmount = amount;
    }
}