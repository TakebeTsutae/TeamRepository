using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossElementCollider : MonoBehaviour
{
    // プレイヤーの攻撃力を取得
    int _playerAttack;
    PlayerController1 _playerController;
    BOSS _bossScript;
    public int _currentBossHp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject obj = GameObject.Find("player");
        GameObject bossObj = GameObject.Find("Boss");
        //　↓スクリプトがついてあるゲームオブジェクトを取得する
        _playerController = obj.GetComponent<PlayerController1>();
        _bossScript = bossObj.GetComponent<BOSS>();
        _currentBossHp = _bossScript.bossStartHp;
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ボスのHPを減らす
        if (collision.gameObject.tag == "weponAttack")
        {
            // 統合したときに使用（プレイヤーの攻撃力取得のためのやつ）
            _playerAttack = _playerController._attackTotal;
            _currentBossHp -= _playerAttack;
            //Debug.Log("ボスHP : " + _currentBossHp);
        }
    }

    public int GetBossHp()
    {
        return _currentBossHp;
    }
}
