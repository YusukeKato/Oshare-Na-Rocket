using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

    // 現在のステージ番号を表示
    public GameObject stageNumberCanvas;
    GameObject playerPointText;

    // 色々なflag
    public bool isEnemyDead;
    public bool isPlayerDead;

    // playerのプレハブ
    public GameObject Player;

    // GameOverを示すオブジェクト
    public GameObject GameOverPrefab;

    void Start () {

        // 初期化
        isEnemyDead = false;
        isPlayerDead = false;

        // Canvasを生成
        GameObject sc = Instantiate(stageNumberCanvas);

        GetComponent<DataManager>().DataLoadFunc();

        // テキストを初期化
        GameObject stageNumberText = GameObject.Find("StageNumberText");
        stageNumberText.GetComponent<Text>().text = GetComponent<DataManager>().stageNumber.ToString();
        playerPointText = GameObject.Find("PlayerPointText");
        playerPointText.GetComponent<Text>().text = "POINT:" + GetComponent<DataManager>().playerPoint.ToString();

        // Playerを生成
        PlayerFunc();
	}

    // playerを生成
    void PlayerFunc()
    {
        GameObject p = Instantiate(Player, Vector3.zero, Quaternion.identity);
    }

    void Update () {
        // Enemyが全滅していたらクリア
        if(isEnemyDead == true)
        {
            playerPointText.GetComponent<Text>().text = "POINT:" + GetComponent<DataManager>().playerPoint.ToString();

            isEnemyDead = false;
            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemyArray.Length == 0)
            {
                GetComponent<DataManager>().stageNumber += 1;

                // セーブポイント設定
                savePointFunc();

                // データをセーブ
                GetComponent<DataManager>().DataSaveFunc();

                // n秒間待ってからシーン遷移
                string sceneName = "Stage" + GetComponent<DataManager>().stageNumber.ToString().PadLeft(4, '0');
                if (GetComponent<DataManager>().stageNumber > 10) sceneName = "StartScene";
                StartCoroutine(LoadSceneFunc(sceneName, 3));
            }
        }
        if(isPlayerDead == true)
        {
            isPlayerDead = false;

            // セーブポイント設定
            savePointFunc();

            // データをセーブ
            GetComponent<DataManager>().DataSaveFunc();

            // n秒間待ってからゲームオーバー表示
            StartCoroutine(LoadGameOverFunc(2));

            // n秒間待ってからシーン遷移
            StartCoroutine(LoadSceneFunc("StartScene", 5));
        }
    }

    // セーブ機能

    void savePointFunc()
    {
        int s = GetComponent<DataManager>().stageNumber;
        if(s % 5 >= 1)
        {
            int ss = s / 5;
            ss = (ss * 5) + 1;
            GetComponent<DataManager>().savePoint = ss;
        }
    }

    IEnumerator LoadGameOverFunc(int n)
    {
        yield return new WaitForSeconds(n);

        GameObject go = Instantiate(GameOverPrefab);
        go.transform.Rotate(0, -30f, 0);
    }

    IEnumerator LoadSceneFunc (string s, int n)
    {
        yield return new WaitForSeconds(n);
        
        SceneManager.LoadScene(s);
    }
}
