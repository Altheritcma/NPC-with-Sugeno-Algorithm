using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnaCerita
{
	public class BossCTRL : MonoBehaviour 
	{

		public bool facingRight, isStop, isGrounded, isAttack, canDoubleJump, isSpawn, isSpawn2, 
		diJarakDekat, diJarakSedang, diJarakJauh, isDestination_1, isDestination_2, isDestination_3, isDestination_4;

		public float jarakJauh, jarakDekat, jarakSedang, timeForAnimTeleport, resetdelayBeforeJumpBack, SpawnCooldown, TakeFireCooldown;

		public Transform efekTeleportTransform, efekTeleportTransform2, destination_1, destination_2, destination_3, destination_4, playerPos;
		public Transform[] spawnPoint;

		public GameObject efekDamage, efekTeleport, enemy, explosion;

		private int randPosX;

		private float goForTeleport, timeForJumpBack, delayBeforeJumpBack, timeDestroyPoint8;

		private Transform banaspati2, pointJumpBack;

		public Transform banaspati;

		Animator anim, camAnim;

		PointSpawnEnemy pointSpawn;

		PointSpawnEnemy2 pointSpawn2;

		CharacterController controller;

		MoveToRandomPosition mtrp;


		void Awake()
		{
			anim = GetComponent<Animator>();
			camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
//			banaspati2 = GameObject.Find ("BanaspatiBoss").transform;
			playerPos = GameObject.FindGameObjectWithTag("Player").transform;
			pointJumpBack = GameObject.FindGameObjectWithTag ("PointJumpBack").transform;
			pointSpawn = GameObject.Find("Point 5").GetComponent<PointSpawnEnemy> ();
			pointSpawn2 = GameObject.Find("Point 3").GetComponent<PointSpawnEnemy2> ();
			controller = GameObject.Find("Boss").GetComponent<CharacterController> ();
			mtrp = GameObject.Find ("PointTeleport").GetComponentInChildren<MoveToRandomPosition> ();
		}


		void Start()
		{
					
			facingRight = true;
		
			delayBeforeJumpBack = resetdelayBeforeJumpBack;
		
			goForTeleport = timeForAnimTeleport;
		
			timeForJumpBack = 0.2f;

			timeDestroyPoint8 = 2f;

			banaspati.gameObject.SetActive (false);
		
		}



		void FixedUpdate()
		{

			//  jika posisi player ada di atas boss, boss tidak terlihat bug/muter2
			Debug.DrawRay (transform.position, Vector2.up * 30, Color.yellow);	
			if (Physics2D.Raycast (transform.position, Vector2.up, 30, LayerMask.GetMask ("Player"))) {
				Flip (flip: false);

			}



			if (diJarakJauh) {
				
				MoveToTarget (moveToTarget: true);

				SpawnEnemy ();
			}



			if (diJarakSedang) {

				SpawnEnemy ();
				MoveToTarget (moveToTarget: false);
				TakeFire ();

			}
				

			DiJarakJauh ();
			DiJarakSedang ();
			DiJarakDekat ();


			JumpBack ();
			PerilakuDiluarJarakJauh ();


			JikaNyawaPenuh ();
			JikaNyawaSedang ();
			JikaNyawaSedikit ();

			CheckDestination ();

			if (controller.health <= 0) {
				Destroy (gameObject);
				StopMoving ();
				Instantiate (efekDamage, transform.position, Quaternion.identity);
				Instantiate (explosion, transform.position, Quaternion.identity);
				camAnim.SetTrigger("MegaShake");
				GameSoundManager.PlaySound ("EnemyDeath");
				GameSoundManager.PlaySound ("explosion");
			}


			Collider2D hit = Physics2D.OverlapCircle (transform.position, 2f, LayerMask.GetMask("Wall"));
			if (hit) {
				mtrp.MoveToRandomPos ();
			}
		}



		public void StopMoving()
		{
			controller.StopMoving ();
			anim.SetInteger ("State", 0);
		}


		public void CheckDestination()
		{
			if (isDestination_1) {
				destination_1.gameObject.SetActive (true);

				if (destination_1.position.x > transform.position.x) {
					anim.SetInteger ("State", 1);
					controller.WalkRight ();
				}

				if (destination_1.position.x < transform.position.x) {
					anim.SetInteger ("State", 1);
					controller.WalkLeft ();
				}


				Collider2D hit = Physics2D.OverlapCircle (transform.position, 0.2f, LayerMask.GetMask("Point 1"));

				if (hit) 
				{
					StopMoving ();
				}
			}

			if (isDestination_2) {
				destination_2.gameObject.SetActive (true);

				if (destination_2.position.x < transform.position.x) {
					anim.SetInteger ("State", 1);
					controller.WalkLeft ();
				} 


				if (destination_2.position.x > transform.position.x) {
					anim.SetInteger ("State", 1);
					controller.WalkRight ();
				}

				Collider2D hit = Physics2D.OverlapCircle (transform.position, 0.2f, LayerMask.GetMask ("Point 4"));

				if (hit) 
				{
					StopMoving ();
					Instantiate (efekTeleport, efekTeleportTransform2.position, Quaternion.identity);
				}


			}

			if (isDestination_3) {
				destination_3.gameObject.SetActive (true);

				Collider2D hit2 = Physics2D.OverlapCircle (transform.position, 0.2f, LayerMask.GetMask("Point 2"));

				if (hit2) 
				{
					StopMoving ();
					isDestination_1 = false;
				}
			}

			if (isDestination_4) {
				destination_4.gameObject.SetActive (true);

				Collider2D hit2 = Physics2D.OverlapCircle (transform.position, 0.2f, LayerMask.GetMask("Point 8"));

				if (hit2) {

					StopMoving ();

					timeDestroyPoint8 -= Time.deltaTime;
				}
			}




			if (!isDestination_1) {

				destination_1.gameObject.SetActive (false);
			}

			if (!isDestination_2) {

				destination_2.gameObject.SetActive (false);
			}

			if (!isDestination_3) {

				destination_3.gameObject.SetActive (false);
			}

			if (!isDestination_4) {

				destination_4.gameObject.SetActive (false);
			}
		}



		public void PerilakuDiluarJarakJauh()
		{
			if (playerPos != null) {
				if (Vector2.Distance (playerPos.position, transform.position) > jarakJauh) {

					StopMoving ();
				}
			}
		}



		public void JumpBack()
		{

			if (timeForJumpBack <= 0) {

				timeForJumpBack = 0.5f;

				diJarakDekat = false;

				diJarakSedang = true;
			}


			if (timeForJumpBack >= 0) {

				if (diJarakDekat) {

					timeForJumpBack -= Time.deltaTime;

					diJarakSedang = false;

					if (playerPos.position.x < transform.position.x) {
						anim.SetInteger ("State", 1);
						controller.WalkRight ();
					} 


					if (playerPos.position.x > transform.position.x) {
						anim.SetInteger ("State", 1);
						controller.WalkLeft ();
					}
				}
			}

		}




		void MoveToTarget(bool moveToTarget)
		{
			if (moveToTarget == true) {
				if (playerPos.position.x < transform.position.x) {
					anim.SetInteger ("State", 1);
					controller.WalkLeft ();
				} 


				if (playerPos.position.x > transform.position.x) {
					anim.SetInteger ("State", 1);
					controller.WalkRight ();
				}
			} 

			else {

				controller.StopMoving ();
			} 

			Collider2D hit = Physics2D.OverlapCircle (transform.position, 2f, LayerMask.GetMask ("Player"));
			if (hit) {

				controller.StopMoving ();

			} 
		}


		void Flip(bool flip)
		{
			if (flip == true) {
				if (playerPos.position.x < transform.position.x && !facingRight || playerPos.position.x > transform.position.x && facingRight) {

					facingRight = !facingRight;
					Vector3 scale = transform.localScale;
					scale.x *= -1;
					transform.localScale = scale;
				}
			} 

			else
			{
				controller.StopMoving ();
			}
		}



		void SpawnEnemy()
		{
			SpawnCooldown -= Time.deltaTime;


			if (SpawnCooldown <= 1f) {

				anim.SetInteger ("State", 2);

			}

			if (isSpawn) {

				if (pointSpawn != null && pointSpawn.enabled && pointSpawn.CanSpawn) {
					pointSpawn.Spawn (true);
				}
			}
		}





		void TakeFire()
		{
			TakeFireCooldown -= Time.deltaTime;


			if (TakeFireCooldown <= 1f) {

				anim.SetInteger ("State", 5);

			}

			if (isSpawn2) {

				if (pointSpawn2 != null && pointSpawn2.enabled && pointSpawn2.CanSpawn) {
					pointSpawn2.Spawn (true);
				}
			}
		}



		void JikaNyawaPenuh()
		{
			if (controller.health >= 50) 
			{

				isDestination_1 = false;
				isDestination_2 = false;
				isDestination_3 = false;
				isDestination_4 = false;

				if (TakeFireCooldown <= 0f) {
					isSpawn2 = !isSpawn2;
					TakeFireCooldown = Random.Range (5f, 10f);


				}


				if (SpawnCooldown <= 0f) {
					isSpawn = !isSpawn;
					SpawnCooldown = Random.Range (5f, 8f);


				}
			}
		}



		void JikaNyawaSedang()
		{
			if (controller.health <= 50 && controller.health >= 25) 
			{

				if (TakeFireCooldown <= 0f) {
					isSpawn2 = !isSpawn2;
					TakeFireCooldown = Random.Range (5f, 10f);
				}

				mtrp.SpawnRandom ();
				
				pointSpawn.enabled = false;

				if (banaspati != null) {
					banaspati.gameObject.SetActive (true);
				}

				isDestination_1 = true;
				isDestination_2 = false;
				isDestination_3 = true;
				isDestination_4 = false;
			}

			if (banaspati == null) {
				isDestination_1 = false;
				isDestination_3 = false;

				pointSpawn.enabled = true;
			}


			if (banaspati == null && isDestination_3 == false) {
				isDestination_2 = true;
				isDestination_4 = true;

			}


			if (timeDestroyPoint8 <= 0 && isDestination_2 == true) {

				isDestination_2 = false;
				isDestination_4 = false;
			}



		}



		void JikaNyawaSedikit()
		{
			if (controller.health <= 25 && controller.health >= 0) {

				if (TakeFireCooldown <= 0f) {
					isSpawn2 = !isSpawn2;
					TakeFireCooldown = Random.Range (4f, 8f);

				}

				if (SpawnCooldown <= 0f) {
					isSpawn = !isSpawn;
					SpawnCooldown = Random.Range (5f, 8f);


				}

				mtrp.SpawnRandom ();

			}
		}



		void DiJarakJauh()
		{
			if (playerPos != null) {
				if (Vector2.Distance (transform.position, playerPos.position) < jarakJauh) {

					diJarakJauh = true;
					diJarakSedang = false;
				}
			}
		}


		void DiJarakSedang()
		{
			if (playerPos != null) {
				if (Vector2.Distance (transform.position, playerPos.position) < jarakSedang) {

					diJarakJauh = false;
					diJarakSedang = true;

				}
			}
		}



		void DiJarakDekat()
		{
			if (playerPos != null) {
				if (Vector2.Distance (transform.position, playerPos.position) < jarakDekat) {
					diJarakDekat = true;
				}
			}
		}




		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere (transform.position, jarakJauh);

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere (transform.position, jarakSedang);

			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (transform.position, jarakDekat);
		}

//		[Header("Edited Value")]
//
//		public float speedX, jarakJauh, jarakDekat, jarakSedang, jumpSpeedY, 
//		delayBeforeDoubleJump, ResetdelayBeforeJumpBack, timeForAnimTeleport, SpawnCooldown, TakeFireCooldown;
//
//		public int health, damage;
//		private Transform playerPos, banaspati, pointJumpBack;
//		public Transform efekTeleportTransform, destination_1, destination_2, destination_3, destination_4;
//		private float goForTeleport, timeForJumpBack, speed;
//		public Slider HealthBar;
//		public GameObject efekDamage, efekTeleport;
//
//
//		[Header("Misc")]
//		float delayBeforeJumpBack;
//		public bool facingRight, isStop, isGrounded, isAttack, canDoubleJump, isSpawn, diJarakDekat, diJarakSedang, diJarakJauh;
//
//		Animator anim;
//		Rigidbody2D rb;
//		PointSpawnEnemy pointSpawn;
//		PointSpawnEnemy2 pointSpawn2;
//
//		CharacterController controller;
//
//
//
//
//
//
//		void Awake()
//		{
//			anim = GetComponent<Animator>();
//			rb = GetComponent<Rigidbody2D>();
//			banaspati = GameObject.Find ("BanaspatiBoss").transform;
//			playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
//			pointJumpBack = GameObject.FindGameObjectWithTag ("PointJumpBack").transform;
//			pointSpawn = GameObject.Find("Point 5").GetComponent<PointSpawnEnemy> ();
//			pointSpawn2 = GameObject.Find("Point 3").GetComponent<PointSpawnEnemy2> ();
//			controller = GetComponent<CharacterController> ();
//		}
//
//
//		void Start()
//		{
//			
//			facingRight = true;
//
//			delayBeforeJumpBack = ResetdelayBeforeJumpBack;
//
//			goForTeleport = timeForAnimTeleport;
//
//			timeForJumpBack = 0.5f;
//
//
//
//		}
//
//
//		void Update()
//		{
//			HealthBar.value = health;
//		}
//
//
//
//
//		void FixedUpdate()
//		{
//
//			Flip (flip: true);
//
//			//  jika posisi player ada di atas boss, boss tidak terlihat bug/muter2
//			Debug.DrawRay (transform.position, Vector2.up * 30, Color.yellow);	
//			if (Physics2D.Raycast (transform.position, Vector2.up, 30, LayerMask.GetMask ("Player"))) {
//				Flip (flip: false);
//			
//			}
//				
//
//			controller.MovePlayer (speed);
//
//
//			if (diJarakJauh) {
//				
////				MoveToTarget (moveToTarget: true);
//
//				SpawnEnemy ();
//			
//			}
//				
//			if (diJarakSedang) {
//
//				SpawnEnemy ();
//
////				MoveToTarget (moveToTarget: false);
//
//				TakeFire ();
//			}
//
//			if (isStop) {
//				controller.StopMoving ();
//			}
//
//			DiJarakJauh ();
//
//			DiJarakSedang ();
//
//			DiJarakDekat ();
//
//			JumpBack ();
//
//
//			JikaNyawaPenuh ();
//			JikaNyawaSedang ();
//
//		}
//
//
//
//
//
//
//
//
//		public void JumpBack()
//		{
//			
//			if (timeForJumpBack <= 0) {
//
//				timeForJumpBack = 0.5f;
//
//				diJarakDekat = false;
//
//				diJarakSedang = true;
//			}
//
//
//			if (timeForJumpBack >= 0) {
//
//				if (diJarakDekat) {
//				
//					timeForJumpBack -= Time.deltaTime;
//
//					diJarakSedang = false;
//
//					if (playerPos.position.x < transform.position.x) {
//						anim.SetInteger ("State", 1);
//						WalkRight (10f);
//					} 
//
//
//					if (playerPos.position.x > transform.position.x) {
//						anim.SetInteger ("State", 1);
//						WalkLeft (10f);
//					}
//				}
//			}
//				
//		}
//			
//
//
//
//		void MoveToTarget(bool moveToTarget)
//		{
//			if (moveToTarget == true) {
//				isStop = false;
//				if (playerPos.position.x < transform.position.x) {
//					anim.SetInteger ("State", 1);
//					controller.WalkLeft ();
//				} 
//
//
//				if (playerPos.position.x > transform.position.x) {
//					anim.SetInteger ("State", 1);
//					controller.WalkRight ();
//				}
//			} 
//
//			else {
//				
//				isStop = true;
//			} 
//
//			Collider2D hit = Physics2D.OverlapCircle (transform.position, 2f, LayerMask.GetMask ("Player"));
//			if (hit) {
//				
//				isStop = true;
//
//			} 
//		}
//			
//
//
//		void Flip(bool flip)
//		{
//			if (flip == true) {
//				if (playerPos.position.x < transform.position.x && !facingRight || playerPos.position.x > transform.position.x && facingRight) {
//				
//					facingRight = !facingRight;
//					Vector3 scale = transform.localScale;
//					scale.x *= -1;
//					transform.localScale = scale;
//				}
//			} 
//
//			else
//			{
//				isStop = true;
//			}
//		}
//
//
//		public void WalkRight(float isSpeed)
//		{
//			rb.velocity = new Vector2 (isSpeed, rb.velocity.y);
//		}
//
//
//		public void WalkLeft(float isSpeed)
//		{
//			rb.velocity = new Vector2 (-isSpeed, rb.velocity.y);
//		}
//
//
//		public void StopMoving()
//		{
////			rb.velocity = Vector2.zero;
//			speed = 0;
//			anim.SetInteger ("State", 0);
//		}
//
//
//		public void TakeDamage(int Damage){
//
//			Instantiate(efekDamage, transform.position, Quaternion.identity);
//			health -= Damage;
//		}
//
//
//
//		void SpawnEnemy()
//		{
//			SpawnCooldown -= Time.deltaTime;
//
//			if (SpawnCooldown < 0f) {
//				isSpawn = !isSpawn;
//				SpawnCooldown = Random.Range (4f, 8f);
//
//
//			}
//
//			if (SpawnCooldown <= 2f) {
//				
//				anim.SetInteger ("State", 2);
//
//			}
//
//			if (isSpawn) {
//
//				if (pointSpawn != null && pointSpawn.enabled && pointSpawn.CanSpawn) {
//					pointSpawn.Spawn (true);
//				}
//			}
//		}
//
//
//
//		void TakeFire()
//		{
//			TakeFireCooldown -= Time.deltaTime;
//
//
//
//			if (TakeFireCooldown < 0f) {
//				isSpawn = !isSpawn;
//				TakeFireCooldown = Random.Range (8f, 4f);
//
//
//			}
//
//			if (TakeFireCooldown <= 2f) {
//
//				anim.SetInteger ("State", 5);
//
//			}
//
//			if (isSpawn) {
//
//				if (pointSpawn2 != null && pointSpawn2.enabled && pointSpawn2.CanSpawn) {
//					pointSpawn2.Spawn (true);
//				}
//			}
//		}
//
//
//
//		void JikaNyawaPenuh()
//		{
//			if (health > 75) 
//			{
//				banaspati.gameObject.SetActive (false);
//
//				destination_1.gameObject.SetActive (false);
//
//				destination_2.gameObject.SetActive (false);
//			}
//		}
//
//
//
//		void JikaNyawaSedang()
//		{
//			if (health < 75) 
//			{
//				pointSpawn.enabled = false;
//				if (banaspati != null)
//				{
//					banaspati.gameObject.SetActive (true);
//				}
//
//				destination_1.gameObject.SetActive (true);
//
//				if (destination_1.position.x < transform.position.x) {
//					anim.SetInteger ("State", 1);
//					WalkLeft (speed);
//				} 
//
//
//				if (destination_1.position.x > transform.position.x) {
//					anim.SetInteger ("State", 1);
//					WalkRight (speed);
//				}
//
//				Collider2D hit = Physics2D.OverlapCircle (transform.position, 0.2f, LayerMask.GetMask("Point 1"));
//
//				if (hit) 
//				{
//					isStop = true;
//				}
//
//				Collider2D hit2 = Physics2D.OverlapCircle (transform.position, 0.2f, LayerMask.GetMask("Point 2"));
//
//				if (hit2) 
//				{
//					isStop = true;
//					destination_1.gameObject.SetActive(false);
//				}
//					
//			}
//
//			if (banaspati == null) {
//
//				isStop = false;
//
//				destination_2.gameObject.SetActive (true);
//				destination_1.gameObject.SetActive (false);
//
//				if (destination_2.position.x < transform.position.x) {
//					anim.SetInteger ("State", 1);
//					WalkLeft (speed);
//				} 
//
//
//				if (destination_2.position.x > transform.position.x) {
//					anim.SetInteger ("State", 1);
//					WalkRight (speed);
//				}
//
//				Collider2D hit = Physics2D.OverlapCircle (transform.position, 0.2f, LayerMask.GetMask("Point 4"));
//
//				if (hit) 
//				{
//					isStop = true;
//				}
//			}
//		}
//
//
//
//		void DiJarakJauh()
//		{
//			if (Vector2.Distance (transform.position, playerPos.position) < jarakJauh) {
//
//				diJarakJauh = true;
//				diJarakSedang = false;
//			}
//		}
//
//
//		void DiJarakSedang()
//		{
//			if (Vector2.Distance (transform.position, playerPos.position) < jarakSedang) {
//
//				diJarakJauh = false;
//				diJarakSedang = true;
//
//			}
//		}
//
//
//
//		void DiJarakDekat()
//		{
//			if (Vector2.Distance (transform.position, playerPos.position) < jarakDekat)
//			{
//				diJarakDekat = true;
//			}
//		}
//
//
//
//
//		void OnDrawGizmosSelected()
//		{
//			Gizmos.color = Color.blue;
//			Gizmos.DrawWireSphere (transform.position, jarakJauh);
//
//			Gizmos.color = Color.yellow;
//			Gizmos.DrawWireSphere (transform.position, jarakSedang);
//
//			Gizmos.color = Color.red;
//			Gizmos.DrawWireSphere (transform.position, jarakDekat);
//		}

	}
}
