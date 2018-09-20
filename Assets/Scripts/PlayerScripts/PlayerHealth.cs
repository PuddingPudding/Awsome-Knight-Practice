using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float m_fHP = 100f;

    private bool m_bIsShielded;
    
    public bool Shielded
    {
        get { return this.m_bIsShielded;  }
        set { this.m_bIsShielded = value; }
    }

    public void TakeDmg(float _fDmg)
    {
        if(!this.m_bIsShielded)
        {
            this.m_fHP -= _fDmg;

            if(m_fHP <= 0)
            {

            }
        }
    }

    public void Heal(float _fAmount)
    {
        this.m_fHP += _fAmount;
        print("玩家目前血量" + m_fHP);
    }
}
