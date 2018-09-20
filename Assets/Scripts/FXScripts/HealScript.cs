using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour
{
    [SerializeField] private float m_fHealAmount = 20;
    // Use this for initialization
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Heal(this.m_fHealAmount);
    }
}
