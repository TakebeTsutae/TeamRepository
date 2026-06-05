using JetBrains.Annotations;
using UnityEngine;

public class itemDrop : MonoBehaviour
{
    public GameObject itemPrefab;
    void Update()
    {
        print(enemy._enemyHp);
        Debug.Log("enemy._enemyHp");
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       

        // プレイヤーに触れたら
        if(other.CompareTag("Player"))
        {

            DropItem(); // アイテムを出す
            Destroy(gameObject); // 敵を消す

            Debug.Log("敵を倒した！");
        }
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
