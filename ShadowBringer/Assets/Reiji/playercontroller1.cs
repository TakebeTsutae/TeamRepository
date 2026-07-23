using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;


public class PlayerController1 : MonoBehaviour
{
    // アイテム所持判定のための変数

    // PlayerController.cs の中にこれを追加
    public string currentItem;

    private GameObject _itemManager;

    private const int kMaxHp= 6;

    public static PlayerController1 instance;
    //  プレイヤーのHP
    public int _playerHp = kMaxHp;

    public int _attackTotal { get; private set;  } = 0;

    // 攻撃力
    public int _attack { get; private set; } = 0;

    public int _attackWeapon = 3;

    // 移動速度
    public float moveSpeed = 7f;

    // アクセサリーによる追加移動速度
    private float _accessoriesMoveSpeed;

    public int currentHP = 6;
    public int maxHP = 6;

    



    // ジャンプ力
    Rigidbody2D rigid2D;
    float jumpForce = 370f;

    // 入力値
    private float moveInput;
    public bool isFacingRight = true;

    // 地面にいるか
    private bool isGrounded;

    // 地面についてるうえで移動しているか
    private bool isRunning;

    // リスト要素
    private int _arrayElement;

    // リスト1番目の情報取得用変数
    private string _firstItem;

    // タグの取得変数
    public string[] _accessories = new string[3];

    // 武器の所持変数
    public string weapon;

    // tagを取得したか(ItemPickupにてtrueとfalseをいじる)
    public bool _Gettag = false;

    private bool isGameOverTriggered = false;

    private bool _item = false;

    private bool _GetKey=false;

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

    [Header("Damage Settings")]
    [SerializeField] private float invincibleTime = 1.0f;
    private float invincibleCounter;

    [Header("Audio")]
    private AudioSource audioSource;

    [SerializeField] private AudioSource _runAudioSource;
    [SerializeField] private AudioSource _seAudioSource;

    [SerializeField] private AudioClip _attackAudioClip;
    [SerializeField] private AudioClip _jumpAudioClip;


    [SerializeField] private string gameover;

    public Animator _anim;
    private string currentAnimation = "";

    private bool isAttacking = false;

    private SpriteRenderer spriteRenderer;
    public float flashDuration = 0.2f;  // 赤くなっている時間（秒）

    
    // -----------------------------------------------------------------------
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _accessoriesMoveSpeed = 0f;
        Application.targetFrameRate = 60;
        rigid2D = GetComponent<Rigidbody2D>();
        _itemManager = GameObject.Find("ItemManager");
        _arrayElement = 0;
        _Gettag = false;
        weapon = "Ken";

        // ゲームが始まった瞬間に自分についているAnimatorコンポーネントを自動で覚えさせます
        _anim = GetComponent<Animator>();
        _anim.Play("Idle", 0, 0f);
        transform.localScale = new Vector3(2, 2, 1);

        // 自分についているSpriteRendererを取得しておく
        spriteRenderer= GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();


    }

    void Update()
    {
       if(Ec._isTime == false)
        {
            _attackTotal = _attackWeapon + _attack;


            if (Keyboard.current.eKey.isPressed)
            {
                _GetKey = true;
            }
            else
            {
                _GetKey = false;
            }



            if (_Gettag == true)
            {
                // 配列の範囲内に収まるように安全弁をつける
                if (_arrayElement > 0 && _arrayElement <= _accessories.Length)
                {
                    _accessories[_arrayElement - 1] = currentItem;
                    Status(_accessories[_arrayElement - 1]);
                }

                if (_arrayElement >= 3)
                {
                    List(_accessories[0]);
                    _arrayElement = 2; // 古いものを捨てたので、現在の所持数を2に補正する
                }

                _Gettag = false;
            }

            if (isAttacking)
            {
                // A,Dキー / ←→キー
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                {
                    moveInput = -1f - _accessoriesMoveSpeed;
                    isFacingRight = false;
                    transform.localScale = new Vector3(-2, 2, 1);

                    Debug.Log("左キーが押されました。");
                }
                else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                {
                    moveInput = 1f + _accessoriesMoveSpeed;
                    isFacingRight = true;
                    transform.localScale = new Vector3(2, 2, 1);

                    Debug.Log("右キーが押されました。");
                }
                else
                {
                    moveInput = 0f;
                }
                // Attack実行中はほかのアニメーションが再生されないようにする
            }
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                moveInput = -1f - _accessoriesMoveSpeed;
                isFacingRight = false;
                transform.localScale = new Vector3(-2, 2, 1);

                Debug.Log("左キーが押されました。");
                // 【追加】左に動いているので「Walk」を再生
                ChangeAnimation("Walk");
            }
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                moveInput = 1f + _accessoriesMoveSpeed;
                isFacingRight = true;
                transform.localScale = new Vector3(2, 2, 1);

                Debug.Log("右キーが押されました。");
                // 【追加】右に動いているので「Walk」を再生
                ChangeAnimation("Walk");
            }
            else
            {
                moveInput = 0f;
                if (isAttacking) return;
                currentAnimation = "";
                //   Debug.Log("止まりました！Idleを再生します");
                ChangeAnimation("Idle");
            }

            // 効果音追加中
            if (Keyboard.current.dKey.isPressed && isGrounded || Keyboard.current.aKey.isPressed && isGrounded)
            {
                if (!isRunning)
                {
                    _runAudioSource.Play();
                    isRunning = true;
                }
            }
            else
            {
                if (isRunning)
                {
                    _runAudioSource.Stop();
                    isRunning = false;
                }
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

            if (Mouse.current.rightButton.wasPressedThisFrame && dashCooldownCounter <= 0f)
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

            if (invincibleCounter > 0)
            {
                invincibleCounter -= Time.deltaTime;
            }

            if (isDashing) return;

            if (isDead() && !isGameOverTriggered)
            {
                //_playerHp = kMaxHp;
                SceneManager.LoadScene("GameOver");

            }
        }
       

    }

    public void TakeDamage(int damageAmount)
    {
        if (invincibleCounter > 0f)
        {
            return;
        }

        // ダメージを適用
        _playerHp -= damageAmount;
        Debug.Log($"ダメージを{damageAmount}受けた！残りHP:{_playerHp}");

        // 無敵タイマーをセット(満タンにする)
        invincibleCounter = invincibleTime;
    }

    public void PlayerAttackAnimation()
    {
        Debug.Log("AttackAnimationが呼ばれた！");
        isAttacking = true;
        currentAnimation = "";
        ChangeAnimation("Attack");
        StartCoroutine(AttackAnimationEnd());

        audioSource.PlayOneShot(_attackAudioClip);
    }

    private IEnumerator AttackAnimationEnd()
    {
        // 攻撃アニメーションのクリップの長さだけ待つ
        yield return new WaitForSeconds(0.36f); // 攻撃アニメーションの長さに合わせて変更している
        isAttacking = false;
        ChangeAnimation("Idle");
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

        // ダッシュのカウントの設定
        if (Mouse.current.rightButton.wasPressedThisFrame && dashCooldownCounter <= 0f)
        {
            dashCooldownCounter = dashCooldown; // タイマーを1秒満タンにセットする
            StartCoroutine(DashRoutine());
        }

       
    }

    public void ChangeAnimation(string newAnimation)
    {
        //Debug.Log("ChangeAnimationが呼ばれた：" + newAnimation);
        //Debug.Log("現在のアニメーション：" + currentAnimation);

        //Debug.Log("Play直前:" + newAnimation);
        // もし「今再生中のアニメーション」と「次に再生したいアニメーショ」が同じなら、何もしない
     //   Debug.Log("現在：" + currentAnimation + "→次：" + newAnimation);
        if (currentAnimation == newAnimation) return;

        // 違うアニメーションのときだけ、新しく再生する
        _anim.Play(newAnimation);

       // Debug.Log("Playしました：" + newAnimation);
        // 今再生しているアニメーションの名前を上書きして記憶する
        currentAnimation = newAnimation;
    }

    // tagにぶつかったときの処理たち
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if(collision.gameObject.CompareTag("hari"))
        {
            isGrounded = true;
            TakeDamage(1);
        }
        
        
        
    }

    // 接触した瞬間の処理（敵の攻撃など、キー入力に関係ないもの）
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyAttack"))
        {
            TakeDamage(1);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Debug.Log("敵にぶつかった！");
            Debug.Log(_playerHp);

            StartCoroutine(FrashRed());
        }
    }

    IEnumerator FrashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = Color.white;
    }

    // 【追加】トリガーに触れている間、毎フレーム呼び出される処理
    // 【修正版】トリガーに触れている間、毎フレーム呼び出される処理
    void OnTriggerStay2D(Collider2D collision)
    {
        // すでにアイテム取得処理が始まっている（_GettagがTrue）なら、このフレームは何もせず返す
        if (_Gettag) return;

        // アイテムのタグを持っている場合のみ、Eキーの入力をチェックする
        if (collision.CompareTag("Up") || collision.CompareTag("Speed") || collision.CompareTag("Tue") || collision.CompareTag("Ken") || collision.CompareTag("HP"))
        {
            // 触れている間にEキーが押されたら取得
            if (Keyboard.current.eKey.isPressed)
            {
                

                if (collision.CompareTag("Up"))
                {
                    _arrayElement++;
                    currentItem = "Up";
                }
                else if (collision.CompareTag("Speed"))
                {
                    _arrayElement++;
                    currentItem = "Speed";
                }
                else if (collision.CompareTag("Ken"))
                {
                    weapon = "Ken";
                    _attackWeapon = 3;
                }
                else if (collision.CompareTag("Tue"))
                {

                    weapon = "Tue";
                    _attackWeapon = 2;
                }
                else if (collision.CompareTag("HP"))
                {
                    _arrayElement++;
                    weapon = "HP";
                    currentHP += 20;
                }

                //  Debug.Log($"currentItemを取得しました: {currentItem}");

                // 2重判定を防ぐために即座にコライダーを無効化する
                collision.enabled = false;


                _Gettag = true;
                Destroy(collision.gameObject);
            }
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
        else if (accessories == "HP")
        {
            maxHP += 1;     // 最大HPを増加
            currentHP += 1; // 今のHPも一緒に増やす（回復も兼ねる）

            Debug.Log("最大HP増加:" + currentHP + " / " + maxHP);
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
        else if (accessories == "HP")
        {
            maxHP -= 1;

            // HPがmaxを超えないように調整
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
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