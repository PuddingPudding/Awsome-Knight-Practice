using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEffects : MonoBehaviour
{
    [SerializeField] private GameObject m_groundImpactSpawn;
    [SerializeField] private GameObject m_kickFXSpawn;
    [SerializeField] private GameObject m_fireTornadoSpawn;
    [SerializeField] private GameObject m_fireShieldSpawn;
    [SerializeField]
    private GameObject m_groundImpactPrefab, m_kickFXPrefab, m_fireTornadoPrefab,
        m_fireShieldPrefab, m_healFXPrefab, m_thunderFXPrefab;

    void GroundImpact()
    {
        Instantiate(m_groundImpactPrefab, m_groundImpactSpawn.transform.position, Quaternion.identity);
    }

    void Kick()
    {
        Instantiate(m_kickFXPrefab, m_kickFXSpawn.transform.position, Quaternion.identity);
    }

    void FireTornado()
    {
        Instantiate(m_fireTornadoPrefab, m_fireTornadoSpawn.transform.position, Quaternion.identity);
    }

    void FireShield()
    {
        GameObject fireObj = Instantiate(m_fireShieldPrefab, m_fireShieldSpawn.transform.position, Quaternion.identity) as GameObject;
        fireObj.transform.SetParent(this.transform);
    }

    void Heal()
    {
        Vector3 temp = this.transform.position;
        temp.y += 2;
        GameObject healObj = Instantiate(m_healFXPrefab, temp, Quaternion.identity) as GameObject;
        healObj.transform.SetParent(this.transform);
    }

    void ThunderAttack()
    {
        for (int i = 0; i < 8; i++)
        {
            Vector3 pos = Vector3.zero;
            if (i == 0)
            {
                pos = new Vector3(this.transform.position.x - 4, this.transform.position.y + 2, this.transform.position.z);
            }
            else if (i == 1)
            {
                pos = new Vector3(this.transform.position.x + 4, this.transform.position.y + 2, this.transform.position.z);
            }
            else if (i == 2)
            {
                pos = new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z + 4);
            }
            else if (i == 3)
            {
                pos = new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z - 4);
            }
            else if (i == 4)
            {
                pos = new Vector3(this.transform.position.x + 2.5f, this.transform.position.y + 2, this.transform.position.z + 2.5f);
            }
            else if (i == 5)
            {
                pos = new Vector3(this.transform.position.x - 2.5f, this.transform.position.y + 2, this.transform.position.z + 2.5f);
            }
            else if (i == 6)
            {
                pos = new Vector3(this.transform.position.x + 2.5f, this.transform.position.y + 2, this.transform.position.z - 2.5f);
            }
            else if (i == 7)
            {
                pos = new Vector3(this.transform.position.x - 2.5f, this.transform.position.y + 2, this.transform.position.z - 2.5f);
            }
            Instantiate(m_thunderFXPrefab, pos, Quaternion.identity);
        }
    }
}
