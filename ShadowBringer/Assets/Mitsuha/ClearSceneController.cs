using UnityEngine;
using UnityEngine.SceneManagement;


public class ClearSceneController : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "TitleScene";

    public void OnTitleButton()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
