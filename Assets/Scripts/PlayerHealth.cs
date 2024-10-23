using UnityEngine;
using Player;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private int initialHealth = 3;

    private int _currentHealth;

    private void Start ()
    {
        _currentHealth = initialHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }
}
