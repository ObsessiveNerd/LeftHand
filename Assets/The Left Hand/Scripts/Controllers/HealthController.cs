using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{
    public TextMeshProUGUI HealthUI;

    public int Health;
    private const int m_MaxHealth = 10;

    private void Update()
    {
        if(HealthUI != null)
            HealthUI.text = $"Health: {Health}";
    }

    public void TakeDamage(int damageAmount)
    {
        Health = Mathf.Clamp(Health - damageAmount, 0, m_MaxHealth);
        if (Health == 0)
            Die();
    }

    public void Heal(int healAmount)
    {
        Debug.Log("Before: " + Health);
        Health = Mathf.Clamp(Health + healAmount, 0, m_MaxHealth);
        Debug.Log("After: " + Health);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
