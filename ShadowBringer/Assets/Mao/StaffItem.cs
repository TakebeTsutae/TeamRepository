using UnityEngine;

public class StaffItem : MonoBehaviour
{
    GameObject player;

   private bool playerInRange = false;
   private WeaponManager ws;

    // Eキーで拾う↓
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if(ws != null)
            {
                ws.SwitchToStaff();
            }

            Debug.Log("杖を拾った");

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInRange = true;
        ws = other.GetComponent<WeaponManager>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
        ws = null;
    }

    // 触れたらアイテムが消える↓
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // Playerのスクリプトを所得
            WeaponManager ws = other.GetComponent<WeaponManager>();

            if (ws != null)
            {
                ws.SwitchToStaff();
            }

            Debug.Log("杖を拾った!");

            Destroy(gameObject); //アイテム(自分)を消す

            
        }
    }
    void GetItem()
    {
        Debug.Log("アイテムをゲットした!");
        Destroy(gameObject); // アイテム消す
    }
    */



}
