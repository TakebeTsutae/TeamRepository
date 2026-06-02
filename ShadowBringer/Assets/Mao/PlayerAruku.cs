using UnityEngine;

public class PlayerAruku : MonoBehaviour
{
    public float speed = 5f; // 動く速さ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = 0;

        if(Input.GetKey(KeyCode.D))
        {
            move = 1; // 右
        }
        if(Input.GetKey(KeyCode.A))
        {
            move = -1;
        }

        transform.Translate(move * speed * Time.deltaTime, 0, 0);
    }
}
