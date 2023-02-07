using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int healthPoint;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(pickupSound, transform.position);
            // Rendre de la vie
            PlayerHealth.instance.HealPlayer(healthPoint);
            Destroy(gameObject);
        }
    }
}
