using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBOSS_STATE
{
    NONE,
    IDLE,
    PAUSE,
    ATTACK,
    DEATH
}

[RequireComponent(typeof(EnemyHealth))]
public class BossStateChecker : MonoBehaviour
{
    [SerializeField] private Transform m_playerTarget;
    private EBOSS_STATE m_bossState = EBOSS_STATE.NONE;
    private float m_fDistanceToTarget;
    private EnemyHealth m_bossHealth;

    // Use this for initialization
    void Awake()
    {
        m_bossHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        this.SetState();
    }
    private void SetState()
    {
        this.m_fDistanceToTarget = Vector3.Distance(transform.position, m_playerTarget.transform.position);

        if (m_bossState != EBOSS_STATE.DEATH)
        {
            if (m_fDistanceToTarget > 3 && m_fDistanceToTarget <= 15)
            {
                m_bossState = EBOSS_STATE.PAUSE;
            }
            else if (m_fDistanceToTarget > 15)
            {
                m_bossState = EBOSS_STATE.IDLE;
            }
            else if (m_fDistanceToTarget <= 3)
            {
                m_bossState = EBOSS_STATE.ATTACK;
            }
            else
            {
                m_bossState = EBOSS_STATE.NONE;
            }

            if (m_bossHealth.GetHp() <= 0)
            {
                m_bossState = EBOSS_STATE.DEATH;
            }
        }
    }
    public EBOSS_STATE BossState
    {
        get { return this.m_bossState; }
        set { this.m_bossState = value; }
    }
}
