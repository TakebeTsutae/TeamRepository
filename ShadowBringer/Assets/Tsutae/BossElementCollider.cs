using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossElementCollider : MonoBehaviour
{
    // プレイヤーの攻撃力を取得
    int _playerAttack;
    PlayerController1 _playerController;
    BOSS _bossScript;
    private int _maxBossHp;
    public int currentBossHp;

    // 被弾処理の変数
    bool _isHitting;    // 攻撃が当たっている状態かどうか
    bool _isHit; // 仮
   
    float _hitCount = 0;    // 攻撃が当たってからの秒数

    BossHpBar _bossHpBar;

    GameObject _bossIdleAttack; // ダメージを与える当たり判定
    // 色を変えるための変数
    SpriteRenderer _spriteRenderer;

    // ボスが死んだときにシーン移動する
    [SerializeField] string nextSceneName = "4_MainClearScne";　// 移動するシーン

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ゲームオブジェクトを取得する
        GameObject _obj = GameObject.Find("player");
        GameObject _bossObj = GameObject.Find("Boss");
        GameObject _bossAnim = GameObject.Find("BossAnimation");
        // ダメージを与える当たり判定
        _bossIdleAttack = GameObject.Find("BossIdleAttack");

        // スクリプトを取得
        _playerController = _obj.GetComponent<PlayerController1>();
        _bossScript = _bossObj.GetComponent<BOSS>();
        _bossHpBar = _bossObj.GetComponent<BossHpBar>();

        _maxBossHp = _bossScript.bossStartHp;
        currentBossHp = _maxBossHp;
        // 色を変えるためにスプライトレンダラーを取得
        _spriteRenderer = _bossAnim.gameObject.GetComponent<SpriteRenderer>();
        // 始めは攻撃が当たっていない
        _isHitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        _hitCount += Time.deltaTime;
        // 色が変わる秒数
        //_hitChangeCount += Time.deltaTime;
        // ボスが死んだらシーンを移動する
        if (currentBossHp <= 0)
        {
            currentBossHp = 0;
            OnMoveClearScene();
        }
        OnSetBossAttack();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isHit)
        {
            return;
        }
        //ボスのHPを減らす
        // ヒットクールタイム中は攻撃を食らわない
        if (collision.gameObject.tag == "weponAttack")
        {
            _hitCount = 0;
            _isHitting = true;
            // 統合したときに使用（プレイヤーの攻撃力取得のためのやつ）
            _playerAttack = _playerController._attackTotal;
            currentBossHp -= _playerAttack;
            //Debug.LogError(currentBossHp);
            // 点滅処理
            StartCoroutine(OnHit());
        }
        else
        {
            _isHitting = false;
        }
    }

    /*
    void SetisDaed(bool isDaed)
    {
        _isBossDaed = isDaed;
        Debug.LogError("SetisBossDaed:" + _isBossDaed);
    }
    

    public bool GetisDaed()
    {
        //Debug.LogError("GetisBossDaed:" + _isBossDaed);
        return isDaed;
    }
    */
    void OnMoveClearScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    // 点滅処理
    private IEnumerator OnHit()
    {
        // 途中
        // isHit = true;
        //_hitChangeCount = 0;
        //HPバーを動かす
        _bossHpBar.TakeDamage();

        for (int i = 0; i < 3; i++)
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
          
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    public void OnSetBossAttack()
    {
        
        // ヒットクールタイム中はダメージを与える当たり判定を消す
        if (_isHitting)
        {
            
            // 攻撃を食らったら点滅する
            OnHit();
            //_bossIdleAttack.SetActive(false);
        }
        else if(!_isHitting)
        {
            
        }
        
    }
}
