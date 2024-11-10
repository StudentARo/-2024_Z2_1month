using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Jump = Animator.StringToHash("jump");
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float _moveSpeed = 4.0f;  // Value for horizontal movement
    [SerializeField] private float _jumpForce = 7.0f;  // Value for vertical movement
    [SerializeField] [Range(0.0f,50.0f)] private float _groundFriction = 0.8f;   //Value should be set in range of 0-1 (1 means no friction from 'ground', 0 means friction to high to move on 'ground') 
    [SerializeField] [Range(0.0f,10.0f)] private float _airFriction = 0.8f; 
    [SerializeField] private float _coyoteTime = 0.2f;
    
    private Rigidbody2D _rigidBody2D;   //This stores rigidbody in order to affect Player movement using physics 
    private float _inputHorizontal; //This stores input value for horizontal movement from Human Player
    private bool _isJump; //This stores input value for vertical movement from Human Player
    private bool _isDoubleJumpPossible;   //This stores input value for vertical movement from Human Player
    public bool _isGrounded;
    
    private float _coyoteTimeCounter;
    
    [SerializeField]
    private ContactFilter2D contactFilter;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            ApplyFriction();
        }
        else
        {
            ApplyAirFriction();
        }
        
        PerformHorizontalMovement();
        
        if (_isJump)
        {
            PerformVerticalMovement();
        }
    }
    private void Update()
    {
        CheckIsOnGround();
        GetInputs();

        if (_isGrounded)
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void PerformHorizontalMovement()
    {
        //Horizontal movement
        if (Math.Abs(_inputHorizontal) > 0)
        {
            playerAnimator.SetBool(Run, true);
            _rigidBody2D.velocity = new Vector2(_inputHorizontal * _moveSpeed, _rigidBody2D.velocity.y);
            
            //Sets the Player Sprite to horizontal movement direction
            float direction = Mathf.Sign(_inputHorizontal);
            transform.localScale = new Vector3(direction, 1, 1);
        }
        else
        {
            playerAnimator.SetBool(Run, false);
        }
    }

    private void PerformVerticalMovement()
    {
        //Vertical movement
        if (_isJump && _coyoteTimeCounter > 0)
        {
            playerAnimator.SetTrigger(Jump);
            _rigidBody2D.drag = 0;
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, 0);
            _rigidBody2D.AddForce(_rigidBody2D.transform.up * _jumpForce, ForceMode2D.Impulse);
            _isDoubleJumpPossible = true;
            _coyoteTimeCounter = 0;
        } 
        else if (_isJump && _isDoubleJumpPossible)
        {
            playerAnimator.SetTrigger(Jump);
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, 0);
            _rigidBody2D.AddForce(_rigidBody2D.transform.up * _jumpForce, ForceMode2D.Impulse);
            _isDoubleJumpPossible = false;
        }
        _isJump = false;
    }

    private void ApplyFriction()
    {
        //Checks if Player is standing on the ground and applies drag force (friction)
        if (Mathf.Abs(_inputHorizontal) < 0.1)
        {
            _rigidBody2D.drag = _groundFriction;
        }
        else
        {
            _rigidBody2D.drag = 0;
        }
    }

    private void ApplyAirFriction()
    {
        _rigidBody2D.drag = _airFriction;
    }

    private void GetInputs()
    {
        _inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (!_isJump && Input.GetButtonDown("Jump"))
        {
            _isJump = true;
        }
    }
    
    private void CheckIsOnGround()
    {
        _isGrounded = _rigidBody2D.IsTouching(contactFilter);
    }
}