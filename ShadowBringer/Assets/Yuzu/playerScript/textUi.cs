using UnityEngine;
using TMPro;

// 特定文字のY座標を変更するクラス
public class SingleCharacterY : MonoBehaviour
{
    private TextMeshProUGUI tmpText;
    public int charIndex = 0; // 変更したい文字のインデックス
    public float offsetY = 20f; // 持ち上げる高さ

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        MoveCharacterY();
    }

    private void Update()
    {
        MoveCharacterY();
    }

    void MoveCharacterY()
    {
        tmpText.ForceMeshUpdate(); // メッシュ更新
        TMP_TextInfo textInfo = tmpText.textInfo;

        if (charIndex < 0 || charIndex >= textInfo.characterCount) return;
        TMP_CharacterInfo charInfo = textInfo.characterInfo[charIndex];
        if (!charInfo.isVisible) return;

        // 対象文字の頂点にオフセットを加算
        int vIndex = charInfo.vertexIndex;
        int mIndex = charInfo.materialReferenceIndex;
        Vector3[] vertices = textInfo.meshInfo[mIndex].vertices;
        Vector3 offset = new Vector3(0, offsetY, 0);

        for (int i = 0; i < 4; i++) vertices[vIndex + i] += offset;

        tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices); // 更新反映
    }
}