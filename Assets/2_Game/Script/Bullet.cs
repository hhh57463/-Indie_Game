using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Transform GameTrans;
    float fBulletSpeed = 0.0f;

	// Use this for initialization
	void Start () {
        GameTrans = GameObject.Find("Game").transform;
        transform.parent = GameTrans.transform;

        fBulletSpeed = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * fBulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("LimitWall"))
        {
            Destroy(gameObject);
        }
    }

}
