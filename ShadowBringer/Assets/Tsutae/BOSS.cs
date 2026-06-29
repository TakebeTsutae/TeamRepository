using System.Collections;
using UnityEngine;

public class BOSS : MonoBehaviour
{ 
    [Header("ボスHP")]
    public int bossHp = 500;
    [SerializeField,Header("大きさ")]
    float _bossScale = 2.0f; // ボスの大きさ
    bool _isDaed = false; // ボスの死亡判定

    // アニメーションの登録
    Animator animator;
    RuntimeAnimatorController runtimeController;
    private SpriteRenderer sr;
    public AnimationClip idleClip;
    public AnimationClip walkClip;
    public AnimationClip attackPreClip;
    public AnimationClip attackClip;
    public AnimationClip dashPreClip;
    [SerializeField] private Sprite dashSprite;
   // [SerializeField] private float time = 0;
   // [SerializeField] private int idx = 0;
    //[SerializeField] private AnimationClip dashClip;


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
        // Animationコンポーネントを取得
        animator = GetComponent<Animator>();
        // SpriteRendererを取得
        this.sr = GetComponent<SpriteRenderer>();

        // 空のRuntimeAnimatorControllerを作成
        runtimeController = new AnimatorOverrideController();
        animator.runtimeAnimatorController = runtimeController;

        // 初期状態はIdle
        PlayClip(idleClip);
        animator.SetBool("isAttack", false);

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
        yield return MeleeAttack();
        // 後退
        yield return Retreat();

        yield return JumpStamp();

        yield return MeleeAttack();

        if (_attackCounter >= _spawnerNum)
        {
            _attackCounter = 0;
            yield return Retreat();

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

        float dir =
            player.position.x > transform.position.x
            ? -1.0f
            : 1.0f;

        // 後退を始める位置
        Vector3 startPos = transform.position;

        Vector3 targetPos = 
            startPos + Vector3.right * dir * dashRetreatDistance;

        while(Vector2.Distance(transform.position, targetPos) > 0.05f)
        {
            // 突進前のアニメーションを再生
            PlayClip(dashPreClip); 
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

        this.sr.sprite = this.dashSprite;

        while(Vector2.Distance(transform.position, targetPos) > 0.05f)
        {
            // 突進のアニメーションを再生
           // PlayAnimation("BossDash");
            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    dashSpeed * Time.deltaTime);

            yield return null;
        }
    }

    // 通常攻撃
    private IEnumerator MeleeAttack()
    {
        //  animator.SetTrigger("Attack");
        // 攻撃始め、攻撃のアニメーションを再生
        PlayClip(attackPreClip);
        PlayClip(attackClip);
        Debug.Log("攻撃!");

        yield return new WaitForSeconds(1.0f);
    }

    // スポナー召喚
    private IEnumerator SummonSpawner()
    {
        // 攻撃はじめのアニメーションを再生
        PlayClip(attackPreClip);
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
            // 歩くアニメーションを再生
            PlayClip(walkClip);
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

        // ボスが死んだとき
        if (bossHp <= 0)
        {
            _isDaed = true;
            
        }
    }

    // アニメーションを再生する関数
    void PlayClip(AnimationClip clip)
    {
        if (clip == null) return;

        // AnimationOverrideControllerを使って一時的に差し替え
        var overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;

        // ベースのステート名は"Default"として登録
        overrideController["Default"] = clip;

        animator.runtimeAnimatorController = overrideController;
        animator.Play("Default", 0, 0f); // 即再生
    }
    /*
    void PlayAnimation(string clipName)
    {
        if (!anim.IsPlaying(clipName))
        {
            anim.CrossFade(clipName, 0.1f); // スムーズに切り替え
        }
        Debug.Log("再生中 : " +  clipName);
    }
    */
}
