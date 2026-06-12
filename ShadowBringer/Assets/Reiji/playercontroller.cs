using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private const int kMaxHp= 3;

    //  プレイヤーのHP
    private int _playerHp = kMaxHp;

    // 攻撃力
    public int _attack = 3;

    // 移動速度
    public float moveSpeed = 7f;

    // アクセサリーによる追加移動速度
    private float _accessoriesMoveSpeed;

    // ジャンプ力
    Rigidbody2D rigid2D;
    float jumpForce = 370f;

    // 入力値
    private float moveInput;
    public bool isFacingRight = true;

    // 地面にいるか
    private bool isGrounded;

    // リスト要素
    private int _arrayElement;

    // リスト1番目の情報取得用変数
    private string _firstItem;

    // タグの取得変数
    private string[] _accessories = new string[3];

    // tagを取得したか(ItemPickupにてtrueとfalseをいじる)
    public bool _Gettag = false;

    [Header("Jump Settings")]
    // --- 追加: コヨーテタイムの設定 (秒) ---
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteCounter;

    // --- 追加: クールタイムの設定 (秒) ---
    [SerializeField] private float jumpCooldown = 0.5f;
    private float jumpCooldownCounter;

    [Header("Dash Settings")]
    [SerializeField] private float dashForce = 25f;
    private bool isDashing = false;

    // ダッシュのクールタイム用の箱(関数)
    [SerializeField] private float dashCooldown = 1.0f;  // ダッシュの待ち時間(１秒)
    private float dashCooldownCounter;                    // 残り時間を数えるタイマー

    // -----------------------------------------------------------------------
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
       Debug.Log(_arrayElement);
        if (_arrayElement > 0)
        {
            _accessories[_arrayElement - 1] = accessories._item;   // タグの取得をする
        }


        if (_Gettag == true)
        {

            Status(_accessories[_arrayElement - 1]);
            if (_arrayElement >= 3)
            {
                List(_accessories[0]);
            }
            _Gettag = false;

        }

        // A,Dキー / ←→キー
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f-_accessoriesMoveSpeed;
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

        // 2. ジャンプのクールタイムの計算
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
        if (Mouse.current.rightButton.wasPressedThisFrame&&dashCooldownCounter<=0f)
        {
            dashCooldownCounter = dashCooldown;
            StartCoroutine(DashRoutine());
        }

        // ダッシュのクールタイムの計算
        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime; // タイマーが0になるまで毎フレーム減速する
        }

        if (isDashing) return;

        if(isDead())
        {
            _playerHp = kMaxHp;
            
        }
        // Debug.Log(isDead());
      //  Debug.Log(_playerHp);

    }

    // ダッシュ
    /*
      public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // ↓ダッシュのアニメーションを追加するためのコード
       // Animator.SetTrigger(dashParamHash);
        StartCoroutine(DashRoutine());
    }
    */

    // ダッシュ用コルーチン
    private IEnumerator DashRoutine()
    {
        isDashing = true;

        rigid2D.linearVelocity = new Vector2(0, rigid2D.linearVelocity.y);

        float i = 0;
        float deltaTime = 0;
        while (deltaTime < 0.2f)
        {
            if (isFacingRight) rigid2D.MovePosition(rigid2D.position += new Vector2((dashForce - i) * Time.deltaTime, 0));  // 右を向いてるとき
            else rigid2D.MovePosition(rigid2D.position -= new Vector2((dashForce - i) * Time.deltaTime, 0));             // 左を向いてるとき
            i += Time.deltaTime * 20f;
            deltaTime += Time.deltaTime;
            yield return null;
        }
        isDashing = false;
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        // 左右移動
        rigid2D.linearVelocity = new Vector2(moveInput * moveSpeed, rigid2D.linearVelocity.y);

        if (Mouse.current.rightButton.wasPressedThisFrame && dashCooldownCounter <= 0f)
        {
            dashCooldownCounter = dashCooldown; // タイマーを1秒満タンにセットする
            StartCoroutine(DashRoutine());
        }
    }

    // 地面に触れた
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if(collision.gameObject.CompareTag("hari"))
        {
            isGrounded = true;
            _playerHp -= 1;
        }
        if(collision.gameObject.CompareTag("enemy"))
        {
            _playerHp -= 1;
            Debug.Log("敵にぶつかった！");
            Debug.Log(_playerHp);
        }
        if(collision.gameObject.CompareTag("enemyAttack"))
        {  
            _playerHp -= 1;
        }
        
    }

    void OnTriggerEnter2D(Collision collision)
    {

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
    private void Status(string accessories)// アイテムをゲットした
    {

        if (accessories == "Up")
        {

            _attack += 2;
        }
        else if (accessories == "Speed")
        {
            
            _accessoriesMoveSpeed += 1.2f;
        }
        else
        {
            return;
        }



    }
    private void List(string accessories)　// アイテムを失い効果が消える
    {


        if (accessories == "Up")
        {

            _attack -= 2;
        }
        else if (accessories == "Speed")
        {
  
            _accessoriesMoveSpeed -= 1.2f;
        }
        else
        {
            return;
        }
  
        _accessories[0] = _accessories[1];
        _accessories[1] = _accessories[2];
      


    }

    public int GetHP()
    {
        return _playerHp;
    }
    public bool isDead()
    {
        return _playerHp <= 0;
    }
}