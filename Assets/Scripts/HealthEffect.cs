using UnityEngine;

public class HealEffect : MonoBehaviour, IPickupEffect
{
    [SerializeField] private float healAmount = 1f;

    public void Apply(PlayerController player)
    {
        player.playerHealth += healAmount;
        if (player.playerHealth > player.playerMaxHealth)
        {
            player.playerHealth = player.playerMaxHealth;
        }

        UIController.Instance.UpdateHealthSlider();
    }
}
