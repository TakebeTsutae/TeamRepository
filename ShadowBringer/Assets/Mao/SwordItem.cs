using UnityEngine;

public class SwordItem : MonoBehaviour
{
    GameObject player;

    private bool playerInRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // 範囲内にいてEキーおしたら
       if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("剣を拾った!");

            // 剣を消す
            Destroy(gameObject);
        }
    }

    

    // 触れたらアイテムが消える↓
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            WeaponManager ws = other.GetComponent<WeaponManager>();

            if(ws != null )
            {
                ws.SwitchToSword();
            }

            Debug.Log("剣を拾った!");

            Destroy(gameObject); //アイテム(自分)を消す




        }
    }
    */
   
}
