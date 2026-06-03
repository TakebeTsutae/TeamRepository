using UnityEngine;

public class ItemAttack : MonoBehaviour
{
    // インスペクターで、プレイヤーと武器のオブジェクトを登録する
 //   public PlayerAttck player;
//    public Equipment weaponOnGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            // ここでplayerAttackのEquipWeaponを「呼び出している」！
            // 引数（かっこの中）に、地面にある武器のデータを渡す
 //           player.EquipWeapon(weaponOnGround);

            // 装備したので、地面のアイテムは消す
            Destroy(gameObject);
        }
    }
}
