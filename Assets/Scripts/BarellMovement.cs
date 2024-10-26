using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMovement : MonoBehaviour
{

    [SerializeField]
    private float barrellSpeed = 1f;

    [SerializeField]
    private Rigidbody2D barrellBody;

    [SerializeField]
    private float force;
    // Start is called before the first frame update
    void Start()
    {
        barrellBody = GetComponent<Rigidbody2D>();

        barrellBody.angularVelocity = barrellSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        barrellBody.angularVelocity = barrellSpeed;
    }
    void BarrellCollision2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerCollider = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerCollider != null)
            {
                Vector2 pushDir = collision.transform.position-transform.position;
                pushDir.Normalize();
                playerCollider.AddForce(pushDir * force, ForceMode2D.Force);
            }

        }
    }
}