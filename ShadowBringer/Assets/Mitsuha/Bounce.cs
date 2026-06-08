using Unity.VisualScripting;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    //ジャンプの高さ調整
    private float bounce = 20.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bounce"))
            _rb.AddForce(new Vector2(0, bounce), ForceMode2D.Impulse);
    }
   
}
