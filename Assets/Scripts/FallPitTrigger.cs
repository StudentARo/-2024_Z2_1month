using UnityEngine;

public class FallPitTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject respawnPoint;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.RespawnPlayer();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    private void RespawnPlayer()
    {
        var playerGameObject = GameObject.FindGameObjectWithTag("Player");

        if (playerGameObject == null)
        {
            return;
        }

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