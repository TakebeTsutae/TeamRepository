
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class enemyotamesi : MonoBehaviour
{
    // Header ← ヘッダー　インスペクタを見やすくする
    // enemycollisionのスクリプトがついているオブジェクトをcheckcollisionの中にぶち込む
    [Header("接触判定")] public enemycollision checkcollision;
    [Header("接触判定")] public enemycollision checkcollision1;
    [Header("DropItem")] public GameObject itemSpeed;
                         public GameObject itemUp;
    //[Header("攻撃オブジェクト")] public GameObject attack;

    private float posx; // transformのx方向
    private float posy; // transformのy方向

   

    private int _enemyHp;
    private int _playerAttack;

    private int rand; // ランダム変数

    private bool rightTleftF = false; // 反転するかどうかのフラグ
    private bool isDame = false;
    //[SerializeField] 
    private Animator animator;
    Vector2 pos;

    
    private PlayerController1 playerController1;

    private void Start()
    {
        
        this._enemyHp = 8;
        animator = GetComponent<Animator>();
        StartCoroutine(MoveEnemy());
        animator.SetBool("jump", false);
        animator.SetBool("attack", false);
        //attack.SetActive(false);


        GameObject obj = GameObject.Find("player");    //　↓スクリプトがついてあるゲームオブジェクトを取得する
        playerController1 = obj.GetComponent<PlayerController1>();  // 統合したときに使用（プレイヤーの攻撃力取得のためのやつ）
        _playerAttack = PlayerController1.instance._attackTotal;
    }

    private void Update()
    {
        if (Ec._isTime == false)
        {
            pos = this.transform.position;
        }
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
            transform.localScale = new Vector3(5, 5, 1);
        }
        else
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }
        transform.Translate(posx, posy, 0f);  // Translate←引数で指定したベクトル分だけオブジェクトの位置を移動させることができるらしい

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag == "weponAttack")
        {
            // すでにダメージ中なら処理をスキップ
            if (isDame) return;
            isDame = true; // ダメージ中フラグを立てる

            //プレイヤーの攻撃取得
            _playerAttack = playerController1._attackTotal;
            _enemyHp = _enemyHp - _playerAttack;
            GetComponent<SpriteRenderer>().color = Color.red;
            

            Debug.Log($"{_enemyHp}← 敵の体力");

            if (_enemyHp <= 0)
            {
                
                int rand = Random.Range(0, 2);
                if(rand == 0)
                {
                    Instantiate(itemSpeed, pos, Quaternion.identity);
                    //this.gameObject.SetActive(false);
                }
                else if (rand == 1)
                {
                    Instantiate(itemUp, pos, Quaternion.identity);
                    //this.gameObject.SetActive(false);
                }
                else
                {
                    return;
                }

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
            animator.SetBool("jump", false);
            animator.SetBool("attack", false);
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
            animator.SetBool("jump", true);
            posy = 0.1f;
            yield return new WaitForSeconds(1);
            animator.SetBool("jump", false);
            MoveFlag();
            posy = 0f;
            yield return new WaitForSeconds(3);
            animator.SetBool("attack", true);
            posx = 0f;
            //attack.SetActive(true);
            
            yield return new WaitForSeconds(1);
            animator.SetBool("attack", false);
            //attack.SetActive(false);

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
        GetComponent<SpriteRenderer>().color = Color.white;
        isDame = false;

    }

}