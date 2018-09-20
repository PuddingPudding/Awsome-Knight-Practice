using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    private PlayerHealth m_playerHealth;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        m_playerHealth.Shielded = true;
        print("玩家安全了!");
    }

    private void OnDisable()
    {
        m_playerHealth.Shielded = false;
        print("玩家危險了!");
    }
}
