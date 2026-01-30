using UnityEngine;

public class ExperienceEffect : MonoBehaviour, IPickupEffect
{
    [SerializeField] private int experienceAmount = 10;

    public void Apply(PlayerController player)
    {
        player.GetExperience(experienceAmount);
    }
}
