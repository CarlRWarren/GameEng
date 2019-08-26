using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_highScoreText = null;
    [SerializeField] TextMeshProUGUI m_scoreText = null;
    [SerializeField] TylerTestScript m_tylerTestScript = null;
    [SerializeField] BottleLauncher m_bottleLauncher = null;
    int m_score = 0;
    int m_highScore = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            m_highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", m_highScore);
        }

        m_highScoreText.text = m_highScore.ToString();
    }

    void Update()
    {
        m_scoreText.text = m_score.ToString();
        if(m_score > m_highScore)
        {
            m_highScore = m_score;
            PlayerPrefs.SetInt("HighScore", m_highScore);
            m_highScoreText.text = m_highScore.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Test"))
        {
            m_score++;
            m_bottleLauncher.Reset();
            m_tylerTestScript.scored = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Test"))
        {
            m_tylerTestScript.scored = false;
        }
    }
}
