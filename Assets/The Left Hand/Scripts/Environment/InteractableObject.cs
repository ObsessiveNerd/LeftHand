using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public virtual string UseWord => "Whoops!  Looks like someone fucked up.";

    public virtual bool Interact()
    {
        return false;
    }

    public virtual bool Interact(GameObject objectToUse)
    {
        return false;
    }

    InteractableController m_InteractableController;

    // Start is called before the first frame update
    void Awake()
    {
        m_InteractableController = GameObject.FindWithTag("Player").GetComponent<InteractableController>();
    }

    // Update is called once per frame
    protected void Update()
    {
        m_InteractableController.SubmitInteractable(gameObject);
    }
}
