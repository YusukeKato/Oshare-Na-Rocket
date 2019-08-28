using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour {

    public float size;

    GameObject stageManager;

	void Start () {
        StartCoroutine(SizeFunc(0.1f));
    }

    IEnumerator SizeFunc (float n)
    {
        yield return new WaitForSeconds(n);

        stageManager = GameObject.Find("StageManager");
        float s = (stageManager.GetComponent<DataManager>().screenMax.x / 150f) * size;

        if (s < 0.01f) s = 0.07f;

        transform.localScale = new Vector3(s, s, 0.2f);
    }
}
