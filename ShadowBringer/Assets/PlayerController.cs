using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        float moveInput = 0f;

        // Dキーが押されたとき右に移動
        if(Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }
        // Aキーが押されたとき左に移動
        else if(Input.GetKey(KeyCode.D))
        {
            moveInput = -1f;

        }

        // 左右の移動量を計算して移動
        Vector3 moveDistance = new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;
        transform.Translate(moveDistance);
    }
}
