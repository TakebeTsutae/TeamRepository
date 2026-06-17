using System.Collections;
using UnityEngine;

public class BOSS : MonoBehaviour
{ 
    [SerializeField, Header("ボスHP")]
    int bossHp = 500;
    int _playerAttack;
    PlayerController _playerController;

    [SerializeField,Header("プレイヤーの座標")]
    Transform player;

    [SerializeField,Header("攻撃を始めるプレイヤーとの距離")]
    float attackRenge = 8.0f;

    private bool isActing;

    // 後退の変数
    [SerializeField,Header("後退")]
    float retreatDistance = 1.0f;   // 後退で進む距離

    [SerializeField]
    float retreatSpeed = 3.0f;  // 後退のスピード

    // 突進の変数
    [SerializeField,Header("突進")]
    float dashDistance = 5.0f;  // 突進で進む距離

    [SerializeField]
    float dashSpeed = 10.0f;    // 突進のスピード

    // 近接攻撃の変数
    [SerializeField, Header("通常攻撃のアニメーション")]
    Animator animator;

    // スポナー召喚の変数
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
        GameObject obj = GameObject.Find("player");

        //　↓スクリプトがついてあるゲームオブジェクトを取得する
        _playerController = obj.GetComponent<PlayerController>();
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
        // 後退
        yield return RetreatDash();
        // 突進
        yield return Dash();
        // 通常攻撃
        yield return MeleeAttack();
        // 後退
        yield return Retreat();

        yield return SummonSpawner();

        yield return JumpStamp();

        yield return MeleeAttack();

        yield return Retreat();

        //yield return SummonSpawner();

        isActing = false;
    }

    // player方向を向く
    private void FacePlayer()
    {
        if(player.position.x > transform.position.x)
        {
            transform.localScale =
                new Vector3(2, 2, 1);
        }
        else
        {
            transform.localScale = new Vector3(-2, 2, 1);
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
            startPos + Vector3.right * dir * retreatDistance;

        while(Vector2.Distance(transform.position, targetPos) > 0.05f)
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

        while(Vector2.Distance(transform.position, targetPos) > 0.05f)
        {
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
        Debug.Log("攻撃!");

        yield return new WaitForSeconds(1.0f);
    }

    // スポナー召喚
    private IEnumerator SummonSpawner()
    {
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
            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    retreatSpeed * Time.deltaTime);

            yield return null;
        }
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
            startPos + Vector3.up * jumpHeight;

        while(Vector2.Distance(transform.position, topPos) > 1.0f)
        {
            transform.position = 
                Vector3.MoveTowards(
                    transform.position,
                    topPos,
                    8.0f * Time.deltaTime);

            //yield return null;
        }

        // プレイヤー追尾
        while (timer < trackTime)
        {
            timer += Time.deltaTime;

            transform.position =
               new Vector3(
                   player.position.x,
                   transform.position.y,
                   transform.position.z
                   );

            //yield return null;
        }


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ボスのHPを減らす
        if(collision.gameObject.tag == "wepon")
        {
            // 統合したときに使用（プレイヤーの攻撃力取得のためのやつ）
            _playerAttack = _playerController._attackTotal;
            bossHp -= _playerAttack;
        }
    }
}
