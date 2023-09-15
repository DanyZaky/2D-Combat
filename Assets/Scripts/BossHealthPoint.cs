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
    private bool isActive;

    private void Start()
    {
        bossHPObject.SetActive(false);
        maxHP = es.enemyHealth;
        isActive = false;
    }

    private void Update()
    {
        if(pp.waveIndex == (pp.allWave.Length - 1))
        {
            if (!isActive)
            {
                SoundManager.Instance.PlayBGM("BGM - Boss");
                isActive = true;
                
            }
            SoundManager.Instance.StopBGM("BGM - Regular");
            bossHPObject.SetActive(true);
        }
        else
        {
            bossHPObject.SetActive(false);
        }

        hpBarFilled.fillAmount = es.enemyHealth / maxHP;
    }
}
