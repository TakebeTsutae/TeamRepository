using UnityEngine;
using System.Collections;
public class hariControl : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 当たった相手のタグがPlayerかどうかを判定
        if ((collision.gameObject.tag == "hari"))
        {
            // Playerであれば自分自身を0秒後に削除
            Destroy(this.gameObject, 0f);
        }
    }
}

