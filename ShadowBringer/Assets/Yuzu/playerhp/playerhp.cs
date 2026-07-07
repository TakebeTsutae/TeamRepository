using UnityEngine;

public class playerhp : MonoBehaviour
{
    private int _playerhp;
    public GameObject hp_Max1;
    public GameObject hp_Max2;
    public GameObject hp_Max3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp_Max1.SetActive(true);
        hp_Max2.SetActive(true);
        hp_Max3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("player");
        PlayerController1 hp = obj.GetComponent<PlayerController1>();
        _playerhp = PlayerController1.instance._playerHp;
        if (_playerhp < 4)
        {
            hp_Max1.SetActive(true);
            hp_Max2.SetActive(true);
            hp_Max3.SetActive(true);
        }
        if (_playerhp < 3)
        {
            hp_Max1.SetActive(true);
            hp_Max2.SetActive(true);
            hp_Max3.SetActive(false);
        }
        if (_playerhp < 2)
        {
            hp_Max1.SetActive(true);
            hp_Max2.SetActive(false);
        }
        if (_playerhp < 1)
        {
            hp_Max1.SetActive(false);
        }

        /*if (Input.GetKeyUp(KeyCode.E)) 
        {
            if(_playerhp > 0) 
            {
                _playerhp--;
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (_playerhp < 3)
            {
                _playerhp++;
            }
            
        }*/
    }
}
