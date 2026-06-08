using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerOtamesi : MonoBehaviour
{
    // 攻撃力
    int _attack = 0;

    // 移動速度
    public float moveSpeed = 7f;

    // アクセサリーによる追加移動速度
    private float _accessoriesMoveSpeed;


    // ジャンプ力
    Rigidbody2D rigid2D;
    float jumpForce = 300f;

    // リスト要素
    private int _arrayElement;

    // リスト1番目の情報取得用変数
    private string _firstItem;

    // タグの取得変数
    private string[] _accessories = new string[3];

    // 入力値
    private float moveInput;
    public bool isFacingRight = true;

    // 地面にいるか
    private bool isGrounded;

    // tagを取得したか(ItemPickupにてtrueとfalseをいじる)
    public bool _Gettag = false;



    

    [Header("Jump Settings")]
    // --- 追加: コヨーテタイムの設定 (秒) ---
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteCounter;

    // --- 追加: クールタイムの設定 (秒) ---
    [SerializeField] private float jumpCooldown = 0.5f;
    private float jumpCooldownCounter;

    void Start()
    {
        _accessoriesMoveSpeed = 0f;
        Application.targetFrameRate = 60;
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // アクセサリーの情報
        GameObject obj = GameObject.Find("Item");    //　↓スクリプトがついてあるゲームオブジェクトを取得する
        ItemPickup accessories = obj.GetComponent<ItemPickup>();  // タグ取得しているスクリプトを取得する
        _arrayElement = accessories._getAccessoriesCount;
        _accessories[_arrayElement] = accessories._item;   // タグの取得をする
        Debug.Log(_accessories[_arrayElement]);
        if(_Gettag == true) 
        {
            Status(_accessories[_arrayElement]);
        }
        
        


        // A,Dキー / ←→キー
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f- _accessoriesMoveSpeed;
            isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1f+ _accessoriesMoveSpeed;
            isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            moveInput = 0f;
        }

        // --- タイマーの更新処理 ---
        // 1. コヨーテタイムの計算
        if (isGrounded)
        {
            coyoteCounter = coyoteTime; // 地面にいる間は常に満タン(0.2秒)
        }
        else
        {
            coyoteCounter -= Time.deltaTime; // 地面から離れたらカウントダウン
        }

        // 2. クールタイムの計算
        if (jumpCooldownCounter > 0)
        {
            jumpCooldownCounter -= Time.deltaTime; // クールタイムを減らす
        }

        // --- ジャンプ処理 ---
        // 条件：スペースが押された ＆ コヨーテタイム内 ＆ クールタイムが終わっている
        if (Keyboard.current.spaceKey.wasPressedThisFrame && coyoteCounter > 0f && jumpCooldownCounter <= 0f)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);

            // ジャンプ成功時の処理
            coyoteCounter = 0f; // 空中での連続ジャンプを防ぐため、猶予をゼロにする
            jumpCooldownCounter = jumpCooldown; // クールタイム(0.5秒)をセット
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

    // アクセサリーによるステータス処理
    private void Status(string accessories) 
    {
        
        if(accessories == "Up") 
        {
            
            _attack += 2;
        }
        else if (accessories == "Speed")
        {
            Debug.Log("hai");
            _accessoriesMoveSpeed += 0.2f;
        }
        else 
        {
            return;
        }
        _Gettag = false;
        if (_arrayElement >= 3)
        {
            _firstItem = _accessories[0];
            if (_firstItem == "Up")
            {

                _attack -= 2;
            }
            else if (_firstItem == "Speed")
            {
                Debug.Log("hai");
                _accessoriesMoveSpeed -= 0.2f;
            }
            else
            {
                return;
            }
        }

    }
}