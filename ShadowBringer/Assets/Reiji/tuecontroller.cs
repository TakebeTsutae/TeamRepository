using UnityEngine;

public class tuecontroller : MonoBehaviour
{
    [Header("ーー 魔法の型と発射口 ーー")]
    public GameObject bulletPrefab;  // 魔法の球の「型（プレハブ）」
    public Transform firePoint;      // 魔法を発射する場所（杖の先っぽの目印）

    [Header("ーー 魔法の性能調整 ーー")]
    public float cooldownTime = 1.0f; // 次に撃てるまでの待ち時間（1秒）
    public float maxSpeed = 15.0f;     // 弾の最大スピード（上限ストッパー）
    public float powerMultiplier = 3.0f; // 引っ張る力の強さ（ゴムの硬さ）

    private float cooldownTimer = 0.0f; // 砂時計タイマーの実体

    void Update()
    {
        // 毎コマ、砂時計の砂を減らす
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
      
        
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0)
        {
            ShootMagic();
        
        }
        
        // 左クリックが「押された瞬間」かつ「砂時計が0以下」なら発射
        
    }

    public void ShootMagic()
    {
        // 1. マウスカーソルの世界座標を計算
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // 2Dなので奥行きはゼロ

        // 2. 杖の先（firePoint）からカーソルへの「方向」と「距離」を計算
        Vector3 direction = mouseWorldPos - firePoint.position;
        float distance = direction.magnitude; // 離れている距離

        // 3. 方向を「純粋な向き（長さ1）」にする
        Vector3 shotDirection = direction.normalized;

        // 4. 距離に応じてスピードを決めつつ、上限（maxSpeed）でストッパーをかける
        float finalSpeed = Mathf.Clamp(distance * powerMultiplier, 0f, maxSpeed);

        // 5. 魔法の球を世界に生み出す（引数に親子関係を入れないことで、世界直下に独立して生まれる）
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 6. 生まれた球の物理エンジン（Rigidbody2D）に速度をセットする
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shotDirection * finalSpeed;
        }

        // 7. 砂時計をひっくり返す（タイマーリセット）
        cooldownTimer = cooldownTime;
    }
}