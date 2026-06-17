using Unity.VisualScripting;
using UnityEngine;

// 237行目コメントアウトしています

public class BossController : MonoBehaviour
{
    // PlayerControllerスクリプトからtotalAttackを取得する
    //PlayerController playerController;    // 取得したスクリプトを入れる変数

    // ボスの体力
    int _bossHp = 500;

    // 攻撃パターンの管理番号
    int _attackNum = 0;     // 0: 移動
                            // 1: 通常攻撃
                            // 2: 移動と通常攻撃の間
    float _startBossPlayerDel = 20.0f; // Bossが動き出すPlayerとBossの距離 ※要調整
    float _attackCount; // 次の攻撃までの時間をカウントする
    float _moveTime = 1.0f; // 始めの移動行動に移る
    float _attackTime = 2.0f; // 次の攻撃に行くまでの時間
    bool _bossMove;
    bool _attackNomal = false;  // これがtrueになったら通常攻撃

    // 移動
    Rigidbody2D _rb2D;
    Vector3 _velocity; // PlayerとBossの距離
    GameObject _player;
    Vector3 _playerPos; // Playerのポジション

    // 突進のための変数
    bool _moveChenge; // 進む向きを決めるスイッチ
    bool _tossinMae;    // ちょっと後退するスイッチ
    bool _tossin; // 突進を開始するスイッチ
    float _tossinMaeTime = 0.5f; // 後退を始める時間
    float _tossinTime = 0.7f;   // 突進をする時間
    float _tossinSpeed = 20.0f;
    float _moveSpeed = -1f;
    public float _moveRange = 3.0f;


    // 通常攻撃
    public GameObject nomalAttackPre;   // プレハブを入れる
    GameObject _boss;
    

    // 判定を表示している時間
    float _desTime = 0.03f;

    
    // Bossのポジション
    private Vector3 _bossPos;
    // nomalAttackPreを発生させる場所の距離
    float _bossDel = 1.5f;
    private Vector3 _startPos;
    private int derection = 1;

    // ClearScene遷移用オブジェクトの取得
    GameObject _moveClear;
    BossSceneController _clearScene;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _attackCount = 0;
        // PlayerオブジェクトからPlayerControllerスクリプトを取得
        //playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _boss = GameObject.Find("Boss");    // ボスオブジェクトを取得
        _player = GameObject.Find("player"); // Playerオブジェクトを取得
        _moveClear = GameObject.Find("MoveClearScene"); // ClearScene遷移用オブジェクトの取得
        //_clearScene = 
        _rb2D = GetComponent<Rigidbody2D>();

        //右に動く
        _moveChenge = true;
        
       // スタート位置を指定
        _startPos = new Vector3(7, -3, 0);
        transform.position = _startPos;

       // Debug.Log(bossHp);
    }

    private void Update()
    {
        _playerPos = new Vector3(_player.transform.position.x, _player.transform.position.y, 0);
        
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = _startPos + ;
        //transform.Translate()

        // 時間を加算していく
        _attackCount += Time.deltaTime;

        // PlayerとBossの距離の計算
        float _distance = Vector3.Distance(_playerPos, _bossPos);
        Debug.Log(_distance);

        // PlayerとBossの距離が一定距離以内に入ったら
        if( _distance < _startBossPlayerDel)
        {
            
            if(_moveChenge)
            {
                _moveSpeed = 1;
                
            }
            else
            {
                _moveSpeed = -1;
            }

            // 突進前にちょっと後退する
            if (_attackNum == 0 && _attackCount < _tossinMaeTime && _tossinMae)
            {
               // _moveChenge = true;　// 壁に当たったらの判定をする
                _velocity = _rb2D.linearVelocity;
                _velocity.x = _moveSpeed;
                _rb2D.linearVelocity = _velocity;
            }
            else if (_attackNum == 0 && _attackCount >= _tossinMaeTime)
            {
                _attackNum++;
                _attackCount = 0;

                // 後退が終わったときにtrueだったらfalseに
                // falseだったらtrueに
                if (_moveChenge)
                {
                    _moveChenge = false;
                }
                else
                {
                    _moveChenge = true;
                }
                _tossinMae = false;
                _tossin = true;
            }

            // 突進
            if(_attackNum == 1)
            {
                if (_attackCount < _tossinTime && _tossin)
                {
                    _velocity = _rb2D.linearVelocity;
                    _velocity.x = _moveSpeed * _tossinSpeed;
                    _rb2D.linearVelocity = _velocity;
                }
                else if (_attackCount >= _tossinTime)
                {
                    _attackNum++;
                    _attackCount = 0;
                 //   _moveChenge = true; // 壁に当たったらの判定をする
                    _bossMove = true;
                }
            }

            // 移動
            if (_attackNum == 2)
            {

                //  Debug.Log(transform.position);
                if (_attackCount < _attackTime && _bossMove)
                {

                    _velocity = _rb2D.linearVelocity;
                    _velocity.x = _moveSpeed;
                    _rb2D.linearVelocity = _velocity;

                }
                else if (_attackCount >= _attackTime)
                {
                    _bossMove = false;
                    _attackNomal = true;
                    _attackCount = 0;
                    _attackNum++;
                }

            }

            // 通常攻撃
            if (_attackNum == 3)
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
                else if (_attackCount < _desTime && _attackNomal && _moveSpeed > 0)
                {
                    Debug.Log(_attackCount);
                    // _nomalAttackを出現させる位置
                    _bossPos = new Vector3(_boss.transform.position.x + _bossDel, _boss.transform.position.y, 0);
                    // nomalAttackPreを_bossPosの位置に無回転で出現
                    GameObject nomalAttack = Instantiate(nomalAttackPre, _bossPos, Quaternion.identity);
                    // _nomalAttackPreを_desTime後に消す
                    Destroy(nomalAttack, _desTime);
                }
                else if (_attackCount >= _desTime)
                {
                    _attackNum++;
                    _attackNomal = false;
                    _attackCount = 0;
                }
            }

            // 通常攻撃と移動のいい感じの間
            if (_attackNum == 4)
            {
                if (_attackCount < _moveTime)
                {
                    //_moveChenge = false;    // 壁に当たったらの判定をする
                }
                else if (_attackCount >= _moveTime)
                {
                    _attackNum = 0;
                    _attackCount = 0;
                    _tossinMae = true;
                   // _moveChenge = false;   // 壁に当たったらの判定をする
                }
            }
        }

        if(_bossHp <= 0)
        {
         //   _clearScene = GetComponent<ClearSceneController>();
            Destroy(gameObject);
            
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "weponAttack")
        {
            // _bossHpから2(totalAttack)を減らす
            _bossHp -= 3;
            Debug.Log(_bossHp);
        }

        // 壁に当たった時に_moveChengeを変える
        if(collision.gameObject.tag == "Wall")
        {
            if(_moveChenge)
            {
                _moveChenge = false;
            }
            else
            {
                _moveChenge = true;
            }
        }
    }
}
