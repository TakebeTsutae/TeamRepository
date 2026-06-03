using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public GameObject explosionPrefab; // 爆風の「型（プレハブ）」
    public float lifeTime = 0.2f;        // 爆発するまでの時間（0.2秒）

    void Start()
    {
        // 【法律4】生まれた瞬間、0.2秒後に「Explode（爆発）」という劇を始める予約をする
        Invoke("Explode", lifeTime);
    }

    void Explode()
    {
        // 1. 自分が今いる場所に、爆風オブジェクトを生み出す
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 2. 自分（球）の役目は終わりなので消滅する
        Destroy(gameObject);
    }
}