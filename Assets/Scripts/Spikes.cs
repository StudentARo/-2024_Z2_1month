using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int damagePenalty = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damagePenalty);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 800);
        }
    }
}
