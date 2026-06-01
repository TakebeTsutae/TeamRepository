using UnityEngine;

public class Item : MonoBehaviour
{
    GameObject player;

    //追加↓
    private bool isPlayerNear = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 追加↓
        if(isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
           

            Destroy(gameObject); //アイテム(自分)を消す

            Debug.Log("アイテム拾った!");
        }
    }
    
    
}
