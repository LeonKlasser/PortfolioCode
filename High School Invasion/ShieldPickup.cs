using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : Interactable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().EquipArmor();
            other.GetComponent<PlayerHealth>().GiveArmor(1);
        }
        Destroy(gameObject);
    }
}
