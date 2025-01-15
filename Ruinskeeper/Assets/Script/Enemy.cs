using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 3f;
    public float maxHealth = 100f;
    public float currentHealth;
    public float attackDamage = 10f;
    public float attackRange = 0.5f;
    public float detectionRange = 10f;
    public LayerMask characterLayer;
    public Transform character;

    private Rigidbody2D rb;
    private bool isDead = false;
    private float lastAttackTime = 0f; // Son saldırı zamanı
    public float attackCooldown = 2f; // Saldırı arasındaki bekleme süresi (2 saniye)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        if (character == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                character = player.transform;
            }
        }
    }

    void Update()
    {
        if (isDead || character == null) return;

        float distanceToCharacter = Vector2.Distance(transform.position, character.position);

        if (distanceToCharacter <= detectionRange && distanceToCharacter > attackRange)
        {
            ChaseCharacter();
        }
        else if (distanceToCharacter <= attackRange)
        {
            TryAttackCharacter();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("1_Move", false);
        }
    }

    void ChaseCharacter()
    {
        if (character == null) return;

        // Hedefe doğru yön hesapla
        Vector2 direction = (character.position - transform.position).normalized;

        // X ve Y ekseninde pozisyonu hareket ettir
        transform.position = Vector2.MoveTowards(transform.position, character.position, moveSpeed * Time.deltaTime);

        // Hareket animasyonu
        animator.SetBool("1_Move", true);

        // Yüz yönünü belirle
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Sağ tarafa bak
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Sol tarafa bak
        }
    }


    void TryAttackCharacter()
    {
        // Eğer saldırı soğuma süresi dolmadıysa, saldırma
        if (Time.time - lastAttackTime < attackCooldown) return;

        // Soğuma süresi doldu, saldır
        lastAttackTime = Time.time; // Son saldırı zamanını güncelle
        AttackCharacter();
    }

    void AttackCharacter()
    {
        if (character == null) return;

        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("2_Attack");

        Vector2 attackDirection = (character.position - transform.position).normalized;
        Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(
            transform.position + (Vector3)attackDirection * attackRange,
            attackRange,
            characterLayer
        );

        foreach (Collider2D target in hitCharacters)
        {
            if (target != null)
            {
                target.GetComponent<CharacterController>()?.TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth > 0)
        {
            animator.SetTrigger("3_Damaged");
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("4_Death");
        rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;

        // 1 saniye sonra sahneden sil
        Invoke(nameof(RemoveEnemy), 1f);
    }

    void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
