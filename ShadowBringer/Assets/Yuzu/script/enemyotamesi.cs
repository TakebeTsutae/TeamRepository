
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class enemyotamesi : MonoBehaviour
{
    Vector2 pos;
    private void Start()
    {
    }

    void FixedUpdate()
    {
        Rigidbody2D enemyRb = this.GetComponent<Rigidbody2D>(); // enemy‚Ìrigidbodyæ“¾
        Vector2 enemyForce = new Vector2(5.0f, 0.0f); // —Í‚Ìİ’è
        enemyRb.AddForce(enemyForce);   // w’è‚µ‚½—Í‚ğ—^‚¦‚é©w’è‚³‚ê‚½—Í
    }
    
    
}