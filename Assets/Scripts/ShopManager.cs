using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

    DataManager dataManager;

    public Text playerLevelText;
    public Text playerPointText;

    private void Start()
    {
        dataManager = GetComponent<DataManager>();
        dataManager.DataLoadFunc();

        TextUpdate();
    }

    void TextUpdate()
    {
        dataManager.DataSaveFunc();

        playerLevelText.text = "LEVEL : " + dataManager.playerLevel.ToString();
        playerPointText.text = "POINT : " + dataManager.playerPoint.ToString();
    }

    public void PlayerHPButton()
    {
        if(dataManager.playerPoint >= 10)
        {
            dataManager.playerMaxHP += 1;
            dataManager.playerLevel += 1;
            dataManager.playerPoint -= 10;

            TextUpdate();
        }
    }

    public void PlayerSpeedButton()
    {
        if(dataManager.playerPoint >= 10)
        {
            dataManager.playerSpeed += 0.2f;
            dataManager.playerLevel += 1;
            dataManager.playerPoint -= 10;
            
            TextUpdate();
        }
    }

    public void BulletSpeedButton()
    {
        
    }

    public void BulletTypeButton()
    {

    }

    public void ReturnButton()
    {
        SceneManager.LoadScene("StartScene");
    }
}
