using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{

    public GameObject sword;
    public GameObject staff;

    // UI画像
   // public Image weaponSlot;

    // 武器アイコン
   // public Sprite swordSprite;
   // public Sprite staffSprite;

    // プレイヤーの攻撃力を設定しました by零士
    int playerAttack = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sword.SetActive(true); // 剣を表示
        staff.SetActive(false); // 杖は非表示

        // 最初は剣アイコン
        //weaponSlot.sprite = swordSprite;

        // プレイヤーの攻撃力を3にする by零士
        playerAttack = 3;
    }

    public void SwitchToStaff() //杖を拾ったら杖になる
    {
        sword.SetActive(false); // 剣を消す
        staff.SetActive(true);  // 杖を表示

        // アイコン変更
       // weaponSlot.sprite = staffSprite;

    }

    public void SwitchToSword() // 剣を拾ったら剣になる
    {
        sword.SetActive(true);
        staff.SetActive(false);

        // アイコン変更
        //weaponSlot.sprite = swordSprite;

    }  

    // Update is called once per frame
    void Update()
    {
        
    }
}
