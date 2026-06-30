using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    //ゲームマネージャーにぶち込む







    //=============================================







    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 10;
    public int attackCooldown = 50;
    //public float attackCooldownSub = 0.5f; 
    //bool canAttack = true;
    int time = 0;
    private float lastAttackTime = 0f;
    public kenncontroller kenattack;
    //public tuecontroller tuecontroller;
    public PlayerController playerController;

    private PlayerController1 _playerController1;
    //---------------------------------------------------------------
    private void Start()
    {
        kenattack = GetComponent<kenncontroller>();
        //tuecontroller = GetComponent<tuecontroller>();
        playerController = GetComponent<PlayerController>();

        _playerController1 = GetComponent<PlayerController1>();
    }
    void FixedUpdate()
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
        if (Mouse.current.leftButton.wasPressedThisFrame)// && canAttack)
        {
            //Debug.Log("ボタンゲット");
            //if(playerController.weapon == "Ken")

            //Debug.Log("剣ゲット");

            Debug.Log("クリック検知！");
            Attack();


            //_playerController1.ChangeAnimation("Attack");

            //else if(playerController.weapon == "Tue")
            //{
            //    AttackTue();
            //}

            // 攻撃した時間を保存
            lastAttackTime = Time.time;
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