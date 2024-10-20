using UnityEngine;

public class FallPitTrigger : MonoBehaviour
{
    [SerializeField] private GameObject respawnPoint;

    [SerializeField] private int healthPenalty = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.RespawnPlayer(collision);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    private void RespawnPlayer(Collider2D playerCollider)
    {
        var playerGameObject = playerCollider.gameObject;

        if (playerGameObject == null)
        {
            return;
        }

        var playerHealth = playerCollider.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(healthPenalty);

        if (respawnPoint != null)
        {
            playerGameObject.transform.position = respawnPoint.transform.position;
            return;
        }

        var fallbackRespawnPoint = GameObject.FindGameObjectWithTag("Respawn");

        if (fallbackRespawnPoint == null)
        {
            Destroy(playerGameObject);
            return;
        }

        playerGameObject.transform.position = fallbackRespawnPoint.transform.position;
    }
}