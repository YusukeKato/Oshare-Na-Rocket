﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShotBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}