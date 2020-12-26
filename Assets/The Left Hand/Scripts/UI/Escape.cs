using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    protected virtual void Cleanup()
    {
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || AlternativeCloseRequirementMet())
            Cleanup();
    }

    protected virtual bool AlternativeCloseRequirementMet() { return false; }
}
