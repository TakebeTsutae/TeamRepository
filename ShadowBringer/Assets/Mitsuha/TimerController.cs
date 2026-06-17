using UnityEngine;
using TMPro; // TextMeshProを使用する場合。通常のTextの場合は using UnityEngine.UI; と記述

public class TimerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // UIのテキスト
    [SerializeField] private float timeLimit = 60.0f;   // 制限時間（秒）

    void Update()
    {
        // 制限時間を減らす
        timeLimit -= Time.deltaTime;

        // 0以下にならないようにする
        if (timeLimit < 0)
        {
            timeLimit = 0;
            // TODO: ここに制限時間が0になった時の処理（ゲームオーバーなど）を書く
        }

        // 分と秒を計算
        int minutes = Mathf.FloorToInt(timeLimit / 60);
        int seconds = Mathf.FloorToInt(timeLimit % 60);

        // 文字列としてUIに表示 (例: 01:23)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private TextMeshProUGUI textComponent;
    public float speed = 1.5f; // 点滅の速さ

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    
       
    }