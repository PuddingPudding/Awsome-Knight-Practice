using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float m_fDelTime = 2;
    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, m_fDelTime);
    }
}
