using System;
using UnityEngine;
using Enemy;

public class EnemyMeleeTypeAttacks : MonoBehaviour
{
    [SerializeField] private EnemyConfig _enemyConfig;  //Stores config with values of given type of enemy
    
    private float _attackCooldown; //As public for testing purposes
    private int _damageAmount;   //As public for testing purposes
    private float _attackMeleeRange;
    private float _attackMeleeRangeDistance;
    
    [SerializeField] private BoxCollider2D _inRangeCollider;
    [SerializeField] private LayerMask _playerLayer;    //Used in PlayerInMeleeRange method, indicates Player's Layer
    
    private float _cooldownTimer = Mathf.Infinity;
    private Animator _enemyAnimator;
    private string _animatorMeleeAttackTrigerName;

    private EnemyMovementPatrolling _enemyMovementPatrolling;
    
    void Awake()
    {
        _attackCooldown = _enemyConfig.AttackCooldown;
        _damageAmount = _enemyConfig.DamageAmount;
        _attackMeleeRange = _enemyConfig.AttackMeleeRange;
        _attackMeleeRangeDistance = _enemyConfig.AttackMeleeRangeDistance;
        
        _enemyAnimator = GetComponent<Animator>();
        _animatorMeleeAttackTrigerName = _enemyConfig.AnimatorMeleeAttackTrigerName;

        _enemyMovementPatrolling = GetComponent<EnemyMovementPatrolling>();
    }
    
    void Update()
    {
        _cooldownTimer += Time.deltaTime;

        if (isPlayerInMeleeRange())
        {
            if (_enemyMovementPatrolling is not null)
            {
                _enemyMovementPatrolling.enabled = false;
            }
            
            if (_cooldownTimer >= _attackCooldown)
            {
                _cooldownTimer = 0;
                _enemyAnimator.SetTrigger(_animatorMeleeAttackTrigerName);
            }
        }
        else
        {
            if (_enemyMovementPatrolling is not null)
            {
                _enemyMovementPatrolling.enabled = true;
            }
        }
    }

    private bool isPlayerInMeleeRange()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(_inRangeCollider.bounds.center + transform.right * _attackMeleeRange * transform.localScale.x * -1 * _attackMeleeRangeDistance,
                new Vector3(_inRangeCollider.bounds.size.x * _attackMeleeRange, _inRangeCollider.bounds.size.y, _inRangeCollider.bounds.size.z),
                0, 
                Vector2.right, 
                0, 
                _playerLayer);
        
        if (hit.collider is null)
        {
            //Player not in range
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_inRangeCollider.bounds.center + transform.right * _attackMeleeRange * transform.localScale.x * -1 * _attackMeleeRangeDistance, 
            new Vector3(_inRangeCollider.bounds.size.x * _attackMeleeRange, _inRangeCollider.bounds.size.y, _inRangeCollider.bounds.size.z));
    }

    private void DamagePlayer() //See animator events
    {
        if (isPlayerInMeleeRange())
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            playerHealth.TakeDamage(_damageAmount);
        }
    }
    
}
