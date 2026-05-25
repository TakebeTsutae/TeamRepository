using UnityEngine;

public class Jump : MonoBehaviour
{
    // ジャンプの力を定義
    [SerializeField]
    private float jumpForce = 40f;
    
    private void OnTriggerEnter(Collider other)
    {
        // あたった相手のタグがPlayerだった場合
        if (other.gameObject.CompareTag("Player"))
        {
            // 当たった相手のRigidbodyコンポーネントを取得して上向きの力を加える

            other.gameObject.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

}
