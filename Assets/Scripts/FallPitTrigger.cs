using UnityEngine;

public class FallPitTrigger : MonoBehaviour
{
    [SerializeField] private GameObject respawnPoint;

    [SerializeField] private int healthPenalty = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.RespawnPlayer(collision.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        if (player == null)
        {
            return;
        }

        var playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(healthPenalty);

        if (respawnPoint != null)
        {
            player.transform.position = respawnPoint.transform.position;
            return;
        }

        var fallbackRespawnPoint = GameObject.FindGameObjectWithTag("Respawn");

        if (fallbackRespawnPoint == null)
        {
            Destroy(player);
            return;
        }

        player.transform.position = fallbackRespawnPoint.transform.position;
    }
}