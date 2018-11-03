using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet : MonoBehaviour {

    public bool isShoot = true;
    public GameObject shotBullet;
    public GameObject bullet;

    IEnumerator Start()
    {
        while (isShoot)
        {
            // 弾をプレイヤーと同じ姿勢でインスタンス化
            GameObject sb = Instantiate(shotBullet, transform.position, transform.rotation);
            GameObject b = Instantiate(bullet, sb.transform);
            b.transform.position = sb.transform.position;
            b.transform.rotation = sb.transform.rotation;
            // 0.3待つ
            yield return new WaitForSeconds(0.3f);
        }
    }
}
