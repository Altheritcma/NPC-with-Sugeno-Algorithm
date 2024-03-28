using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnaCerita
{
	public class BanaspatiScript : MonoBehaviour {

		[Header ("Stats")]
		public int health;
		public int damage;
		public float cepatJalan;
		public float jarakBerhenti;
		public float jarakPlayerTerpantau;
		public float waktuMulaiDelayMenembak;
		private float timeMulaiShowtandaTanya = 2f;
		private float timeShowTandaTanya;
		private float waktuDelayMenembak;
		private float timeBtwDamage = 1.5f;
		public float m_timerToattack;
		private float m_Time;


		Rigidbody2D rb;

		[Header("Referensi")]
		public GameObject Tembak, efekDamage;
		private Transform player;

		private Animator camAnim;
		public Slider healthBar;
		private SpriteRenderer spr;

		private Animator anim;
		public bool isDead, isHealthActive;
		private bool facingRight = true;
		private bool isPatrol = true;
		//private bool canAttack = false;
		private bool isGrounding;
		public Transform groundDetection, wallDetect;


		// Use this for initialization
		void Start () 
		{
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody2D>();
			spr = GetComponent<SpriteRenderer> ();

			waktuDelayMenembak = waktuMulaiDelayMenembak;
			m_Time = m_timerToattack;
			timeShowTandaTanya = timeMulaiShowtandaTanya;
			healthBar.gameObject.SetActive (false);

		}

		// Update is called once per frame
		void Update () 
		{
			if (player != null){
			
			healthBar.value = health;


			if (spr.IsVisibleFrom (Camera.main)) {
				healthBar.gameObject.SetActive (true);
			}
				


			//membuat musuh secara default bergerak maju ke kanan
			if (isPatrol == true) {
				Patrol ();
				anim.SetBool ("isWalk", true);
			}

			// Untuk menggerakkan musuh ini ke Player
			RaycastHit2D groundInfo2 = Physics2D.Raycast (groundDetection.position, Vector2.down, 2f);

			if (Vector2.Distance (transform.position, player.position) <= jarakPlayerTerpantau) 
			{
				isPatrol = false;
				anim.SetBool ("isWalk", false);
				anim.SetBool ("is!!", true);
				anim.SetBool ("is??", false);


				if (player.position.x < transform.position.x) {

					transform.eulerAngles = new Vector2 (0, -180);
					facingRight = false;

				} if (player.position.x > transform.position.x) {

					transform.eulerAngles = new Vector2 (0, 0);
					facingRight = true;
				}


				if (m_Time <= 0) 
				{
					anim.SetBool ("is!!", false);
					if (Vector2.Distance (transform.position, player.position) > jarakBerhenti) 
					{
						MoveToTarget (move:true);
						anim.SetBool ("isWalk", true);
						if (groundInfo2.collider == false) {
							MoveToTarget (move:false);
						}

					}

					if (waktuDelayMenembak <= 0) 
					{
						Instantiate (Tembak, transform.position, Quaternion.identity);
						anim.SetBool ("isAttack", true);
						waktuDelayMenembak = waktuMulaiDelayMenembak;
					} 
					else 
					{
						waktuDelayMenembak -= Time.deltaTime;
					}

				}
				else
				{

					m_Time -= m_timerToattack * Time.deltaTime;
				}
			} 
			else 
			{
				m_Time = m_timerToattack;
				waktuDelayMenembak = waktuMulaiDelayMenembak;
				isPatrol = true;
				anim.SetBool ("isWalk", true);
				anim.SetBool ("isAttack", false);


			}





			//Untuk menyatakan keputusan dari health musuh ini
//			if (health <= 25) {
//				anim.SetTrigger("stageTwo");
//			}

			if (health <= 0) {
				anim.SetTrigger("death");
				Destroy (gameObject);
					GameSoundManager.PlaySound ("explosion");
			}

			// memberi player jeda waktu untuk menghindar sebelum musuh ini memberi damage lebih
			if (timeBtwDamage > 0) {
				timeBtwDamage -= Time.deltaTime;
			}

			// Untuk mendeklarasikan jumlah float nyawa ke bentuk gamabr bar nyawa
			//healthBar.value = health;
			}
		}

		public void Patrol(){
			transform.Translate (Vector2.right * cepatJalan * Time.deltaTime);
			CheckGroundNWall ();

		}



		public void TakeDamage(int damage)
		{
			health -= damage;

			Instantiate(efekDamage, transform.position, Quaternion.identity);

			GameSoundManager.PlaySound ("EnemyGetDamage");
		}



		public void MoveToTarget(bool move){
			if (move == true) {
				if (player.position.x < transform.position.x) {

					//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
					rb.velocity = Vector2.left * cepatJalan;

					transform.eulerAngles = new Vector2 (0, -180);
					facingRight = false;

				} if (player.position.x > transform.position.x) {

					//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
					rb.velocity = Vector2.right * cepatJalan;
					transform.eulerAngles = new Vector2 (0, 0);
					facingRight = true;
				}
			} else {
				rb.velocity = Vector2.zero;
			}
		}

		public void enemyWaitToAttack(){

			if (m_Time <= 0) {


				m_Time = m_timerToattack; 

			} else {
				Debug.Log ("player");
				m_Time -= Time.deltaTime;

			}
		}
		void Flip(){
			facingRight = !facingRight;
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		public void CheckGroundNWall(){
			RaycastHit2D groundInfo = Physics2D.Raycast (groundDetection.position, Vector2.down, 2f);
			RaycastHit2D wallInfo = Physics2D.Raycast (wallDetect.position, Vector2.up, 2f);
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

			if (wallInfo.collider == true) {
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

		bool AttackRangeCheck(){
			if (Vector2.Distance (transform.position, player.position) <= jarakPlayerTerpantau) {
				return true;
			} else {
				return false;
			}
		}

		bool StopRangeCheck(){
			if (Vector2.Distance (transform.position, player.position) <= jarakBerhenti) {
				return true;
			} else {
				return false;
			}

		}
	}
}