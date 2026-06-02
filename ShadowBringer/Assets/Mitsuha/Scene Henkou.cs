using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHenkou : MonoBehaviour
{
    // マップが切り替わるまでの時間（秒）
    [SerializeField] private float timeUntilNextMap = 60.0f;

    // 遷移先のシーン名（Inspectorで設定）
    [SerializeField] private string nextSceneName = "BossScene";

    private float timer = 0f;

    void Update()
    {
        // 経過時間を加算
        timer += Time.deltaTime;

        // 設定した時間を超えたらシーンを切り替える
        if (timer >= timeUntilNextMap)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}