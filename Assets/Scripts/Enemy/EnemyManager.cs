using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemy;

    void Start () {
        for(int i = 0; i < 10; i++)
        {
            Instantiate(enemy, new Vector3(10f, 10f, 0f), Quaternion.identity);
        }
    }
	
	void Update () {
		
	}
}
