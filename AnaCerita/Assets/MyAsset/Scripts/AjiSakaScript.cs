using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnaCerita
{
	public class AjiSakaScript : MonoBehaviour {

		public int health;
		public float speed;
		public float jumpForce;
		private int extraJump;
		public int extraJumpValue;
		private float longTimeJump;
		public Transform attackPos;
		public LayerMask WhatIsEnemy;
		public LayerMask WhatIsBoss;
		public Animator camAnim;
		public float AttackRange;
		public int damage;

		private float timeBtwAttack;
		public float startTimeBtwAttack;

		private bool facingright = true;
		private bool isGround;
		private bool isJump;
		public Transform groundCheck;
		public float checkRadius;
		public LayerMask WhatIsGround;
		private TuyulScript tuyul;
		private DewataCengkarScript boss;

		private GetInputManager inputManager;

		private Animator anim;
		private Rigidbody2D rb;
		private Projectile objectPeluru;
		private Vector2 moveVelocity;
		public float distance;


		public Slider healthbar;

		private bool isMoving;

		private BanaspatiBossScript banaspati;

		void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
			objectPeluru = GetComponent<Projectile>();
			inputManager = GetComponent<GetInputManager>();
			boss = GameObject.Find("DewataCengkar").GetComponent<DewataCengkarScript> ();
			banaspati = GameObject.Find ("BanaspatiBoss").GetComponent<BanaspatiBossScript> ();

		}


		private void Start()
		{
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody2D>();
			objectPeluru = GetComponent<Projectile>();

			extraJump = extraJumpValue;

		}

		private void Update()
		{
			anim.SetBool ("isAttack", false);

			if (timeBtwAttack <= 0) {
				if (inputManager.VerticalInput < 0) {
					//camAnim.SetTrigger ("shake");


					//anim.SetTrigger ("isAttack 0");
					anim.SetBool ("isAttack", true);
					Collider2D[] hit = Physics2D.OverlapCircleAll (attackPos.position, AttackRange, WhatIsEnemy);
					for (int i = 0; i < hit.Length; i++) {
						hit [i].GetComponent<TuyulScript> ().TakeDamage (damage);

					}

					Collider2D hit2 = Physics2D.OverlapCircle (attackPos.position, AttackRange, WhatIsBoss);

					if (hit2) {
						if (boss.health >= 75) {
							boss.TakeDamage (3);
						}
						if (boss.health <= 75) {
							boss.TakeDamage (0);
						}
							
					}

					Collider2D hit3 = Physics2D.OverlapCircle (attackPos.position, AttackRange, LayerMask.GetMask("BanaspatiBoss"));

					if (hit3) {
						banaspati.TakeDamage (damage);
					}

				}
			} else {
				timeBtwAttack -= Time.deltaTime;
			}


			if (isGround == true) {
				
				anim.SetBool ("isJump", false);

				extraJump = extraJumpValue;
			}



			healthbar.value = health;
		}

		private void FixedUpdate()
		{
			isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, WhatIsGround);

			
			transform.Translate(inputManager.CurrentInputHorizontal * speed * Time.deltaTime);

			if (inputManager.HorizontalInput > 0) {
				if (facingright == false) {
					Flip ();
				}
				anim.SetBool ("isRunning", true);

			} else if (inputManager.HorizontalInput < 0) {
				if (facingright == true) {
					Flip ();
				}
				anim.SetBool ("isRunning", true);
			} else if (inputManager.HorizontalInput == 0) {
				anim.SetBool ("isRunning", false);
			}

			if (inputManager.VerticalInput > 0) {


				if (extraJump > 0) {

					rb.velocity = Vector2.up * jumpForce;
					extraJump --;
					longTimeJump -= Time.deltaTime;

				}


				if (extraJump == 0 && isGround == true) {

					rb.velocity = Vector2.up * jumpForce;
					isGround = false;

				}

				anim.SetBool ("isJump", true);

			}

		}

		public void TakeDamage(int Damage){
			health -= Damage;
		}

		void Flip(){
			facingright = !facingright;
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		void OnDrawGizmosSelected(){
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (groundCheck.position, checkRadius);

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere (attackPos.position, AttackRange);
		}
	}
}