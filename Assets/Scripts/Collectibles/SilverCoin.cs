using Collectibles;
using UnityEngine;

namespace SilverCoin
{
    public class SilverCoin : Collectible
    {
        [SerializeField] private SilverCoinConfig _silverCoinConfig;
        protected override void Collect()
        {
            PlayerScore playerScore = FindObjectOfType<PlayerScore>();
            playerScore.AddScore(_silverCoinConfig.ScorePointsAmount);
            Destroy(gameObject);
        }
    }
    
}
