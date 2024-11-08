using System;
using UnityEngine;
using Player;

public class PlayerHealth: MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    private int _currentHealth;

    private void Start ()
    {
        _currentHealth = _playerConfig.BaseHealth;
    }
    
    public void Heal(int healAmount)
    {
        if (healAmount < 0)
        {
            return;
        }

        if (_currentHealth + healAmount > _playerConfig.MaxHealth)
        {
            _currentHealth = _playerConfig.MaxHealth;
            return;
        }

        _currentHealth += healAmount;
    }
    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            return;
        }

        _currentHealth -= damage;
        
        if (_currentHealth < _playerConfig.MinHealth)
        {
            Destroy(gameObject);
        }
    }
}
