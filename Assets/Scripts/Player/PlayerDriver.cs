using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriver : MonoBehaviour {

    Rigidbody playerRigidbody;
    float playerSpeed = 1.0f;

    // mouse
    Vector3 mousePositionFirst = new Vector3(0, 0, 0);

    // touch
    Vector2 touchPositionFrist = new Vector2(0, 0);

    // 移動制限
    Vector3 min;
    Vector3 max;

	void Start () {
        Debug.Log("!!START!!");
        playerRigidbody = GetComponent<Rigidbody>();
        // スクリーンの大きさを取得
        min = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10f));
        max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10f));
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
            playerRigidbody.velocity = new Vector3(0, 0, 0);
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
        playerRigidbody.velocity = direction * playerSpeed;
        // 進行方向に向かせる(2Dであることに気をつける)
        float angle = GetAngle(direction);
        transform.eulerAngles = new Vector3(0, 0, angle - 90f);
        // 移動制限を設ける
        Vector3 playerPosition = transform.position;
        float margin_x = max.x / 20f;
        float margin_y = max.y / 20f;
        playerPosition.x = Mathf.Clamp(playerPosition.x, min.x + margin_x, max.x - margin_x);
        playerPosition.y = Mathf.Clamp(playerPosition.y, min.y + margin_y, max.y - margin_y);
        transform.position = playerPosition;
    }

    float GetAngle(Vector3 v)
    {
        float rad = Mathf.Atan2(v.y, v.x);
        return rad * Mathf.Rad2Deg;
    }
}
