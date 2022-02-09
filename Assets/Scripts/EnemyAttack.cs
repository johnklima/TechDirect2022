using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{

    public float enemyCooldown = 1;
    public int damage = 1;

    private bool playerInRange = false;
    private bool canAttack = true;

    private void Update()
    {
         if (playerInRange && canAttack)
         {
             GameObject.Find("Player").GetComponent<PlayerHealth>().currentHealth -= damage;
             StartCoroutine(AttackCooldown());
         }
    }
        
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            playerInRange = false;
        }
    }
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
    }
}