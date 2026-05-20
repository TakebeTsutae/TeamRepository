using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float jumpForce = 300.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            transform.Translate(-1, 0, 0);
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            transform.Translate(1, 0, 0);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }
    }
}
    