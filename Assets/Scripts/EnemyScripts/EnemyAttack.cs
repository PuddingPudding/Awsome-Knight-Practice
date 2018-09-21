using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float m_fDamageAmount;

    private Transform m_playerTarget;
    private Animator m_anim;
    private bool m_bFinishedAtk = true;
    private float m_fDamageDistance = 2;
    private PlayerHealth m_playerHealth;

    void Awake()
    {
        m_playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        m_anim = this.GetComponent<Animator>();

        m_playerHealth = m_playerTarget.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.m_bFinishedAtk)
        {
            this.DealDamage(CheckIfAttacking());
        }
        else
        {
            if (!this.m_anim.IsInTransition(0) && this.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                this.m_bFinishedAtk = true;
            }
        }
    }

    bool CheckIfAttacking()
    {
        bool bIsAttacking = false;

        if (!m_anim.IsInTransition(0) && m_anim.GetCurrentAnimatorStateInfo(0).IsName("Atk1")
            || m_anim.GetCurrentAnimatorStateInfo(0).IsName("Atk2"))
        {
            if (m_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                bIsAttacking = true;
                m_bFinishedAtk = false;
            }
        }
        return bIsAttacking;
    }

    void DealDamage(bool _bIsAttacking)
    {
        if (_bIsAttacking)
        {
            if (Vector3.Distance(transform.position, m_playerTarget.position) <= m_fDamageDistance)
            {
                m_playerHealth.TakeDmg(this.m_fDamageAmount);
            }
        }
    }
}
