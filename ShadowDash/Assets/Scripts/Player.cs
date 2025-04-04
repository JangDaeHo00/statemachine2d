using UnityEngine;

public class Player : Entity
{
    [Header("이동 정보")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("대시 정보")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    [Header("공격 정보")]
    [SerializeField] private float comboTime = 0.3f;
    private float comboTimeCounter;
    private bool isAttacking;
    private int comboCounter;

    private float xInput;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        CheckInput();
        Movement();

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeCounter -= Time.deltaTime;

        FlipController();
        AnimatorControllers();

    }

    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;
        if (comboCounter > 2)
            comboCounter = 0;

    }

    

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse0))    // Mouse0이 왼클릭, Mouse1이 우클릭
        {
            StartAttackEvent();
        }
    }

    private void StartAttackEvent()
    {
        if (!isGrounded)
            return;

        if (comboCounter < 0)    // ctrl r r 하면 위아래 변수 다 바뀐다.
            comboCounter = 0;

        isAttacking = true;
        comboTimeCounter = comboTime;
    }

    private void DashAbility()
    {
        if(dashCooldownTimer < 0 && !isAttacking)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }

    private void Movement()
    {
        if(isAttacking)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        else if(dashTime > 0)
        {
            rb.linearVelocity = new Vector2(facingDir * dashSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        }
    }

    private void Jump()
    {
        if(isGrounded)
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    //Alt + 화살표키
    private void AnimatorControllers()
    {

        bool isMoving = rb.linearVelocity.x != 0;

        anim.SetFloat("yVelocity", rb.linearVelocityY);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);

    }

    

    private void FlipController()
    {
        if(rb.linearVelocity.x > 0 && !facingRight)     // rb.linearVelocity : 오른쪽을 누른 상태
        {
            Flip();
        }
        else if(rb.linearVelocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
    }

}