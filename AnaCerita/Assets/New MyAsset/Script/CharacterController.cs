using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnaCerita
{
	public class CharacterController : MonoBehaviour 
	{
		[Header("Stats")]
		public float speedX, jumpSpeedY, delayBeforeDoubleJump, AttackRange, extraJumpValue;

		private float speed, extraJump;

		public bool facingRight, jumping, isAttack, canDoubleJump, isGrounded;

		public Transform checGround;

		Rigidbody2D rb;
		Animator anim;
		CheckGround checkGround2;
		CheckGround checkGround;
		BossCTRL boss;


		public int health, damage;

		public Slider healthbar;




		void Start()
		{	
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody2D> ();
			boss = GameObject.Find("Boss").GetComponent<BossCTRL>();
			checkGround = GameObject.Find ("AjiSaka").GetComponentInChildren<CheckGround> ();
			checkGround2 = GameObject.Find ("Boss").GetComponentInChildren<CheckGround> ();
			facingRight = true;
			jumping = false;
			canDoubleJump = false;

		}



		void FixedUpdate()
		{
			if (this.tag == "Player") {
				isGrounded = checkGround.isGrounded;
			}

			if (this.tag == "Boss") {
				isGrounded = checkGround2.isGrounded;
			}

			healthbar.value = health;

			HandleJumpNFalling ();

			//Gerakan Player
			MovePlayer(speed);

			Flip (speed, flip: true);
		}




		public void TakeDamage(int Damage){
			health -= Damage;

			if (this.tag == "Boss") {
				GameSoundManager.PlaySound ("EnemyGetDamage");
			}

			if (this.tag == "Player") {
				GameSoundManager.PlaySound ("PlayerGetDamage");
			}
		}




		public void MovePlayer(float playerSpeed)
		{

			if(playerSpeed < 0 && !jumping || playerSpeed > 0 && !jumping || checkGround.isGrounded && playerSpeed > 0 || checkGround.isGrounded && playerSpeed < 0)
			{
				if (this.tag == "Player") {
					anim.SetInteger ("State", 2);
				}

				if (this.tag == "Boss" + "Enemy") {
					anim.SetInteger ("State", 1);
				}
			}

			if (playerSpeed == 0 && !jumping || playerSpeed == 0 && checkGround.isGrounded) 
			{
				anim.SetInteger ("State", 0);
			}

			rb.velocity = new Vector2 (speed, rb.velocity.y);
		}
			


		public void HandleJumpNFalling()
		{
			if (jumping)
			{
				if (rb.velocity.y > 0) {
					if(this.tag == "Player")
					{
						anim.SetInteger ("State", 3);
					}
				} 
				else 
				{
					if (this.tag == "Player") 
					{
						anim.SetInteger ("State", 1);
					}
				}
			}
		}




		public void Flip(float charSpeed, bool flip)
		{
			if (flip == true) {
				if (this.tag == "Player") {
					if (charSpeed > 0 && !facingRight || charSpeed < 0 && facingRight) {

						facingRight = !facingRight;
						Vector3 scale = transform.localScale;
						scale.x *= -1;
						transform.localScale = scale;
					}
				}

				if (this.tag == "Boss" && boss.playerPos != null) {
					if (boss.playerPos.position.x < this.transform.position.x && !facingRight || boss.playerPos.position.x > this.transform.position.x && facingRight) {

						facingRight = !facingRight;
						Vector3 scale = transform.localScale;
						scale.x *= -1;
						transform.localScale = scale;
					}
				}
			} 

			else 
			{

			}
		}


		public void WalkRight()
		{
			speed = speedX;
		}



		public void WalkLeft()
		{
			speed = -speedX;
		}



		public void StopMoving()
		{
			speed = 0;
		}
			

		public void Jump ()
		{
			if (this.tag == "Player") {
				GameSoundManager.PlaySound ("PlayerJump");
			}
			
			if (checkGround.isGrounded) {
				checkGround.isGrounded = false;
				jumping = true;
				rb.velocity = Vector2.up * jumpSpeedY;
				//anim.SetInteger ("State", 3);
				Invoke ("EnableDoubleJump", delayBeforeDoubleJump);

			}

			if (canDoubleJump) {
				canDoubleJump = false;
				jumping = true;
				rb.velocity = Vector2.up * jumpSpeedY;
				//anim.SetInteger ("State", 3);
			}

		}


		public void EnableDoubleJump()
		{
			canDoubleJump = true;
		}
			

	}
}