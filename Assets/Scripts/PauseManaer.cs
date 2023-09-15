using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManaer : MonoBehaviour
{
    public GameObject pausePanel, ButtonPause;
    void Start()
    {
        OnClickResume();
    }

    public void OnClickPause()
    {
        pausePanel.SetActive(true);
        ButtonPause.SetActive(false);
        Time.timeScale = 0;
    }

    public void OnClickResume()
    {
        pausePanel.SetActive(false);
        ButtonPause.SetActive(true);
        Time.timeScale = 1;
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SoundManager.Instance.StopBGM("BGM - Regular");
        SoundManager.Instance.StopBGM("BGM - Boss");
        Time.timeScale = 1;
    }

    public void OnClickHome()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
