using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMove))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Image m_imgCD1;
    [SerializeField] private Image m_imgCD2;
    [SerializeField] private Image m_imgCD3;
    [SerializeField] private Image m_imgCD4;
    [SerializeField] private Image m_imgCD5;
    [SerializeField] private Image m_imgCD6;
    [SerializeField] private MouseScript m_mouseScript;

    private int[] m_iArrFadeImg = new int[] { 0, 0, 0, 0, 0, 0 };
    private Animator m_anim;
    private bool m_bCanAtk = true;
    private PlayerMove m_playerMove;

    void Awake()
    {
        this.m_anim = this.GetComponent<Animator>();
        this.m_playerMove = this.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.m_anim.IsInTransition(0) && this.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Stand") )
        {
            this.m_bCanAtk = true;
        }
        else
        {
            this.m_bCanAtk = false;
        }

        this.CheckToFade();
        this.CheckInput();
    }

    public void CheckInput()
    {
        if(this.m_anim.GetInteger("Atk") == 0)
        {
            this.m_playerMove.FinishedMovement = false;

            if(!this.m_anim.IsInTransition(0) && this.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
            {
                this.m_playerMove.FinishedMovement = true;
            }
        }

        int iSkillChoice = -1; //選擇施展的技能，沒有按的話預設為-1(沒放招)
        if(Input.GetKeyDown(KeyCode.Q) )
        {
            iSkillChoice = 0; //技能1
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            iSkillChoice = 1;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            iSkillChoice = 2;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            iSkillChoice = 3;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            iSkillChoice = 4;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            iSkillChoice = 5;
        }

        if(iSkillChoice > -1)
        {
            this.m_playerMove.TargetPosition = this.transform.position;
            //m_iArrFadeImg[0]代表著索引值為0的圖片，也就是第一個技能
            if (this.m_playerMove.FinishedMovement && this.m_iArrFadeImg[iSkillChoice] != 1 && this.m_bCanAtk)
            {
                this.m_iArrFadeImg[iSkillChoice] = 1;
                this.m_anim.SetInteger("Atk", iSkillChoice +1);
            }
        }
        else
        {
            this.m_anim.SetInteger("Atk", 0);
        }

        if(Input.GetKey(KeyCode.Space) )
        {
            Vector3 targetPos = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray , out hit))
            {
                targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }

            transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(targetPos - transform.position), 15 * Time.deltaTime);
        }
    } //check input

    void CheckToFade()
    {
        if(this.m_iArrFadeImg[0] == 1)
        {
            this.m_mouseScript.ClearCursor();
            if(FadeAndWait(this.m_imgCD1 , 1.0f))
            {
                this.m_iArrFadeImg[0] = 0;
            }
        }
        if (this.m_iArrFadeImg[1] == 1)
        {
            this.m_mouseScript.ClearCursor();
            if (FadeAndWait(this.m_imgCD2, 1.5f))
            {
                this.m_iArrFadeImg[1] = 0;
            }
        }
        if (this.m_iArrFadeImg[2] == 1)
        {
            this.m_mouseScript.ClearCursor();
            if (FadeAndWait(this.m_imgCD3, 10))
            {
                this.m_iArrFadeImg[2] = 0;
            }
        }
        if (this.m_iArrFadeImg[3] == 1)
        {
            this.m_mouseScript.ClearCursor();
            if (FadeAndWait(this.m_imgCD4, 5))
            {
                this.m_iArrFadeImg[3] = 0;
            }
        }
        if (this.m_iArrFadeImg[4] == 1)
        {
            this.m_mouseScript.ClearCursor();
            if (FadeAndWait(this.m_imgCD5, 3.5f))
            {
                this.m_iArrFadeImg[4] = 0;
            }
        }
        if (this.m_iArrFadeImg[5] == 1)
        {
            this.m_mouseScript.ClearCursor();
            if (FadeAndWait(this.m_imgCD6, 13))
            {
                this.m_iArrFadeImg[5] = 0;
            }
        }
    }

    bool FadeAndWait(Image _fadeImg , float _fFadeTime)
    {
        bool bFaded = false;
        if(_fadeImg == null)
        {
            return bFaded;
        }

        if(!_fadeImg.gameObject.activeInHierarchy)
        {
            _fadeImg.gameObject.SetActive(true);
            _fadeImg.fillAmount = 1f;
        }
        _fadeImg.fillAmount -= (1 / _fFadeTime) * Time.deltaTime;
        if(_fadeImg.fillAmount <= 0)
        {
            _fadeImg.gameObject.SetActive(false);
            bFaded = true;
        }
        return bFaded;
    }
}
