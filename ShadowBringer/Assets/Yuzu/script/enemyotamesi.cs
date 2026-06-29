
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class enemyotamesi : MonoBehaviour
{
    // Header ← ヘッダー　インスペクタを見やすくする
    // enemycollisionのスクリプトがついているオブジェクトをcheckcollisionの中にぶち込む
    [Header("接触判定")] public enemycollision checkcollision;
    [Header("接触判定")] public enemycollision checkcollision1;

    [Header("攻撃オブジェクト")] public GameObject attack;

    private float posx; // transformのx方向
    private float posy; // transformのy方向

    private bool rightTleftF = false; // 反転するかどうかのフラグ
    Vector2 pos;
    private void Start()
    {
        StartCoroutine(MoveEnemy());
        attack.SetActive(false);
    }

    void FixedUpdate()
    {

        if (checkcollision.isOn )
        {
            
            if(posx != 0f)
            {
                Debug.Log("壁反転");
                rightTleftF = !rightTleftF; // フラグの反転
                MoveFlag();
            }
        }
        if (checkcollision1.isOn1 && posy == 0f)
        {
            
            if (posx != 0f)
            {
                Debug.Log("足反転");
                rightTleftF = !rightTleftF; // フラグの反転
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
        transform.Translate(posx, posy, 0f);  // Translate←引数で指定したベクトル分だけオブジェクトの位置を移動させることができるらしい

    }
    private IEnumerator MoveEnemy()
    {
        while (true)
        {
            
            MoveFlag();
            posy = 0f;
            yield return new WaitForSeconds(3);
            
            
            if (rightTleftF)
            {
                posx = 0.05f;
            }
            else
            {
                posx = -0.05f;
            }
            posy = 0.1f;
            yield return new WaitForSeconds(1);

            MoveFlag();
            posy = 0f;
            yield return new WaitForSeconds(3);

            posx = 0f;
            attack.SetActive(true);
            yield return new WaitForSeconds(1);
            attack.SetActive(false);

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