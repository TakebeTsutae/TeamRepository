using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public GameObject sword;
    public GameObject staff;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sword.SetActive(true); // 剣を表示
        staff.SetActive(false); // 杖は非表示
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
    }
    
    public void ItemAttack() // 装備を装着
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
