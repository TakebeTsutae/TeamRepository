
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class enemyotamesi : MonoBehaviour
{
    // Header ← ヘッダー　インスペクタを見やすくする
    // enemycollisionのスクリプトがついているオブジェクトをcheckcollisionの中にぶち込む
    [Header("接触判定")] public enemycollision checkcollision;

    private float posx; // transformのx方向

    private bool rightTleftF = false; // 反転するかどうかのフラグ
    Vector2 pos;
    private void Start()
    {
        StartCoroutine(MoveEnemy());
    }

    void FixedUpdate()
    {
        
        if (checkcollision.isOn)
        {
            rightTleftF = !rightTleftF; // フラグの反転
            if(posx != 0f)
            {
                MoveFlag();
            }
        }
        if (rightTleftF)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.Translate(posx, 0f, 0f);  // Translate←引数で指定したベクトル分だけオブジェクトの位置を移動させることができるらしい

    }
    private IEnumerator MoveEnemy()
    {
        while (true)
        {
            
            MoveFlag();

            yield return new WaitForSeconds(3);
            posx = 0f;
        }
    }

    void MoveFlag()
    {
        if (rightTleftF)
        {
            posx = 0.1f;
        }
        else
        {
            posx = -0.1f;
        }
    }
    

}