using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject, IUnlockable
{
    public Key KeyToUnlock;
    public bool Locked;
    public GameObject DestroyWhenUnlocked;
    public string DescriptionText;
    public Action OnUnlock;
    public AudioClip DoorMovingClip;
    public AudioClip DoorUnlocked;

    AudioSource m_Audio;
    bool m_IsOpen = false;
    bool m_DoorStateChanging = false;

    new void Awake()
    {
        base.Awake();
        m_Audio = gameObject.AddComponent<AudioSource>();
        m_Audio.volume = 0.01f;
    }

    public override string UseWord
    {
        get
        {
            if (Locked)
                return "Unlock";
            if(!m_IsOpen)
                return "Open";
            return "Close";
        }
    }

    public override bool Interact()
    {
        if (Locked)
        {
            UIFactory.CreateDialogue(DescriptionText, true, null, 
                new FactoryButton("Use Item", 
                () =>
                {
                    UIFactory.Clear();
                    FindObjectOfType<Inventory>().OpenForUseWith((item) => Interact(item));
                }));
            
            return true;
        }

        if (!m_IsOpen)
            Open();
        else
            Close();
        return true;
    }

    public override bool Interact(GameObject objectToUse)
    {
        if (objectToUse == null)
            return false;

        Key key = objectToUse.GetComponent<Key>();
        if (key != null && key.KeyId == KeyToUnlock.KeyId)
        {
            Unlock();
            return true;
        }

        return false;
    }

    public void Open()
    {
        if (!Locked)
        {
            m_Audio.PlayOneShot(DoorMovingClip);
            StartCoroutine(ChangeDoorState(60f, true));
        }
    }

    public void Close()
    {
        m_Audio.PlayOneShot(DoorMovingClip);
        StartCoroutine(ChangeDoorState(-60f, false));
    }

    IEnumerator ChangeDoorState(float rotateDegrees, bool isOpen)
    {
        if (m_DoorStateChanging)
            yield break;

        m_DoorStateChanging = true;
        float duration = 0.5f;
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + rotateDegrees;
        float t = 0.0f;
        GetComponent<BoxCollider>().enabled = false;

        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.localEulerAngles = new Vector3(0f, yRotation, 0f);
            yield return null;
        }

        m_IsOpen = isOpen;
        GetComponent<BoxCollider>().enabled = true;
        m_DoorStateChanging = false;
    }

    public void Unlock()
    {
        if (DestroyWhenUnlocked != null)
            Destroy(DestroyWhenUnlocked);
        Locked = false;
        m_Audio.PlayOneShot(DoorUnlocked);
        OnUnlock?.Invoke();
    }
}
