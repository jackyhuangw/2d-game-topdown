using UnityEngine;

[System.Serializable]
public class LootEntry
{
    public GameObject prefab;
    [Range(0f, 1f)] public float dropChance;
}
public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float health;
    [SerializeField] private int experienceToGive;
    [SerializeField] private float pushTime;
    private float pushCounter;
    [SerializeField] private GameObject destroyEffect;
    private Vector3 direction;

    [Header("Loot Table")]
    [SerializeField] private LootEntry[] lootTable;

    [Header("XP Drop")]
    [SerializeField] private GameObject xpOrbPrefab; // prefab orb exp
    void FixedUpdate()
    {
        if (PlayerController.Instance.gameObject.activeSelf)
        {
            // face the player
            if (PlayerController.Instance.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            // push back
            if (pushCounter > 0)
            {
                pushCounter -= Time.deltaTime;
                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed;
                }
                if (pushCounter <= 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed);
                }
            }

            // move towards player
            direction = (PlayerController.Instance.transform.position - transform.position).normalized;

            rb.linearVelocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        DamageNumberController.Instance.CreateNumber(damage, transform.position);
        pushCounter = pushTime;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        DropLoot();
        DropExperience();

        Instantiate(destroyEffect, transform.position, transform.rotation);
        // PlayerController.Instance.GetExperience(experienceToGive);
        AudioController.Instance.PlayModifiedSound(AudioController.Instance.enemyDie);

        Destroy(gameObject);
    }

    void DropLoot()
    {
        foreach (var entry in lootTable)
        {
            if (entry.prefab == null) continue;

            if (Random.value <= entry.dropChance)
            {
                Instantiate(entry.prefab, transform.position, Quaternion.identity);
            }
        }
    }

    void DropExperience()
    {
        if (xpOrbPrefab == null) return;

        // spawn satu orb di posisi enemy
        Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
    }
}
