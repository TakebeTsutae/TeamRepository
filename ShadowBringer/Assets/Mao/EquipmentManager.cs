using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Transform weaponRenderer;
    public Transform itemRenderer;

    GameObject currentWeapon;
    GameObject currentItem;
    /*
    // アイテムの種類をみて処理を分ける
    public void Equip(ItemData item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                EquipWwapon(item);
                break;
                
                case ItemType.Item:
                EqpuipItem(item);
                break;
        }
    }

    void EquipWeapon(ItemData item)
    {
        currentWeapon = item.icon;
        weaponRenderer.sprite = currentWeapon;
    }

    void EquipItem(ItemData item)
    {
        currentItem = item.icon;
        itemRenderer.sprite = currentItem;
    }
    */
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
