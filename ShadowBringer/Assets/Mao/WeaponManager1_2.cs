
using UnityEngine;
using UnityEngine.UI;
// yuzu
public class WeaponManager1_2 : MonoBehaviour
{
    // UI画像
    public Image weaponSlot;
    public Image weaponSlot1;
    public Image weaponSlot2;
    // マオ追加↓
    public Text hpText;
    //         ↑

    // 武器アイコン
    public Sprite swordSprite;
    public Sprite staffSprite;
    public Sprite _up;
    public Sprite _speed;
    // マオ追加↓
    public Sprite _hp;
    //         ↑

    // プレイヤーの参照を保存する変数（処理の軽量化用）
    private PlayerOtamesi playerScript;

   
    
    void Start()
    {
        // 最初は剣アイコン
        weaponSlot.sprite = swordSprite;
        weaponSlot1.sprite = null;
        weaponSlot2.sprite = null;

        // Start時にプレイヤーのスクリプトを1回だけ取得しておく
        
    }

    public void SwitchToStaff()
    {
        weaponSlot.sprite = staffSprite;
    }

    public void SwitchToSword()
    {
        weaponSlot.sprite = swordSprite;
    }

    void Update()
    {

        GameObject obj = GameObject.Find("player");
        PlayerController playerScript = obj.GetComponent<PlayerController>();
        
        // プレイヤーのスクリプトが取得できていない場合は何もしない
        if (playerScript == null) return;




        if(playerScript.weapon == "Ken")
        {
            weaponSlot.sprite = swordSprite;
        }
        else if (playerScript.weapon == "Tue")
        {
            weaponSlot.sprite = staffSprite;
        }

        // 【1つ目のアクセサリー（weaponSlot1）の判定】
        if (playerScript._accessories[0] == "Up")
        {

            weaponSlot1.sprite = _up;
        }
        else if (playerScript._accessories[0] == "Speed")
        {

            weaponSlot1.sprite = _speed;
        }
        // マオ追加↓
        else if (playerScript._accessories[0] == "HP")
        {
            weaponSlot1.sprite = _hp;
            
        }
        //         ↑
        else // null、またはそれ以外の文字のときは画像を消す
        {
            weaponSlot1.sprite = null;
        }

        // 【2つ目のアクセサリー（weaponSlot2）の判定】
        if (playerScript._accessories[1] == "Up")
        {
            weaponSlot2.sprite = _up;
        }
        else if (playerScript._accessories[1] == "Speed")
        {
            weaponSlot2.sprite = _speed;
        }
        // マオ追加↓
        else if (playerScript._accessories[1] == "HP")
        {
            weaponSlot2.sprite = _hp;
        }
        //         ↑
        else // null、またはそれ以外の文字のときは画像を消す
        {
            weaponSlot2.sprite = null;
        }
        // HP更新
       // UpdateHPUI();
    }
}
