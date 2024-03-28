using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class PeluruBoss : MonoBehaviour {

		[Header("Stats")]
		public float cepatLajuPeluru;
		public int damage;


		[Header("Referensi")]
		private Transform player;
		private Vector2 target;
		public GameObject explosion;
		public GameObject explosionTwo;
		private Animator camAnim;
		private CharacterController ajisaka;


		// Use this for initialization
		void Start () {
			player = GameObject.FindGameObjectWithTag ("Player").transform;

			target = new Vector2 (player.position.x, player.position.y);

			ajisaka = GameObject.Find("AjiSaka").GetComponent<CharacterController> ();
		}

		// Update is called once per frame
		void Update () {
			transform.position = Vector2.MoveTowards (transform.position, target, cepatLajuPeluru * Time.deltaTime);

			if (transform.position.x == target.x && transform.position.y == target.y) {
				DestroyPeluru ();
			}


			Collider2D hit = Physics2D.OverlapCircle (transform.position, 0.2f, LayerMask.GetMask("Player"));
			if (hit) {
				
				camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
				camAnim.SetTrigger ("Shake");
				ajisaka.TakeDamage (damage);
				DestroyPeluru ();

			}

		}

//		void OnTriggerEnter2D(Collider2D other){
//			if (other.CompareTag ("Player")) {
//				camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
//				camAnim.SetTrigger("shake");
//				ajisaka.TakeDamage (damage);
//				DestroyPeluru ();
//			}
//		}

		void DestroyPeluru(){
			Instantiate(explosion, transform.position, Quaternion.identity);
			Instantiate(explosionTwo, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}