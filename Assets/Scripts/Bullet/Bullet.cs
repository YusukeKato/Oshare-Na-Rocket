using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 4f;

	void Start () {
        GetComponent<Rigidbody>().velocity = transform.up.normalized * bulletSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
