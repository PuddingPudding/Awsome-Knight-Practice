using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float m_fFollowHeight = 8f; //跟蹤高度
    [SerializeField] private float m_fFollowDistance = 6f; //跟蹤距離
    [SerializeField] private Transform m_player;

    private float m_fTargetHeight;
    private float m_fCurrentHeight;
    private float m_fCurrentRotation;

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.m_fTargetHeight = this.m_player.position.y + this.m_fFollowHeight; //最終要達到的高度
        this.m_fCurrentRotation = this.transform.eulerAngles.y;
        this.m_fCurrentHeight = Mathf.Lerp(this.transform.position.y, this.m_fTargetHeight, 0.9f * Time.deltaTime);
        //Lerp會去計算出參數1向參數2邁進多少%的值，例如今天後面填1的話就會直接回傳參數2，填0.5f的話就會回傳兩者相加的平均
        //this.m_fCurrentHeight = this.m_fTargetHeight; //上面版本能讓畫面上下浮動時變得平順
        Quaternion euler = Quaternion.Euler(0, this.m_fCurrentRotation, 0);
        Vector3 targetPos = this.m_player.position - (euler * Vector3.forward) * this.m_fFollowDistance;
        targetPos.y = this.m_fCurrentHeight;

        this.transform.position = targetPos;
        this.transform.LookAt(this.m_player);
    }
}
