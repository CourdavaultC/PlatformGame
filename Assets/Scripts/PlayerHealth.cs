using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float InvicibilityTimeAfterHit = 3f;
    public float invicibilityFlashDelay= 0.2f;

    public bool isInvicible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;
    
    public AudioClip hitSound;
    public static PlayerHealth instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }    

    void Start()
    {
       currentHealth = maxHealth;
       healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public void HealPlayer(int amount)
    {
        // Si la vie est au max
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if(!isInvicible)
        {
            AudioManager.instance.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            // vérifier si joueur est toujours vivant
            if (currentHealth <=0)
            {
                Die();
                return;
            }

            isInvicible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
        }
    }

    public void Die()
    {
        //Bloquer les mouvements du perso
        PlayerMovement.instance.enabled = false;
        //Jouer l'animation
        PlayerMovement.instance.animator.SetTrigger("Die");
        //Empêcher les interacts physiques
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.rb.velocity = Vector3.zero;
        PlayerMovement.instance.playerCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator InvicibilityFlash()
    {
        while (isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(InvicibilityTimeAfterHit);
        isInvicible = false;
    }
}
