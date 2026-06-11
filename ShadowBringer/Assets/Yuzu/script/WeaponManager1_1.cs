using UnityEngine;
using UnityEngine.UI;

public class WeaponManager1_1 : MonoBehaviour
{
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
    private PlayerOtamesi playerScript;

    void Start()
    {
        // 最初は剣アイコン
        weaponSlot.sprite = swordSprite;
        weaponSlot1.sprite = _up;
        weaponSlot2.sprite = _speed;

        // Start時にプレイヤーのスクリプトを1回だけ取得しておく
        GameObject obj = GameObject.Find("Player");
        if (obj != null)
        {
            playerScript = obj.GetComponent<PlayerOtamesi>();
        }
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
        // プレイヤーのスクリプトが取得できていない場合は何もしない
        if (playerScript == null) return;

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
