using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown
{
    public float CooldownSeconds;

    public bool IsReady = true;

    public IEnumerator BeginCooldown()
    {
        IsReady = false;
        yield return new WaitForSeconds(CooldownSeconds);
        IsReady = true;
    }
}
