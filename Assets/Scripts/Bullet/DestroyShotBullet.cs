using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShotBullet : MonoBehaviour {

	void Start () {
        Destroy(gameObject, 10f);
	}
}
