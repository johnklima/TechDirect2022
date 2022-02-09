using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public Text currentHealthLabel;
    public int currentHealth;

   public void Start()
    {
        currentHealth = maxHealth;
        UpdateGUI();
    }

    void UpdateGUI()
    {
        currentHealthLabel.text = currentHealth.ToString();
    }

    public void AlterHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateGUI();
    }

}
