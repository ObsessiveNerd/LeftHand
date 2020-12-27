using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{
    public TextMeshProUGUI HealthUI;
    public GameObject BloodSpurt;

    public int Health;
    private const int m_MaxHealth = 10;

    private void Update()
    {
        if (HealthUI != null)
            HealthUI.text = $"Health: {Health}";
    }

    public void TakeDamage(int damageAmount)
    {
        Health = Mathf.Clamp(Health - damageAmount, 0, m_MaxHealth);
        StartCoroutine(DestroyAfterTime(Instantiate(BloodSpurt, transform.position, Quaternion.identity)));
        if (Health == 0)
            Die();
    }

    public void Heal(int healAmount)
    {
        Health = Mathf.Clamp(Health + healAmount, 0, m_MaxHealth);
    }

    IEnumerator DestroyAfterTime(GameObject go)
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(go);
    }

    public void Die()
    {
        GetComponentInChildren<Animator>().SetBool(AnimatorVariables.Dead, true);
        GetComponent<IContoller>().Die();
    }
}

