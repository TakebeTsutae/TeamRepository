using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            transform.Translate(-3, 0, 0);
        }

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            transform.Translate(3, 0, 0);
        }

    }
}
