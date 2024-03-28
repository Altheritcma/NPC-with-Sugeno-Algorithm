using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnaCerita
{
	public class BanaspatiBossScript : MonoBehaviour {

		[Header("------Status------")]
		public int health;
		public int damage;
		public float minAttackCooldown = 0.25f, maxAttackCooldown = 2f, range;
		public Slider healthBar;
		public GameObject efekDamage, explosion;

		[Header("-----Referensi-----")]
		private bool hasSpawn;
		private Vector2 movement;
		private PointSpawnEnemy2 poinSpawn;
		private MoveTerbangScript movefly, moveFly;
		private Animator anim, camAnim;
		private SpriteRenderer spr;

		private float aiCoolDown;
		private bool isAttacking;
		private Vector2 positionTarget;

		private Transform playerPos;

		bool facingRight;


		void Awake()
		{
			poinSpawn = GetComponentInChildren<PointSpawnEnemy2> ();

			movefly = GetComponent<MoveTerbangScript>();

			anim = GetComponent<Animator> ();

			camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();

			spr = GetComponent<SpriteRenderer> ();

			playerPos = GameObject.FindGameObjectWithTag ("Player").transform; 

		}

		void Start()
		{
			hasSpawn = false;

			facingRight = true;

			movefly.enabled = false;

//			moveFly.enabled = false;

			healthBar.gameObject.SetActive (false);

			GetComponent<Collider2D> ().enabled = false;

			poinSpawn.enabled = false;

			isAttacking = false;

			aiCoolDown = maxAttackCooldown;
		}


		void Update()
		{

			healthBar.value = health;


			// Check if the enemy has spawned
			if (hasSpawn == false)
			{
				// We check only the first renderer for simplicity.
				// But we don't know if it's the body, and eye or the mouth...
				if (playerPos != null) {
					if (Vector2.Distance (transform.position, playerPos.position) < range) {
						Spawn ();
					}
				}
			}
			else
			{
				// AI
				//------------------------------------
				// Move or attack. permute. Repeat.
				aiCoolDown -= Time.deltaTime;

				if (aiCoolDown <= 0f)
				{
					isAttacking = !isAttacking;
					aiCoolDown = Random.Range(1f, 0.5f);
					positionTarget = Vector2.zero;

					// Set or unset the attack animation
					//anim.SetBool("Attack", isAttacking);
				}

				// Attack
				//----------
				if (isAttacking)
				{
					// Stop any movement
					movefly.direction = Vector2.zero;

					if (poinSpawn != null && poinSpawn.enabled && poinSpawn.CanSpawn)
						{
						poinSpawn.Spawn(true);
						anim.SetBool ("isAttack", true);
						}
					}
				// Move
				//----------
				else
				{
					// Define a target?
					if (positionTarget == Vector2.zero)
					{
						// Get a point on the screen, convert to world
//						Vector2 randomPoint = new Vector2(-Random.Range(0.8f, 1f), -Random.Range(0.4f, 1f));

						Vector2 randomPoint = new Vector2 (Random.Range(0.3f, 0.8f), Random.Range(0.4f, 0.8f));

						positionTarget = Camera.main.ViewportToWorldPoint(randomPoint);
					}
						


					// Are we at the target? If so, find a new one
					if (GetComponent<Collider2D>().OverlapPoint(positionTarget))
					{
						// Reset, will be set at the next frame
						positionTarget = Vector2.zero;
					}

					// Go to the point
					Vector3 direction = ((Vector3)positionTarget - this.transform.position);

					// Remember to use the move script
					movefly.direction = Vector3.Normalize(direction);
				}
			}
		}



		void FixedUpdate()
		{
			if (health <= 0) {
				Destroy(gameObject);
				Instantiate (efekDamage, transform.position, Quaternion.identity);
				Instantiate (explosion, transform.position,Quaternion.identity);
				camAnim.SetTrigger("MegaShake");
				GameSoundManager.PlaySound ("explosion");
			}

			Flip (flip: true);
		}



		void Flip(bool flip)
		{
			if (flip == true && playerPos != null) {
				if (playerPos.position.x < transform.position.x && !facingRight || playerPos.position.x > transform.position.x && facingRight) {

					facingRight = !facingRight;
					Vector3 scale = transform.localScale;
					scale.x *= -1;
					transform.localScale = scale;
				}
			} 

			else
			{
				//Add Something here
			}
		}



		private void Spawn()
		{
			hasSpawn = true;

			// Enable everything
			// -- Collider
			GetComponent<Collider2D>().enabled = true;
			// -- Moving
			movefly.enabled = true;
//			moveFly.enabled = true;

			healthBar.gameObject.SetActive (true);
			// -- Shooting

			poinSpawn.enabled = true;



		}

		public void TakeDamage(int damage)
		{
			health -= damage;
			Instantiate(efekDamage, transform.position, Quaternion.identity);
			GameSoundManager.PlaySound ("EnemyGetDamage");
		}

//		void OnTriggerEnter2D(Collider2D otherCollider2D)
//		{
//			// Taking damage? Change animation
//			ShotScript shot = otherCollider2D.gameObject.GetComponent<ShotScript>();
//			if (shot != null)
//			{
//				if (shot.isEnemyShot == false)
//				{
//					// Stop attacks and start moving awya
//					aiCoolDown = Random.Range(minAttackCooldown, maxAttackCooldown);
//					isAttacking = false;
//
//					// Change animation
//					anim.SetTrigger("Hit");
//				}
//			}
//		}

		void OnDrawGizmos()
		{
			// A little tip: you can display debug information in your scene with Gizmos
			if (hasSpawn && isAttacking == false)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(positionTarget, 0.5f);
			}

			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere (transform.position, range);
		}
			
	}	
}