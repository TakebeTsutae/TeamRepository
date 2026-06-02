using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // ダメージを受けたときに呼ぶ
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth<=0)
        {
            Die();
;        }
    }
    void Die()
    {
        // 敵が倒れた時の処理(エフェクト再生などを入れてもOK)
        Destroy(gameObject);
    }
}
