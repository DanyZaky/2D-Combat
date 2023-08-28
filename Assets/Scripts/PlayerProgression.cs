using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgression : MonoBehaviour
{
    public CameraFollow cf;
    public GameObject player;
    public GameObject panelWin;

    public GameObject[] waveCollider;
    public GameObject[] enemyWave1;
    public GameObject[] enemyWave2;
    public GameObject[] enemyWave3;
    public GameObject[] allEnemy;

    private bool isWave1Done, isWave2Done;

    private void Start()
    {
        for (int i = 0; i < enemyWave1.Length; i++)
        {
            enemyWave1[i].GetComponent<EnemyState>().isFollow = true;
        }

        isWave1Done = true;
        isWave2Done = true;
    }

    private void Update()
    {
        CheckWave(enemyWave1, 30, waveCollider[0]);
        CheckWave(enemyWave2, 50, waveCollider[1]);

        if(isWave1Done)
        {
            if (player.transform.position.x >= 12)
            {
                cf.minXLimit = 10;
                for (int i = 0; i < enemyWave2.Length; i++)
                {
                    enemyWave2[i].GetComponent<EnemyState>().isFollow = true;
                }
                isWave1Done = false;
            }
        }

        if(isWave2Done)
        {
            if (player.transform.position.x >= 32)
            {
                cf.minXLimit = 30;
                for (int i = 0; i < enemyWave3.Length; i++)
                {
                    enemyWave3[i].GetComponent<EnemyState>().isFollow = true;
                }
                isWave2Done = false;
            }
        }

        CheckWin();
    }

    private void CheckWave(GameObject[] ObjWave, float maxLimit, GameObject waveColl)
    {
        bool allDestroyed = true;

        foreach (GameObject obj in ObjWave)
        {
            if (obj != null)
            {
                allDestroyed = false;
                break;
            }
        }

        if (allDestroyed)
        {
            Debug.Log("All objects are destroyed.");
            cf.maxXLimit = maxLimit;
            waveColl.SetActive(false);
        }
    }

    private void CheckWin()
    {
        bool allDestroyed = true;

        foreach (GameObject obj in allEnemy)
        {
            if (obj != null)
            {
                allDestroyed = false;
                break;
            }
        }

        if (allDestroyed)
        {
            Debug.Log("All objects are destroyed.");
            panelWin.SetActive(true);
        }
    }
}
