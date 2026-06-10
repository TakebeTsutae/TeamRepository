using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleController : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Hirayama1";

    public void OnStarButton()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
