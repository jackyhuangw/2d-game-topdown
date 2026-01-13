using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupItem : MonoBehaviour
{
    // [SerializeField] private AudioClip pickupSound; // opsional

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

        // if (pickupSound != null)
        // {
        //     AudioController.Instance.PlayModifiedSound(pickupSound);
        // }

        Destroy(gameObject);
    }
}
