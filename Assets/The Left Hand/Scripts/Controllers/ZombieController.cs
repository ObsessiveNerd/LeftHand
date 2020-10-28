﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    enum ZombieState
    {
        Waiting,
        Moving,
        Attacking
    }

    public float MoveSpeed;
    public float FollowRange;
    public float AttackRange;
    public float AttackSpeed;
    public int Damage;

    ZombieState m_State;
    GameObject m_Target;
    Rigidbody m_RigidBody;
    bool m_IsAttacking = false;

    void Start()
    {
        m_State = ZombieState.Waiting;
        m_Target = GameObject.FindGameObjectWithTag("Player");
        m_RigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_State = ZombieState.Waiting;
        if (ShouldMoveToPlayer())
            m_State = ZombieState.Moving;
        if (ShouldAttack())
            m_State = ZombieState.Attacking;

        switch (m_State)
        {
            case ZombieState.Waiting:
                break;
            case ZombieState.Moving:
                Move();
                break;
            case ZombieState.Attacking:
                StartCoroutine(Attack());
                break;
        }
    }

    bool ShouldMoveToPlayer()
    {
        float distance = Vector3.Distance(transform.position, m_Target.transform.position);
        if (distance < FollowRange && distance > AttackRange)
            return true;
        return false;
    }

    bool ShouldAttack()
    {
        float distance = Vector3.Distance(transform.position, m_Target.transform.position);
        if (distance < AttackRange && !m_IsAttacking)
            return true;
        return false;
    }

    void Move()
    {
        Vector3 direction = (m_Target.transform.position - transform.position).normalized;
        m_RigidBody.velocity = direction * MoveSpeed;
        Quaternion.LookRotation(direction);
    }

    IEnumerator Attack()
    {
        m_IsAttacking = true;
        m_Target.GetComponent<HealthController>().TakeDamage(Damage);
        yield return new WaitForSeconds(AttackSpeed);
        m_IsAttacking = false;
    }
}
