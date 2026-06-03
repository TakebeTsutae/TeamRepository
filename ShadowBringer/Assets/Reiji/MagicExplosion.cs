using UnityEngine;

public class MagicExplosion : MonoBehaviour
{
    public float destroyTime = 0.1f; // 爆風が消えるまでの時間（0.1秒）

    void Start()
    {
        // 【法律5】生まれた瞬間、0.1秒後に自分自身（爆風）を消滅させる
        // 剣のときと同じように、ここに敵と当たったときのダメージ処理（OnTriggerEnter2Dなど）を繋げられるよ！
        Destroy(gameObject, destroyTime);
    }
}