using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI m_highScoreText = null;
    [SerializeField] TextMeshProUGUI m_scoreText = null;
    int m_score = 0;
    //int m_highScore = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Test"))
        {
            m_score++;
        }
    }

    void Update()
    {
        m_scoreText.text = m_score.ToString();
    }
}
