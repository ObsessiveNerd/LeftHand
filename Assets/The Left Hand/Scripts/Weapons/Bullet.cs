using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage; 

    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            var healthController = other.GetComponent<HealthController>();
            if (healthController != null)
                healthController.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
