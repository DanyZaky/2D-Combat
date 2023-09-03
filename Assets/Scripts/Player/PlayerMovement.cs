using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float jumpDelay = 3f;
    public float playerHealth = 10;
    private Rigidbody rb;
    private bool canJump = true;
    public Animator anim;
    private bool isFacingRight = true; // Menyimpan arah hadap karakter

    public VariableJoystick variableJoystick;

    public GameObject shadow, attackArea;
    public Image PlayerBar;
    private bool isAttack;
    private string attackType;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAttack = false;
        attackArea.SetActive(false);
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

        if(isAttack)
        {
            anim.Play(attackType);
        }

        shadow.transform.position = new Vector3(transform.position.x, shadow.transform.position.y, transform.position.z);

        PlayerBar.fillAmount = playerHealth / 10f;
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
        StartCoroutine(AttackDelay());
    }

    public void SpecialAttack()
    {
        StartCoroutine(AttacAnimkDelay("SpecialAttack"));
        StartCoroutine(AttackDelay());
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

    private IEnumerator AttackDelay()
    {
        attackArea.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackArea.SetActive(false);
    }
}
