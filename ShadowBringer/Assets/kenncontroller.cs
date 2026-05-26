using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class kenncontroller : MonoBehaviour
{
   public GameObject kennhanntei;
    GameObject playerTransform;

    bool isFacingRight;

    private void Start()
    {
        playerTransform = GameObject.Find("player");
    }
    public void PlayerAttack() 
    {
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            isFacingRight = false;
            if (isFacingRight == false)
            {
                Vector2 playerPos = new Vector2(playerTransform.transform.position.x - 1, playerTransform.transform.position.y);
                GameObject kennhannteiPre = Instantiate(kennhanntei, playerPos, Quaternion.identity);
                Destroy(kennhannteiPre, 0.1f); // ←攻撃判定の残る長さ
            }
        }
        
        if (Keyboard.current.dKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            isFacingRight = true;
            if (isFacingRight == true)
            {
                Vector2 playerPos = new Vector2(playerTransform.transform.position.x + 1, playerTransform.transform.position.y);
                GameObject kennhannteiPre = Instantiate(kennhanntei, playerPos, Quaternion.identity);
                Destroy(kennhannteiPre, 0.1f); // ←攻撃判定の残る長さ
            }
        }
        
    }
        
public void OnTriggerEnter2D(Collider2D collision)
    {
        // 相手のTagがEnemyなら
        if (collision.gameObject.tag == "enemy")
        {
            // 敵を消す
            Destroy(collision.gameObject);
            Debug.Log("当たった");

            // ゲームマネージャーからエネミーにダメージを与える関数を呼び出す↓
        }
    }
}
