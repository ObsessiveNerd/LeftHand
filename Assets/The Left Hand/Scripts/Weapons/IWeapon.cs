using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    string WeaponName { get; }
    void Attack(GameObject source, Vector3 direction);
    AudioClip AttackSound { get; }
}

public interface IReloadable
{
    int CurrentAmmo { get; }
    int MaxAmmo { get; }
    void Reload(int reloadBullets, out int leftOverBullets);
}
