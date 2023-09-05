using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgression : MonoBehaviour
{
    public CameraFollow cf;
    public GameObject player;
    public GameObject panelWin;

    public EnemyWave[] allWave;
    public GameObject[] allEnemy;

    private int index;

    private void Start()
    {
        index = 0;

        for (int i = 0; i < allWave.Length; i++)
        {
            allWave[i].thisWaveCollider.SetActive(true);
        }

        for (int i = 0; i < allWave[index].enemyWave.Length; i++)
        {
            allWave[index].enemyWave[i].GetComponent<EnemyState>().isFollow = true;
        }
    }

    private void Update()
    {
        if (index != allWave.Length)
        {
            bool allDestroyed = true;
            cf.maxXLimit = allWave[index].maxCameraLimit;

            foreach (GameObject obj in allWave[index].enemyWave)
            {
                if (obj != null)
                {
                    allDestroyed = false;
                    Debug.Log(index);
                    break;
                }
            }

            if(allDestroyed)
            {
                allWave[index].thisWaveCollider.SetActive(false);
                index += 1;

                for (int i = 0; i < allWave[index].enemyWave.Length; i++)
                {
                    allWave[index].enemyWave[i].GetComponent<EnemyState>().isFollow = true;
                }
            }
        }
        else if(index >= allWave.Length)
        {
            Debug.Log("win");
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
