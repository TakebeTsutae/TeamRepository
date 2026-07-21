using UnityEngine;

public class Ec : MonoBehaviour
{
    public GameObject Bg;
    public GameObject Text;

    public static bool _isTime;
    //public GameObject Yes;
    //public GameObject No;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bg.SetActive(false);
        Text.SetActive(false);
        _isTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {

            Bg.SetActive(true);
            Text.SetActive(true);
            _isTime=true;
            Time.timeScale = 0;
        }
        if(Time.timeScale ==1)
        {
            _isTime = false;
            Bg.SetActive(false);
            Text.SetActive(false);
        }
        

    }
    public void OnClick()
    {
        if (this.gameObject.name == "Yes")
        {
            Debug.Log("hai");
            Bg.SetActive(false);
            Text.SetActive(false);
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else if (this.gameObject.name == "No")
        {
            Time.timeScale = 1;
        }
    }
}
