using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleController : MonoBehaviour
{
   public void OnStarButton()
    {
        SceneManager.LoadScene("Hirayama1");
    }
}
