using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyState
{
    IDLE,
    WALK,
    RUN,
    PAUSE,
    GOBACK,
    ATTACK,
    DEATH
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : MonoBehaviour
{
    private float m_fAtkDistance = 1.5f;
    private float m_fAlrtAtkDistance = 8f;
    private float m_fFollowDistance = 15f; //與出生點相距的最大距離(巡邏範圍)
    private float m_fEnemyToPlayerDistance;
    private EnemyState m_enemyCurrentState = EnemyState.IDLE;
    private EnemyState m_enemyLastState = EnemyState.IDLE;
    private Transform m_playerTarget;
    private Vector3 m_initialPos;
    private float m_fMovSpeed = 2;
    private float m_fWalkSpeed = 1;
    private CharacterController m_charController;
    private Vector3 m_whereToMove = Vector3.zero;

    //攻擊值
    private float m_fCurrentAtkTime;
    private float m_fWaitAtkTime = 1f;

    private Animator m_anim;
    private bool m_bFinishedAnim = true;
    private bool m_bFinishedMove = true;

    private NavMeshAgent m_navAgent;
    private Vector3 m_whereToNavigate;

    //生命值部分

    public EnemyState CurrentState { get { return this.m_enemyCurrentState; } }

    // Use this for initialization
    void Start()
    {
        this.m_playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        this.m_navAgent = this.GetComponent<NavMeshAgent>();
        this.m_charController = this.GetComponent<CharacterController>();
        this.m_anim = this.GetComponent<Animator>();

        this.m_initialPos = this.transform.position;
        this.m_whereToNavigate = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        //如果HP<=0我們在設定狀態
        if(m_enemyCurrentState != EnemyState.DEATH)
        {
            this.m_enemyCurrentState = this.SetEnemyState(m_enemyCurrentState, m_enemyLastState
                , m_fEnemyToPlayerDistance);
            if (this.m_bFinishedMove)
            {
                this.GetStateControl(this.m_enemyCurrentState);
            }
            else
            {
                if(!m_anim.IsInTransition(0) && m_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") )
                {
                    this.m_bFinishedMove = true;
                }
                else if(!m_anim.IsInTransition(0) && m_anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk1")
                    || m_anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk2") )
                {
                    m_anim.SetInteger("Atk", 0);
                }
            }
        }
        else
        {
            m_anim.SetBool("Death", true);
            m_charController.enabled = false;
            m_navAgent.enabled = false;

            if(!this.m_anim.IsInTransition(0) && this.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Death") 
                && this.m_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Destroy(this.gameObject, 2f);
            }
        }
    }

    EnemyState SetEnemyState(EnemyState _curState, EnemyState _lastState , float _fEnemyToPlayerDis)
    {
        float fInitDis = Vector3.Distance(m_initialPos, this.transform.position); //計算出生點到自己現在位子的距離
        _fEnemyToPlayerDis = Vector3.Distance(this.transform.position, m_playerTarget.position); //計算自己目前到玩家那的距離

        if(fInitDis > m_fFollowDistance) //如果現在離初始出生點距離太遠(超出巡邏範圍)的話，將狀態改為往回走
        {
            _lastState = _curState;
            _curState = EnemyState.GOBACK;
        }
        else if(_fEnemyToPlayerDis <= m_fAtkDistance) //敵人到玩家間的距離小於等於攻擊距離，則狀態改為攻擊
        {
            _lastState = _curState;
            _curState = EnemyState.ATTACK; 
        }
        else if(_fEnemyToPlayerDis >= m_fAlrtAtkDistance  //敵人到玩家的距離還不至於警戒，且上個狀態是停滯或攻擊的話就先停滯
            && (_lastState == EnemyState.PAUSE || _lastState == EnemyState.ATTACK))
        {
            _lastState = _curState;
            _curState = EnemyState.PAUSE;
        }
        else if(_fEnemyToPlayerDis <= m_fAlrtAtkDistance && _fEnemyToPlayerDis > m_fAtkDistance)
        {
            if(_curState !=  EnemyState.GOBACK || _lastState == EnemyState.WALK)
            {
                _lastState = _curState;
                _curState = EnemyState.RUN;
            }            
        }
        else if(_fEnemyToPlayerDis > m_fAlrtAtkDistance 
            && _lastState != EnemyState.GOBACK && _lastState != EnemyState.PAUSE)
        {
            _lastState = _curState;
            _curState = EnemyState.WALK;
        }
        return _curState;
    }

    void GetStateControl(EnemyState _curState)
    {
        if(_curState == EnemyState.RUN || _curState == EnemyState.PAUSE)
        {
            if(_curState != EnemyState.ATTACK)
            {
                Vector3 targetPos = new Vector3(m_playerTarget.position.x, transform.position.y, 
                    m_playerTarget.position.z);

                if(Vector3.Distance(this.transform.position , targetPos) >= 2.1f)
                {
                    this.m_anim.SetBool("Walk", false);
                    this.m_anim.SetBool("Run", true);

                    this.m_navAgent.SetDestination(targetPos);
                }
            }
                
        }
        else if(_curState == EnemyState.ATTACK)
        {
            this.m_anim.SetBool("Run", false);
            this.m_whereToMove.Set(0, 0, 0);

            this.m_navAgent.SetDestination(this.transform.position);

            this.transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(this.m_playerTarget.position - this.transform.position), 5 * Time.deltaTime);

            if(m_fCurrentAtkTime >= m_fWaitAtkTime)
            {
                int iAtkRange = Random.Range(1, 3);

                this.m_anim.SetInteger("Atk", iAtkRange);
                m_bFinishedAnim = false;
                m_fCurrentAtkTime = 0;
            }
            else
            {
                this.m_anim.SetInteger("Atk", 0);
                m_fCurrentAtkTime += Time.deltaTime;            
            }
        }
        else if(_curState == EnemyState.GOBACK)
        {
            this.m_anim.SetBool("Run", true);
            Vector3 targetPos = new Vector3(m_initialPos.x, this.transform.position.y,
                m_initialPos.z);

            m_navAgent.SetDestination(targetPos);

            if(Vector3.Distance(targetPos , m_initialPos) <= 3.5f)
            {
                m_enemyLastState = _curState;
                _curState = EnemyState.WALK;
            }
        }
        else if(_curState == EnemyState.WALK)
        {
            this.m_anim.SetBool("Run", false);
            this.m_anim.SetBool("Walk", true);

            if(Vector3.Distance(this.transform.position , m_whereToNavigate) <= 2f)
            {
                m_whereToNavigate.x = Random.Range(m_initialPos.x - 5, m_initialPos.x + 5);
                m_whereToNavigate.z = Random.Range(m_initialPos.z - 5, m_initialPos.z + 5);
            }
            else
            {
                m_navAgent.SetDestination(m_whereToNavigate);
            }
        }
        else
        {
            this.m_anim.SetBool("Run", false);
            this.m_anim.SetBool("Walk", false);
            this.m_whereToMove.Set(0, 0, 0);
            this.m_navAgent.isStopped = true;
        }
    }
}
