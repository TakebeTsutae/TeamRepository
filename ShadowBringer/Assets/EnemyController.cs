using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 左に動く→攻撃
    // 左に動く→ジャンプ
    // 移動速度
    float _speed;

    // 更新後の位置
    Vector3 _inputVelocity;
    Rigidbody2D _rigid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speed = 0.01f;
        _inputVelocity = transform.position;
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     //   transform.position = _inputVelocity + _speed;
    }
}
