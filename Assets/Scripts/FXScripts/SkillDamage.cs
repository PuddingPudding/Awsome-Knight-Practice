using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour
{

    [SerializeField] private LayerMask m_enemyLayer;
    [SerializeField] private float m_fRadius = 0.5f;
    [SerializeField] private float m_fDmgCount = 10;

    private EnemyHealth m_enemyHealth;
    private bool m_bColided = false;

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, m_fRadius, m_enemyLayer);

        foreach(Collider c in hits)
        {
            if(c.isTrigger)
            {
                continue;
            }
            m_enemyHealth = c.gameObject.GetComponent<EnemyHealth>();
            m_bColided = true;
        }
        if(this.m_bColided)
        {
            m_enemyHealth.TakeDmg(m_fDmgCount);
            this.enabled = false;
        }
    }
}
