using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDriver : MonoBehaviour {

    public float enemySpeed = 1.0f;
    Rigidbody enemyRigidbody;

    // スクリーンの大きさ
    Vector3 min;
    Vector3 max;

    // 進行方向
    Vector3 enemyDirection;

    void Start () {
        enemyRigidbody = GetComponent<Rigidbody>();
        // スクリーンの大きさを取得
        min = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10f));
        max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10f));
        // ランダムに座標を決める
        Vector3 enemyPosition = new Vector3(0, 0, 0);
        enemyPosition.x = Random.Range(min.x, max.x);
        enemyPosition.y = Random.Range(min.y, max.y);
        if(enemyPosition.x > -2f && enemyPosition.x < 2f && enemyPosition.y > -2f && enemyPosition.y < 2f)
        {
            enemyPosition.x += Random.Range(-5f, 5f);
            enemyPosition.y += Random.Range(-5f, 5f);
        }
        transform.position = enemyPosition;
        // ランダムに進行方向を定める
        Vector3 angle = new Vector3(0, 0, Random.Range(0, 360));
        enemyDirection = Quaternion.Euler(angle) * Vector3.up;
        enemyRigidbody.velocity = enemyDirection * enemySpeed;
	}
	
	void Update () {
        WallCollision();
        EnemyLookAt();
    }

    void EnemyLookAt()
    {
        // 進行方向に向かせる(2Dであることに気をつける)
        float angle = GetAngle(enemyDirection);
        transform.eulerAngles = new Vector3(0, 0, angle - 90f);
    }

    float GetAngle(Vector3 v)
    {
        float rad = Mathf.Atan2(v.y, v.x);
        return rad * Mathf.Rad2Deg;
    }

    void WallCollision()
    {
        if (transform.position.x < min.x)
        {
            Rebound(1);
        }
        if (transform.position.x > max.x)
        {
            Rebound(2);
        }
        if (transform.position.y < min.y)
        {
            Rebound(3);
        }
        if (transform.position.y > max.y)
        {
            Rebound(4);
        }
    }

    // enemyが壁に当たって跳ね返る
    void Rebound(int flag)
    {
        // 壁に当たったら位置を戻す
        Vector3 pos = transform.position;
        if (flag == 1) pos.x = min.x;
        else if (flag == 2) pos.x = max.x;
        else if (flag == 3) pos.y = min.y;
        else if (flag == 4) pos.y = max.y;
        transform.position = pos;
        // XまたはY方向の速度を逆にする
        if(flag == 1 || flag == 2) enemyDirection.x = -enemyDirection.x;
        else if(flag == 3 || flag == 4) enemyDirection.y = -enemyDirection.y;
        // 速度を更新
        enemyRigidbody.velocity = enemyDirection * enemySpeed;
    }
}
