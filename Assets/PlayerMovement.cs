using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float jumpDelay = 3f;
    private Rigidbody rb;
    private bool canJump = true;
    public Animator anim;
    private bool isFacingRight = true; // Menyimpan arah hadap karakter

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if ((horizontalInput < 0 && isFacingRight) || (horizontalInput > 0 && !isFacingRight))
        {
            FlipCharacter();
        }

        if (canJump)
        {
            if (horizontalInput != 0f || verticalInput != 0f)
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
}
