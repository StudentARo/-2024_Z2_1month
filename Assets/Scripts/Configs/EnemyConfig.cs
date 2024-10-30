using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Enemy/Enemy Config", fileName = "Enemy config")]
    public class EnemyConfig : ScriptableObject
    {
        //Enemy Health (HP) values
        [Header("Enemy Hit Points Values")]
        public int BaseHealth = 5;
        public int MaxHealth = 5;
        public int MinHealth = 1;

        [Header("Enemy Movement Values")]
        public float MoveSpeed;
        public float IdlingDuration;

        [Header("Enemy Attack Values")]
        public int DamageAmount;
        public float AttackCooldown;
        public float AttackMeleeRange;
        public float AttackMeleeRangeDistance;

        [Header("Enemy Animator Trigger Names")]
        public string AnimatorMoveTrigerName;
        public string AnimatorMeleeAttackTrigerName;
        public string AnimatorGetHitTrigerName;
        public string AnimatorDieTrigerName;
        
        [Header("Enemy Patrolling values")]
        public float ReturningToPatrolDelay;
        
    }
    
}
