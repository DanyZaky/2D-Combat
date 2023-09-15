using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProgression : MonoBehaviour
{
    public int IndexToScene;
    
    public CameraFollow cf;
    public GameObject player;

    public EnemyWave[] allWave;
    public GameObject[] allEnemy;

    public GameObject animFade;

    public int ProgressToLevel;

    public int waveIndex;
    private bool isWin;

    private void Start()
    {
        SoundManager.Instance.PlayBGM("BGM - Regular");
        waveIndex = 0;

        for (int i = 0; i < allWave.Length; i++)
        {
            allWave[i].thisWaveCollider.SetActive(true);
        }

        for (int i = 0; i < allWave[waveIndex].enemyWave.Length; i++)
        {
            allWave[waveIndex].enemyWave[i].GetComponent<EnemyState>().isFollow = true;
        }

        isWin = false;

        StartCoroutine(FadeBlack("fade out"));
    }

    private void Update()
    {
        if (waveIndex < allWave.Length)
        {
            bool allDestroyed = true;
            cf.maxXLimit = allWave[waveIndex].maxCameraLimit;

            foreach (GameObject obj in allWave[waveIndex].enemyWave)
            {
                if (obj != null)
                {
                    allDestroyed = false;
                    Debug.Log(waveIndex);
                    break;
                }
            }

            if(allDestroyed)
            {
                allWave[waveIndex].thisWaveCollider.SetActive(false);
                waveIndex += 1;

                for (int i = 0; i < allWave[waveIndex].enemyWave.Length; i++)
                {
                    allWave[waveIndex].enemyWave[i].GetComponent<EnemyState>().isFollow = true;
                }
            }
        }
        else if(waveIndex >= allWave.Length)
        {
            isWin = true;
            PlayerPrefs.SetInt("Progress Level", ProgressToLevel);
            SoundManager.Instance.StopBGM("BGM - Boss");
            StartCoroutine(FadeBlack("fade in"));
        } 
    }

    private IEnumerator FadeBlack(string name)
    {
        animFade.GetComponent<Animator>().Play(name);
        animFade.SetActive(true);
        yield return new WaitForSeconds(2f);
        if(isWin)
        {
            SceneManager.LoadScene(IndexToScene);
        }
        else
        {
            animFade.SetActive(false);
        }
    }
}

[System.Serializable]
public class EnemyWave
{
    public GameObject[] enemyWave;
    public GameObject thisWaveCollider;
    public float maxCameraLimit;
}
