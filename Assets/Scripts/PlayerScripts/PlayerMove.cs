using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private Animator m_anim;
    private CharacterController m_charController;
    private CollisionFlags m_collisionFlags = CollisionFlags.None;
    private float m_fMoveSpeed = 5f;
    private bool m_bCanMove;
    private bool m_bFinishedMovement = true;
    private Vector3 m_v3TargetPos = Vector3.zero;
    private Vector3 m_v3PlayerMove = Vector3.zero;
    private float m_fToPointDistance; //到目標點的距離
    private float m_fGravity = 9.8f;
    private float m_fHeight;

    private void Awake()
    {
        this.m_anim = this.GetComponent<Animator>();
        this.m_charController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.MoveThePlayer();
        this.m_charController.Move(this.m_v3PlayerMove);
    }

    private void MoveThePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) //如果射線有碰到東西，hit代表它所碰到的東西
            {
                if (hit.collider is TerrainCollider)//如果射線觸及到的目標碰撞器為地形碰撞器
                {
                    this.m_fToPointDistance = Vector3.Distance(this.transform.position, hit.point);

                    if (this.m_fToPointDistance >= 1)
                    {
                        this.m_bCanMove = true;
                        this.m_v3TargetPos = hit.point;
                    }
                }
            }
        }

        if (this.m_bCanMove)
        {
            this.m_anim.SetFloat("Walk", 1f);

            Vector3 v3TargetTemp = new Vector3(this.m_v3TargetPos.x, this.transform.position.y, this.m_v3TargetPos.z);

            transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(v3TargetTemp - this.transform.position), 15 * Time.deltaTime);

            this.m_v3PlayerMove = this.transform.forward * this.m_fMoveSpeed * Time.deltaTime;

            if (Vector3.Distance(this.transform.position, this.m_v3TargetPos) <= 0.5f)
            {
                this.m_bCanMove = false;
            }
        }
        else
        {
            this.m_v3PlayerMove.Set(0, 0, 0);
            this.m_anim.SetFloat("Walk", 0);
        }
    }
}
