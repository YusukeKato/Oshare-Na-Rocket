using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDriver : MonoBehaviour {

    public int enemyHP = 1;
    public float enemySpeed = 1.0f;
    public float enemySize = 1f;
    public int enemyPoint = 1;

    public GameObject stageManager;

    Rigidbody enemyRigidbody;

    public GameObject enemyParticleSystem;

    // 進行方向
    Vector3 enemyDirection;
    Vector3 enemyPosition_before = Vector3.zero;
    bool isEnemyMove = false;

    void Start () {
        enemyRigidbody = GetComponent<Rigidbody>();
        stageManager = GameObject.Find("StageManager");

        // ランダムに進行方向を定める
        Vector3 angle = new Vector3(0, 0, Random.Range(0, 360));
        enemyDirection = Quaternion.Euler(angle) * Vector3.up;
        enemyRigidbody.velocity = enemyDirection * enemySpeed;
	}
	
	void Update () {
        WallCollision();
        SetDirection();
    }

    void SetDirection()
    {
        if(isEnemyMove == false)
        {
            isEnemyMove = true;
            enemyPosition_before = transform.position;
        }
        if ((transform.position - enemyPosition_before).magnitude >= 0.005f)
        {
            isEnemyMove = false;
            EnemyLookAt((transform.position - enemyPosition_before).normalized);
        }
    }

    // 進行方向に向かせる(2Dであることに気をつける)
    void EnemyLookAt(Vector3 d)
    {
        float angle = GetAngle(d);
        transform.eulerAngles = new Vector3(0, 0, angle - 90f);
    }

    float GetAngle(Vector3 v)
    {
        float rad = Mathf.Atan2(v.y, v.x);
        return rad * Mathf.Rad2Deg;
    }

    // 壁との衝突を判定
    void WallCollision()
    {
        if (transform.position.x < stageManager.GetComponent<DataManager>().screenMin.x)
        {
            Rebound(1);
        }
        if (transform.position.x > stageManager.GetComponent<DataManager>().screenMax.x)
        {
            Rebound(2);
        }
        if (transform.position.y < stageManager.GetComponent<DataManager>().screenMin.y)
        {
            Rebound(3);
        }
        if (transform.position.y > stageManager.GetComponent<DataManager>().screenMax.y)
        {
            Rebound(4);
        }
    }

    // enemyが壁に当たって跳ね返る
    void Rebound(int flag)
    {
        // 壁に当たったら位置を戻す
        Vector3 pos = transform.position;
        if (flag == 1) pos.x = stageManager.GetComponent<DataManager>().screenMin.x;
        else if (flag == 2) pos.x = stageManager.GetComponent<DataManager>().screenMax.x;
        else if (flag == 3) pos.y = stageManager.GetComponent<DataManager>().screenMin.y;
        else if (flag == 4) pos.y = stageManager.GetComponent<DataManager>().screenMax.y;
        transform.position = pos;
        // XまたはY方向の速度を逆にする
        if(flag == 1 || flag == 2) enemyDirection.x = -enemyDirection.x;
        else if(flag == 3 || flag == 4) enemyDirection.y = -enemyDirection.y;
        // 速度を更新
        enemyRigidbody.velocity = enemyDirection * enemySpeed;
    }

    // 衝突時のイベント
    void OnCollisionEnter(Collision col)
    {
        // playerの弾以外だったら無効
        if (col.gameObject.tag != "PlayerBullet") return;

        // 衝突した弾を削除
        Destroy(col.gameObject);

        // enemyのHPを1減らす
        enemyHP -= 1;

        // enemyのHPが0になったら削除
        if (enemyHP <= 0)
        {
            // Particle System を起動
            GameObject eps = Instantiate(enemyParticleSystem, transform.position, transform.rotation);
            Destroy(eps, 2f);

            // Enemyオブジェクトを削除
            Destroy(gameObject);

            // Enemyを倒した分のポイントを加算
            stageManager.GetComponent<DataManager>().playerPoint += enemyPoint;
            stageManager.GetComponent<DataManager>().DataSaveFunc();

            // StageManagerのフラグを立てる
            stageManager.GetComponent<StageManager>().isEnemyDead = true;
        }
    }
}
