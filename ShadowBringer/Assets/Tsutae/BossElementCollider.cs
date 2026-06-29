using UnityEngine;

public class BossElementCollider : MonoBehaviour
{
    // プレイヤーの攻撃力を取得
    int _playerAttack;
    PlayerController _playerController;
    BOSS bossScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject obj = GameObject.Find("player");
        GameObject bossObj = GameObject.Find("Boss");
        //　↓スクリプトがついてあるゲームオブジェクトを取得する
        _playerController = obj.GetComponent<PlayerController>();
        bossScript = bossObj.GetComponent<BOSS>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ボスのHPを減らす
        if (collision.gameObject.tag == "wepon")
        {
            // 統合したときに使用（プレイヤーの攻撃力取得のためのやつ）
            _playerAttack = _playerController._attackTotal;
            bossScript.bossHp -= _playerAttack;
            Debug.Log("ボスHP : " + bossScript.bossHp);
        }
    }

    // BossのHpを返す
    public void GetBossHp()
    {

    }
}
