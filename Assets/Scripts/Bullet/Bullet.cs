using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 5f;

    void Start () {
        // 速度を与える
        GetComponent<Rigidbody>().velocity = transform.up.normalized * bulletSpeed;
        Destroy(gameObject, 10f);
	}
}
