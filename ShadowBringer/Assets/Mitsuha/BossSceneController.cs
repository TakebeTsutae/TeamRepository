using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSceneController : MonoBehaviour
{

    [SerializeField] private string nextSceneName = "BossScene";

    void OnClearScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
