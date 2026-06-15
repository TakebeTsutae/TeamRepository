using UnityEngine;

public class Itemgetcount : MonoBehaviour
{
    public int _itemCount;
    
    public void Start()
    {
        _itemCount = 0;
    }
    public void Update()
    {
    }

    public void AddCount()
    {
        if(_itemCount == 3)
        {
            _itemCount = 2;
        }
        _itemCount++;
        
    }

    public int GetCount()
    {
        return _itemCount;
    }

}
