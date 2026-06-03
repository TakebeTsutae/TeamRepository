using UnityEngine;

public class StaffItem1 : MonoBehaviour
{
    GameObject player;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // Playerのスクリプトを所得
            WeaponManager1 ws = other.GetComponent<WeaponManager1>();

            if (ws != null)
            {
                ws.SwitchToStaff();
            }

            Debug.Log("杖を拾った!");

            Destroy(gameObject); //アイテム(自分)を消す

            
        }
    }
    
    
}
