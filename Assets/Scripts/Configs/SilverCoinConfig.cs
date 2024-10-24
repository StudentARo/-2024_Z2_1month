using UnityEngine;

namespace SilverCoin 
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Silver Coin Config", fileName = "Silver Coin config")]
    public class SilverCoinConfig : ScriptableObject
    {
        public int ScorePointsAmount = 1;
    }
}
