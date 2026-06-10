using System.Collections;
using System.Threading;
using UnityEngine;

public class MainSpawn : MonoBehaviour
{


    private float _enemy1_x,_enemy1_y;
    private float _enemy2_x, _enemy2_y;
    private float _enemy3_x, _enemy3_y;
    private float _enemy4_x, _enemy4_y;

    int _spawnCount = 0;
    float _timer= 0f;
    float _timeInterval = 10f;

    public GameObject EnemyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnCount = 0;
        _enemy1_x = this.transform.position.x - 10f;
        _enemy1_y = this.transform.position.y - 10f;
        _enemy2_x = this.transform.position.x + 10f;
        _enemy2_y = this.transform.position.y + 10f;
        _enemy3_x = this.transform.position.x - 7f;
        _enemy3_y = this.transform.position.y + 7f;
        _enemy4_x = this.transform.position.x + 7f;
        _enemy4_y = this.transform.position.y - 7f;
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        Spown();


        
        if(_spawnCount <= 0)
        {
            _spawnCount++;
            Vector2 pos1 = new Vector2(_enemy1_x, _enemy1_y);
            Vector2 pos2 = new Vector2(_enemy2_x, _enemy2_y);
            Vector2 pos3 = new Vector2(_enemy3_x, _enemy3_y);
            Vector2 pos4 = new Vector2(_enemy4_x, _enemy4_y);

            Instantiate(EnemyPrefab, pos1, Quaternion.identity);
            Instantiate(EnemyPrefab, pos2, Quaternion.identity);
            Instantiate(EnemyPrefab, pos3, Quaternion.identity);
            Instantiate(EnemyPrefab, pos4, Quaternion.identity);
        }
    }
    void Spown() 
    {
        if (_timer > 0f) return;
        _timer += _timeInterval;
       
            _spawnCount++;
            Vector2 pos1 = new Vector2(_enemy1_x, _enemy1_y);
            Vector2 pos2 = new Vector2(_enemy2_x, _enemy2_y);
            Vector2 pos3 = new Vector2(_enemy3_x, _enemy3_y);
            Vector2 pos4 = new Vector2(_enemy4_x, _enemy4_y);

            Instantiate(EnemyPrefab, pos1, Quaternion.identity);
            Instantiate(EnemyPrefab, pos2, Quaternion.identity);
            Instantiate(EnemyPrefab, pos3, Quaternion.identity);
            Instantiate(EnemyPrefab, pos4, Quaternion.identity);
        
    }
    
}
