using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSecript : MonoBehaviour {


	void OnTriggerEnter2D (Collider2D col)
	{
		ScoreTextScript.coinAmount += 50;
		Destroy (gameObject);
	}
}
