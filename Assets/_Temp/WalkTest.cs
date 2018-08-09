using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTest : MonoBehaviour
{

    private Animator m_anim;

    // Use this for initialization
    void Start()
    {
        this.m_anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.m_anim.SetBool("Walking", true);
            this.m_anim.SetFloat("TiredValue", 1);
        }
        else
        {
            this.m_anim.SetBool("Walking", false);
            this.m_anim.SetFloat("TiredValue", 0);
        }
    }
}
