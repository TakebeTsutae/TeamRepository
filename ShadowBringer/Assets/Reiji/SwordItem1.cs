using UnityEngine;

public class SwordItem1 : MonoBehaviour
{
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            WeaponManager1 ws = other.GetComponent<WeaponManager1>();

            if(ws != null )
            {
                ws.SwitchToSword();
            }

            Debug.Log("剣を拾った!");

            Destroy(gameObject); //アイテム(自分)を消す




        }
    }
    
    
}
