using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f;
    public float maxHealth = 100f;
    public float currentHealth;
    public float attackDamage = 10f;
    public float attackRange = 0.7f;
    public LayerMask enemyLayer;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isDead = false;
    private bool isDamaged = false; // Hasar animasyonu sırasında hareketi durdurmak için

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        // Hareket girişlerini al (hasar almadıysa)
        if (!isDamaged)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        // Hareket animasyonu kontrolü
        bool isMoving = movement.magnitude > 0;
        animator.SetBool("1_Move", isMoving);

        // Saldırı kontrolü
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        // Karakterin yüz yönünü belirle
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Sağ tarafa bak
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Sol tarafa bak
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // Hareket vektörünü normalize ederek hızın sabit kalmasını sağla
        Vector2 normalizedMovement = movement.normalized;

        // Hareket uygula
        rb.linearVelocity = normalizedMovement * moveSpeed;
    }

    void Attack()
    {
        animator.SetTrigger("2_Attack");

        // Saldırı alanı ve düşmanları tespit et
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            (Vector2)transform.position + (transform.localScale.x > 0 ? Vector2.left : Vector2.right) * attackRange,
            attackRange,
            enemyLayer
        );

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth > 0)
        {
            animator.SetTrigger("3_Damaged");
            isDamaged = true; // Hasar aldığında hareketi durdur
            Invoke(nameof(EndDamageAnimation), 1f); // 1 saniye sonra hareketi tekrar aktif et
        }
        else
        {
            Die();
        }
    }

    void EndDamageAnimation()
    {
        isDamaged = false; // Hasar animasyonu bittikten sonra hareketi aktif et
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("4_Death"); // Ölüm animasyonu tetikle
        rb.linearVelocity = Vector2.zero; // Hareketi durdur
        GetComponent<Collider2D>().enabled = false; // Çarpışmayı devre dışı bırak
        Invoke(nameof(RemoveCharacter), 1f); // 1 saniye sonra karakteri yok et
    }

    void RemoveCharacter()
    {
        Destroy(gameObject); // Karakteri yok et
    }

    void OnDrawGizmosSelected()
    {
        // Saldırı menzilini sahnede görselleştir
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + (transform.localScale.x > 0 ? Vector2.left : Vector2.right) * attackRange, attackRange);
    }
}
