using UnityEngine;

public class BossscenePlayerPoss : MonoBehaviour
{
    private GameObject player;

    private Vector2 InitPoss = new(-19, -3);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.Find("player");

        player.transform.position= InitPoss;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
