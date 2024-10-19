using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;   //This stores rigidbody in order to affect Player movement using physics 
    private float _inputHorizontal; //This stores input value for horizontal movement from Human Player
    private bool _performJump;
    private bool _inAir;
    private Vector2 _currentVelocity;   //This stores Vector2 value of calculated movement
    
    [SerializeField] private float _moveSpeed = 10.0f;  // Value for horizontal movement
    [SerializeField] private float _jumpForce = 10.0f;  // Value for vertical movement
    
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        _currentVelocity = new Vector2(_inputHorizontal * _moveSpeed, _rigidBody2D.velocity.y);

        //Check if in air
        if (_rigidBody2D.velocity.y != 0)
        {
            _inAir = true;
        }
        else
        {
            _inAir = false;
        }
        
        //Check if can jump
        if (Input.GetButton("Jump") && !_inAir)
        {
            _performJump = true;
        }
        else
        {
            _performJump = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayerRigidBody();
    }

    private void MovePlayerRigidBody()
    {
            _rigidBody2D.velocity = _currentVelocity;

            if (_performJump)
            {
                _performJump = false;
                _rigidBody2D.AddForce(new Vector2(0.0f, _jumpForce), ForceMode2D.Impulse);
            }
    }

    /*private Vector2 calculateMovement()
    {
        
    }*/
    
}
