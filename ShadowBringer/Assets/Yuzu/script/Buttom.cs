using UnityEngine;

public class Buttom : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        if(this.gameObject.name =="Yes")
        {
            Debug.Log("hai");
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
