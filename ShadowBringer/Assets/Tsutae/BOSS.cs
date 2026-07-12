using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BOSS : MonoBehaviour
{ 
    [Header("ボスHP")]
    public int bossStartHp = 500;
    public int _bossHp; // 現在のbossHpを入れる変数

    // ダメージ処理のスクリプトを取得
    GameObject _bossDamage; // ボスがダメージを受ける当たり判定のオブジェクト
                            // 常に有効化

    GameObject _bossColl;  // ボスがダメージを与える当たり判定のオブジェクト
                         // 攻撃の度に無効化
    BossElementCollider _elementCollider;

    // 音を鳴らす変数
    AudioSource _audioSource;
    [SerializeField, Header("突進前後退の効果音")]
    AudioClip _retraetSound;    // 突進前の後退の効果音
    [SerializeField, Header("突進の効果音")]
    AudioClip _dashAttackSound; // 突進の効果音
    [SerializeField, Header("攻撃の効果音")]
    AudioClip _attackSound; // 攻撃の効果音
    [SerializeField, Header("ジャンプの効果音")]
    AudioClip _jumpSound;   // ジャンプするときの効果音
    [SerializeField, Header("着地した時の効果音")]
    AudioClip _jumpStampSound;  // 着地した時の効果音

    // ゲーム中のボスの大きさ
    [SerializeField,Header("大きさ")]
    float _bossScale = 2.0f; // ボスの大きさ

    // アニメーション
    Animator animator;
    private SpriteRenderer baseSr;
    private SpriteRenderer animSr;
    [SerializeField,Header("ダッシュ中の見た目")] private Sprite dashSprite;

    // 攻撃の管理
    [SerializeField,Header("プレイヤーの座標")]
    Transform player;
    [SerializeField,Header("攻撃を始めるプレイヤーとの距離")]
    float attackRenge = 10.0f;
    private bool isActing;
    // 一連の攻撃を何回終えたか
    int _attackCounter = 0;
    // 攻撃の状態
    //bool _isAttacking = false;


    // 後退の変数
    [SerializeField,Header("後退")]
    float dashRetreatDistance = 1.0f;   // 突進前の後退で進む距離
    float retreatDistance = 5.0f;   // 後退で進む距離
    [SerializeField]
    float retreatSpeed = 3.0f;  // 突進前後退のスピード
    // 突進前後退の攻撃判定
    GameObject _retreatDash;

    // 通常攻撃の変数
    GameObject attackSensorController;

    // 突進の変数
    [SerializeField,Header("突進")]
    float dashDistance = 5.0f;  // 突進で進む距離
    [SerializeField]
    float dashSpeed = 10.0f;    // 突進のスピード
    GameObject _bossDashColl;    // 当たり判定

    // スポナー召喚の変数
    [SerializeField, Header("スポナー召喚をするのは何巡目か")]
    int _spawnerNum = 3;
    [SerializeField, Header("召喚するスポナー")]
    GameObject spawnerPre;
    // 後々二つにする
    [SerializeField]
    Transform summonPoint;

    // ジャンプスタンプの変数
    //[SerializeField,Header("ジャンプスタンプ攻撃")]
    //float jumpHeight = 5.0f;

    private void Start()
    {
        // Animationコンポーネントを取得
        GameObject bossAnim = GameObject.Find("BossAnimation");
        animator = bossAnim.GetComponent<Animator>();

        // 音を鳴らすためのAudioSourceコンポーネントを取得
        _audioSource = GetComponent<AudioSource>();

        // ボスの攻撃当たり判定を取得
        _bossColl = GameObject.Find("BossIdleAttack");
        // 攻撃以外では常に使う
        _bossColl.SetActive(true); 

        // 突進前後退の攻撃判定の取得
        _retreatDash = GameObject.Find("RetreatDash");
        // 突進前後退以外では使わない
        _retreatDash.SetActive(false);

        // 突進時の攻撃判定を取得
        _bossDashColl = GameObject.Find("BossDash");
        // 突進以外は使わない
        _bossDashColl.SetActive(false);

        // 通常攻撃の攻撃判定を取得
        attackSensorController = GameObject.Find("NomalAttack");
        // 通常攻撃以外には使わない
        attackSensorController.SetActive(false);

        // SpriteRendererを取得
        this.baseSr = GetComponent<SpriteRenderer>();
        animSr = bossAnim.GetComponent<SpriteRenderer>();
        baseSr.enabled = false;
        animSr.enabled = true;

        // 初期アニメーションはIdle
        animator.SetBool("isWalk", false);

        // ボスの開始HPはbossStartHp
        _bossHp = bossStartHp;
      

    }

    void Update()
    {
        // isActingがtrueのときreturn返す
        if (isActing)
            return;

        float distance = 
            Vector2.Distance(transform.position, player.position);

        // プレイヤーが近いとき
        if(distance <= attackRenge)
        {
            // 攻撃開始
            StartCoroutine(AttackRoutine());
        }

        // 減らしたHPを_bossHpに代入
        //_bossHp = _elementCollider._currentBossHp;
        //Debug.LogError("_bossHp:" + _bossHp);
    }

    private IEnumerator AttackRoutine()
    {
        isActing = true;

        FacePlayer();
        // 突進前後退
        yield return RetreatDash();
        // 突進
        yield return Dash();
        // 通常攻撃
        yield return MeleeAttack();
        // 後退
        yield return Retreat();
        // ジャンプスタンプ
        yield return JumpStamp();
        // 通常攻撃
        yield return MeleeAttack();

        if (_attackCounter >= _spawnerNum)
        {
            _attackCounter = 0;
            animator.SetBool("isWalk", true);
            yield return Retreat();
            animator.SetBool("isWalk", false);

            yield return SummonSpawner();
        }

        //yield return SummonSpawner();
        _attackCounter++;

        isActing = false;
    }

    // player方向を向く
    private void FacePlayer()
    {
        if(player.position.x > transform.position.x)
        {
            transform.localScale =
                new Vector3(_bossScale, _bossScale, 1);
        }
        else
        {
            transform.localScale = new Vector3(-_bossScale, _bossScale, 1);
        }
    }

    // 突進前の後退
    private IEnumerator RetreatDash()
    {
        FacePlayer();
        // 突進前のアニメーションを再生
        _bossColl.SetActive(false);
        animator.SetTrigger("isDashPre");
        // 1.0f秒待つ
        yield return new WaitForSeconds(1.0f); 
        // 攻撃判定を表示
        _retreatDash.SetActive(true);
        // 攻撃音を鳴らす
        _audioSource.PlayOneShot(_retraetSound);
        // 1.0f秒待つ
        yield return new WaitForSeconds(0.6f);
        _retreatDash.SetActive(false);
        _bossColl.SetActive(true);

        float dir =
            player.position.x > transform.position.x
            ? -1.0f
            : 1.0f;

        // 後退を始める位置
        Vector3 startPos = transform.position;

        Vector3 targetPos = 
            startPos + Vector3.right * dir * dashRetreatDistance;
        
        Debug.Log("ボスの突進！");
        

        while (Vector2.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    retreatSpeed * Time.deltaTime);

            yield return null;
        }
    }

    // 突進
    private IEnumerator Dash()
    {
        FacePlayer();
        // 攻撃判定を有効化、当たり判定を無効化
        _bossDashColl.SetActive(true);
        _bossColl.SetActive(false);

        float dir =
            player.position.x > transform.position.x
            ? 1.0f
            : -1.0f;

        Vector3 targetPos =
            transform.position + Vector3.right * dir * dashDistance;

        animSr.enabled = false;
        baseSr.enabled = true;

        while(Vector2.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    dashSpeed * Time.deltaTime);

            yield return null;
        }

        // 攻撃判定を無効化、当たり判定を有効化
        _bossDashColl.SetActive(false);
        _bossColl.SetActive(true);

        // 突進のスプライトを非表示、アニメーションのスプライトを表示
        baseSr.enabled = false;
        animSr.enabled = true;
        yield return null;
    }

    // 通常攻撃
    private IEnumerator MeleeAttack()
    {
        FacePlayer();
        // 攻撃はじめのアニメーションを再生
        animator.SetTrigger("isAttackPre");
        // 1.0f待つ
        yield return new WaitForSeconds(0.5f);
        // 攻撃アニメーションを再生
        animator.SetTrigger("isAttack");
        attackSensorController.SetActive(true); // 攻撃判定
        _bossColl.SetActive(false);  　// ボスの当たり判定
        yield return new WaitForSeconds(0.5f);
        attackSensorController.SetActive(false);    // 攻撃判定
        _bossColl.SetActive(true);  // ボスの当たり判定

        Debug.Log("攻撃!");

        yield return null;
    }

    // スポナー召喚
    private IEnumerator SummonSpawner()
    {
        // 攻撃はじめのアニメーションを再生
        animator.SetTrigger("isAttackPre");
        GameObject spawner = Instantiate(
            spawnerPre,
            summonPoint.position,
            Quaternion.identity);

        yield return new WaitForSeconds(1.0f);

        Destroy(spawner);
    }

    // ゆっくり後退する
    private IEnumerator Retreat()
    {
        FacePlayer();
        // 歩くアニメーションを再生
        animator.SetBool("isWalk", true);
        // 1秒まつ
        yield return new WaitForSeconds(1.0f);

        float dir =
            player.position.x > transform.position.x
            ? -1.0f
            : 1.0f;

        // 後退を始める位置
        Vector3 startPos = transform.position;

        Vector3 targetPos =
            startPos + Vector3.right * dir * retreatDistance;

        while (Vector2.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    retreatSpeed * Time.deltaTime);
            yield return null;
        }
        // 1秒まつ
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("isWalk", false);
        yield return null;
    }


    // ジャンプスタンプ
    private IEnumerator JumpStamp()
    {
        float trackTime = 2.0f;
        float timer = 0f;

        
        // 真上へジャンプ
        float jumpHeight = 7.0f;

        Vector3 startPos = transform.position;

        Vector3 topPos =
            new Vector3(player.position.x, startPos.y) + Vector3.up * jumpHeight;

        while(Vector2.Distance(transform.position, topPos) > 1.0f)
        {
            transform.position = 
                Vector3.MoveTowards(
                    transform.position,
                    topPos,
                    20.0f * Time.deltaTime);

            yield return null;
        }

        // プレイヤー追尾
        //timer += Time.deltaTime;
        while (timer < trackTime)
        {
            timer += Time.deltaTime;
            transform.position =
               new Vector3(
                   player.position.x,
                   transform.position.y,
                   transform.position.z
                   );

            yield return null;
        }
        //timer = 0;

        // 落下位置確定
        float lockX = transform.position.x;

        

        // 落下
        Vector3 groundPos =
            new Vector3(
                lockX,
                startPos.y,
                startPos.z);
        yield return new WaitForSeconds(1.0f);

        while (Vector2.Distance(transform.position, groundPos) > 0.05f)
        {
            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    groundPos,
                    15f * Time.deltaTime);

            yield return null;
        }

    }
}
