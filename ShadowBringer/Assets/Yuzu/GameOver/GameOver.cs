using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject click;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        click.SetActive(false);
        // 自身のSpriteRendererを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 初期状態は非表示（透明）にする
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }

    // マウスカーソルがコライダーに入ったとき
    void OnMouseEnter()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true; // 表示
        }
    }

    // マウスカーソルがコライダーから出たとき
    void OnMouseExit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // 非表示
        }
    }
    void OnMouseDown()
    {
        
        StartCoroutine(Click());

    }

    // クリック時の具体的な処理を書くための関数（自由に変更してください）
    
    private IEnumerator Click() 
    {
        Debug.Log(gameObject.name + " がクリックされました！");
        click.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        click.SetActive(false);
        if (this.gameObject.tag == "title")
        {
            SceneManager.LoadScene("1_MainTitleScene");   //タイトルのシーン名記入
        }
        else if (this.gameObject.tag == "retry")
        {
            SceneManager.LoadScene("2_main");   //リトライのシーン名記入

        }
    }


}

