using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class kenncontroller : MonoBehaviour
{
    public GameObject kennhanntei;
    GameObject playerTransform;

    bool isFacingRight;

    public GameObject clone;

    GameObject wepon;
    private void Start()
    {

        playerTransform = GameObject.Find("player");

        //wepon=GameObject
    }
    public void PlayerAttack()
    {
        GameObject obj = GameObject.Find("player");
        WeaponManager1_1 playerScript = obj.GetComponent<WeaponManager1_1>();
        tuecontroller tue = obj.GetComponent<tuecontroller>();
        Vector2 playerPos;

        // 右向きなら
        if (isFacingRight)
        {
            playerPos = new Vector2(
                playerTransform.transform.position.x + 1,
                playerTransform.transform.position.y
            );
        }

        // 左向きなら
        else
        {
            playerPos = new Vector2(
                playerTransform.transform.position.x - 1,
                playerTransform.transform.position.y
            );
        }
        // 剣がtrueのときに剣の判定を出す
        // 杖も同様
        if(playerScript.weaponSlot.sprite == playerScript.swordSprite)
        {
            GameObject kennhannteiPre = Instantiate(kennhanntei, playerPos, Quaternion.identity);
        }
        else if(playerScript.weaponSlot.sprite == playerScript.staffSprite){
            tue.ShootMagic();
            Debug.Log("wwww");
        }



        clone = GameObject.Find("kencollider(Clone)");
        Destroy(clone, 0.1f);
    }

    void Update()
    {
        
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            isFacingRight = false;
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            isFacingRight= true;
        }
    }
       
}
