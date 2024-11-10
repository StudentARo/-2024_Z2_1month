using UnityEngine;
using Enemy;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyConfig _enemyConfig;
    private Animator _enemyAnimator;
    public int _currentHealth;  //
    private bool isDead;
    private void Awake ()
    {
        _currentHealth = _enemyConfig.BaseHealth;
        _enemyAnimator = GetComponent<Animator>();
    }
    
    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            return;
        }
        
            _currentHealth -= damage;

            if (!isDead && _currentHealth >= _enemyConfig.MinHealth)
            {
                _enemyAnimator.SetTrigger(_enemyConfig.AnimatorGetHitTrigerName);
            }

            if (_currentHealth < _enemyConfig.MinHealth && !isDead)
            {   
                isDead = true;
                _enemyAnimator.SetTrigger(_enemyConfig.AnimatorDieTrigerName);
            }
        
    }
    
    public void EnemyDeath()
    {
            Destroy(gameObject);
    }
    
}
