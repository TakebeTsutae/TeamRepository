using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class MainEnemy : MonoBehaviour
{
    public GameObject _attackLeft;
    public GameObject _attackRight;
    public GameObject _footRight;
    public GameObject _footLeft;

    public float moveSpeed = -1f;
    public float _moveSpeed = 1f;
    public float jumpForceX = 2f;
    public float jumpForceY = 5f;

    private int kabe = 0;
    private int _playerAttack;
    private int _countFoot = 0;    // 足がいくつ離れているかのカウント

    public int _enemyHp = 8;

    private Rigidbody2D rb;
    private bool isJumping = false;


    Vector2 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._enemyHp = 8;
        /*if (this.gameObject.tag == "enemy") 
        {
            _enemyHp = 8;
        }*/
        
        kabe = 0;
        _attackLeft.SetActive(false);
        _attackRight.SetActive(false);
        _footLeft.SetActive(true);
        _footRight.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        GameObject obj = GameObject.Find("player");    //　↓スクリプトがついてあるゲームオブジェクトを取得する
       // PlayerOtamesi _playerOtamesi = obj.GetComponent<PlayerOtamesi>();  // 統合したときに使用（プレイヤーの攻撃力取得のためのやつ）
       // _playerAttack = _playerOtamesi._attack;
        StartCoroutine(Action());
    }

    // Update is called once per frame
    void Update()
    {
        print(_enemyHp);
        
    }
    void FixedUpdate()
    {
        
        if (!isJumping)
        {
            velocity = rb.linearVelocity;
            velocity.x = moveSpeed;
            rb.linearVelocity = velocity;
        }

      
        
        


    }
    private IEnumerator Action()
    {
        while (true)
        {
            if (kabe % 2 == 0)
            {
                _moveSpeed = -1f;
            }
            else
            {
                _moveSpeed = 1f;
            }


            moveSpeed = _moveSpeed;
            yield return new WaitForSeconds(3f);
            moveSpeed = 0f;
            if (kabe % 2 == 0)
            {
                _attackLeft.SetActive(true);
            }
            else
            {
                _attackRight.SetActive(true);
            }
            yield return new WaitForSeconds(1f);
            
                _attackLeft.SetActive(false);
            
                _attackRight.SetActive(false);
            
            moveSpeed = _moveSpeed;
            yield return new WaitForSeconds(3f);
            isJumping = true;
            if (kabe % 2 == 0)
            {
                rb.AddForce(new  Vector2(-jumpForceX, jumpForceY),ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(jumpForceX, jumpForceY), ForceMode2D.Impulse);
            }
            moveSpeed = _moveSpeed;


            yield return new WaitForSeconds(1f);

            isJumping = false;
            
            
        }




    }
    private void OnCollisionEnter2D(Collision2D collision)  // playerとの衝突は無視するようにのちに設定すること
    {

        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        collision.GetContacts(contacts);

        foreach (ContactPoint2D contact in contacts)
        {
            if (Mathf.Abs(contact.normal.x) > 0.7f)
            {
                kabe++;
                _moveSpeed *= -1f;
                if (moveSpeed != 0f)
                {
                    moveSpeed = _moveSpeed;
                }
                break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("kenAttack")) 
        {
           // _enemyHp = _enemyHp - player._attack;

            if (_enemyHp <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("tueAttack"))
        {
            // _enemyHp = _enemyHp - player._attack;

            // 

            if (_enemyHp <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("Ground"))
        {
            _countFoot--;

            if (_countFoot <= 0)
            {
                _countFoot = 0;
                if (this.gameObject.tag == "_enemyFoot")
                {

                    //this.gameObject.SetActive(true);
                }
            }
        }
        
            
    }
    private void OnTriggerExit2D(Collider2D other) // Exit -> 離れた瞬間
    {
        _countFoot++;
        if(this.gameObject.tag == "_enemyFoot") 
        {
           // this.gameObject.SetActive(false) ;
        }
        kabe++;
        if (kabe % 2 == 0)
        {
            _moveSpeed = -1f;
        }
        else
        {
            _moveSpeed = 1f;
        }
        if (moveSpeed != 0f)
        {
            moveSpeed = _moveSpeed;
        }
    }
}
