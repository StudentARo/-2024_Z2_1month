using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField]
    private int healthPenalty = 1;
    
    private static readonly int CollisionAnimation = Animator.StringToHash("Collision");

    public void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Animator>().SetTrigger(CollisionAnimation);
            
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(healthPenalty);
            
            var playerBody = collision.gameObject.GetComponent<Rigidbody2D>();
            playerBody.AddForce(-playerBody.transform.forward * 500);
            playerBody.AddForce(Vector2.up * 500);
        }
    }
}