using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject losePanel;
    
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float jumpDelay = 3f;
    public float playerHealth;
    public float maxPlayerHealth;
    private Rigidbody rb;
    private bool canJump = true;
    public Animator anim;
    private bool isFacingRight = true; // Menyimpan arah hadap karakter

    public VariableJoystick variableJoystick;

    public GameObject shadow, attackArea, specialAttackArea;
    public GameObject hpBar;
    public Image PlayerBar;
    private bool isAttack;
    private string attackType;

    public Button specialAttackButton;
    public Image specialAttackCooldownImage;
    public float cooldownSpecialAttack;
    public int specialAttackAmount;
    private bool isCooldownSpecial;
    private float currentCooldownTimeSpecial;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAttack = false;
        attackArea.SetActive(false);
        specialAttackArea.SetActive(false);
        maxPlayerHealth = playerHealth;

        specialAttackAmount = 0;
        isCooldownSpecial = false;
        currentCooldownTimeSpecial = cooldownSpecialAttack;

        losePanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(variableJoystick.Horizontal, 0f, variableJoystick.Vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        //PC
        Vector3 movementPC = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movementPC);

        if ((variableJoystick.Horizontal < 0 && isFacingRight) || (variableJoystick.Horizontal > 0 && !isFacingRight) || (horizontalInput < 0 && isFacingRight) || (horizontalInput > 0 && !isFacingRight))
        {
            FlipCharacter();
        }

        if (canJump && !isAttack)
        {
            if (variableJoystick.Horizontal != 0f || variableJoystick.Vertical != 0f || horizontalInput != 0f || verticalInput != 0f)
            {
                anim.Play("Run");
            }
            else
            {
                anim.Play("Idle");
            }

            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                StartCoroutine(EnableJump(1f));
            }
        }

        if(playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }

        if(playerHealth <= 0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }

        if(isAttack)
        {
            anim.Play(attackType);
        }

        shadow.transform.position = new Vector3(transform.position.x, shadow.transform.position.y, transform.position.z);

        PlayerBar.fillAmount = playerHealth / maxPlayerHealth;

        CooldownSpecialAttack();
    }

    private IEnumerator EnableJump(float time)
    {
        canJump = false;
        anim.Play("Jump");
        yield return new WaitForSeconds(time);
        canJump = true;
    }

    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Memutar skala horizontal
        transform.localScale = theScale;
    }

    public void BasicAttack()
    {
        StartCoroutine(AttacAnimkDelay("BasicAttack"));
        StartCoroutine(AttackDelay(attackArea));
    }

    public void SpecialAttack()
    {
        specialAttackAmount += 1;

        StartCoroutine(AttacAnimkDelay("SpecialAttack"));
        StartCoroutine(AttackDelay(specialAttackArea));
    }

    private void CooldownSpecialAttack()
    {
        if(specialAttackAmount >= 3)
        {
            specialAttackCooldownImage.gameObject.SetActive(true);
            isCooldownSpecial = true;
        }

        if(isCooldownSpecial)
        {
            specialAttackButton.interactable = false;
            currentCooldownTimeSpecial -= 1 * Time.deltaTime;
            specialAttackCooldownImage.fillAmount = currentCooldownTimeSpecial / cooldownSpecialAttack;

            if (currentCooldownTimeSpecial <= 0)
            {
                isCooldownSpecial = false;
                specialAttackAmount = 0;
            }
        }

        if (!isCooldownSpecial)
        {
            specialAttackButton.interactable = true;
            specialAttackCooldownImage.gameObject.SetActive(false);
            currentCooldownTimeSpecial = cooldownSpecialAttack;
        }
    }

    public void Jump()
    {
        if (canJump && !isAttack)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(EnableJump(1f));
        }
    }

    private IEnumerator AttacAnimkDelay(string typeAttack)
    {
        attackType = typeAttack;
        if(isAttack == false)
        {
            isAttack = true;
            yield return new WaitForSeconds(0.3f);
            isAttack = false;
        }
    }

    private IEnumerator AttackDelay(GameObject area)
    {
        area.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        area.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
