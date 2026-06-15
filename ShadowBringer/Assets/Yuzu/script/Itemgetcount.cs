using UnityEngine;

public class Itemgetcount : MonoBehaviour
{
    private int _itemCount;
    
    public void Start()
    {
        _itemCount = 0;
    }

    public void AddCount()
    {
        _itemCount++;
    }

    public int GetCount()
    {
        return _itemCount;
    }

}
