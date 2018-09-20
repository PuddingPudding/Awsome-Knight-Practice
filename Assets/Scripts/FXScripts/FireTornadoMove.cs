using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornadoMove : MonoBehaviour
{
    [SerializeField] private LayerMask m_enemyLayer;
    [SerializeField] private float m_fRadius = 0.5f;
    [SerializeField] private float m_fDmgCount = 10;
    [SerializeField] private GameObject m_fireExplosion;

    private EnemyHealth m_enemyHealth;
    private bool m_bCollided;

    private float m_fSpeed = 3;

    // Use this for initialization
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
        this.CheckForDamage();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * (m_fSpeed * Time.deltaTime));
    }

    void CheckForDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, m_fRadius, m_enemyLayer);

        foreach(Collider c in hits)
        {
            m_enemyHealth = c.gameObject.GetComponent<EnemyHealth>();
            this.m_bCollided = true;
        }
        if(this.m_bCollided)
        {
            m_enemyHealth.TakeDmg(m_fDmgCount);
            Vector3 temp = transform.position;
            temp.y += 2;
            Instantiate(m_fireExplosion, temp, Quaternion.identity);
            //this.enabled = false;
            Destroy(this.gameObject);
        }
    }
}
