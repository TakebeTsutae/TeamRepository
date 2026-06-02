using System.Collections;
using UnityEngine;

public class enemy : MonoBehaviour
{
    int a;
    public GameObject Coli;
    private int _enemyHp = 8;
    private Rigidbody2D rb;
    public float speed = 5f;
    private bool tach = false;
    float moveHorizontal = -0.01f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Coli.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        

        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        rb.MovePosition(rb.position + movement);
        StartCoroutine(Time());
        
        /*float moveHorizontal = -0.01f;
        
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);

        rb.MovePosition(rb.position + movement);
        */
    }
    private IEnumerator Time()
    {


        moveHorizontal = -0.01f;
        yield return new WaitForSeconds(3);
        moveHorizontal = 0;
        a++;
        Coli.SetActive(true);
        yield return new WaitForSeconds(1);
        print(a);
        Coli.SetActive(false);
        moveHorizontal = -0.01f;
        /*yield return new WaitForSeconds(3);
        moveHorizontal = 0;*/
    }
    
}
