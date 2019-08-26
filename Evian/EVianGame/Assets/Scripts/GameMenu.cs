using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject m_menuButtons = null;
    [SerializeField] GameObject m_highscores = null;
    [SerializeField] AudioSource m_music = null;

    //private bool beenPlayed = false;

    //void Start()
    //{     
    //    if (!beenPlayed)
    //    {
    //        m_music.Play();
    //        beenPlayed = true;
    //        DontDestroyOnLoad(m_music);
    //    }
    //}
    
    public void LoadGame()
    {
        SceneManager.LoadScene("GoalMovement");
    }

    public void quitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    public void loadHighscores()
    {
        m_menuButtons.SetActive(false);
        m_highscores.SetActive(true);
    }

    public void closeHighscore()
    {
        m_menuButtons.SetActive(true);
        m_highscores.SetActive(false);
    }
}
