﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI Ammo;
    public Animator Animator;
    public float MoveSpeed;
    public GameObject RightHandContainer;

    public IWeapon CurrentEquipment
    {
        get
        {
            return m_CurrentWeapon;
        }
    }

    private float m_BaseMoveSpeed;
    private bool m_IsAttacking;
    private Rigidbody m_RigidBody;
    private IWeapon m_CurrentWeapon;
    private GameObject m_CurrentWeaponInstance;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_BaseMoveSpeed = MoveSpeed;
    }

    void Update()
    {
        CheckForAttacking();        
        ProcessMovement();

        DebugControls();

        if (Ammo != null)
        {
            if (m_CurrentWeapon != null && m_CurrentWeapon is IReloadable)
            {
                IReloadable reloadable = m_CurrentWeapon as IReloadable;
                Ammo.text = $"Ammo: {reloadable.CurrentAmmo}/{reloadable.MaxAmmo}";
            }
            else if (m_CurrentWeapon != null)
                Ammo.text = $"Weapon: {m_CurrentWeapon.WeaponName}";
            else
                Ammo.text = "No Weapon Equiped";
        }
    }

    void DebugControls()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            var inventory = GetComponent<Inventory>();
            inventory.Open();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(m_CurrentWeapon is IReloadable)
            {
                (m_CurrentWeapon as IReloadable).Reload(5, out int leftover);
                Debug.Log("Leftover bullets: " + leftover);
            }
        }
    }

    void CheckForAttacking()
    {
        if (Input.GetMouseButton(1))
        {
            m_RigidBody.velocity = Vector3.zero;
            m_IsAttacking = true;
        }
        else
        {
            MoveSpeed = m_BaseMoveSpeed;
            m_IsAttacking = false;
        }

        if(m_IsAttacking)
        {
            //Get the Screen positions of the object
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //Get the angle between the points
            float angle = MathUtility.AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

            //Ta Daaa
            transform.rotation = Quaternion.Euler(new Vector3(0f, 270-angle, 0f));
        }

        if (m_IsAttacking && Input.GetMouseButtonDown(0))
        {
            if (m_CurrentWeapon != null)
            {
                Animator.SetTrigger(AnimatorVariables.Attack);
                m_CurrentWeapon?.Attack(gameObject, transform.forward);
            }
        }
    }

    void ProcessMovement()
    {
        if (m_IsAttacking)
        {
            Animator.SetBool(AnimatorVariables.Idle, false);
            Animator.SetBool(AnimatorVariables.Run, false);
            return;
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));//.normalized;
        m_RigidBody.velocity = movement * MoveSpeed;

        if (movement != Vector3.zero)
        {
            Animator.SetBool(AnimatorVariables.Idle, false);
            Animator.SetBool(AnimatorVariables.Run, true);
            transform.rotation = Quaternion.LookRotation(movement);
        }
        else
        {
            Animator.SetBool(AnimatorVariables.Run, false);
            Animator.SetBool(AnimatorVariables.Idle, true);
        }
    }

    public void Equip(IWeapon weapon)
    {
        if (weapon != null)
        {
            m_CurrentWeapon = weapon;
            if (m_CurrentWeapon is Handgun)
                Animator.SetBool(AnimatorVariables.HasGun, true);
            if (m_CurrentWeapon is Knife)
                Animator.SetBool(AnimatorVariables.HasMelee, true);

            m_CurrentWeaponInstance = Instantiate(Resources.Load<GameObject>(weapon.WeaponName), RightHandContainer.transform);
            m_CurrentWeaponInstance.GetComponent<InteractableObject>().enabled = false;
        }
    }

    public void UnequipWeapon()
    {
        m_CurrentWeapon = null;
        Animator.SetBool(AnimatorVariables.HasMelee, false);
        Animator.SetBool(AnimatorVariables.HasGun, false);
        Destroy(m_CurrentWeaponInstance);
    }
}
