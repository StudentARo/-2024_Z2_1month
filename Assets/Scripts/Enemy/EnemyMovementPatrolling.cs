using System;
using UnityEngine;
using Enemy;
using Random = UnityEngine.Random;

public class EnemyMovementPatrolling : MonoBehaviour
{
    [Header("Enemy Config")]
    [SerializeField] private EnemyConfig _enemyConfig;
    
    [Header("Patrolling Points & Settings")]
    [SerializeField] private GameObject _patrollingRouteLeftEdge;
    [SerializeField] private GameObject _patrollingRouteRightEdge;
    
    [Header("Player's Transform")]
    [SerializeField] private Transform _playerTransform;

    private Transform _currentPoint;
    private Rigidbody2D _rigidBody2D;
    private Animator _enemyAnimator;
    
    private float _moveSpeed;   //Enemy's move speed
    private float _idlingDuration;  //Enemy's idling duration time
    private float _idleTimer = Mathf.Infinity;
    private float _patrollingRouteDistanceCheck = 0.5f;

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
        _currentPoint = gameObject.transform;
        setCurrentDestination(_movingDirection);

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
        if (_returningToPatrol)
        {
            ReturnToPatrol();
        }
        
        Move(_movingDirection);
        
        if (hasReachedDestination())
        {
                if (_currentDestinationPoint == _patrollingRouteLeftEdge.transform) //Checks if reached left destinationPoint
                {
                    _idleTimer +=  Time.deltaTime;   //Starts ideling timer
                    if (_idleTimer <= _idlingDuration)   //Checks if should idle 
                    {
                        _movingDirection = 0;
                    }
                    else
                    {
                        //Start moving to destination point on the right side
                        _movingDirection = 1;
                        setCurrentDestination(_movingDirection);
                        FlipSprite();
                        _idleTimer = 0;
                        _lastMovingDirection = _movingDirection;
                    }
                }
                else if (_currentDestinationPoint == _patrollingRouteRightEdge.transform)   //Checks if reached right destinationPoint
                {
                    _idleTimer +=  Time.deltaTime; 
                    if (_idleTimer <= _idlingDuration)
                    {
                        _movingDirection = 0;
                    }
                    else
                    {
                        //Start moving the enemy to destination point on the left side
                        _movingDirection = -1;
                        setCurrentDestination(_movingDirection);
                        FlipSprite();
                        _idleTimer = 0;
                        _lastMovingDirection = _movingDirection;
                    }
                }
        }
    }
    
    private void ReturnToPatrol()   
    {
        _idleTimer +=  Time.deltaTime;   //Starts ideling timer
        if (_idleTimer <= _returningToPatrolDelay)   //Checks if should idle 
        {
            _movingDirection = 0;   //Wait before going back to patrolling
            
            //Face the player
            if (_playerTransform != null)
            {
                if (_playerTransform.position.x - gameObject.transform.position.x < 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
        else
        {
            _movingDirection = _lastMovingDirection;
            FlipSprite();
            _returningToPatrol = false;
            _idleTimer = 0;
        }
    }

    private bool hasReachedDestination()
    {
        if (Math.Abs(_currentPoint.position.x - _currentDestinationPoint.position.x) < _patrollingRouteDistanceCheck)
        {
            return true;
        }
        return false;
    }

    private void setCurrentDestination(int movingDirection)
    {
        if (movingDirection > 0)
        {
            _currentDestinationPoint = _patrollingRouteRightEdge.transform;
        }
        else if (movingDirection < 0)
        {
            _currentDestinationPoint = _patrollingRouteLeftEdge.transform;
        }
    }

    private void Move(int movingDirection)
    {
        if (movingDirection > 0)
        {
            //moving right
            _enemyAnimator.SetBool(_enemyConfig.AnimatorMoveTrigerName, true);
            _rigidBody2D.velocity = new Vector2( _moveSpeed * movingDirection, _rigidBody2D.velocity.y);
        }
        else if (movingDirection < 0)
        {
            //moving left
            _enemyAnimator.SetBool(_enemyConfig.AnimatorMoveTrigerName, true);
            _rigidBody2D.velocity = new Vector2( _moveSpeed * movingDirection, _rigidBody2D.velocity.y);
        }
        else
        {
            //not moving
            _enemyAnimator.SetBool(_enemyConfig.AnimatorMoveTrigerName, false);
            _rigidBody2D.velocity = new Vector2( _moveSpeed * movingDirection, _rigidBody2D.velocity.y);
        }
    }
    
    private void FlipSprite()
    {
            transform.localScale = new Vector3(_movingDirection *-1, 1, 1);
    }
}
