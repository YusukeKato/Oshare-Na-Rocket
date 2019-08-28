using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour {

    // save key
    string stageNumberKey = "stageNumberKey";

    string savePointKey = "savePointKey";

    string playerLevelKey = "playerLevelKey";
    string playerMaxHPKey = "playerMaxHPKey";
    string playerHPKey = "playerHPKey";
    string playerSpeedKey = "playerSpeedKey";
    string playerShotKey = "playerShotKey";

    string playerPointKey = "playerPoint";

    // ステージ番号を管理
    public int stageNumber;

    // プレイヤー管理
    public int playerLevel;
    public int savePoint;
    public int playerMaxHP;
    public int playerHP;
    public float playerSpeed;
    public int playerShot;

    // プレイヤーのポイント
    public int playerPoint;

    // スクリーンの大きさ
    public Vector3 screenMin;
    public Vector3 screenMax;

    private void Start()
    {
        // 初期化
        stageNumber = 1;
        savePoint = 1;
        playerLevel = 0;
        playerMaxHP = 1;
        playerHP = 1;
        playerSpeed = 2f;
        playerShot = 0;
        playerPoint = 0;

        screenMin = new Vector3(10, 10, 0);
        screenMax = new Vector3(10, 10, 0);

        // スクリーンの大きさを取得
        screenMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10f));
        screenMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10f));
    }

    public void DataSaveFunc()
    {
        PlayerPrefs.SetInt(stageNumberKey, stageNumber);
        PlayerPrefs.SetInt(savePointKey, savePoint);
        PlayerPrefs.SetInt(playerLevelKey, playerLevel);
        PlayerPrefs.SetInt(playerMaxHPKey, playerMaxHP);
        PlayerPrefs.SetInt(playerHPKey, playerHP);
        PlayerPrefs.SetFloat(playerSpeedKey, playerSpeed);
        PlayerPrefs.SetInt(playerShotKey, playerShot);
        PlayerPrefs.SetInt(playerPointKey, playerPoint);
        PlayerPrefs.Save();
    }

    public void DataLoadFunc()
    {
        stageNumber = PlayerPrefs.GetInt(stageNumberKey, 1);
        savePoint = PlayerPrefs.GetInt(savePointKey, 1);
        playerLevel = PlayerPrefs.GetInt(playerLevelKey, 0);
        playerMaxHP = PlayerPrefs.GetInt(playerMaxHPKey, 1);
        playerHP = PlayerPrefs.GetInt(playerHPKey, 1);
        playerSpeed = PlayerPrefs.GetFloat(playerSpeedKey, 2f);
        playerShot = PlayerPrefs.GetInt(playerShotKey, 0);
        playerPoint = PlayerPrefs.GetInt(playerPointKey, 0);
    }
}
