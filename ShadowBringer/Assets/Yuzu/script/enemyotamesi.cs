
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class enemyotamesi : MonoBehaviour
{
    [Header("接触判定")] public enemycollision checkcollision;

    private float posx;

    private bool rightTleftF = false;
    Vector2 pos;
    private void Start()
    {
    }

    void FixedUpdate()
    {
        
        if (checkcollision.isOn)
        {
            rightTleftF = !rightTleftF;
        }
        if (rightTleftF)
        {
            posx = 0.1f;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            posx = -0.1f;
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.Translate(posx, 0f, 0f);  // Translate←引数で指定したベクトル分だけオブジェクトの位置を移動させることができるらしい

    }
    

}