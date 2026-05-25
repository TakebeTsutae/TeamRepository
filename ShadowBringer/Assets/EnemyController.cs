using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 左に動く→攻撃
    // 左に動く→ジャンプ
    Vector3 speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = new Vector3(-0.01f, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + speed;
    }
}
