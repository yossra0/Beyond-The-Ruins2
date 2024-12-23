using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health System")]
    public float healthPoints;
    public float maxHealth;

    // Unity's start method to initialize health
    void Start()
    {
        // Initialize health points based on maxHealth
        healthPoints = maxHealth;
    }

    // Method to get current health points
    public float GetHealth()
    {
        return healthPoints;
    }

    // Method to get health percentage (0 - 1 scale)
    public float GetHealthPercentage()
    {
        return healthPoints / maxHealth;
    }

    // Method to apply damage
    public void Damage(float damageAmount)
    {
        healthPoints -= damageAmount;

        // Ensure health doesn't go below 0
        if (healthPoints <= 0) 
        {
            healthPoints = 0;
            // Trigger death (optional)
            Die();
        }

        // You can add a callback or event here for when the health changes
        Debug.Log("Player took damage! Current Health: " + healthPoints);
    }

    // Method to heal the player
    public void Heal(float healAmount)
    {
        healthPoints += healAmount;

        // Ensure health doesn't exceed maxHealth
        if (healthPoints > maxHealth)
        {
            healthPoints = maxHealth;
        }

        // You can add a callback or event here for when health changes
        Debug.Log("Player healed! Current Health: " + healthPoints);
    }

    // Optional method to handle death logic
    private void Die()
    {
        // You can trigger death logic here like playing animations, restarting the game, etc.
        Debug.Log("Player has died!");
    }
}
