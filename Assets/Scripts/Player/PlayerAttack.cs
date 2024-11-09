using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerAttackConfig _playerAttackConfig;  //Stores player attack configuration

    private float _chargeTime = 0.0f;
    private bool _throwCharge = true;

    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _thrownProjectile;
    private string _animatorMeleeAttackTrigerName;
    private string _animatorAirAttackTrigerName;
    private string _animatorThrownAttackTrigerName;

    void Start()
    {
        _animatorMeleeAttackTrigerName = _playerAttackConfig.meleeAttackTriggerName;
        _animatorAirAttackTrigerName = _playerAttackConfig.airAttackTriggerName;
        _animatorThrownAttackTrigerName = _playerAttackConfig.thrownAttackTriggerName;
    }
    
    void Update()
    {
        HandleInputs();
    }
    
    private void HandleInputs()
    {
        HandleThrowAttack();
        HandleMeleeAttack();
    }

    private void HandleThrowAttack()
    {
        if (Input.GetKey(KeyCode.X))
        {
            _chargeTime += Time.deltaTime;
        }
        else if (_chargeTime > 1.0f && _throwCharge)
        {
            _chargeTime = 0.0f;
            _throwCharge = false;
            _playerAnimator.SetTrigger(_animatorThrownAttackTrigerName);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            _chargeTime = 0.0f;
            _throwCharge = true;
        }
    }
    
    private void HandleMeleeAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (_playerMovement._isGrounded)
            {
                _playerAnimator.SetTrigger(_animatorMeleeAttackTrigerName);
            }
            else
            {
                _playerAnimator.SetTrigger(_animatorAirAttackTrigerName);
            }
        }
    }

    // Called via throw animation event
    private void SpawnProjectile()
    {
        Instantiate(_thrownProjectile, transform.position, transform.rotation).GetComponent<Projectile>().direction = transform.localScale.x;
    }
}
