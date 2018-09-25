using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float m_fHP = 100f;
    [SerializeField] private Image m_healthBar;

    private bool m_bIsShielded;

    private Animator m_anim;

    private void Awake()
    {
        m_anim = this.GetComponent<Animator>();
    }

    public bool Shielded
    {
        get { return this.m_bIsShielded; }
        set { this.m_bIsShielded = value; }
    }

    public void TakeDmg(float _fDmg)
    {
        if (!this.m_bIsShielded)
        {
            this.m_fHP -= _fDmg;
            print("玩家承受 " + _fDmg + " 傷害，還剩 " + m_fHP + " HP");
            if (m_fHP <= 0)
            {
                m_anim.SetBool("Death", true);

                if (!m_anim.IsInTransition(0) && m_anim.GetCurrentAnimatorStateInfo(0).IsName("Death")
                    && m_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
                {
                    //玩家死去
                    //摧毀玩家
                    Destroy(this.gameObject, 2);
                }
            }
        }
        this.m_healthBar.fillAmount = m_fHP / 100;
    }

    public void Heal(float _fAmount)
    {
        this.m_fHP += _fAmount;
        if(m_fHP > 100)
        {
            m_fHP = 100;
        }
        print("玩家目前血量" + m_fHP);
        this.m_healthBar.fillAmount = m_fHP / 100;
    }
}
