
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class enemyotamesi : MonoBehaviour
{
    public GameObject _attackLeft;
    public GameObject _attackRight;
    public GameObject _footRight;
    public GameObject _footLeft;

    public float moveSpeed = -1f;
    public float _moveSpeed = 1f;
    public float jumpForceX = 2f;
    public float jumpForceY = 5f;

    private int kabe = 0;
    private int _playerAttack;
    private int _countFoot = 0;

    public int _enemyHp = 8;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isDame = false;       // ダメージ用のフラグ
    private bool isTurning = false;    // ★方向転換の連続判定を防ぐ専用フラグ

    Vector2 velocity;

    void Start()
    {
        this._enemyHp = 8;
        kabe = 0;
        _attackLeft.SetActive(false);
        _attackRight.SetActive(false);
        _footLeft.SetActive(true);
        _footRight.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        this._countFoot = 0;

        StartCoroutine(Action());
    }

    void Update()
    {
        // プレイヤーの攻撃力取得など（必要に応じて）
    }

    void FixedUpdate()
    {
        if (!isJumping)
        {
            velocity = rb.linearVelocity;
            velocity.x = moveSpeed;
            rb.linearVelocity = velocity;
        }
    }

    private IEnumerator Action()
    {
        while (true)
        {
            // コルーチン側で毎回 _moveSpeed をリセットするのをやめ、
            // 現在の向き（_moveSpeedの符号）を維持するように修正
            if (moveSpeed != 0f)
            {
                moveSpeed = Mathf.Sign(_moveSpeed) * 1f; // 現在の向きを維持
            }

            yield return new WaitForSeconds(3f);

            moveSpeed = 0f;
            if (kabe % 2 == 0) _attackLeft.SetActive(true);
            else _attackRight.SetActive(true);

            yield return new WaitForSeconds(1f);

            _attackLeft.SetActive(false);
            _attackRight.SetActive(false);

            moveSpeed = _moveSpeed;
            yield return new WaitForSeconds(3f);

            isJumping = true;
            if (kabe % 2 == 0)
            {
                rb.AddForce(new Vector2(-jumpForceX, jumpForceY), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(jumpForceX, jumpForceY), ForceMode2D.Impulse);
            }

            // ジャンプ中も向きを維持
            moveSpeed = _moveSpeed;

            yield return new WaitForSeconds(1f);
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーとの衝突は無視、かつ方向転換中でない場合のみ処理
        if (collision.gameObject.CompareTag("Player") || isTurning) return;

        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        collision.GetContacts(contacts);

        foreach (ContactPoint2D contact in contacts)
        {
            // 横方向の衝突判定
            if (Mathf.Abs(contact.normal.x) > 0.7f)
            {
                StartCoroutine(TurnRoutine());
                break;
            }
        }
    }

    // ★方向転換を安全に行うためのコルーチン
    private IEnumerator TurnRoutine()
    {
        isTurning = true;
        kabe++;
        _moveSpeed *= -1f;
        if (moveSpeed != 0f)
        {
            moveSpeed = _moveSpeed;
        }

        // 0.1秒間は連続で反転しないようにする（めり込み対策）
        yield return new WaitForSeconds(0.1f);
        isTurning = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("weponAttack"))
        {
            if (isDame) return;
            isDame = true;

            _enemyHp = _enemyHp - _playerAttack;

            if (_enemyHp <= 0)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(ResetDamageFlag(0.2f));
            }
        }

        if (other.CompareTag("Ground"))
        {
            this._countFoot--;
            if (this._countFoot <= 0)
            {
                this._countFoot = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        this._countFoot++;

        // ★ここでもkabe++や速度変更を行っていますが、OnCollisionEnterと重複して
        // 挙動がおかしくなる原因になるため、必要に応じて整理することをおすすめします。
        kabe++;
        _moveSpeed = (kabe % 2 == 0) ? -1f : 1f;
        if (moveSpeed != 0f)
        {
            moveSpeed = _moveSpeed;
        }
    }

    private IEnumerator ResetDamageFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDame = false;
    }
}