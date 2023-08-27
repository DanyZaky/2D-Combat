using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyState : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int damageAmount = 10;
    public float enemyHealth = 15;
    public Animator anim;
    public Image enemyHealthBar;

    private Transform player;
    private GameObject playerObj;
    public bool isMoving;
    private bool isAttack;
    private int direction = 1;

    public float attackDelay = 2.0f;
    private float startDelay;
    private int indexAnimAttack;
    private bool isAttacking;
    private bool isDie;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        startDelay = attackDelay;
        isAttacking = false;
        isDie = false;
    }

    private void Update()
    {
        if(!isDie)
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
        else
        {
            StartCoroutine(DieCourotine());
        }

        if (enemyHealth <= 0)
        {
            isDie = true;
        }

        enemyHealthBar.fillAmount = enemyHealth / 15f;
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

        anim.Play("Enemy Run");
    }

    private void EnemyAttack()
    {
        if(isAttack)
        {
            if (indexAnimAttack == 1) { anim.Play("Enemy Attack 1"); }
            else if(indexAnimAttack == 2) { anim.Play("Enemy Attack 2"); }
            else if(indexAnimAttack == 3) { anim.Play("Enemy Attack 3"); }

            if(isAttacking)
            {
                playerObj.GetComponent<PlayerMovement>().playerHealth -= 1;
                isAttacking = false;
            }

            startDelay -= Time.deltaTime;
        }
        else
        {
            anim.Play("Enemy Idle");
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
        anim.Play("Enemy Death");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isMoving = true;
        }
    }
}
