using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscores : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] m_highscores = new TextMeshProUGUI[5];
    [SerializeField] List<int> m_topScores = new List<int>();

    int m_score;
    public bool m_loadedHighscores = false;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            m_score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            PlayerPrefs.SetInt("Score", m_score);
        }
    }

    private void Update()
    {
        if(m_loadedHighscores == false)
        {
            m_loadedHighscores = true;
            foreach(int num in m_topScores)
            {
                if (num < m_score)
                {
                    int index = m_topScores.IndexOf(num);
                    m_topScores[index] = m_score;
                    break;
                }
            }
            setScores();
        }
    }

    private void setScores()
    {
        int i = 0;
        foreach(int num in m_topScores)
        {
            m_highscores[i].text = num.ToString("D5");
            i++;
        }
    }

    private void loadScores()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            m_topScores.Add(PlayerPrefs.GetInt("HighScore"));
        }
        if (PlayerPrefs.HasKey("HighScore_2"))
        {
            m_topScores.Add(PlayerPrefs.GetInt("HighScore_2"));
        }
        if (PlayerPrefs.HasKey("HighScore_3"))
        {
            m_topScores.Add(PlayerPrefs.GetInt("HighScore_3"));
        }
        if (PlayerPrefs.HasKey("HighScore_4"))
        {
            m_topScores.Add(PlayerPrefs.GetInt("HighScore_4"));
        }
        if (PlayerPrefs.HasKey("HighScore_5"))
        {
            m_topScores.Add(PlayerPrefs.GetInt("HighScore_5"));
        }
        setScores();
    }

    void Start()
    {
        loadScores();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("HighScore_1", int.Parse(m_highscores[0].text));
        PlayerPrefs.SetInt("HighScore_1", int.Parse(m_highscores[1].text));
        PlayerPrefs.SetInt("HighScore_1", int.Parse(m_highscores[2].text));
        PlayerPrefs.SetInt("HighScore_1", int.Parse(m_highscores[3].text));
        PlayerPrefs.SetInt("HighScore_1", int.Parse(m_highscores[4].text));
    }
}
