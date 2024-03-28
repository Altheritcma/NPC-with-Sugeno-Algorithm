using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestScriptTwo : MonoBehaviour {
	public Transform player = null;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (player) {

			Vector2 forwards = transform.TransformDirection(Vector2.left);
			Vector2 toOther = player.position - transform.position;

			if (Vector2.Dot (forwards, toOther) < 0) {
				Debug.Log ("you here");
			}
		}
	}
		
}
