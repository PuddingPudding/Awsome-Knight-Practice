using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFXDamage : MonoBehaviour
{
    [SerializeField] private LayerMask m_playerMask;
    [SerializeField] private float m_fRaius = 0.3f;
    [SerializeField] private float m_fDamageCount = 10;

    private PlayerHealth m_playerHealth;
    private bool m_bCollided;

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, this.m_fRaius, this.m_playerMask);

        foreach(Collider c in hits)
        {
            this.m_playerHealth = c.gameObject.GetComponent<PlayerHealth>();
            this.m_bCollided = true;
        }
        if(m_bCollided)
        {
            this.m_playerHealth.TakeDmg(this.m_fDamageCount);
            this.enabled = false;
        }
    }
}
