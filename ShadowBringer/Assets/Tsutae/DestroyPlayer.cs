using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
    private GameObject _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _player = GameObject.Find("player");
        Destroy(_player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
