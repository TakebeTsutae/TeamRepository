using UnityEngine;

public class PlayerAttck : MonoBehaviour
{
    public int attackPower = 10; // 初期攻撃力

    public void IncreaseAttack(int amount)
    {
        attackPower += amount;
        Debug.Log("攻撃力:" + attackPower);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
