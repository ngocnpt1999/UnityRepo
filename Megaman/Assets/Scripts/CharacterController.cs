using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private double delayTime;
    private double maxDelayTime;
    private double chargeTime;
    private bool canAction;

    private int jumpCount;

    private const int idle = 0;
    private const int move = 1;
    private const int jump = 2;
    private const int fallDown = 3;
    private const int charge = 7;
    private const int combo1 = 8;
    private const int combo2 = 9;
    private const int combo3 = 10;
    private const int skyAttack = 11;
    private const int chargeAttack = 12;

    public GameObject sword;
    public VariableJoystick variableJoystick;
    public Animator animator;
    public Rigidbody2D body2D;
    public double speed;
    public double jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        delayTime = 0;
        maxDelayTime = 0.25;
        chargeTime = 0;
        canAction = true;
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAction == true)
        {
            if (body2D.velocity.y < -1)
            {
                FallingDown();
            }
            else if (body2D.velocity.y == 0)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("falldown") ||
                    animator.GetCurrentAnimatorStateInfo(0).IsName("skyattack"))
                {
                    Idle();
                }
            }

            if (animator.GetInteger("status") == chargeAttack)
            {
                Idle();
            }

            if (animator.GetInteger("status") >= combo1 && animator.GetInteger("status") <= combo3)
            {
                delayTime += Time.deltaTime;
                if (delayTime >= maxDelayTime)
                {
                    Idle();
                }
            }
            else
            {
                if (animator.GetInteger("status") != charge)
                {
                    if (variableJoystick.Direction.x > 0)
                    {
                        MoveRight();
                    }
                    else if (variableJoystick.Direction.x < 0)
                    {
                        MoveLeft();
                    }
                    else
                    {
                        if (body2D.velocity.y == 0)
                        {
                            Idle();
                        }
                    }
                }
            }
        }
        else
        {
            if (animator.GetInteger("status") >= 8)
            {
                sword.GetComponent<SwordController>().Slash();
            }
        }
    }

    public void Idle()
    {
        delayTime = 0;
        jumpCount = 0;
        animator.SetInteger("status", idle);
    }

    public void MoveLeft()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            animator.SetInteger("status", move);
        }
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        gameObject.transform.Translate(new Vector3((float)(-speed * Time.deltaTime), 0, 0));
    }

    public void MoveRight()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            animator.SetInteger("status", move);
        }
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        gameObject.transform.Translate(new Vector3((float)(speed * Time.deltaTime), 0, 0));
    }

    public void Charge()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("move"))
        {
            chargeTime = 0;
            animator.SetInteger("status", charge);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("charge"))
        {
            chargeTime += Time.deltaTime;
        }
    }

    public void Jump()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("move"))
        {
            animator.SetInteger("status", 2);
            body2D.AddForce(new Vector2(0, (float)jumpHeight), ForceMode2D.Impulse);
        }
        else
        {
            if (jumpCount < 1)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
                {
                    body2D.AddForce(new Vector2(0, (float)(jumpHeight - 3.5)), ForceMode2D.Impulse);
                    jumpCount++;
                }
            }
        }
    }

    void FallingDown()
    {
        animator.SetInteger("status", fallDown);
    }

    void AttackCombo1()
    {
        delayTime = 0;
        canAction = false;
        animator.SetInteger("status", combo1);
    }

    void AttackCombo2()
    {
        delayTime = 0;
        canAction = false;
        animator.SetInteger("status", combo2);
    }

    void AttackCombo3()
    {
        delayTime = 0;
        canAction = false;
        animator.SetInteger("status", combo3);
    }

    void SkyAttack()
    {
        canAction = false;
        animator.SetInteger("status", skyAttack);
    }

    void ChargeAttack()
    {
        canAction = false;
        animator.SetInteger("status", chargeAttack);
    }

    public void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("jump") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("falldown"))
        {
            SkyAttack();
        }
        else if (animator.GetInteger("status") == combo1)
        {
            AttackCombo2();
        }
        else if (animator.GetInteger("status") == combo2)
        {
            AttackCombo3();
        }
        else if (chargeTime >= 0.5)
        {
            ChargeAttack();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("charge") ||
                 animator.GetInteger("status") == combo3)
        {
            AttackCombo1();
        }
    }

    public void AttackAnimationFinished()
    {
        sword.GetComponent<SwordController>().ResetPosition();
        canAction = true;
    }

    public bool CanAction()
    {
        return canAction;
    }
}