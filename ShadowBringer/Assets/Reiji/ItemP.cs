using UnityEngine;

public class ItemP : MonoBehaviour
{
    GameObject player;
    public string _item;
    public int _getAccessoriesCount;

    //追加↓
    private bool isPlayerNear = false;
    // インスペクターで、プレイヤーと武器のオブジェクトを登録する
//  public PlayerAttack player;
//  public Equipment weapoonOnGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _getAccessoriesCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_getAccessoriesCount == 3) 
        {
            _getAccessoriesCount = 2;
        }
        // アクセサリーの情報
        GameObject obj = GameObject.Find("player");    //　↓スクリプトがついてあるゲームオブジェクトを取得する
        PlayerController _booltag = obj.GetComponent<PlayerController>();  // タグ取得しているスクリプトを取得する
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            _item = "Up";   // 文字列の変数_itemに文字列"Up"を代入する
            _getAccessoriesCount++;
            _booltag._Gettag = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            _item = "Speed";    // 文字列の変数_itemに文字列"Speed"を代入する
            _getAccessoriesCount++;
            _booltag._Gettag = true;
        }

        // 追加↓
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // ここでPlayerAttack のEnquipWaeponを「呼び出している!」
            // 引数(かっこの中)に、地面にある武器のデータを渡す
            //          player.Equals(weaponOnGroud);
            
            GetItem();
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
           

            Destroy(gameObject); //アイテム(自分)を消す

            Debug.Log("アイテム拾った!");
        }
    }
    */

    // 追加↓
    void GetItem()
    {
        Debug.Log("アイテムをゲットした!");
        Destroy(gameObject); // アイテム消す
    }
   
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

}
