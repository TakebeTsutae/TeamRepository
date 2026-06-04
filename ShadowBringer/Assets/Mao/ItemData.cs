using UnityEngine;

// 追加
public enum ItemType
{
    Weapon,
    Item
}

// Inspectorで編集できるように↓
[System.Serializable]

public class ItemData : MonoBehaviour
{
    public string itemName;
    public ItemType itemType;
    public Sprite icon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
