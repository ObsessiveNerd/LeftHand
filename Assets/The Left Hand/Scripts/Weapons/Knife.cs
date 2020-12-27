using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : InteractableObject, IWeapon
{
    public int Damage;
    public float Distance;
    public GameObject Prefab;
    public AudioClip AttackClip;

    Vector3 m_StartPosition;
    Quaternion m_StartRot;
    Vector3 m_StartScale;
    Cooldown m_Cooldown;
    public override string UseWord
    {
        get 
        {
            return "Pickup knife";
        }
    }

    public string WeaponName
    {
        get
        {
            return "Knife";
        }
    }

    public AudioClip AttackSound => AttackClip;

    void Start()
    {
        m_Cooldown = new Cooldown();
    }

    new void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Debug.DrawRay(player.transform.position, player.transform.forward);
    }

    public void Attack(GameObject source, Vector3 direction)
    {
        if (m_Cooldown == null)
            m_Cooldown = new Cooldown();

        RaycastHit hitInfo;
        var player = GameObject.FindGameObjectWithTag("Player");
        Debug.DrawRay(player.transform.position, player.transform.forward);
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hitInfo))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider?.gameObject.tag == "Enemy" &&
                Vector3.Distance(hitInfo.collider.gameObject.transform.position, source.transform.position) < Distance &&
                m_Cooldown.IsReady)
            {
                var healthController = hitInfo.collider.gameObject.GetComponent<HealthController>();
                healthController.TakeDamage(Damage);
                if(isActiveAndEnabled)
                    StartCoroutine(m_Cooldown.BeginCooldown());
            }
        }
    }

    public override bool Interact()
    {
        Pickup.PickupItem(gameObject);
        return true;
    }

    public override bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
