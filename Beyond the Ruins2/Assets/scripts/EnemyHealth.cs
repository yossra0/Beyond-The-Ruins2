using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 5;
    int currentHealth;
    void Awake()
    {
        currentHealth = startingHealth;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;  
        if(currentHealth <= 0) 
        {
            Destroy(this.gameObject);
        }
    }
}
