using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnaCerita
{
	public class TuyulScript : MonoBehaviour {

		[Header ("Stats")]
		public int health;
		public int damage;
		public float cepatJalan;
		public float jarakBerhenti;
		public float jarakPlayerTerpantau;
		private float timeBtwDamage;
		public float m_timerToattack;
		private float m_Time;
		public Transform attackPos;
		public LayerMask WhatisEnemies;
		public float attackRange;
		private float dazedTime;
		public float startDazedTime = 2f;

		Rigidbody2D rb;
		CharacterController ajisaka;

		[Header("Referensi")]
		private Transform player;

		public Animator camAnim;
		public Slider healthBar;

		private Animator anim;
		public bool isDead;
		private bool facingRight = true;
		private bool isGrounding;
		private bool isPlayer;
		public Transform groundDetection;
		public Transform AboveDetection;

		public GameObject EfekDamage;

		Collider2D enemiesToDamage;




		// Use this for initialization
		void Start () {
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody2D>();
			ajisaka = GameObject.Find("AjiSaka").GetComponent<CharacterController> ();

			MoveToTarget (move: true);
			//anim.SetBool ("isWalk", true);
		}



		// Update is called once per frame
		void Update () {
			RaycastHit2D groundInfo2 = Physics2D.Raycast (transform.position, Vector2.up, 100f);
			Debug.DrawRay (transform.position, Vector2.up, Color.red);
			//		if (Vector2.Distance (transform.position, player.position) > jarakBerhenti) {
			//			MoveToTarget (move: true);
			//		}

			if (dazedTime <= 0) {
				MoveToTarget (move: true);
			} else {
				MoveToTarget (move: false);
				dazedTime -= Time.deltaTime;
			}

			MoveToTarget (move: true);
			//Vector2 up = AboveDetection.TransformDirection(Vector2.up) * 30;

			Debug.DrawRay (AboveDetection.position, Vector2.up * 30, Color.yellow);	
			if (Physics2D.Raycast (AboveDetection.position, Vector2.up, 30, WhatisEnemies)) {

				MoveToTarget (move: false);
				//transform.eulerAngles = new Vector2(0,0);
			}

			Collider2D hit = Physics2D.OverlapCircle (attackPos.position, attackRange, WhatisEnemies);
			if (hit && hit.tag == "Player") {
				MoveToTarget (move: false);
				if (timeBtwDamage <= 0) {
					ajisaka.TakeDamage (damage);
					timeBtwDamage = m_timerToattack;
				} else {
					timeBtwDamage -= Time.deltaTime;
				}
			}

			if (health <= 0) {
				Destroy (gameObject);
				GameSoundManager.PlaySound ("EnemyDeath");
			}
		}

		public void MoveToTarget(bool move){
			if (move == true) {
				if (player.position.x < transform.position.x) {

					//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
					rb.velocity = Vector2.left * cepatJalan;

					transform.eulerAngles = new Vector2 (0, 0);

					//facingRight = false;

				} else {

					//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
					rb.velocity = Vector2.right * cepatJalan;

					transform.eulerAngles = new Vector2 (0, -180);
					//facingRight = true;
				}

			} else {
				rb.velocity = Vector2.zero;

			}
		}


		public void TakeDamage(int Damage){
			dazedTime = startDazedTime;
			Instantiate(EfekDamage, transform.position, Quaternion.identity);
			health -= Damage;
			GameSoundManager.PlaySound ("EnemyGetDamage");
		}


		void Flip(){
			facingRight = !facingRight;
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		public void CheckGround(){
			RaycastHit2D groundInfo = Physics2D.Raycast (groundDetection.position, Vector2.down, 2f);

			Debug.DrawRay (groundDetection.position, Vector2.down, Color.red);

			if (groundInfo.collider == false) {
				if (facingRight == true) {
					transform.eulerAngles = new Vector2 (0, -180);
					//Flip();
					facingRight = false;
				} else {
					transform.eulerAngles = new Vector2 (0, 0);
					//Flip();
					facingRight = true;
				}
			}
		}


		public void CheckAbove(){
			Vector2 up = AboveDetection.TransformDirection(Vector2.up) * 30;

			Debug.DrawRay (AboveDetection.position, up, Color.yellow);
			if (Physics2D.Raycast (AboveDetection.position, up, WhatisEnemies)) {

				Debug.Log ("above");
				MoveToTarget (move: false);
				transform.eulerAngles = new Vector2 (0, 0);
			}
		}



		void OnDrawGizmosSelected(){
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (attackPos.position, attackRange);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			//		attackPos1 = attackPos;
			//		attackrange1 = attackRange;
			//		// deal the player damage ! 
			//		if (other.CompareTag ("Player")) {
			//			Debug.Log ("OnTrigger");
			//			MoveToTarget (move: false);
			//
			//
			//			if (timeBtwDamage <= 0) {
			//				other.GetComponent<AjiSakaScript> ().health -= damage;
			//
			//			}
			//		}
		}
		private void OnTriggerExit2D(Collider2D other){
			//MoveToTarget (move:true);

		}
	}
}