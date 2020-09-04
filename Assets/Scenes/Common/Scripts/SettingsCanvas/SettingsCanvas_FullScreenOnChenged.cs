﻿using UnityEngine;
using UnityEngine.UI;

public class SettingsCanvas_FullScreenOnChenged : MonoBehaviour
{
    // Toggle
    Toggle fullScreenToggle;

    // Start is called before the first frame update
    void Start()
    {
        // Toggleを取得
        fullScreenToggle = GetComponent<Toggle>();
        // 画面の状態に合わせてToggleの状態を変更
        if (Screen.fullScreen)
        {
            fullScreenToggle.isOn = true;
        }
    }

    // Toggleを変更時の処理
    public void OnChenged()
    {
        // 設定を保存
        int boolToInt;
        if (fullScreenToggle.isOn)
        {
            boolToInt = 1;
        }
        else {
            boolToInt = 0;
        }
        PlayerPrefs.SetInt("isFullScreen", boolToInt);
    }
}