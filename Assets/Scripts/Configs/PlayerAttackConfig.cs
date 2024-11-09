using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Player Attack Config", fileName = "Player Attack config")]
    public class PlayerAttackConfig : ScriptableObject
    {
        [Header("Player Damage Values")]
        public int meleeAttackDamage = 3;
        public int thrownAttackDamage = 5;
        
        [Header("Player Knockback Values")]
        public int meleeAttackKnockbackForce = 2;
        public int thrownAttackKnockbackForce = 0;
        
        [Header("Animation triggers")]
        public string meleeAttackTriggerName;
        public string airAttackTriggerName;
        public string thrownAttackTriggerName;
    }
    
}
