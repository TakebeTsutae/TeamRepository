
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class enemyotamesi : MonoBehaviour
{
    // Header ← ヘッダー　インスペクタを見やすくする
    // enemycollisionのスクリプトがついているオブジェクトをcheckcollisionの中にぶち込む
    [Header("接触判定")] public enemycollision checkcollision;
    [Header("接触判定")] public enemycollision checkcollision1;

    [Header("攻撃オブジェクト")] public GameObject attack;

    private float posx; // transformのx方向
    private float posy; // transformのy方向

    private int _enemyHp;
    private int _playerAttack;

    private bool rightTleftF = false; // 反転するかどうかのフラグ
    private bool isDame = false;
    Vector2 pos;
    private void Start()
    {
        this._enemyHp = 8;
        StartCoroutine(MoveEnemy());
        attack.SetActive(false);
    }

    private void Update()
    {
        GameObject obj = GameObject.Find("player");    //　↓スクリプトがついてあるゲームオブジェクトを取得する
        PlayerController _playerController = obj.GetComponent<PlayerController>();  // 統合したときに使用（プレイヤーの攻撃力取得のためのやつ）
        _playerAttack = _playerController._attack;
    }
    void FixedUpdate()
    {

        if (checkcollision.isOn )
        {
            
            if(posx != 0f)
            {
                Debug.Log("壁反転");
                rightTleftF = !rightTleftF; // フラグの反転
                MoveFlag();
            }
        }
        if (checkcollision1.isOn1 && posy == 0f)
        {
            
            if (posx != 0f)
            {
                Debug.Log("足反転");
                rightTleftF = !rightTleftF; // フラグの反転
                MoveFlag();
            }
        }
        if (rightTleftF)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.Translate(posx, posy, 0f);  // Translate←引数で指定したベクトル分だけオブジェクトの位置を移動させることができるらしい

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("weponAttack"))
        {
            // すでにダメージ中なら処理をスキップ
            if (isDame) return;
            isDame = true; // ダメージ中フラグを立てる
            _enemyHp = _enemyHp - _playerAttack;
            if (_enemyHp <= 0)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                // 0.2秒後にダメージを受け付ける状態に戻す
                StartCoroutine(ResetDamageFlag(0.2f));
            }
        }
    }
    private IEnumerator MoveEnemy()
    {
        while (true)
        {
            
            MoveFlag();
            posy = 0f;
            yield return new WaitForSeconds(3);
            
            
            if (rightTleftF)
            {
                posx = 0.05f;
            }
            else
            {
                posx = -0.05f;
            }
            posy = 0.1f;
            yield return new WaitForSeconds(1);

            MoveFlag();
            posy = 0f;
            yield return new WaitForSeconds(3);

            posx = 0f;
            attack.SetActive(true);
            yield return new WaitForSeconds(1);
            attack.SetActive(false);

        }
    }

    void MoveFlag()
    {
        if (rightTleftF)
        {
            posx = 0.1f;
        }
        else
        {
            posx = -0.1f;
        }
    }
    private IEnumerator ResetDamageFlag(float delay)

    {

        yield return new WaitForSeconds(delay);

        isDame = false;

    }

}