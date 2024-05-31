using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private GameObject moneyDrop;

    private void Start()
    {
        currentHealth = _maxHealth;
        currentArmor = _maxArmor;
        moneyDrop = Resources.Load<GameObject>("Prefabs/Money Drop");
    }


    protected override void CheckHealth()
    {
        if (currentHealth > _maxHealth)
        {
            currentHealth = _maxHealth;
        }

        if (currentHealth <= 0)
        {
            Instantiate(moneyDrop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
