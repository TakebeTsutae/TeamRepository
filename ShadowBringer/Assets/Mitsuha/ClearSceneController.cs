using UnityEngine;
using UnityEngine.SceneManagement;


public class ClearSceneController : MonoBehaviour
{

    // 遷移先のシーン名（Inspectorで設定）
    [SerializeField] private string nextSceneName; 


    bool _moveBossScene = true;
    public void OnTitleButton()
    {
        SceneManager.LoadScene(nextSceneName);
    }

   

    public void OnRetryButton()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
