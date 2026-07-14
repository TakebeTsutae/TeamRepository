using UnityEngine;

public class ItemPos : MonoBehaviour
{
    private GameObject playerObj;
    private Vector2 playerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObj = GameObject.Find("enemySuraimu");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = playerObj.transform.position;
        this.transform.position = new Vector2 (playerPos.x, playerPos.y+0.3f);
    }
}
