using System;
using System.Numerics;
using UnityEngine;
using Enemy;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyMovementBasic : MonoBehaviour
{
    [Header("Enemy Config")]
    [SerializeField] private EnemyConfig _enemyConfig;

    [Header("Patrolling Points & Settings")]
    [SerializeField] private bool shouldPatrol;
    [SerializeField] private bool shouldChase;
    [SerializeField] private Collider2D _defenseZone;
    
    [Header("Player's Info")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private LayerMask _playerLayer;

    private Transform _currentPositionPoint;
    private Rigidbody2D _rigidBody2D;
    private Animator _enemyAnimator;
    
    private float _moveSpeed;   //Enemy's move speed
    private float _idlingDuration;  //Enemy's idling duration time
    private float _idleTimer = Mathf.Infinity;
    private float _destinationReachDistanceCheck = 0.5f;

    [Range(-1,1)] private int _movingDirection; //Main movement controller [-1 = moving left (-x), 0 = not moving, 1 = moving right (+x)] 
    private int _lastMovingDirection;
    
    private Transform _currentDestinationPoint;
    private float _returningToPatrolDelay;
    private bool _returningToPatrol;
    
    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _enemyAnimator = GetComponent<Animator>();
        
        _moveSpeed = _enemyConfig.MoveSpeed;
        _idlingDuration = _enemyConfig.IdlingDuration;
        _idleTimer = Random.Range(0, _enemyConfig.IdlingDuration);  //This randomizes initial _idleTimer, so enemy starts to move at diffrent times

        _movingDirection = Random.Range(0, 2) * 2 - 1;  //This sets initial _movingDirection to either -1 or 1 (excluding 0)
        _lastMovingDirection = _movingDirection;
        _currentPositionPoint = gameObject.transform;
        //setCurrentDestination(_movingDirection);

        _returningToPatrolDelay = _enemyConfig.ReturningToPatrolDelay;
        _returningToPatrol = false;
    }

    private void OnDisable()
    {
        _movingDirection = 0;
        _enemyAnimator.SetBool(_enemyConfig.AnimatorMoveTrigerName, false);
    }

    private void OnEnable()
    {
        _returningToPatrol = true;
    }
    
    void Update()
    {
        _movingDirection = ResolveMovingDirection(gameObject.transform, _playerTransform, _destinationReachDistanceCheck);
        Move();
    }
    

    private bool hasReachedDestination()
    {
        if (Math.Abs(_currentPositionPoint.position.x - _currentDestinationPoint.position.x) < _destinationReachDistanceCheck)
        {
            return true;
        }
        return false;
    }

    private bool CheckPlayerBreachedArea(Transform playerTransform, Collider2D defenseZone, LayerMask playerLayer)
    {
        Vector2 rayOrigin = new Vector2(defenseZone.bounds.center.x - defenseZone.bounds.size.x/2, defenseZone.bounds.center.y);
        Vector2 rayDestination = new Vector2(defenseZone.bounds.center.x + defenseZone.bounds.size.x/2, defenseZone.bounds.center.y);
        Vector2 rayBoxSize = new Vector2(0.01f, defenseZone.bounds.size.y);
        
        RaycastHit2D hit =
            Physics2D.BoxCast(rayOrigin, rayBoxSize, 0, Vector2.right, Vector2.Distance(rayOrigin, rayDestination), playerLayer);
        
        if (hit.collider is null)
        {
            //Player not in range
            return false;
        }
        return true;
    }
    
    private int ResolveMovingDirection(Transform currentPoint,Transform destinationPoint, float destinationReachDistanceCheck)
    {
        if (Math.Abs(currentPoint.position.x - destinationPoint.position.x) <= _destinationReachDistanceCheck)  //currentPoint same as DestinationPoint
        {
            return 0;
        }
        
        if (currentPoint.position.x < destinationPoint.position.x)  //currentPoint is to the left
        {
            return 1;
        } 
        
        if (currentPoint.position.x > destinationPoint.position.x)  //currentPoint is to the right
        {
            return -1;
        }

        return 0;
    }

    private void Move()
    {
        if (_movingDirection != 0)
        {
            _enemyAnimator.SetBool(_enemyConfig.AnimatorMoveTrigerName, true);
            _rigidBody2D.velocity = new Vector2( _moveSpeed * _movingDirection, _rigidBody2D.velocity.y);
            FlipSprite();
        }
        else
        {
            //not moving
            _enemyAnimator.SetBool(_enemyConfig.AnimatorMoveTrigerName, false);
            _rigidBody2D.velocity = new Vector2( _moveSpeed * _movingDirection, _rigidBody2D.velocity.y);
        }
    }
    
    private void FlipSprite()
    {
            transform.localScale = new Vector3(_movingDirection *-1, 1, 1);
    }
}
