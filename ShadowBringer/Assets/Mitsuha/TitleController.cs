using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleController : MonoBehaviour
{
    [SerializeField] private string nextSceneName; 

    public void OnStarButton()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    
    public void OnExitButtion()
    {
        Application.Quit();
    }
}
