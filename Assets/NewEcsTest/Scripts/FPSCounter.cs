using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float m_Accum = 0; // FPS accumulated over the interval
    private int m_Frames = 0; // Frames drawn over the interval
    private float m_Timeleft; // Left time for current interval

    [SerializeField] private TextMeshProUGUI m_FpsCounterText = null;
    [SerializeField] private string m_TextFormat = "";
    [SerializeField] private float m_UpdateInterval = 0.5F;

    private void Start()
    {
        m_Timeleft = m_UpdateInterval;
    }

    private void Update()
    {
        m_Timeleft -= Time.deltaTime;
        m_Accum += Time.timeScale / Time.deltaTime;
        ++m_Frames;

        if (m_Timeleft <= 0.0)
        {
            float fps = m_Accum / m_Frames;
            m_FpsCounterText.text = string.Format(m_TextFormat, fps);
            if (fps < 30)
            {
                m_FpsCounterText.color = Color.yellow;
            }
            else if (fps < 10)
            {
                m_FpsCounterText.color = Color.red;
            }
            else
            {
                m_FpsCounterText.color = Color.green;
            }

            m_Timeleft = m_UpdateInterval;
            m_Accum = 0.0F;
            m_Frames = 0;
        }
    }
}
