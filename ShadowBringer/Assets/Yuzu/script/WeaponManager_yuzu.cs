using UnityEngine;
using UnityEngine.UI;

public class WeaponManager_yuzu : MonoBehaviour
{

    /*public GameObject sword;
    public GameObject staff;

    int playerAttack = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sword.SetActive(true); // 剣を表示
        staff.SetActive(false); // 杖は非表示
        playerAttack = 3;
    }

    public void SwitchToStaff() //杖を拾ったら杖になる
    {
        sword.SetActive(false); // 剣を消す
        staff.SetActive(true);  // 杖を表示
    }

    public void SwitchToSword() // 剣を拾ったら剣になる
    {
        sword.SetActive(true);
        staff.SetActive(false);
    }*/

    // UI画像
    public Image weaponSlot;
    public Image weaponSlot1;
    public Image weaponSlot2;

    // 武器アイコン
    public Sprite swordSprite;
    public Sprite staffSprite;
    public Sprite _up;
    public Sprite _speed;

    // プレイヤーの参照を保存する変数（処理の軽量化用）
    private PlayerController_yuzu playerScript;

    void Start()
    {
        
        // 最初は剣アイコン
        weaponSlot.sprite = swordSprite;
        weaponSlot1.sprite = null;
        weaponSlot2.sprite = null;
        
        // Start時にプレイヤーのスクリプトを1回だけ取得しておく
        GameObject obj = GameObject.Find("player");
        if (obj != null)
        {
            PlayerController_yuzu playerScript = obj.GetComponent<PlayerController_yuzu>();
        }
    }
    
    public void SwitchToStaff()
    {
      //  weaponSlot.sprite = staffSprite;
    }
    
    public void SwitchToSword()
    {
      //  weaponSlot.sprite = swordSprite;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーのスクリプトが取得できていない場合は何もしない
        if (playerScript == null) return;
        Debug.Log(playerScript._accessories[0]);
        // 【1つ目のアクセサリー（weaponSlot1）の判定】
        if (playerScript._accessories[0] == "Up")
        {
            weaponSlot1.sprite = _up;
        }
        else if (playerScript._accessories[0] == "Speed")
        {
            weaponSlot1.sprite = _speed;
        }
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
        else // null、またはそれ以外の文字のときは画像を消す
        {
            weaponSlot2.sprite = null;
        }
    }
}
