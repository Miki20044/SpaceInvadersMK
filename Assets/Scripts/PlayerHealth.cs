using UnityEngine;
using UnityEngine.SceneManagement; // Needed to reload the scene

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 1; // Player dies in one hit
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount = 1)
    {
        currentHealth -= amount;
        Debug.Log("Player hit! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died! Resetting game...");
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}