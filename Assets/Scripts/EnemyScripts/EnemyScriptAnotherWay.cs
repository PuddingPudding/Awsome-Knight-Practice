using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScriptAnotherWay : MonoBehaviour
{
    [SerializeField] private Transform[] m_walkPoints;
    private int m_iWalkIndex = 0;
    private Transform m_playerTarget;
    private Animator m_anim;
    private NavMeshAgent m_navAgent;
    private float m_fWalkDis = 8;
    private float m_fAtkDis = 2;
    private float m_fCurAtkTime;
    private float m_fWaitAtkTime = 1;
    private Vector3 m_nextDestination;


    // Use this for initialization
    void Awake()
    {
        m_playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        m_anim = this.GetComponent<Animator>();
        m_navAgent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float fDistance = Vector3.Distance(this.transform.position, m_playerTarget.position);
        if(fDistance > m_fWalkDis)
        {
            if(m_navAgent.remainingDistance <= 0.5f)
            {
                m_navAgent.isStopped = false; //已暫停為假，所以還在運作

                m_anim.SetBool("Walk", true);
                m_anim.SetBool("Run", false);
                m_anim.SetInteger("Atk", 0);

                m_nextDestination = m_walkPoints[m_iWalkIndex].position;
                m_navAgent.SetDestination(m_nextDestination);

                if(m_iWalkIndex >= m_walkPoints.Length - 1)
                {
                    m_iWalkIndex = 0;
                }
                else
                {
                    m_iWalkIndex++;
                }
            }
        }
        else
        {
            if(fDistance > m_fAtkDis)
            {
                m_navAgent.isStopped = false;

                m_anim.SetBool("Walk", false);
                m_anim.SetBool("Run", true);
                m_anim.SetInteger("Atk", 0);

                m_navAgent.SetDestination(m_playerTarget.position);
            }
            else
            {
                m_navAgent.isStopped = true;

                m_anim.SetBool("Run", false);

                Vector3 targetPos = new Vector3(m_playerTarget.position.x, this.transform.position.y
                    , m_playerTarget.position.z);

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation 
                    , Quaternion.LookRotation(targetPos - this.transform.position), 5 * Time.deltaTime);

                if(m_fCurAtkTime >= m_fWaitAtkTime)
                {
                    int iAtkRange = Random.Range(1, 3);
                    m_anim.SetInteger("Atk", iAtkRange);
                    m_fCurAtkTime = 0;
                }
                else
                {
                    m_anim.SetInteger("Atk", 0);
                    m_fCurAtkTime += Time.deltaTime;
                }
            }
        }
    }
}
