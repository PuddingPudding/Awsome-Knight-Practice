using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecialAttack : MonoBehaviour
{
    [SerializeField] private GameObject m_bossFire;
    [SerializeField] private GameObject m_bossMagic;
    [SerializeField] private Transform m_playerTarget;

    // Use this for initialization
    void Awake()
    {
    }

    public void BossFireTornado()
    {
        Instantiate(this.m_bossFire, this.m_playerTarget.position
            , Quaternion.Euler(0, Random.Range(0, 360), 0));
    }

    public void BossSpell()
    {
        Vector3 tempPos = this.m_playerTarget.position;
        tempPos.y += 1.5f;
        Instantiate(this.m_bossMagic, tempPos, Quaternion.identity);
    }
}
