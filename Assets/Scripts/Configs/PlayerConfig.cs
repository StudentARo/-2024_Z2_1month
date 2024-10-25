using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Player Config", fileName = "Player config")]
    public class PlayerConfig : ScriptableObject
    {
        //Player Health (HP) values
        public int BaseHealth = 3;
        public int MaxHealth = 5;
        public int MinHealth = 1;
        
        //Player Score values
        public int BaseScore = 0;
    }
    
}
