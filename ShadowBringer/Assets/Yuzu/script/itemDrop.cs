using JetBrains.Annotations;
using UnityEngine;

public class itemDrop : MonoBehaviour
{
    public GameObject itemPrefab;
    private int _enemyHp;
    private int _count;

    void Start()
    {
        _count = 0;
    }
   

    void Update()
    {
        GameObject obj = GameObject.FindWithTag("Enemy");
        enemy enemy = obj.GetComponent<enemy>();
        _enemyHp = 8;
        

        if (_enemyHp <= 0 && _count == 0)
        {
            _count++;
            DropItem(); // アイテムを出す
            this.gameObject.SetActive(false);


        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        // プレイヤーに触れたら
        
        
    }

    void DropItem()
    {
        Vector2 offset = new Vector2(10f, 0f); // 横にずらす

        Instantiate(
            itemPrefab,
            transform.position, Quaternion.identity
            );
    }
}
