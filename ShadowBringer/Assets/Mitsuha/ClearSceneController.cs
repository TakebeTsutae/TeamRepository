using UnityEngine;
using UnityEngine.SceneManagement;


public class ClearSceneController : MonoBehaviour
{

    // 遷移先のシーン名（Inspectorで設定）
    [SerializeField] private string nextSceneName = "TitleScene";

    private float timer = 0f;

    bool _moveBossScene = true;
    public void OnTitleButton()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
