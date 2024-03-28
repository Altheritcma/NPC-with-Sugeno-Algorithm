using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePanel : MonoBehaviour {
	public float speed;
	public Transform point;
	public RectTransform panel;
	public float  startTime;
	public float journeylength;
	public Transform point2;
	// Use this for initialization
	void Start () {
		point = GameObject.FindGameObjectWithTag("Point").transform;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void MovingPanel()
	{
		float distCovered =(Time.time - startTime) * speed;
		float fracJourney = distCovered / journeylength;

//		float steps = speed * Time.deltaTime;
//		RectTransform rt = GetComponent<RectTransform>();
//		panel.position = Vector3.MoveTowards(panel.position, point2.position, fracJourney);

		panel.position = Vector3.Lerp (point.position, point2.position, fracJourney);

	}

	public void LoadScene(){
	
	}
}
