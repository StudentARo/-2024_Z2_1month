using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class BarrelMovement : MonoBehaviour
{
    [SerializeField]
    private float barrellSpeed = 1f;
    [SerializeField]
    private Rigidbody2D barrellBody;
    [SerializeField]
    private float force;
    private int barellDmg;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        barrellBody = GetComponent<Rigidbody2D>();
        barrellBody.velocity = transform.right * barrellSpeed;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * barrellSpeed * Time.deltaTime;
        }
    }
    void BarrellCollision2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
          
               
                PlayerHealth playerHP = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHP != null)
                {
                    playerHP.TakeDamage(barellDmg);
                }

              
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
            
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.AddForce(pushDirection * force, ForceMode2D.Impulse);
                    Destroy(gameObject);
                }
            }
        }
}