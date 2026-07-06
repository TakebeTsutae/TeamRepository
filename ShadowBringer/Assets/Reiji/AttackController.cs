using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    //ゲームマネージャーにぶち込む







    //=============================================







    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 10;
    public float PlayerAttackCooldown = 0.5f;
    private float PlayerAttackCooldownCounter;
  
    //bool canAttack = true;
    int time = 0;
    private float lastAttackTime = 0f;
    public kenncontroller kenattack;
    //public tuecontroller tuecontroller;

    private bool canAttack = true;

    [SerializeField]
    private PlayerController1 _playerController1;
    //---------------------------------------------------------------
    private void Start()
    {
        kenattack = GetComponent<kenncontroller>();
        //tuecontroller = GetComponent<tuecontroller>();


    }
    void Update()
    {
        //Debug.Log("Update動いてる");
        // canAttackをtrueにする
        /*
        time++;
         if (time>= 50)
         {
             time -= attackCooldown;
             time = 0;
         }
         if(attackCooldown<=0)
         {
             canAttack = true;
         }
         else
         {
             canAttack = false;
         }
        */
        // 今攻撃できるか？
        //bool canAttack =
        //    Time.time >= lastAttackTime + attackCooldown;



        // J押し中 ＆ 攻撃可能
        if (Mouse.current.leftButton.wasPressedThisFrame&& canAttack)
        {
            //Debug.Log("ボタンゲット");
            //if(playerController.weapon == "Ken")

            //Debug.Log("剣ゲット");

            Debug.Log("クリック検知！");
            Attack();

            StartCoroutine(AttackRoutine());

            canAttack = true;

            //_playerController1.ChangeAnimation("Attack");

            //else if(playerController.weapon == "Tue")
            //{
            //    AttackTue();
            //}

            // 攻撃した時間を保存
            
        }
    }

    void Attack()
    {

        kenattack.PlayerAttack();
        _playerController1.PlayerAttackAnimation();
        Debug.Log("剣攻撃");

        // 攻撃範囲内の敵をまとめて取得
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
            );

        foreach (Collider2D enemy in hitEnemies)
        {
            var health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }
        Debug.Log("Attack実行");
    }

    private IEnumerator AttackRoutine()
    {
        canAttack = false;
        Debug.Log("開始 canAttack=" + canAttack + "time=" + Time.time);

        yield return new WaitForSeconds(PlayerAttackCooldown);

          canAttack = true;

        Debug.Log("終了 canAttack=" + canAttack + "time=" + Time.time);
    }
    /*
    private void FixedUpdate()
    {
        // 攻撃のクールダウンのカウントの設定
        if (Mouse.current.leftButton.wasPressedThisFrame && PlayerAttackCooldownCounter <= 0f)
        {
            PlayerAttackCooldownCounter = PlayerAttackCooldown; // タイマーを1秒満タンにセットする
            StartCoroutine(AttackRoutine());
        }
    }
    */

    void AttackTue()
    {

        //tuecontroller.ShootMagic();

        // 攻撃範囲内の敵をまとめて取得
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
            );

        foreach (Collider2D enemy in hitEnemies)
        {
            var health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }
        Debug.Log("Attack実行");
    }

    // エディタ上で攻撃範囲を表示
    /*void OnDrawGizmosSelected()
    {
        if(attackPoint!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }*/
}