using UnityEngine;

public class BossController : MonoBehaviour
{
    // PlayerControllerスクリプトからtotalAttackを取得する
    //PlayerController playerController;    // 取得したスクリプトを入れる変数

    // ボスの体力
    int bossHp = 500;

    // 攻撃パターンの管理番号
    int _attackNum = 0;

    // 通常攻撃
    GameObject _nomalAttack;

    // 突進のための変数
    public float _moveSpeed = 2.0f;
    public float _moveRange = 3.0f;
    private Vector3 _startPos;
    private int derection = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // PlayerオブジェクトからPlayerControllerスクリプトを取得
        //playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        _nomalAttack = GameObject.Find("NomalAttack");
        _nomalAttack.SetActive(false);
        
        _startPos = transform.position;

        Debug.Log(bossHp);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnNomalAttack();
        }
    }

    // 通常攻撃
    public void OnNomalAttack()
    {
        if(_nomalAttack != null)
        {
            // 現在の状態を反転
            bool isActive = _nomalAttack.activeSelf;
            _nomalAttack.SetActive(!isActive);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = _startPos + ;
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
