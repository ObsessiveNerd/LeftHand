using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : InteractableObject, IWeapon, IReloadable, IInventoryItem
{
    public GameObject Bullet;
    public int StartingBullets;
    public int kMaxBullets = 10;
    public GameObject Prefab;

    private int m_BulletCount;

    public string WeaponName
    {
        get
        {
            return "Handgun";
        }
    }

    public int MaxAmmo
    {
        get
        {
            return kMaxBullets;
        }
    }

    public int CurrentAmmo
    {
        get
        {
            return m_BulletCount;
        }
    }

    public override string UseWord
    {
        get
        {
            return "Pickup Handgun";
        }
    }

    GameObject IInventoryItem.Prefab => Prefab;

    void Start()
    {
        m_BulletCount = Mathf.Clamp(StartingBullets, 0, kMaxBullets);
    }

    public void Attack(GameObject source, Vector3 direction)
    {
        if (m_BulletCount > 0)
        {
            GameObject bullet = Instantiate(Bullet);
            bullet.transform.position = source.transform.position;
            bullet.transform.rotation = Quaternion.LookRotation(direction);
            bullet.GetComponent<Rigidbody>().velocity = direction * 10f;

            m_BulletCount--;
        }
    }

    public void Reload(int numberOfBullets, out int leftOverBullets)
    {
        int bulletDifference = kMaxBullets - (m_BulletCount + numberOfBullets);
        if (bulletDifference < 0)
            leftOverBullets = bulletDifference * -1;
        else
            leftOverBullets = 0;

        m_BulletCount += (numberOfBullets - leftOverBullets);
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
