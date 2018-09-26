using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    [SerializeField] private Texture2D m_cursorTexture;
    [SerializeField] private GameObject m_mousePoint;
    [SerializeField] private Transform m_player;
    private GameObject m_currentMousePoint = null;
    private CursorMode m_mode = CursorMode.ForceSoftware;
    //要求游標按照我們所寫的去變話，不要跟著當下所運行之平台
    private Vector2 m_v2HotSpot = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        Cursor.SetCursor(this.m_cursorTexture, this.m_v2HotSpot, this.m_mode);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray , out hit))
            {
                if(hit.collider is TerrainCollider)
                {
                    Vector3 v3Temp = hit.point;
                    v3Temp.y += 0.35f;
                    if(this.m_currentMousePoint == null) //初次點擊
                    {
                        this.m_currentMousePoint = 
                            Instantiate(this.m_mousePoint, v3Temp, Quaternion.identity) 
                            as GameObject;
                    }
                    else
                    {
                        this.m_currentMousePoint.SetActive(true);
                        this.m_currentMousePoint.transform.position = v3Temp;
                    }
                }
            }
        }
        if(this.m_currentMousePoint != null)
        {
            if (Vector3.Distance(this.m_currentMousePoint.transform.position, this.m_player.position) <= 1.1f)
            {
                this.ClearCursor();
            }
        }        
    }

    public void ClearCursor()
    {
        this.m_currentMousePoint.SetActive(false);
    }
}
