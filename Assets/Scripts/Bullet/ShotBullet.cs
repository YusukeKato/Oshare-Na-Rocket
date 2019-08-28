using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet : MonoBehaviour {

    // 撃つ間隔
    public float waitTime;

    // 弾の速度
    public float bulletSpeed;

    // 弾の発射角度
    public float shotAngle;

    // 弾の発射方向を回転
    public int isShotType;

    // 弾の回転角
    public float AngleParam;

    public GameObject shotBullet;
    public GameObject bullet;

    public GameObject player;

    IEnumerator Start()
    {
        // とりあえず2秒待つ
        yield return new WaitForSeconds(2f);

        player = GameObject.Find("Player(Clone)");

        while (true)
        {
            ShotFunc();
            
            // waitTime分待つ
            yield return new WaitForSeconds(waitTime);
        }
    }

    void ShotFunc()
    {
        // shotBulletを生成（弾の大元）
        GameObject sb = Instantiate(shotBullet, transform.position, transform.rotation);
        float angle = ShotAngleFunc();
        sb.transform.Rotate(0, 0, angle);

        // 子としてBulletを生成
        GameObject b = Instantiate(bullet, sb.transform);
        b.transform.position = sb.transform.position;
        b.transform.rotation = sb.transform.rotation;
        //b.GetComponent<SizeManager>().size = GetComponent<SizeManager>().size;

        // 速度を決定
        b.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
    }

    float ShotAngleFunc()
    {
        if(isShotType == 1) // 回転しながら
        {
            float m = Time.time * AngleParam;
            while(m > 360f)
            {
                m -= 360f;
            }
            return m + shotAngle;
        }
        if(isShotType == 2) // ランダムに
        {
            return Random.Range(0, 360);
        }
        if(isShotType == 3) // Playerに向けて
        {
            Vector3 d = Vector3.zero;
            if(player != null) d = (player.transform.position - transform.position).normalized;
            float a = GetAngle(d);
            return a - transform.eulerAngles.z - 90f + shotAngle;
        }
        else
        {
            return shotAngle;
        }
    }

    float GetAngle(Vector3 v)
    {
        float rad = Mathf.Atan2(v.y, v.x);
        return rad * Mathf.Rad2Deg;
    }
}
