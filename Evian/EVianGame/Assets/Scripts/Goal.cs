using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] Transform m_target = null;
    [SerializeField] float m_speed = 0.5f;

    void Update()
    {
        float time = m_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_target.position, time);
        if (transform.position == m_target.position)
        {
            m_target.transform.position = new Vector3(Random.Range(-5.0f, 5.0f),Random.Range(0.0f, 3.0f), m_target.position.z);
        }
    }
}
