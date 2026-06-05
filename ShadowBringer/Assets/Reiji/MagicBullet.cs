using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    [Header("ーー 爆風の型（プレハブ） ーー")]
    public GameObject explosionPrefab; // ここにbakuhuucolliderのプレハブを入れるよ！

    [Header("ーー 爆発するまでの時間 ーー")]
    public float lifeTime = 1.0f;       // 【君のこだわり！】1秒後に爆発するように設定したよ

    void Start()
    {
        // 生まれた瞬間、1秒後（lifeTime）に「Explode（爆発）」という劇（関数）を始める予約をする
        Invoke("Explode", lifeTime);
    }

    void Explode()
    {
        // 1. もし爆風の型が登録されていたら、自分が今いる場所に爆風を生み出す
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // 2. 自分（小さな球）の役目は終わりなので、世界から消滅する
        Destroy(gameObject);
    }
}