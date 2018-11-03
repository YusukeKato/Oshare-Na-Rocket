using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriver : MonoBehaviour {

    Vector3 PlayerPosition = new Vector3(0, 0, 0);
    Rigidbody PlayerRigidbody;
    float PlayerSpeed = 1.0f;

    // mouse
    Vector3 MousePositionFirst = new Vector3(0, 0, 0);

    // touch
    Vector2 TouchPositionFrist = new Vector2(0, 0);

	void Start () {
        Debug.Log("!!START!!");
        PlayerRigidbody = GetComponent<Rigidbody>();
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
            MousePositionFirst = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) // クリック中
        {
            PlayerMove(MousePositionFirst, Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0)) // 離した時
        {
            PlayerRigidbody.velocity = new Vector3(0, 0, 0);
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
                TouchPositionFrist = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // タッチ中
            {
                PlayerMove(TouchPositionFrist, touch.position);
            }
            if (touch.phase == TouchPhase.Ended) // 離した時
            {
                PlayerRigidbody.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    // スライドさせる方向に移動
    void PlayerMove (Vector3 v1, Vector3 v2)
    {
        Vector3 direction = (v2 - v1).normalized;
        PlayerRigidbody.velocity = direction * PlayerSpeed;
        // 進行方向に向かせる(2Dであることに気をつける)
        float angle = GetAngle(direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    float GetAngle(Vector3 v)
    {
        float rad = Mathf.Atan2(v.y, v.x);
        return rad * Mathf.Rad2Deg;
    }
}
