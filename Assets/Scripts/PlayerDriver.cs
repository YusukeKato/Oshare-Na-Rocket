using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriver : MonoBehaviour {

    Rigidbody playerRigidbody;
    GameObject stageManager;

    public GameObject playerParticleSystem;

    // mouse
    Vector3 mousePositionFirst = Vector3.zero;

    // touch
    Vector2 touchPositionFrist = Vector2.zero;

	void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        stageManager = GameObject.Find("StageManager");
        stageManager.GetComponent<DataManager>().DataLoadFunc();
    }
	
	void Update () {
        PlayerDrive();
	}

    void PlayerDrive ()
    {
        if (Application.isEditor) // Unityエディタ
        {
            PlayerMouse();
        }
        else // 実機（スマホ）
        {
            PlayerTouch();
        }
    }

    // PCのマウス操作
    void PlayerMouse ()
    {
        if (Input.GetMouseButtonDown(0)) // クリック時
        {
            mousePositionFirst = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) // クリック中
        {
            PlayerMove(mousePositionFirst, Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0)) // 離した時
        {
            playerRigidbody.velocity = Vector3.zero;
        }
    }

    // スマホのタッチ操作
    void PlayerTouch ()
    {
        if (Input.touchCount > 0) // タッチ数
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // タッチ時
            {
                touchPositionFrist = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // タッチ中
            {
                PlayerMove(touchPositionFrist, touch.position);
            }
            if (touch.phase == TouchPhase.Ended) // 離した時
            {
                playerRigidbody.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    void PlayerMove (Vector3 v1, Vector3 v2)
    {
        // 指をスライドさせた方向に移動
        Vector3 direction = (v2 - v1).normalized;
        playerRigidbody.velocity = direction * stageManager.GetComponent<DataManager>().playerSpeed;

        // 進行方向に向かせる(2Dであることに気をつける)
        float angle = GetAngle(direction);
        transform.eulerAngles = new Vector3(0, 0, angle - 90f);

        // 移動制限を設ける
        transform.position = MoveLimit(transform.position);
    }

    float GetAngle(Vector3 v)
    {
        float rad = Mathf.Atan2(v.y, v.x);
        return rad * Mathf.Rad2Deg;
    }

    Vector3 MoveLimit(Vector3 v)
    {
        float margin_x = stageManager.GetComponent<DataManager>().screenMax.x / 20f;
        float margin_y = stageManager.GetComponent<DataManager>().screenMax.y / 20f;
        v.x = Mathf.Clamp(v.x, stageManager.GetComponent<DataManager>().screenMin.x + margin_x, stageManager.GetComponent<DataManager>().screenMax.x - margin_x);
        v.y = Mathf.Clamp(v.y, stageManager.GetComponent<DataManager>().screenMin.y + margin_y, stageManager.GetComponent<DataManager>().screenMax.y - margin_y);
        return v;
    }

    // 衝突時のイベント
    void OnCollisionEnter(Collision col)
    {
        // enemyの弾以外なら無効
        if (col.gameObject.tag != "EnemyBullet") return;

        // 弾を削除
        Destroy(col.gameObject);

        // playerのHPを1減少
        stageManager.GetComponent<DataManager>().playerHP -= 1;

        if (stageManager.GetComponent<DataManager>().playerHP <= 0)
        {
            // Particle System を起動
            GameObject pps = Instantiate(playerParticleSystem, transform.position, transform.rotation);
            Destroy(pps, 2f);

            // Playerオブジェクトを削除
            Destroy(gameObject);

            // PlayerのGameOverフラグを立てる
            stageManager.GetComponent<StageManager>().isPlayerDead = true;
        }
    }
}
