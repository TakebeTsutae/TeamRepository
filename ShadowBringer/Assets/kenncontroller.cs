using UnityEngine;

public class kenncontroller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 相手のTagがEnemyなら
        if (collision.CompareTag("enemy"))
        {
            // 敵を消す
            Destroy(collision.gameObject);
        }
    }
}