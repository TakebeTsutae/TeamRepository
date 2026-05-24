using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb2d;
    private Vector2 movement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Rigidbody2Dのコンポーネントを取得
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // キーボードの入力を取得 (水平: 左右, 垂直: 上下)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // 移動する方向をVector2に格納
        movement = new Vector2(moveX, moveY).normalized;
       }


        void FixedUpdate()
        {
            // 物理演算に基づいた移動処理
            rb2d.linearVelocity = movement * moveSpeed;


        }
}
    