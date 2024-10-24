using UnityEngine;

namespace Potions
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Health Potion Config", fileName = "Health Potion config")]
    
    public class HealthPotionConfig : ScriptableObject
    {
        public string Name;
        [TextArea] public string Description;
        public Sprite UISprite;

        public int HealAmount;
    }
    
}

