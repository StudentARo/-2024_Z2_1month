using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;  // Value for horizontal movement
    [SerializeField] private float _jumpForce = 25.0f;  // Value for vertical movement
    [SerializeField] [Range(0.0f,1.0f)] private float _dragForce = 0.8f;   //Value should be set in range of 0-1 (1 means no friction from 'ground', 0 means friction to high to move on 'ground') 
    
    private Rigidbody2D _rigidBody2D;   //This stores rigidbody in order to affect Player movement using physics 
    [SerializeField] private BoxCollider2D _groundCheck;  //This stores collider 'groundCheck' attached as component to the Player
    [SerializeField] private LayerMask _groundLayer; //This stores Layer related to what's considered 'Ground' Layer
    private float _inputHorizontal; //This stores input value for horizontal movement from Human Player
    private bool _isGrounded;   //For testing purposes, stores value for being grounded
    private bool _jump; //This stores input value for horizontal movement from Human Player
    private bool _doubleJump;
    
    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        ApplyFriction();
    }
    private void Update()
    {
        getInputs();
        ApplyMovementToThePlayer();
        HandleJump();
    }

    private void ApplyMovementToThePlayer()
    {
        //Horizontal movement
        if (Math.Abs(_inputHorizontal) > 0)
        {
            _rigidBody2D.velocity = new Vector2(_inputHorizontal * _moveSpeed, _rigidBody2D.velocity.y);
            
            //Sets the Player Sprite to horizontal movement direction
            float direction = Mathf.Sign(_inputHorizontal);
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    private void HandleJump()
    {
        //Vertical movement
        if (_jump && CheckGround())
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x,_jumpForce);
            _doubleJump = true;
        } 
        else if (_jump && _doubleJump)
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x,_jumpForce);
            _doubleJump = false;
        }
    }

    private void ApplyFriction()
    {
        //Checks if Player is standing on the ground and applies drag force (friction)
        if (CheckGround() && _inputHorizontal == 0)
        {
            _rigidBody2D.velocity *= _dragForce;
        }
    }

    private void getInputs()
    {
        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        _jump = Input.GetKeyDown(KeyCode.UpArrow);
    }

    private bool CheckGround()
    {
        return Physics2D.OverlapAreaAll(_groundCheck.bounds.min, _groundCheck.bounds.max, _groundLayer).Length > 0;
    }
}