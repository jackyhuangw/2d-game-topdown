using UnityEngine;

public class MagnetPickup : MonoBehaviour
{
    [SerializeField] private float magnetRange = 4f;
    [SerializeField] private LayerMask collectibleLayer = -1; // semua layer dulu

    private void Update()
    {
        // Cari semua PickupItem di radius magnet
        Collider2D[] items = Physics2D.OverlapCircleAll(
            transform.position,
            magnetRange,
            collectibleLayer
        );

        foreach (Collider2D itemCollider in items)
        {
            PickupItem pickup = itemCollider.GetComponent<PickupItem>();
            if (pickup != null)
            {
                pickup.StartAttracting(transform);
            }
        }
    }
}
