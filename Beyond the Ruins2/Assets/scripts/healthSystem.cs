using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float healthPoints;
    private float maxHealth;

    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        healthPoints = maxHealth;
    }

    public float gethealth()
    {
        return healthPoints;
    }

    public float gethealthprecentage()
    {
        return healthPoints / maxHealth;
    }

    // Start is called before the first frame update
    public void Damage(int damageAmount)
    {
        healthPoints -= damageAmount;
        if (healthPoints <= 0)
        {
            healthPoints = 0;
        }
    }

    // Update is called once per frame
    public void heal(int healAmount)
    {
        healthPoints += healAmount;
        if (healthPoints > maxHealth)
        {
            healthPoints = maxHealth;
        }
    }
}
