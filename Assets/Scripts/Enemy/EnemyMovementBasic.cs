using System;
using UnityEngine;
using Enemy;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class EnemyMovementBasic : MonoBehaviour
{
    [Header("Enemy Config")]
    [SerializeField] private EnemyConfig _enemyConfig;

    [Header("Defense Zone & Settings")]
    [SerializeField] private bool _shouldPatrol;
    [SerializeField] private bool _shouldChase;
    [SerializeField] private Collider2D _defenseZone;
    [SerializeField] private GameObject _patrolPointPrefab;
    
    [Header("Player's Info")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private LayerMask _playerLayer;
    
    private Rigidbody2D _rigidBody2D;
    private Animator _enemyAnimator;
    private Collider2D _enemyBoxCollider;
    
    private float _moveSpeed;   //Enemy's move speed
    private float _idlingDuration;  //Enemy's idling duration time
    private float _idleTimer = Mathf.Infinity;
    private float _destinationReachDistanceCheck;
    [Range(-1,1)] private int _movingDirection; //Main movement controller [-1 = moving left (-x), 0 = not moving, 1 = moving right (+x)] 
    
    private GameObject _leftedgeDefenseZonePoint;
    private GameObject _rightedgeDefenseZonePoint;
    private Transform _currentDestinationPoint;
    private Transform _lastPatrolPoint;
    private float _returningToPatrolDelay;
    private bool _returningToPatrol;
    
    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _enemyAnimator = GetComponent<Animator>();
        
        _enemyBoxCollider = GetComponent<BoxCollider2D>();
        _destinationReachDistanceCheck = (_enemyBoxCollider.bounds.size.x / 2) + _enemyConfig.DestinationReachDistanceCheck;
        
        _moveSpeed = _enemyConfig.MoveSpeed;
        _idlingDuration = _enemyConfig.IdlingDuration;
        _idleTimer = Random.Range(0, _enemyConfig.IdlingDuration);  //This randomizes initial _idleTimer, so enemy starts to move at diffrent times
        
        setDefenseZonePatrolPoints();
        _currentDestinationPoint = gameObject.transform;
        
        _movingDirection = Random.Range(0, 2) * 2 - 1;  //This sets initial _movingDirection to either -1 or 1 (excluding 0)

        _returningToPatrolDelay = _enemyConfig.ReturningToPatrolDelay;
        _returningToPatrol = false;
    }
    
    void Update()
    {
        if (CheckPlayerNearby() && CheckPlayerBreachedArea(_playerTransform, _defenseZone, _playerLayer))
        {
            ResolvePlayerFacingDirection();
        }
        else
        {
            if (_shouldChase && _shouldPatrol)
            {
                if (CheckPlayerBreachedArea(_playerTransform, _defenseZone, _playerLayer))
                {
                    _currentDestinationPoint = _playerTransform;
                }
                else
                {
                    _currentDestinationPoint =  SelectPatrollingPoint();
                }
            } 
            else if (_shouldChase)
            {
                if (CheckPlayerBreachedArea(_playerTransform, _defenseZone, _playerLayer))
                {
                    _currentDestinationPoint = _playerTransform;
                }
                else
                {
                    _currentDestinationPoint = gameObject.transform;
                }
            } 
            else if (_shouldPatrol)
            {
                _currentDestinationPoint =  SelectPatrollingPoint();
            }
        }
        ResolveMovingDirection(gameObject.transform, _currentDestinationPoint);
        Move();
    }

    private void ResolvePlayerFacingDirection()
    {
        if (gameObject.transform.position.x < _playerTransform.position.x)  //currentPoint is to the left
        {
            FlipSprite(-1);
        } 
        
        if (gameObject.transform.position.x > _playerTransform.position.x)  //currentPoint is to the right
        {
            FlipSprite(1);
        }
    }
    private bool CheckPlayerNearby()
    {
        //If Player will be too close, the enemy will stop moving and will face the player
        if (Math.Abs(gameObject.transform.position.x - _playerTransform.position.x) <= _destinationReachDistanceCheck)
        {
            return true;
        }
        return false;
    }
    private Transform SelectPatrollingPoint()
    {
        if (hasReachedDestination() &&
            _currentDestinationPoint.position.x == _leftedgeDefenseZonePoint.transform.position.x)
        {
            _lastPatrolPoint = _rightedgeDefenseZonePoint.transform;
            return _rightedgeDefenseZonePoint.transform;
        }
        else if (hasReachedDestination() &&
            _currentDestinationPoint.position.x == _rightedgeDefenseZonePoint.transform.position.x)
        {
            _lastPatrolPoint = _leftedgeDefenseZonePoint.transform;
            return _leftedgeDefenseZonePoint.transform;
        } 
        else if ((_currentDestinationPoint.position.x != _leftedgeDefenseZonePoint.transform.position.x) &&
                 (_currentDestinationPoint.position.x != _rightedgeDefenseZonePoint.transform.position.x))
        {
            int direction = Random.Range(0, 2) * 2 - 1;
            if (direction < 0)
            {
                _lastPatrolPoint = _leftedgeDefenseZonePoint.transform;
                return _leftedgeDefenseZonePoint.transform;
            }
            else
            {
                _lastPatrolPoint = _rightedgeDefenseZonePoint.transform;
                return _rightedgeDefenseZonePoint.transform;
            }
        }
        else
        {
            return _lastPatrolPoint;
        }
        
    }
    
    private bool hasReachedDestination()
    {
        if (Math.Abs(gameObject.transform.position.x - _currentDestinationPoint.position.x) < _destinationReachDistanceCheck)
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
    
    private void ResolveMovingDirection(Transform currentPoint,Transform destinationPoint)
    {
        if (hasReachedDestination() || CheckPlayerNearby())  //currentPoint same as DestinationPoint
        {
            _movingDirection = 0;
        }
        else
        {
            if (currentPoint.position.x < destinationPoint.position.x)  //currentPoint is to the left
            {
                _movingDirection = 1;
            } 
        
            if (currentPoint.position.x > destinationPoint.position.x)  //currentPoint is to the right
            {
                _movingDirection = -1;
            }
        }
    }

    private void Move()
    {
        if (_movingDirection != 0)
        {
            _enemyAnimator.SetBool(_enemyConfig.AnimatorMoveTrigerName, true);
            _rigidBody2D.velocity = new Vector2( _moveSpeed * _movingDirection, _rigidBody2D.velocity.y);
            FlipSprite();
            return;
        }
            //not moving
            _enemyAnimator.SetBool(_enemyConfig.AnimatorMoveTrigerName, false);
            _rigidBody2D.velocity = new Vector2( _moveSpeed * _movingDirection, _rigidBody2D.velocity.y);
        
    }
    
    private void FlipSprite()
    {
        if (_movingDirection != 0)
        {
            transform.localScale = new Vector3(_movingDirection *-1, 1, 1);
        }
    }
    
    private void FlipSprite(int direction)
    {
        if (direction != 0)
        {
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }
    
    private void setDefenseZonePatrolPoints()
    {
        Vector3 leftedgeDefenseZonePoint = new Vector3(_defenseZone.bounds.center.x - _defenseZone.bounds.size.x/2,
            _defenseZone.bounds.center.y, _defenseZone.bounds.center.z);
        Vector3 rightedgeDefenseZonePoint = new Vector3(_defenseZone.bounds.center.x + _defenseZone.bounds.size.x/2,
            _defenseZone.bounds.center.y, _defenseZone.bounds.center.z);
        
        _leftedgeDefenseZonePoint = Instantiate(_patrolPointPrefab, leftedgeDefenseZonePoint, Quaternion.identity, _defenseZone.transform.parent);  //Creates a patrol destination left point
        _rightedgeDefenseZonePoint = Instantiate(_patrolPointPrefab, rightedgeDefenseZonePoint, Quaternion.identity, _defenseZone.transform.parent); //Creates a patrol destination right point
    }
    
}
