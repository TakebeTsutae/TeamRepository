using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTitleContllor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnStarButton()
    {
        SceneManager.LoadScene("Main");
    }
}
