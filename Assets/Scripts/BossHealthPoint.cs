using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthPoint : MonoBehaviour
{
    public GameObject bossHPObject;
    public PlayerProgression pp;
    public Image hpBarFilled;
    public EnemyState es;

    private float maxHP;

    private void Start()
    {
        bossHPObject.SetActive(false);
        maxHP = es.maxEnemyHealth + 20;
    }

    private void Update()
    {
        if(pp.waveIndex == (pp.allWave.Length - 1))
        {
            bossHPObject.SetActive(true);
        }
        else
        {
            bossHPObject.SetActive(false);
        }

        hpBarFilled.fillAmount = es.enemyHealth / maxHP;
    }
}
