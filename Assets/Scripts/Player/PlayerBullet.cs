using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public bool isShoot = true;
    public GameObject playerBullet;

    IEnumerator Start()
    {
        while(isShoot)
        {
            // 弾をプレイヤーと同じ姿勢でインスタンス化
            Instantiate(playerBullet, transform.position, transform.rotation);
            // 0.3待つ
            yield return new WaitForSeconds(0.3f);
        }
    }
}
