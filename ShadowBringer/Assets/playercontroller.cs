using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 移動速度
    public float moveSpeed = 5f;

    // ジャンプ力
    Rigidbody2D rigid2D;
    float jumpForce = 300f;

    // Rigidbody2D

    // 入力値
    private float moveInput;

    // 地面にいるか
    private bool isGrounded;

    void Start()
    {
        Application.targetFrameRate = 60;
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // A,Dキー / ←→キー
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1f;
        }
        else
        {
            moveInput = 0f;
        }
      // space
        if(Keyboard.current.spaceKey.wasPressedThisFrame &&
                this.rigid2D.linearVelocityY == 0)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }
    }

    void FixedUpdate()
    {
        // 左右移動
       rigid2D.linearVelocity = new Vector2(moveInput * moveSpeed, rigid2D.linearVelocity.y);
    }

    // 地面に触れた
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // 地面から離れた
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}