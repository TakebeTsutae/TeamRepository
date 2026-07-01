using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossElementCollider : MonoBehaviour
{
    // プレイヤーの攻撃力を取得
    int _playerAttack;
    PlayerController1 _playerController;
    BOSS bossScript;
    int currentBossHp;
    bool _isDaed = false; // ボスの死亡判定

    [SerializeField] string nextSceneName = "ClearScne";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject obj = GameObject.Find("player");
        GameObject bossObj = GameObject.Find("Boss");
        //　↓スクリプトがついてあるゲームオブジェクトを取得する
        _playerController = obj.GetComponent<PlayerController1>();
        bossScript = bossObj.GetComponent<BOSS>();
        currentBossHp = bossScript.bossHp;
    }

    // Update is called once per frame
    void Update()
    {

        // ボスが死んだとき
        if (currentBossHp <= 0)
        {
            _isDaed = true;
            OnMoveClearScene();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ボスのHPを減らす
        if (collision.gameObject.tag == "WeponAttack")
        {
            // 統合したときに使用（プレイヤーの攻撃力取得のためのやつ）
            _playerAttack = _playerController._attackTotal;
            currentBossHp -= _playerAttack;
            Debug.Log("ボスHP : " + currentBossHp);
        }
    }

    void OnMoveClearScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
