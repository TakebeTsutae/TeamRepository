using UnityEngine;
// まお
public class Item : MonoBehaviour
{
    GameObject player;
    GameObject itemManager;
    public string _item;
    public int _getAccessoriesCount;

    public bool _itemSyutoku;



    //追加↓
    private bool isPlayerNear = false;

    // インスペクターで、プレイヤーと武器のオブジェクトを登録する
//  public PlayerAttack player;
//  public Equipment weapoonOnGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemManager = GameObject.Find("ItemManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (_getAccessoriesCount == 3)
        {
            _getAccessoriesCount = 2;
        }
        GameObject obj = GameObject.Find("player");    //　↓スクリプトがついてあるゲームオブジェクトを取得する
        PlayerController _booltag = obj.GetComponent<PlayerController>();  // タグ取得しているスクリプトを取得する
        // 追加↓
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (this.gameObject.tag == "Up")
            {
                itemManager.GetComponent<Itemgetcount>().AddCount();

                _booltag._accessories[
                    itemManager.GetComponent<Itemgetcount>().GetCount() - 1
                ] = "Up";
                
                
                _booltag._Gettag = true;

            }
            if (this.gameObject.tag == "Speed")
            {
                itemManager.GetComponent<Itemgetcount>().AddCount();

                _booltag._accessories[itemManager.GetComponent<Itemgetcount>().GetCount() - 1] = "Speed";

                _booltag._Gettag = true;

            }

            /*if (this.gameObject.tag =="Up")
            {
                _item = "Up";
                //_getAccessoriesCount++;
                itemManager.GetComponent<Itemgetcount>().AddCount();
                Debug.Log("tete"+itemManager.GetComponent<Itemgetcount>().GetCount());
                _booltag._Gettag = true;
            }
            else if (this.gameObject.tag == "Speed")
            {
                _item = "Speed";
                //_getAccessoriesCount++;
                itemManager.GetComponent<Itemgetcount>().AddCount();
                _booltag._Gettag = true;
            }*/
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
