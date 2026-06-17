
using UnityEngine;

public class ItemPickup1 : MonoBehaviour
{
    private GameObject itemManager;
    public string _item;
    public int _getAccessoriesCount;

    // static変数はすべてのアイテムで共有されるため、今回は不要、または扱い方に注意が必要です
    // static public bool _itemSyutoku; 

    private bool isPlayerNear = false;
    private PlayerController _booltag; // プレイヤーの参照を保存する変数

    void Start()
    {
        // 事前にItemManagerを探しておく
        itemManager = GameObject.Find("ItemManager");
    }

    void Update()
    {
        /*if (_getAccessoriesCount == 3)
        {
            _getAccessoriesCount = 2;
        }
        */
        // Eキーが押され、かつプレイヤーが近くにいるとき
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // _booltag が正しく取得できているか確認
            if (_booltag != null)
            {
                /*if (this.gameObject.CompareTag("Up"))
                {
                    PlayerController._itemhensu += 1;
                    _item = "Up";
                    itemManager.GetComponent<Itemgetcount>().AddCount();
                    Debug.Log("tete" + itemManager.GetComponent<Itemgetcount>().GetCount());

                    // PlayerController側のフラグをtrueにする
                    _booltag._Gettag = true;
                }
                else if (this.gameObject.CompareTag("Speed"))
                {
                    PlayerController._itemhensu += 2;
                    _item = "Speed";
                    itemManager.GetComponent<Itemgetcount>().AddCount();
                    Debug.Log("Player" + PlayerController._itemhensu);
                    // PlayerController側のフラグをtrueにする
                    _booltag._Gettag = true;
                }*/

                // アイテム取得処理（自身を削除）
                itemManager.GetComponent<Itemgetcount>().AddCount();
                _booltag._Gettag = true;
                GetItem();
            }
            
        }
        /*if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (_booltag != null)
            {
                // 1. まずItemManagerから、現在のカウント（何個目のアイテムか）を取得
                Itemgetcount counter = itemManager.GetComponent<Itemgetcount>();
                counter.AddCount(); // カウントを増やす
                int index = counter.GetCount() - 1; // 配列の添字用に -1 する

                if (CompareTag("Up"))
                {
                    _item = "Up";
                    itemManager.GetComponent<Itemgetcount>().AddCount();

                    // 💡プレイヤー側の currentItem に "Up" を代入する
                    _booltag.currentItem = "Up";

                    _booltag._Gettag = true;
                }
                else if (CompareTag("Speed"))
                {
                    _item = "Speed";
                    itemManager.GetComponent<Itemgetcount>().AddCount();

                    // 💡プレイヤー側の currentItem に "Speed" を代入する
                    _booltag.currentItem = "Speed";

                    _booltag._Gettag = true;
                }

                GetItem();
            }
        }*/
    }
    private void OnDestroy()
    {
        // 親オブジェクトのスクリプトを取得して通知
        destro parent = GetComponentInParent<destro>();
        if (parent != null)
        {

            parent.OnChildDestroyed(gameObject);
        }
    }
    void GetItem()
    {
        // 拾ったらこのアイテム自体を画面から消去する
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ぶつかった相手がPlayerの場合
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            // ぶつかったプレイヤーから「PlayerController」スクリプトをここで1回だけ取得する（効率的！）
            _booltag = other.GetComponent<PlayerController>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            _booltag = null; // 離れたら参照をクリア
        }
    }
}
