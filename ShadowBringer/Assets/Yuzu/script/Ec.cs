using UnityEngine;

public class Ec : MonoBehaviour
{
    public GameObject Bg;
    public GameObject Text;
    //public GameObject Yes;
    //public GameObject No;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bg.SetActive(false);
        Text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Bg.SetActive(true);
            Text.SetActive(true);
            Time.timeScale = 0;
        }
        

    }
}
