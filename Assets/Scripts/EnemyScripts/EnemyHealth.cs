using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private float m_fHealth = 100f;

    public float GetHp()
    {
        return this.m_fHealth;
    }

    public void TakeDmg(float _fDmg)
    {
        m_fHealth -= _fDmg;
        print("敵人承受" + _fDmg + "傷害，還剩" + m_fHealth + "HP");
        if(m_fHealth <= 0)
        {

        }
    }
}
