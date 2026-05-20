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
    public float attackCooldown = 0.5f;

    private float lastAttackTime = 0f;
    public kenncontroller kenattack;
    private void Start()
    {
        kenattack = GetComponent<kenncontroller>();
    }
    void Update()
    {
        bool canAttack = Time.time >= lastAttackTime + attackCooldown;


        // 攻撃ボタンが押され、クールタイムも経過している時だけ攻撃
        if(Keyboard.current.jKey.isPressed && canAttack)
        {
            Attack();
            lastAttackTime = Time.time;
            Debug.Log("攻撃");
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
    }

    // エディタ上で攻撃範囲を表示
    void OndrawGizmosSelected()
    {
        if(attackPoint!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
