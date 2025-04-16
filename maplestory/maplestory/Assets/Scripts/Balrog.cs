using UnityEngine;

public class Balrog : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
        

    [Header("Attack Effect")]
    [SerializeField] private GameObject AttackEffectPrefab;
    [SerializeField] private GameObject AttackPrefab;


    [Header("Attack Range")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckDistance = 5f;
    [SerializeField] private LayerMask whatIsTarget;
    
    private float xInput;

    #region Components
    private Rigidbody2D rb;
    private Animator anim;
    #endregion

    public int facingDir { get; private set; } = 1;
    private bool facingLeft = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        FlipController();

        if (Input.GetKeyDown(KeyCode.P))
            Dead();

        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
    }
   


    private void Move()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        anim.SetBool("Move", true);

        if (xInput == 0)
            anim.SetBool("Move", false);        
    }

    private void Flip()
    {
        facingDir *= -1;
        facingLeft = !facingLeft;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController()
    {
        if (rb.linearVelocity.x < 0 && !facingLeft)
            Flip();
        else if (rb.linearVelocity.x > 0 && facingLeft)
            Flip();
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");        
    }


    //스폰 어택 이펙트는 애니메이션에서 이벤트 추가해서 함수 처리해줬음
    public void SpawnAttackEffect()
    {
        GameObject go = Instantiate(AttackEffectPrefab, transform.position, Quaternion.identity);
        go.transform.localScale = new Vector3(facingDir, 1, 1);
        Destroy(go, 0.8f);
    }

    //얘는 레이캐스트히트2D로 빔을 쏴서 거기에 닿은 대상을 맞춰야됨
    public void SpawnClawEffect()
    {
        RaycastHit2D hit = Physics2D.Raycast(targetCheck.position, Vector2.left * facingDir, targetCheckDistance, whatIsTarget);

        if (hit.collider != null)
        {
            GameObject go =  Instantiate(AttackPrefab, hit.transform.position, Quaternion.identity);
            Destroy(go, 1f);
        }        
    }

    private void Dead()
    {
        anim.SetBool("Dead", true);
        Destroy(gameObject, 1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(targetCheck.position, new Vector3(targetCheck.position.x - targetCheckDistance * facingDir, targetCheck.position.y, 0));
    }

    //잡 주석 추가
}
