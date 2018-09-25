using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BossStateChecker))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class BossControl : MonoBehaviour
{
    [SerializeField] private Transform m_playerTarget;
    [SerializeField] private float m_fWaitAtkTime = 1;
    private BossStateChecker m_bossStateChecker;
    private NavMeshAgent m_navAgent;
    private Animator m_anim;
    private bool m_bFinishedAtk = true;
    private float m_fCurrentAtkTime;


    // Use this for initialization
    void Awake()
    {
        m_bossStateChecker = this.GetComponent<BossStateChecker>();
        m_navAgent = this.GetComponent<NavMeshAgent>();
        m_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.m_bFinishedAtk)
        {
            this.GetStateControl();
        }
        else
        {
            m_anim.SetInteger("Atk", 0);
            if (!this.m_anim.IsInTransition(0) && this.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                this.m_bFinishedAtk = true;
            }
        }
    }

    private void GetStateControl()
    {
        if (this.m_bossStateChecker.BossState == EBOSS_STATE.DEATH)
        {
            this.m_navAgent.isStopped = true;
            this.m_anim.SetBool("Death", true);
            Destroy(this.gameObject, 3);
        }
        else
        {
            if (this.m_bossStateChecker.BossState == EBOSS_STATE.PAUSE)
            {
                this.m_navAgent.isStopped = false;
                this.m_anim.SetBool("Run", true);
                this.m_navAgent.SetDestination(this.m_playerTarget.position);
            }
            else if (this.m_bossStateChecker.BossState == EBOSS_STATE.ATTACK)
            {
                this.m_anim.SetBool("Run", false);
                Vector3 targetPosition = new Vector3(this.m_playerTarget.position.x, this.transform.position.y
                    , this.m_playerTarget.position.z);

                transform.rotation = Quaternion.Slerp(transform.rotation
                    , Quaternion.LookRotation(targetPosition - this.transform.position), 5 * Time.deltaTime);

                if(this.m_fCurrentAtkTime >= this.m_fWaitAtkTime)
                {
                    int iAtkRange = Random.Range(1, 5);
                    this.m_anim.SetInteger("Atk", iAtkRange);

                    this.m_fCurrentAtkTime = 0;
                    this.m_bFinishedAtk = false;
                }
                else
                {
                    this.m_anim.SetInteger("Atk", 0);
                    this.m_fCurrentAtkTime += Time.deltaTime;
                }
            }
            else
            {
                this.m_anim.SetBool("Run", false);
                this.m_navAgent.isStopped = true;
            }
        }
    }
}
