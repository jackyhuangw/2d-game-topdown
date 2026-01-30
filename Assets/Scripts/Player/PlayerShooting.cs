using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Shooting Settings")]
    [SerializeField] private float fireCooldown = 0.5f;

    private float fireTimer;
    private Vector2 lastMoveDir = Vector2.down; // default arah kanan
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        UpdateLastMoveDirection();
        HandleShooting();
    }

    private void UpdateLastMoveDirection()
    {
        Vector2 input = playerController.GetMoveInput();

        if (input.sqrMagnitude > 0.01f)
        {
            lastMoveDir = input.normalized;
        }
    }

    private void HandleShooting()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireCooldown;
        }
    }

    private void Shoot()
    {
        Bullet newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        newBullet.Init(lastMoveDir);
    }
}
