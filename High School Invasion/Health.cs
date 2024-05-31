using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    // Use this in the inspector to set max health of object
    public float _maxHealth;
    public float currentHealth;

    [Header("Armor Settings")]
    public bool hasArmor;
    public float _maxArmor;
    public float currentArmor;

    /// <summary>
    ///     Check if health is lower than 0, basically checks if dead. And check if value doesn't acceed max value
    /// </summary>
    protected virtual void CheckHealth()
    {
        if (currentHealth > _maxHealth)
        {
            currentHealth = _maxHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }

    /// <summary>
    ///     Take an amount of health from an object.
    /// </summary>
    /// <param name="amount">Amount of health to take</param>
    public void TakeHealth(float amount)
    {
        if (hasArmor)
        {
            if (currentArmor >= amount)
            {
                currentArmor -= amount;
                return;
            }
            else
            {
                amount -= currentArmor;
                currentArmor = 0;
            }
        }
        
        currentHealth -= amount;
        CheckHealth();

        if (hasArmor)
        {
            currentArmor = 0;
        }
    }


    /// <summary>
    ///     Give an amount of health to object.
    /// </summary>
    /// <param name="amount">Amount of health to give</param>
    public void GiveHealth(int amount)
    {
        currentHealth += amount;
        CheckHealth();
    }

    /// <summary>
    ///     Give an amount of armor to object.
    /// </summary>
    /// <param name="amount">Amount of armor to give</param>
    public void GiveArmor(int amount)
    {
        currentArmor += amount;
    }

    public float GetHealth()
    {
        return currentHealth;
    }
    
    public float GetArmor()
    {
        return currentArmor;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public float GetMaxArmor()
    {
        return _maxArmor;
    }
    
}
