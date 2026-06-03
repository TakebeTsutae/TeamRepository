using UnityEngine;

public class BossController : MonoBehaviour
{
    // PlayerControllerスクリプトからtotalAttackを取得する
    //PlayerController playerController;    // 取得したスクリプトを入れる変数

    // ボスの体力
    int bossHp = 500;

    // 突進のための変数
    public float _moveSpeed = 2.0f;
    public float _moveRange = 3.0f;
    private Vector3 _startPositon;
    private int derection = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // PlayerオブジェクトからPlayerControllerスクリプトを取得
        //playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _startPositon = transform.position;

        Debug.Log(bossHp);
    }

  

    // Update is called once per frame
    void Update()
    {
        //transform.Translate()
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // _bossHpからtotalAttackを減らす
            //_bossHp -= playerController.totalAttack;
        }
    }
}
