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
        _bossElementScript = GameObject.Find("BossIdleDamage").GetComponent<BossElementCollider>();

        // ボスのHpを代入
        _maxHp = _bossScript.bossStartHp;

        slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _currentHp = _bossElementScript.currentBossHp;
    }

    public void TakeDamage()
    {
        // HPゲージを更新
        slider.value = (float)_currentHp / _maxHp;
    }
}
