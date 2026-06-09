using UnityEngine;

public class BossController : MonoBehaviour
{
    // PlayerControllerスクリプトからtotalAttackを取得する
    //PlayerController playerController;    // 取得したスクリプトを入れる変数

    // ボスの体力
    int _bossHp = 500;

    // 攻撃パターンの管理番号
    int _attackNum = 0;
    float _attackCount; // 次の攻撃までの時間をカウントする
    float _moveTime = 0.5f; // 始めの移動行動に移る
    float _attackTime = 2.0f; // 次の攻撃に行くまでの時間
    bool _bossMove = true;
    bool _attackNomal = false;  // これがtrueになったら通常攻撃

    // 移動
    Rigidbody2D _rb2D;
    Vector3 _velocity; // PlayerとBossの距離
    GameObject _player;
    Vector3 _playerPos; // Playerのポジション

   

    // 通常攻撃
    public GameObject nomalAttackPre;   // プレハブを入れる
    GameObject _boss;
    

    // 判定を表示している時間
    float _desTime = 0.03f;

    // 突進のための変数
    float _moveSpeed = -1f;
    public float _moveRange = 3.0f;
    // Bossのポジション
    private Vector3 _bossPos;
    // nomalAttackPreを発生させる場所の距離
    float _bossDel = 1.5f;
    private Vector3 _startPos;
    private int derection = 1;

    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _attackCount = 0;
        // PlayerオブジェクトからPlayerControllerスクリプトを取得
        //playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _boss = GameObject.Find("Boss");    // ボスオブジェクトを取得
        _player = GameObject.Find("Player"); // Playerオブジェクトを取得
        _rb2D = GetComponent<Rigidbody2D>();

        _playerPos = new Vector3(_player.transform.position.x, _player.transform.position.y, 0);
       // スタート位置を指定
        _startPos = new Vector3(4, -4, 0);
        transform.position = _startPos;

       // Debug.Log(bossHp);
    }

    private void Update()
    {
       
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnNomalAttack();
        }
        */
    }



    // 通常攻撃
    /*
    public void OnNomalAttack()
    {
        if(_nomalAttack != null)
        {
            // 現在の状態を反転
            bool isActive = _nomalAttack.activeSelf;
            _nomalAttack.SetActive(!isActive);
        }
    }
    */

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = _startPos + ;
        //transform.Translate()

        // 時間を加算していく
        _attackCount += Time.deltaTime;

        // 移動
        if( _attackNum == 0)
        {
            
          //  Debug.Log(transform.position);
            if (_attackCount < _attackTime && _bossMove)
            {
               
                _velocity = _rb2D.linearVelocity;
                _velocity.x = _moveSpeed;
                _rb2D.linearVelocity = _velocity;

            }
            else if(_attackCount >= _attackTime)
            {
                _bossMove = false;
                _attackNomal = true;
                _attackCount = 0;
                _attackNum++;
            }
            
        }

        // 通常攻撃
        if (_attackNum == 1)
        { 
            if (_attackCount < _desTime && _attackNomal && _moveSpeed < 0)
            {
                
                // _nomalAttackを出現させる位置
                _bossPos = new Vector3(_boss.transform.position.x - _bossDel, _boss.transform.position.y, 0);
                // nomalAttackPreを_bossPosの位置に無回転で出現
                GameObject nomalAttack = Instantiate(nomalAttackPre, _bossPos, Quaternion.identity);
                // _nomalAttackPreを_desTime後に消す
                Destroy(nomalAttack, _desTime);
            }
            else if(_attackCount < _desTime && _attackNomal && _moveSpeed > 0)
            {
                Debug.Log(_attackCount);
                // _nomalAttackを出現させる位置
                _bossPos = new Vector3(_boss.transform.position.x + _bossDel, _boss.transform.position.y, 0);
                // nomalAttackPreを_bossPosの位置に無回転で出現
                GameObject nomalAttack = Instantiate(nomalAttackPre, _bossPos, Quaternion.identity);
                // _nomalAttackPreを_desTime後に消す
                Destroy(nomalAttack, _desTime);
            }
            else if(_attackCount >= _desTime)
            {
                _attackNum ++;
                _attackNomal = false;
                _attackCount = 0;
            } 
        }

        // 通常攻撃と移動のいい感じの間
        if(_attackNum == 2)
        {
            if (_attackCount < _moveTime) 
            {
                _bossMove = false;
            }
            else if(_attackCount >= _moveTime)
            {
                _attackNum = 0;
                _attackCount = 0;
                _bossMove = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "tueAttack")
        {
            // _bossHpから2(totalAttack)を減らす
            _bossHp -= 2;
        }
        if(collision.gameObject.tag == "kenAttack")
        {
            _bossHp -= 3;
        }
    }
}
