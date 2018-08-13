using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    [SerializeField] Texture2D m_cursorTexture;
    //[SerializeField] GameObject m_mousePoint;
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

    }
}
