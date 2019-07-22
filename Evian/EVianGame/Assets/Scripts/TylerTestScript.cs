using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TylerTestScript : MonoBehaviour
{
    [SerializeField] List<GameObject> wakeUpObjects = new List<GameObject>();
    [SerializeField] AudioSource m_sfx = null;

    float m_timer = 2.0f;
    const float m_timerReset = 2.0f;
    bool startTimer = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach(GameObject obj in wakeUpObjects)
            {
                obj.SetActive(true);
            }           
            startTimer = true;
        }
        if (startTimer)
        {
            m_timer -= Time.deltaTime;
            if (m_timer <= 1.1f && m_timer >= 1.0f)
            {
                m_sfx.Play();
            }
            if (m_timer <= 0.0f)
            {
                startTimer = false;
                m_timer = m_timerReset;                
                foreach (GameObject obj in wakeUpObjects)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
