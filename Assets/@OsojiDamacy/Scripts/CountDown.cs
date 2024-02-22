using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDown : MonoBehaviour
{
    public static float CountDownTime;  // カウントダウンタイム
    public TextMeshProUGUI TextCountDown;              // 表示用テキストUI
    private bool _isCountDown;

    // Use this for initialization
    void Start()
    {
        CountDownTime = 60f;  // カウントダウン開始秒数をセット
    }

    public void StartCountDown()
    {
        _isCountDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isCountDown) return;
        // カウントダウンタイムを整形して表示
        TextCountDown.text = String.Format("Time: {0:00.0}", CountDownTime);
        // 経過時刻を引いていく
        CountDownTime -= Time.deltaTime;
        // 0.0秒以下になったらカウントダウンタイムを0.0で固定（止まったように見せる）
        if (CountDownTime <= 0.0F)
        {
            CountDownTime = 0.0F;
        }
    }
}