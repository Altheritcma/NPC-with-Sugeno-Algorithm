using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnaCerita
{
	public class AjiSakaController : MonoBehaviour 
	{
		private Animator anim;
		private CharacterController controller, boss;
		private BoxCollider2D boxColl;
		private float speed;

		public Transform attackPos, banaspati;

		public float attackRange;

		private int damage;


		void Start()
		{
			anim = GetComponent<Animator>();
			controller = GameObject.Find("AjiSaka").GetComponent<CharacterController>();
			boss = GameObject.Find ("Boss").GetComponent<CharacterController>();
			boxColl = GameObject.Find("AjiSaka").GetComponentInChildren<BoxCollider2D>();
		}


		void Update()
		{
			if (controller.health <= 0) {
				Destroy (gameObject);
			}

			damage = controller.damage;
			//kontrol ke arah kiri player
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				controller.WalkLeft ();
			}

			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				controller.StopMoving ();
			}


			//kontrol ke arah ke kanan player
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				controller.WalkRight ();
			}

			if (Input.GetKeyUp (KeyCode.RightArrow)) {
				controller.StopMoving ();
			}


			//kontrol lompat player
			if (Input.GetKeyDown (KeyCode.UpArrow) && controller.isGrounded) {
				controller.Jump ();
			}

			if (Input.GetKeyDown (KeyCode.UpArrow) && controller.canDoubleJump) {
				controller.Jump ();
			}

			if (Input.GetKeyDown (KeyCode.Space)) {
				Punch ();
			}
		}



		public void Punch ()
		{

			anim.SetInteger ("State", 5);



			Collider2D[] hit = Physics2D.OverlapCircleAll (attackPos.position, attackRange, LayerMask.GetMask("Tuyul"));
			for (int i = 0; i < hit.Length; i++) {
				hit [i].GetComponent<TuyulScript> ().TakeDamage (damage);

			}
			Collider2D[] hit2 = Physics2D.OverlapCircleAll (attackPos.position, attackRange, LayerMask.GetMask("Boss"));
			for (int i = 0; i < hit2.Length; i++) {
				if (boss.health > 50) {
					hit2 [i].GetComponent<CharacterController> ().TakeDamage (damage);
				}

				if (boss.health < 50) {
					hit2 [i].GetComponent<CharacterController> ().TakeDamage (0);
				}

				if (banaspati == null) {
					hit2 [i].GetComponent<CharacterController> ().TakeDamage (damage);
				}
			}

			Collider2D[] hit3 = Physics2D.OverlapCircleAll (attackPos.position, attackRange, LayerMask.GetMask("BanaspatiBoss"));
			for (int i = 0; i < hit3.Length; i++) {
				hit3 [i].GetComponent<BanaspatiBossScript> ().TakeDamage (damage);

			}

			Collider2D[] hit4 = Physics2D.OverlapCircleAll (attackPos.position, attackRange, LayerMask.GetMask("Banaspati"));
			for (int i = 0; i < hit4.Length; i++) {
				hit4 [i].GetComponent<BanaspatiScript> ().TakeDamage (damage);

			}

			Collider2D[] hit5 = Physics2D.OverlapCircleAll (attackPos.position, attackRange, LayerMask.GetMask("Kebleg"));
			for (int i = 0; i < hit5.Length; i++) {
				hit5 [i].GetComponent<BanaspatiBossScript> ().TakeDamage (damage);

			}

		}
			

		void OnTriggerStay2D(Collider2D other)
		{
			if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Ground") {
				boxColl.isTrigger = false;
			}
		}



		void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Ground") {
				boxColl.isTrigger = true;
			}
		}



		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (attackPos.position, attackRange);

		}









//		public float speedX, jumpSpeedY, delayBeforeDoubleJump, AttackRange;
//
//		private float speed;
//		private bool facingRight, jumping, isGrounded, isAttack, canDoubleJump;
//
//		Animator anim;
//		Rigidbody2D rb;
//		BossCTRL boss;
//		BanaspatiBossScript banaspati;
//
//		public Transform attackPos, checGround;
//
//		
//
//		public Slider healthbar;
//
//
//		void Awake()
//		{
//			boss = GameObject.Find("Boss").GetComponent<BossCTRL> ();
//			banaspati = GameObject.Find ("BanaspatiBoss").GetComponent<BanaspatiBossScript> ();
//		}
//
//
//
//		void Start()
//		{
//			anim = GetComponent<Animator>();
//			rb = GetComponent<Rigidbody2D>();
//			facingRight = true;
//		}
//
//
//		void FixedUpdate()
//		{
//
//
//			healthbar.value = health;
//
//
//			//Gerakan Player
//			MovePlayer(speed);
//
//			Flip (speed);
//
//			HandleJumpNFalling ();
//		
//
//
//
//			//kontrol ke arah kiri player
//			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
//				WalkLeft ();
//			}
//
//			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
//				StopMoving ();
//			}
//
//
//			//kontrol ke arah ke kanan player
//			if (Input.GetKeyDown (KeyCode.RightArrow)) {
//				WalkRight ();
//			}
//
//			if (Input.GetKeyUp (KeyCode.RightArrow)) {
//				StopMoving ();
//			}
//
//
//			//kontrol lompat player
//			if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
//				Jump ();
//			}
//
//			if (Input.GetKeyDown (KeyCode.Space) && canDoubleJump) {
//				Jump ();
//			}
//
//
//
//			if (Input.GetKeyDown(KeyCode.B)) 
//			{
//
//				Punch ();
//
//			}
//
//
//		}
//
//
//		public void TakeDamage(int Damage){
//			health -= Damage;
//		}
//
//
//
//		public void MovePlayer(float charSpeed)
//		{
//			if(charSpeed < 0 && !jumping || charSpeed > 0 && !jumping)
//			{
//				if (this.tag == "Player") {
//					anim.SetInteger ("State", 2);
//				}
//			}
//
//			if (charSpeed == 0 && !jumping) 
//			{
//				anim.SetInteger ("State", 0);
//			}
//
//			rb.velocity = new Vector2 (speed, rb.velocity.y);
//		}
//
//
//
//		public void HandleJumpNFalling()
//		{
//			if (jumping)
//			{
//				if (rb.velocity.y > 0) {
//					anim.SetInteger ("State", 3);
//				} 
//				else 
//				{
//					anim.SetInteger ("State", 1);
//				}
//			}
//		}
//			
//
//		public void Flip(float charSpeed)
//		{
//			if (charSpeed > 0 && !facingRight || charSpeed < 0 && facingRight) 
//			{
//				
//				facingRight = !facingRight;
//				Vector3 scale = transform.localScale;
//				scale.x *= -1;
//				transform.localScale = scale;
//			}
//		}
//
//
//		public void CheckGround()
//		{
//			Collider2D whatIsDetect = Physics2D.OverlapCircle (checGround.position, 0.2f, LayerMask.GetMask("Ground"));
//			if (whatIsDetect) {
//				isGrounded = true;
//				jumping = false;
//				isAttack = true;
//				canDoubleJump = false;
//				anim.SetInteger ("State", 0);
//			}
//		}
//			
//
//
//		public void WalkRight()
//		{
//			speed = speedX;
//		}
//
//
//		public void WalkLeft()
//		{
//			speed = -speedX;
//		}
//
//
//		public void StopMoving()
//		{
//			speed = 0;
//		}
//
//
//		public void Jump ()
//		{
//
//			if (isGrounded) {
//				isGrounded = false;
//				jumping = true;
//				rb.velocity = Vector2.up * jumpSpeedY;
//				Invoke ("EnableDoubleJump", delayBeforeDoubleJump);
//				anim.SetInteger ("State", 3);
//			}
//
//			if (canDoubleJump) {
//				canDoubleJump = false;
//				rb.velocity = Vector2.up * jumpSpeedY;
//				anim.SetInteger ("State", 3);
//			}
//		}
//
//
//		
//
//		public void EnableDoubleJump()
//		{
//			canDoubleJump = true;
//		}
//

	}
}
