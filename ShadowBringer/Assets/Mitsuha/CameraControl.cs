using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    // プレイヤーのゲームオブジェクトを保持
    public GameObject player;
    // プレイヤーとカメラの位置関係を保持
    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ゲームスタート時でのプレイヤーとカメラの位置関係を記憶
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        
        // プレイヤーの現在位置関係から新しいカメラ位置を作成
        Vector3 vector = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y  -0, -10);
        
        
        // カメラの位置を移動
        transform.position = vector;
    }
}
