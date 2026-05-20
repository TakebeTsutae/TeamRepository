using UnityEngine;

public class kenncontroller : MonoBehaviour
{
   public GameObject kennhanntei;
    GameObject playerTransform;

    private void Start()
    {
        playerTransform = GameObject.Find("player");
    }
    public void PlayerAttack()
    {
        Vector2 playerPos = new Vector2(playerTransform.transform.position.x +1, playerTransform.transform.position.y);
        GameObject kennhannteiPre = Instantiate(kennhanntei, playerPos, Quaternion.identity);
        Destroy(kennhannteiPre, 0.2f);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 相手のTagがEnemyなら
        if (collision.gameObject.tag == "enemy")
        {
            // 敵を消す
            Destroy(collision.gameObject);
        }
    }
}
