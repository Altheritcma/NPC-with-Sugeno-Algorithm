using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnaCerita
{
	public class DewataCengkarScript : MonoBehaviour {

		[Header("-----Status-----")]
		public bool isMoving;
		public bool hasSpawn;
		public bool isSpawn;
		public bool isTeleport;
		public bool isGoTeleport;
		public int health;
		public int damage;
		public float speed;
		public float jarakDekat;
		public float jarakSedang;
		public float jarakJauh;
		public float minAttackCooldown = 0.5f;
		public float maxAttackCooldown = 2f;
		public float timeEfekTeleport = 2f;
		private float startTimeTeleport;

		[Header("-----Misc------")]
		public Slider healthBar;
		public GameObject efekDamage;
		public GameObject efekTeleport;
		public Transform efekTeleportTransform;
		public Transform point1;
		public Transform point2;
		public Transform point3;
		public Transform point4;
		public Transform point5;
		public Transform Destination_1;
		public Transform Destination_2;



		[Header("Referensi")]
		Rigidbody2D rb;
		Animator anim;
		PointSpawnEnemy pointSpawn;
		PointSpawnEnemy2 pointSpawn2;
		SpriteRenderer renderers;
		private float aiCooldown;
		private float aiCooldown2;
		private Transform playerPos;
		private Transform banaspati;


		void Awake()
		{
			rb = GetComponent<Rigidbody2D> ();
			anim = GetComponent<Animator> ();
			pointSpawn = GameObject.Find("Point 5").GetComponent<PointSpawnEnemy> ();
			pointSpawn2 = GameObject.Find ("Point 7").GetComponent<PointSpawnEnemy2> ();
			renderers = GetComponent<SpriteRenderer> ();
			playerPos = GameObject.Find ("AjiSaka").transform;
			banaspati = GameObject.Find ("BanaspatiBoss").transform;
		}



		void Start()
		{
			hasSpawn = false;
			isGoTeleport = false;
			isTeleport = false;

			GetComponent<Collider2D> ().enabled = false;

			pointSpawn.enabled = false;
			pointSpawn2.enabled = false;

			isSpawn = false;
			isMoving = false;

			aiCooldown = maxAttackCooldown;
			aiCooldown2 = maxAttackCooldown;

			startTimeTeleport = timeEfekTeleport;

			banaspati.gameObject.SetActive (false);

		}


		void Update ()
		{


			//untuk convert int health ke tampilan slider
			healthBar.value = health;



		}


		void FixedUpdate()
		{

			// jika bool hasSpawn tdk centang dan jika musuh terdeteksi oleh kamera utama maka bool hasSpawn akan tercentang
			if (hasSpawn == false) {
				if (Vector2.Distance (transform.position, playerPos.position) < jarakJauh) {

					Spawn ();

				}


			} else {

				if (Vector2.Distance (transform.position, playerPos.position) < jarakJauh && Vector2.Distance (transform.position, playerPos.position) > jarakSedang) {
					isMoving = true;
					pointSpawn.enabled = true;
				}

				aiCooldown -= Time.deltaTime;



				if (aiCooldown < 0f) {
					isSpawn = !isSpawn;
					aiCooldown = Random.Range (4f, 8f);


				}

				if (aiCooldown <= 2f) {
					anim.SetBool ("isSpawn", !isSpawn);

				}

				if (isSpawn) {

					if (pointSpawn != null && pointSpawn.enabled && pointSpawn.CanSpawn) {
						pointSpawn.Spawn (true);
					}
				}


			}


			Collider2D hit = Physics2D.OverlapCircle (transform.position, 2f, LayerMask.GetMask ("Point3"));
			if (hit) {
				anim.SetBool ("isWalk", false);
				isMoving = false;
				rb.velocity = Vector2.zero;



			}	



			//jika karakter banaspati hancur maka point2 tdk aktif dan 4 aktif 




			// jika bool isMoving tercentang maka dewata berjalan
			if (isMoving == true) {
				Move ();
			}



				
			// menyatakan keadaan/keputusan dari kondisi jumlah variable int health
			if (health <= 75) {

				point1.gameObject.SetActive (true);
				point2.gameObject.SetActive (true);

				banaspati.gameObject.SetActive (true);

				isMoving = true;
				rb.velocity = new Vector2(speed, rb.velocity.y);
				anim.SetBool ("isWalk", true);
				anim.SetBool ("isSpawn", false);
				isSpawn = false;

				Collider2D hit1 = Physics2D.OverlapCircle (transform.position, 2f, LayerMask.GetMask ("Point1"));
				if (hit1) {
				
					anim.SetBool ("isWalk", false);
					isMoving = false;
					rb.velocity = Vector2.zero;
			
				}

				Collider2D hit2 = Physics2D.OverlapCircle (transform.position, 2f, LayerMask.GetMask ("Point2"));
				if (hit2) {

					point1.gameObject.SetActive (false);

					anim.SetBool ("isSpawn", true);
					isMoving = false;
					rb.velocity = Vector2.zero;





					if (Vector2.Distance (transform.position, playerPos.position) < jarakSedang) {
						pointSpawn2.enabled = false;
					}

					aiCooldown -= Time.deltaTime;



					if (aiCooldown < 0f) {
						isSpawn = !isSpawn;
						aiCooldown = Random.Range (4f, 8f);


					}

					if (aiCooldown <= 2f) {
						anim.SetBool ("isSpawn", !isSpawn);

					}

					if (isSpawn) {

						if (pointSpawn != null && pointSpawn.enabled && pointSpawn.CanSpawn) {
							pointSpawn.Spawn (true);
						}
					}

				}

				Collider2D hit3 = Physics2D.OverlapCircle (transform.position, 2f, LayerMask.GetMask ("Point4"));
				if (hit3) {

					anim.SetBool ("isWalk", false);
					isMoving = false;
					rb.velocity = Vector2.zero;

				}

				Collider2D hit4 = Physics2D.OverlapCircle (transform.position, 2f, LayerMask.GetMask ("Point5"));
				if (hit4) {

					anim.SetBool ("isWalk", false);
					isMoving = false;
					rb.velocity = Vector2.zero;

				}




			}



			if (health <= 0) {

			}



			//untuk menyatakan keadaan jika jarak player dekat, jauh, dan sedang.

			if(Vector2.Distance(transform.position, playerPos.position) < jarakSedang)
			{

					anim.SetBool ("isWalk", false);
					isMoving = false;
					rb.velocity = Vector2.zero;

				pointSpawn.enabled = true;
				pointSpawn2.enabled = true;

					aiCooldown2 -= Time.deltaTime;


					if (aiCooldown2 < 0f) {
					
						isSpawn = !isSpawn;
					aiCooldown2 = Random.Range(1f, 2f);




					}


				if (aiCooldown2 <= 2f) {
					anim.SetBool ("isSpawn", !isSpawn);
					isMoving = false;
					rb.velocity = Vector2.zero;

				}



				if (isSpawn) {

					if (pointSpawn2 != null && pointSpawn2.enabled && pointSpawn2.CanSpawn) {
						pointSpawn2.Spawn (true);
					}
				}
			}

			if (Vector2.Distance (transform.position, playerPos.position) < jarakDekat) {

				if (aiCooldown <= 2f) {
				
					anim.SetBool ("isWalk", true);
					isMoving = true;
					rb.velocity = new Vector2 (speed, rb.velocity.y);
				}

				if (aiCooldown < 0) {
					aiCooldown = Random.Range (2f, 5f);


				} else {
					isMoving = false;
					anim.SetBool ("isWalk", false);
					rb.velocity = Vector2.zero;
				}
			}
		}




		// sebuah fungsi untuk memanggil objek lain dan menampilkannya.
		void Spawn()
		{
			hasSpawn = true;
			isMoving = true;

			GetComponent<Collider2D> ().enabled = true;



		}


		// sebuah fungsi yg di panggil di script lain untuk mengurangi nilai int health
		public void TakeDamage(int Damage){
			
			Instantiate(efekDamage, transform.position, Quaternion.identity);
			health -= Damage;
		}


		// sebuah fungsi untuk membuat dewata berjalan ke kiri dari pandangan kamera utama
		void Move()
		{
			rb.velocity = new Vector2(-speed, rb.velocity.y);
			anim.SetBool ("isWalk", true);
		}

		//sebuah fungsi untuk membuat karakter mengganti posisi hadap dari kiri ke kanan / sebaliknya
		void Flip()
		{
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere (transform.position, jarakJauh);

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere (transform.position, jarakSedang);

			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (transform.position, jarakDekat);
		}
	

	}
}