using UnityEngine;

public class enemycollision : MonoBehaviour
{
    [HideInInspector] public bool isOn = false;
    [HideInInspector] public bool isOn1 = false;
    private string groundTag = "Ground";
    private string enemyTag = "enemy";
    private float ignoreExitTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(ignoreExitTimer > 0)
        {
            ignoreExitTimer -= Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (this.gameObject.tag != "_enemyFoot"&& (collision.gameObject.tag == groundTag || collision.gameObject.tag == enemyTag))
        {
            Debug.Log("壁当たり");
            isOn = true;
        }
        if (this.gameObject.tag == "_enemyFoot")
        {
            if (collision.gameObject.tag == groundTag)
            {
                Debug.Log("床当たり");
                isOn1 =false;
                ignoreExitTimer = 0.1f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (this.gameObject.tag != "_enemyFoot" && (collision.gameObject.tag == groundTag || collision.gameObject.tag == enemyTag))
        {
            Debug.Log("壁離れ");
            isOn = false;
        }
        if (ignoreExitTimer > 0) return;
        if (this.gameObject.tag == "_enemyFoot")
        {
            if (collision.gameObject.tag == groundTag)
            {
                Debug.Log("床離れ");
                isOn1 = true;
            }
        }
        
    }
        
}
