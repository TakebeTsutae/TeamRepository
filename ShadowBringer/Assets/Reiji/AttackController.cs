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
    public float attackCooldown = 3f;
    //public float attackCooldownSub = 0.5f; 
    //bool canAttack = true;
    int time = 0;
    private float lastAttackTime = 0f;
    public kenncontroller kenattack;
    public tuecontroller tuecontroller;
    public PlayerController playerController;
    private void Start()
    {
        kenattack = GetComponent<kenncontroller>();
        tuecontroller = GetComponent<tuecontroller>();
        playerController = GetComponent<PlayerController>();
    }
    void FixedUpdate()
    {
        // canAttackをtrueにする
       /* time++;
        if (time>= 50)
        {
            attackCooldown -= attackCooldownSub;
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
        bool canAttack =
            Time.time >= lastAttackTime + attackCooldown;

        // J押し中 ＆ 攻撃可能
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            //Debug.Log("ボタンゲット");
            //if(playerController.weapon == "Ken")
            {
                //Debug.Log("剣ゲット");
                Attack();
            }

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

        // 攻撃範囲内の敵をまとめて取得
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
            );

        foreach(Collider2D enemy in hitEnemies)
        {
            var health = enemy.GetComponent<EnemyHealth>();
            if(health!=null)
            {
                health.TakeDamage(attackDamage);
            }
        }
        Debug.Log("Attack実行");
    }
    void AttackTue()
    {

        tuecontroller.ShootMagic();

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
