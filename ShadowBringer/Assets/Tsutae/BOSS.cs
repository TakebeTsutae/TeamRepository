using System.Collections;
using UnityEngine;

public class BOSS : MonoBehaviour
{ 
    [Header("ボスHP")]
    public int bossHp = 500;
    [SerializeField,Header("大きさ")]
    float _bossScale = 2.0f; // ボスの大きさ

    // アニメーション
    Animator animator;
    GameObject bossAnim;
    private SpriteRenderer baseSr;
    private SpriteRenderer animSr;
    [SerializeField,Header("ダッシュ中の見た目")] private Sprite dashSprite;
    // デバッグ用
    AnimatorClipInfo[] clipInfo;


    // 一連の攻撃を何回終えたか
    int _attackCounter = 0;

   

    [SerializeField,Header("プレイヤーの座標")]
    Transform player;

    [SerializeField,Header("攻撃を始めるプレイヤーとの距離")]
    float attackRenge = 10.0f;

    private bool isActing;

    // 後退の変数
    [SerializeField,Header("後退")]
    float dashRetreatDistance = 1.0f;   // 突進前の後退で進む距離
    float retreatDistance = 5.0f;   // 後退で進む距離
    [SerializeField]
    float retreatSpeed = 3.0f;  // 突進前後退のスピード

    // 突進の変数
    [SerializeField,Header("突進")]
    float dashDistance = 5.0f;  // 突進で進む距離
    [SerializeField]
    float dashSpeed = 10.0f;    // 突進のスピード

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
        bossAnim = GameObject.Find("BossAnimation");
        // Animationコンポーネントを取得
        animator = bossAnim.GetComponent<Animator>();
        // SpriteRendererを取得
        this.baseSr = GetComponent<SpriteRenderer>();
        animSr = bossAnim.GetComponent<SpriteRenderer>();
        baseSr.enabled = false;
        animSr.enabled = true;

        // 初期状態はIdle
        animator.SetBool("isWalk", false);

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
        //yield return new WaitForSeconds(0.3f); 
        yield return MeleeAttack();
        // 後退
        // 歩くアニメーションを再生
        animator.SetBool("isWalk", true);
        yield return Retreat();
        animator.SetBool("isWalk", false);
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
        animator.SetTrigger("isDashPre");

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
        // 攻撃アニメーションを再生
        animator.SetTrigger("isAttack");
        //string currentClipName = clipInfo[0].clip.name;
        //Debug.LogError("現在のアニメーション : " + currentClipName);

        Debug.Log("攻撃!");

        yield return new WaitForSeconds(1.0f);
    }

    // スポナー召喚
    private IEnumerator SummonSpawner()
    {
        // 攻撃はじめのアニメーションを再生
        //PlayClip(attackPreClip);
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
            //string currentClipName = clipInfo[0].clip.name;
            //Debug.LogError("現在のアニメーション : " + currentClipName);
            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    retreatSpeed * Time.deltaTime);
            yield return null;
        }
        // 1秒まつ
        yield return new WaitForSeconds(1.0f);
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

        // プレイヤーへの着地ダメージ発生

    }
}
