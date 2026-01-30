using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupItem : MonoBehaviour
{
    private bool isAttracted = false;
    private Transform player;
    private float attractSpeed = 5f;

    void Update()
    {
        // Kalau sedang tertarik ke player
        if (isAttracted && player != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                attractSpeed * Time.deltaTime
            );

            // Auto collect kalau sudah dekat player
            if (Vector3.Distance(transform.position, player.position) < 0.5f)
            {
                Collect();
            }
        }
    }

    // Fungsi baru untuk magnet
    public void StartAttracting(Transform playerTransform)
    {
        if (!isAttracted)
        {
            isAttracted = true;
            player = playerTransform;
        }
    }

    // Fungsi collect yang sudah ada
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        // cari semua komponen efek pada GameObject ini
        IPickupEffect[] effects = GetComponents<IPickupEffect>();
        foreach (var effect in effects)
        {
            effect.Apply(player);
        }

        Destroy(gameObject);
    }

    private void Collect()
    {
        PlayerController player = PlayerController.Instance;
        if (player != null)
        {
            IPickupEffect[] effects = GetComponents<IPickupEffect>();
            foreach (var effect in effects)
            {
                effect.Apply(player);
            }
        }
        Destroy(gameObject);
    }
}
