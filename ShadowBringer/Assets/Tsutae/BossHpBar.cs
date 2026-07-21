using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public Slider slider;   // sliderコンポーネント

    BOSS _bossScript;   // 最大HP取得用のスクリプト
    BossElementCollider _bossElementScript; // 現在のHP取得用のスクリプト
    private int _maxHp;  // Bossの最大HPを設定
    private int _currentHp; // 現在のHP 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // スクリプトの取得
        _bossScript = this.GetComponent<BOSS>();
        

        // ボスのHpを代入
        _maxHp = _bossScript.bossStartHp;

        slider.maxValue = _maxHp;
        slider.value = _currentHp;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void TakeDamage()
    {
        Debug.LogError("ダメージを受けました");   // 呼ばれてる
        _bossElementScript = GameObject.FindWithTag("BossElement").GetComponent<BossElementCollider>();
        _currentHp = _bossElementScript.currentBossHp;
        // HPゲージを更新
        slider.value = (float)_currentHp / _maxHp;
    }
}
