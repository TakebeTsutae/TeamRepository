using TMPro;
using UnityEngine;


public class TextController : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    public float speed = 1.5f; // 点滅の速さ

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // 時間の経過に合わせて透明度を0〜1の間でサイン波（波の動き）にする
        float alpha = Mathf.Sin(Time.time * speed) * 0.3f + 0.3f;

        // 色情報を取得し、アルファ値のみを更新
        Color color = textComponent.color;
        color.a = alpha + 0.1f;
        textComponent.color = color;
    }
}

