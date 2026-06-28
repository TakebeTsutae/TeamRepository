using UnityEngine;

public class enemycollision : MonoBehaviour
{
    [HideInInspector] public bool isOn = false;
    [HideInInspector] public bool isOn1 = false;

    private string groundTag = "Ground";
    private string enemyTag = "enemy";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == groundTag || collision.gameObject.tag == enemyTag)
        {
            isOn = true;
        }
        if (this.gameObject.tag == "_enemyFoot")
        {
            if (collision.gameObject.tag == groundTag)
            {
                isOn1 =false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == groundTag || collision.gameObject.tag == enemyTag)
        {
            isOn = false;
        }
        if (this.gameObject.tag == "_enemyFoot")
        {
            Debug.Log("aa");
            if (collision.gameObject.tag == groundTag)
            {
                isOn1 = true;
            }
        }
    }
        
}
