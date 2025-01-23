using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : Entity
{
    [Header("Move Info")]
    [SerializeField] private float originMoveSpeed;
    [SerializeField] private float jumpForce;

    private float xInput;
    private float playerSpeed;

    [Header("Dash Info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer = 0f;

    [Header("Attack Info")]
    private bool isAttacking;
    private int comboCounter;
    [SerializeField] private float comboCoolTime;
    private float comboTimeWindow;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        CheckInput();
        PlayerMove();
        AnimatorControllers();
        FlipController();
    }

    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;
        if (comboCounter > 2)
        {
            comboCounter = 0;
        }
    }

    private void CheckInput()
    {
        comboTimeWindow -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            StartAttackEvent();
        }

        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }
        
        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void StartAttackEvent()
    {
        if (!isGrounded)
            return;
        if (comboTimeWindow < 0)
        {
            comboCounter = 0;
        }
        comboTimeWindow = comboCoolTime;
        isAttacking = true;
    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0)
        {
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;
        }
    }

    private void PlayerMove()
    {
        if (isAttacking)
        {
            rb.linearVelocity = new Vector2(0, 0);
            return;
        }
        playerSpeed = dashTime > 0 ? dashSpeed : originMoveSpeed;
        rb.linearVelocityX = dashTime > 0 ? facingDir * playerSpeed : xInput * playerSpeed;
        rb.linearVelocityY = dashTime > 0 ? 0 : rb.linearVelocityY;
    }

    private void PlayerJump()
    {
        if (isAttacking || !isGrounded)
            return;
        rb.linearVelocityY = jumpForce;
    }

    private void AnimatorControllers()
    {
        bool isMoving = xInput != 0;
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("yVelocity", rb.linearVelocityY);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDashing", dashTime > 0);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetInteger("comboCounter", comboCounter);
    }

}
