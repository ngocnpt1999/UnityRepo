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
    private const int surf = 4;
    private const int charge = 7;
    private const int combo1 = 8;
    private const int combo2 = 9;
    private const int combo3 = 10;
    private const int skyAttack = 11;
    private const int chargeAttack = 12;
    private const int ultimate = 14;

    public GameObject sword;
    public GameObject bullets;
    public VariableJoystick variableJoystick;
    public Animator animator;
    public Rigidbody2D body2D;
    public CapsuleCollider2D capsuleCollider2D;
    public BoxCollider2D boxCollider2D;
    public double speed;
    public double jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        sword.SetActive(false);
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
            if (body2D.velocity.y < -1 && animator.GetInteger("status") != surf)
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
                if (animator.GetInteger("status") != charge && animator.GetInteger("status") != surf)
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

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("surf"))
                {
                    if (gameObject.GetComponent<SpriteRenderer>().flipX)
                    {
                        gameObject.transform.Translate(new Vector3((float)(-speed * 1.5 * Time.deltaTime), 0, 0));
                    }
                    else
                    {
                        gameObject.transform.Translate(new Vector3((float)(speed * 1.5 * Time.deltaTime), 0, 0));
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

    public void Surf()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("move"))
        {
            animator.SetInteger("status", surf);
            capsuleCollider2D.enabled = false;
            boxCollider2D.enabled = true;
        }
    }

    public void SurfAnimationFinished()
    {
        boxCollider2D.enabled = false;
        capsuleCollider2D.enabled = true;
        Idle();
    }

    void NormalAttack1()
    {
        delayTime = 0;
        canAction = false;
        sword.SetActive(true);
        animator.SetInteger("status", combo1);
    }

    void NormalAttack2()
    {
        delayTime = 0;
        canAction = false;
        sword.SetActive(true);
        animator.SetInteger("status", combo2);
    }

    void NormalAttack3()
    {
        delayTime = 0;
        canAction = false;
        sword.SetActive(true);
        animator.SetInteger("status", combo3);
    }

    void SkyAttack()
    {
        canAction = false;
        sword.SetActive(true);
        body2D.gravityScale = 0.1f;
        animator.SetInteger("status", skyAttack);
    }

    void ChargeAttack()
    {
        canAction = false;
        sword.SetActive(true);
        animator.SetInteger("status", chargeAttack);
    }

    public void UltimateAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("move"))
        {
            canAction = false;
            animator.SetInteger("status", ultimate);
        }
    }

    public void ActivateUltimateBullet()
    {
        bullets.transform.position = new Vector3(gameObject.transform.position.x,
                                                 gameObject.transform.position.y - 0.4f,
                                                 bullets.transform.position.z);
        bullets.GetComponent<BulletController>().ActivateBullet();
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
            NormalAttack2();
        }
        else if (animator.GetInteger("status") == combo2)
        {
            NormalAttack3();
        }
        else if (chargeTime >= 0.5)
        {
            ChargeAttack();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("charge") ||
                 animator.GetInteger("status") == combo3)
        {
            NormalAttack1();
        }
    }

    public void AttackAnimationFinished()
    {
        sword.GetComponent<SwordController>().ResetPosition();
        sword.SetActive(false);
        canAction = true;
        if (animator.GetInteger("status") == chargeAttack || animator.GetInteger("status") == ultimate)
        {
            Idle();
        }
        else if (animator.GetInteger("status") == skyAttack)
        {
            body2D.gravityScale = 1.5f;
        }
    }

    public bool CanAction()
    {
        return canAction;
    }
}