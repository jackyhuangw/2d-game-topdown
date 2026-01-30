using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float damage = 10f;

    private Vector2 moveDirection;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01f)
        {
            direction = Vector2.down;
        }

        moveDirection = direction.normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void OnEnable()
    {
        Invoke(nameof(DestroySelf), lifeTime);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    // Abaikan collision dengan Player
    if (other.CompareTag("Player"))
    {
        return;
    }

    Enemy enemy = other.GetComponent<Enemy>();
    if (enemy != null)
    {
        enemy.TakeDamage(damage);
        DestroySelf();
    }
}


    private void DestroySelf()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
