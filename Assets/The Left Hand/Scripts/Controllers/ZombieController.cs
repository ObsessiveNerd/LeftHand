using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour, IContoller
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
    public Animator Animator;
    public List<AudioClip> Moans = new List<AudioClip>();
    public AudioClip Slash;

    ZombieState m_State;
    GameObject m_Target;
    Rigidbody m_RigidBody;
    bool m_IsAttacking = false;
    NavMeshAgent m_Agent;
    bool m_Dead = false;
    AudioSource m_Audio;

    void Start()
    {
        m_State = ZombieState.Waiting;
        m_Target = GameObject.FindGameObjectWithTag("Player");
        m_RigidBody = GetComponent<Rigidbody>();
        m_Audio = GetComponent<AudioSource>();
        InitNavAgent();
    }

    void InitNavAgent()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = MoveSpeed;
    }

    bool m_AudioIsPlaying = false;
    IEnumerator PlayMoan()
    {
        m_AudioIsPlaying = true;
        yield return new WaitForSeconds(Random.Range(10, 100));
        m_Audio.PlayOneShot(Moans[Random.Range(0, Moans.Count - 1)]);
        m_AudioIsPlaying = false;
    }

    void Update()
    {
        if (m_Dead)
            return;

        if (!m_Audio.isPlaying && !m_AudioIsPlaying)
            StartCoroutine(PlayMoan());

        m_State = ZombieState.Waiting;
        if (ShouldMoveToPlayer())
            m_State = ZombieState.Moving;
        if (ShouldAttack())
            m_State = ZombieState.Attacking;

        switch (m_State)
        {
            case ZombieState.Waiting:
                Animator.SetBool(AnimatorVariables.Idle, true);
                Animator.SetBool(AnimatorVariables.Run, false);
                if(m_Agent.isActiveAndEnabled && m_Agent.isOnNavMesh)
                    m_Agent.isStopped = true;
                break;
            case ZombieState.Moving:
                Animator.SetBool(AnimatorVariables.Idle, false);
                Animator.SetBool(AnimatorVariables.Run, true);
                Move();
                break;
            case ZombieState.Attacking:
                Animator.SetBool(AnimatorVariables.Idle, true);
                Animator.SetBool(AnimatorVariables.Run, false);
                Animator.SetTrigger(AnimatorVariables.Attack);
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
        m_Agent.isStopped = false;
        m_Agent.SetDestination(m_Target.transform.position);
        Vector3 direction = (m_Target.transform.position - transform.position).normalized;
        //m_RigidBody.velocity = direction * MoveSpeed;
        Quaternion.LookRotation(direction);
    }

    IEnumerator Attack()
    {
        m_IsAttacking = true;
        m_Agent.isStopped = true;
        m_Target.GetComponent<HealthController>().TakeDamage(Damage);
        m_Audio.PlayOneShot(Slash);
        yield return new WaitForSeconds(AttackSpeed);
        m_IsAttacking = false;
    }

    public void Die()
    {
        m_Dead = true;
        m_Agent.isStopped = true;
        GetComponent<CapsuleCollider>().enabled = false;
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(200);
        Destroy(gameObject);
    }
}
