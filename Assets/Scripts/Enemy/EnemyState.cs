using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyState : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int damageAmount = 10;
    public float enemyHealth;
    public float maxEnemyHealth;
    public Animator anim;
    public Image enemyHealthBar;

    private Transform player;
    private GameObject playerObj;
    private GameObject cameraObj;
    public bool isMoving;
    private bool isAttack;
    private int direction = 1;

    public float attackDelay = 2.0f;
    private float startDelay;
    private int indexAnimAttack;
    private bool isAttacking;
    private bool isDie;
    public bool isFollow;

    public GameObject textDamage;
    public float offsetTextDamage;

    public GameObject hpBar;

    [Header("Animation Name")]
    public string AnimationRun;
    public string AnimationIdle;
    public string AnimationDeath;
    public string AnimationAttack1;
    public string AnimationAttack2;
    public string AnimationAttack3;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        cameraObj = GameObject.FindGameObjectWithTag("Camera");
        startDelay = attackDelay;

        isAttacking = false;
        isDie = false;

        maxEnemyHealth = enemyHealth;
        hpBar.SetActive(false);
        playerObj.GetComponent<PlayerMovement>().hpBar.SetActive(false);
    }

    private void Update()
    {
        if(!isDie && isFollow)
        {
            if (isMoving)
            {
                EnemyFollow();
            }
            else
            {
                EnemyAttack();
            }
        }

        if(!isFollow)
        {
            anim.Play(AnimationIdle);
        }

        if(isDie)
        {
            StartCoroutine(DieCourotine());
        }

        if (enemyHealth <= 0)
        {
            isDie = true;
        }

        enemyHealthBar.fillAmount = enemyHealth / maxEnemyHealth;
    }

    private void EnemyFollow()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0;
        if (directionToPlayer.x > 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        Vector3 newScale = transform.localScale;
        newScale.x = direction;
        transform.localScale = newScale;
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        anim.Play(AnimationRun);
    }

    private void EnemyAttack()
    {
        if(isAttack)
        {
            if (indexAnimAttack == 1) { anim.Play(AnimationAttack1); }
            else if(indexAnimAttack == 2) { anim.Play(AnimationAttack2); }
            else if(indexAnimAttack == 3) { anim.Play(AnimationAttack3); }

            if(isAttacking)
            {
                playerObj.GetComponent<PlayerMovement>().playerHealth -= damageAmount;
                
                GameObject spawnedPrefab = Instantiate(textDamage, new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + offsetTextDamage, playerObj.transform.position.z), Quaternion.identity);
                spawnedPrefab.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = damageAmount.ToString("0");
                spawnedPrefab.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
                Destroy(spawnedPrefab, 3f);

                StartCoroutine(VisiblePlayerHPBar());

                isAttacking = false;
            }

            startDelay -= Time.deltaTime;
        }
        else
        {
            anim.Play(AnimationIdle);
            isAttacking = true;
            startDelay -= Time.deltaTime;
        }

        if(startDelay <= 0)
        {
            if(isAttack)
            {
                isAttack = false;
                startDelay = attackDelay;
            }
            else
            {
                isAttack = true;
                startDelay = 0.45f;
                indexAnimAttack = Random.Range(1, 3);
            }
        }
    }

    private IEnumerator DieCourotine()
    {
        anim.Play(AnimationDeath);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private IEnumerator VisibleHPBar()
    {
        hpBar.SetActive(true);
        yield return new WaitForSeconds(1f);
        hpBar.SetActive(false);
    }

    private IEnumerator VisiblePlayerHPBar()
    {
        playerObj.GetComponent<PlayerMovement>().hpBar.SetActive(true);
        yield return new WaitForSeconds(1f);
        playerObj.GetComponent<PlayerMovement>().hpBar.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isMoving = false;
        }

        if (other.gameObject.tag == "AttackArea")
        {
            enemyHealth -= 1;

            GameObject spawnedPrefab = Instantiate(textDamage, new Vector3(transform.position.x, transform.position.y + offsetTextDamage, transform.position.z), Quaternion.identity);
            spawnedPrefab.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "1";
            Destroy(spawnedPrefab, 3f);

            StartCoroutine(VisibleHPBar());
        }

        if(other.gameObject.tag == "SpecialAttackArea")
        {
            int damage = Random.Range(2, 4);
            enemyHealth -= damage;

            GameObject spawnedPrefab = Instantiate(textDamage, new Vector3(transform.position.x, transform.position.y + offsetTextDamage, transform.position.z), Quaternion.identity);
            spawnedPrefab.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = damage.ToString();
            Destroy(spawnedPrefab, 3f);

            StartCoroutine(VisibleHPBar());
            StartCoroutine(CameraShake());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isMoving = true;
        }
    }

    private IEnumerator CameraShake()
    {
        cameraObj.GetComponent<Animator>().Play("CameraShake");
        yield return new WaitForSeconds(0.07f);
        cameraObj.GetComponent<Animator>().Play("Idle");
    }
}
