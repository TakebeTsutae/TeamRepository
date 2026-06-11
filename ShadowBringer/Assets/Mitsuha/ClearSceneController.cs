using UnityEngine;
using UnityEngine.SceneManagement;


public class ClearSceneController : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "BossScene";

    void OnClearScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
