using Collectibles;
using UnityEngine;

namespace Potions
{
    public class HealthPotion : Collectible
    {
        [SerializeField] private HealthPotionConfig _healthPotionConfig;
        protected override void Collect()
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            playerHealth.Heal(_healthPotionConfig.HealAmount);
            Destroy(gameObject);
        }
    }
}
