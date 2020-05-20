using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    float m_horizontal;
    public float Horizontal { get { return m_horizontal; } }

    float m_vertical;
    public float Vertical { get { return m_vertical; } }

    bool m_inputEnable = false;
    public bool InputEnable { get { return m_inputEnable; } set { m_inputEnable = value; } }

    public void GetKeyInput()
    {
        if (m_inputEnable)
        {
            m_horizontal = Input.GetAxisRaw("Horizontal");
            m_vertical = Input.GetAxisRaw("Vertical");
        }
    }
}
