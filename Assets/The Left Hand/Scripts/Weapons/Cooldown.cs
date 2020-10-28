using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public float CooldownSeconds;

    protected bool IsReady = true;

    protected IEnumerator BeginCooldown()
    {
        IsReady = false;
        yield return new WaitForSeconds(CooldownSeconds);
        IsReady = true;
    }
}
