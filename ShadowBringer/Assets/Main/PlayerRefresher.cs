using UnityEngine;
using UnityEngine.UI;

public class PlayerRefresher : MonoBehaviour
{
    // playerのWeaponManagerを取得
    WeaponManager1 m_weaponManager1;
    GameObject _player;

    // weaponSlot,weaponSlot1,weaponSlot2に入れるゲームオブジェクト
    GameObject weaponSlot;
    GameObject weaponSlot1;
    GameObject weaponSlot2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.Find("player");
        m_weaponManager1 = _player.GetComponent<WeaponManager1>();

        weaponSlot = GameObject.Find("WeaponAicon");
        weaponSlot1 = GameObject.Find("WeaponAicon");
        weaponSlot2 = GameObject.Find("WeaponAicon");

        m_weaponManager1.weaponSlot = weaponSlot.GetComponent<Image>();
        m_weaponManager1.weaponSlot1 = weaponSlot1.GetComponent<Image>();
        m_weaponManager1.weaponSlot2 = weaponSlot2.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
