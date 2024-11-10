using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BarrelMovement : MonoBehaviour
{
    [SerializeField]
    private float barrelSpeed = 1f; //barell speed
    //[SerializeField]
    private Rigidbody2D barrelBody;
    [SerializeField]
    private float force = 25f; //physics throw player variable
    private int barrelDamage = 1;  // Ustawiamy obra¿enia na 1, aby odbieraæ 1 punkt HP
    private Transform player;
    public float rotationSpeed = 100f;
    void Start()
    {
        barrelBody = GetComponent<Rigidbody2D>();
        barrelBody.velocity = transform.right * barrelSpeed;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
       
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * barrelSpeed * Time.deltaTime;
        }
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    //checking if we are colliding with player and throwing him away with damage.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHP = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHP != null)
            {
                playerHP.TakeDamage(barrelDamage);
            }

            Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.AddForce(pushDirection * force, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}