using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {

    DataManager dataManager;

    public void GoToGameButton()
    {
        // データ初期化
        dataManager = GetComponent<DataManager>();
        dataManager.DataLoadFunc();
        dataManager.stageNumber = dataManager.savePoint;
        dataManager.playerHP = dataManager.playerMaxHP;
        dataManager.DataSaveFunc();

        if (dataManager.stageNumber > 10) dataManager.stageNumber = 10;

        // ゲームスタート
        string sceneName = "Stage" + GetComponent<DataManager>().stageNumber.ToString().PadLeft(4, '0');
        SceneManager.LoadScene(sceneName);
    }

    public void GoToShopButton()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
