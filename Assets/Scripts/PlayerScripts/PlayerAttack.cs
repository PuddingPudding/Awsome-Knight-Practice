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
    }
}
