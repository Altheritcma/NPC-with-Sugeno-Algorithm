using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class EnemyTestScript : MonoBehaviour {

//	public float detectRadius;
//	public float viewpread;
//	public Transform GD;
//
//	Rigidbody2D rb;
//	//AjiSakaScript ajisaka;
//
//	public int health;
//	public int damage;
//	public float cepatJalan;
//
//	GetInputManager inputManager;
//	protected Transform m_Target;
//	private Vector3 m_MoveVector;
//
//	public bool spriteFaceLeft = false;
//	protected SpriteRenderer m_SpriteRenderer;
//	protected Vector2 m_SpriteForward;
//	private float timeBtwDamage;
//	public float m_timerToattack = 2;
//
//	[Header("Scanning settings")]
//	[Tooltip("The angle of the forward of the view cone. 0 is forward of the sprite, 90 is up, 180 behind etc.")]
//	[Range(0.0f,360.0f)]
//	public float viewDirection = 0.0f;
//	[Range(0.0f, 360.0f)]
//	public float viewFov;
//	public float viewDistance;
//	[Range(0.0f,360.0f)]
//	public float viewDirectionBehind = 0.0f;
//	[Range(0.0f, 360.0f)]
//	public float viewBehind;
//	public float viewBehindDistance;
//	[Tooltip("Time in seconds without the target in the view cone before the target is considered lost from sight")]
//	public float timeBeforeTargetLost = 3.0f;
//
//	protected float m_TimeSinceLastTargetView;
//
//	[Header("Melee Attack Data")]
//	//[EnemyMeleeRangeCheckAttribute]
//	public float meleeRange = 3.0f;
//	[Tooltip("if true, the enemy will jump/dash forward when it melee attack")]
//	public bool attackDash;
//	[Tooltip("The force used by the dash")]
//	public Vector2 attackForce;
//
//	[Header("Range Attack Data")]
//	[Tooltip("From where the projectile are spawned")]
//	public Transform shootingOrigin;
//	protected Vector3 m_TargetShootPosition;
//
//	public Transform meleeAttack;
//	public Transform player;
//
//	protected Color m_OriginalColor;
//
//
//	protected Vector3 m_LocalDamagerPosition;
//
//
//
//
//	void Awake(){
//		m_SpriteRenderer = GetComponent<SpriteRenderer>();
//		player = GameObject.Find("AjiSaka").transform;
//		inputManager = GameObject.Find("AjiSaka").GetComponent<GetInputManager>();
//		meleeAttack = GameObject.Find ("MeleeAttackCek").transform;
//		rb = GetComponent<Rigidbody2D>();
//		ajisaka = GameObject.Find("AjiSaka").GetComponent<AjiSakaScript> ();
//		m_OriginalColor = m_SpriteRenderer.color;
//
//
//		meleeAttack.gameObject.SetActive(false);
//		m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
//		if (m_SpriteRenderer.flipX) m_SpriteForward = -m_SpriteForward;
//	}
//
//	// Use this for initialization
//	void Start () {
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//	void FixedUpdate(){
//		UpdateTimers ();
//		ScanForPlayer ();
//		OrientToTarget ();
//		CheckTargetStillVisible ();
//		RememberTargetPos ();
//		CheckMeleeAttack ();
//		Patrol ();
//
//	}
//
//	void UpdateTimers()
//	{
//		if (m_TimeSinceLastTargetView > 0.0f)
//			m_TimeSinceLastTargetView -= Time.deltaTime;
//
////		if (m_FireTimer > 0.0f)
////			m_FireTimer -= Time.deltaTime;
//	}
//
//	public void SetFacingData(int facing)
//	{
//		if (facing == -1)
//		{
////			m_SpriteRenderer.flipX = !spriteFaceLeft;
////			m_SpriteForward = spriteFaceLeft ? Vector2.right : Vector2.left;
//			transform.eulerAngles = new Vector2(0, 0);
//
//
//		}
//		else if (facing == 1)
//		{
////			m_SpriteRenderer.flipX = spriteFaceLeft;
////			m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
//			transform.eulerAngles = new Vector2(0, -180);
//
//		
//		}
//	}
//
////	public void UpdateFacing()
////	{
////		bool faceLeft = m_MoveVector.x < 0f;
////		bool faceRight = m_MoveVector.x > 0f;
////
////		if (faceLeft)
////		{
////			SetFacingData(-1);
////		}
////		else if (faceRight)
////		{
////			SetFacingData(1);
////		}
////	}
//
//
//
//
//	public void ScanForPlayer()
//	{
//		//If the player don't have control, they can't react, so do not pursue them
//		if (GetInputManager.instance.HorizontalInput == 0)
//			return;
//			
//
//		Vector2 dir = GetInputManager.instance.transform.position - transform.position;
//		if (dir.sqrMagnitude > viewDistance * viewDistance)
//		{
//			Debug.Log ("get");
//			return;
//		}
//
//		Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? Mathf.Sign(m_SpriteForward.x) * - viewDirection : Mathf.Sign(m_SpriteForward.x) * viewDirection) * m_SpriteForward;
//
//		if (m_SpriteRenderer.flipX) testForward.x = -testForward.x;
//
//		float angle = Vector3.Angle(testForward, dir);
//
//		if (angle > viewFov * 0.5f)
//		{
//			return;
//		}
//		
//		m_Target = GetInputManager.instance.transform;
//		m_TimeSinceLastTargetView = timeBeforeTargetLost;
//
//		//m_Animator.SetTrigger(m_HashSpottedPara);
//	}
//
//	public void OrientToTarget()
//	{
//		if (m_Target == null)
//			return;
//
//		if (player) {
//
//			Vector2 forwards = transform.TransformDirection(Vector2.left);
//			Vector2 toOther = player.position - transform.position;
//
//			if (Vector2.Dot (forwards, toOther) < 0) {
//				print ("behind");
//				SetFacingData (-1);	
//			} else {
//				SetFacingData (1);
//			}
//		}
//
////		Vector3 toTarget = m_Target.position - transform.position;
////
////		if (Vector2.Dot (toTarget, transform.left) < 0) {
////			SetFacingData (-1);	
////		} else {
////			SetFacingData (1);
////		}
//
//
////		if (toTarget.sqrMagnitude < viewBehindDistance * viewBehindDistance)
////		{
////			Debug.Log ("OrientedTotarget");
////			Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirectionBehind : viewDirectionBehind) * m_SpriteForward;
////			if (m_SpriteRenderer.flipX) testForward.x = -testForward.x;
////
////			float angle = Vector3.Angle(testForward, toTarget);
////
////			if (angle >= viewBehind * 0.5f)
////			{
////				//we reset the timer if the target is at viewing distance.
////				m_TimeSinceLastTargetView = timeBeforeTargetLost;
////				Debug.Log ("You here");
////			}
//
//
//			//CheckMeleeAttack ();
//
////		}
//
//	}
//
//	public void CheckTargetStillVisible()
//	{
//		if (m_Target == null)
//			return;
//
//		Vector3 toTarget = m_Target.position - transform.position;
//
//		if (toTarget.sqrMagnitude < viewDistance * viewDistance)
//		{
//			Debug.Log ("CheckTargetStillVisibility");
//			Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * m_SpriteForward;
//			if (m_SpriteRenderer.flipX) testForward.x = -testForward.x;
//
//			float angle = Vector3.Angle(testForward, toTarget);
//
//			if (angle <= viewFov * 0.5f)
//			{
//				//we reset the timer if the target is at viewing distance.
//				m_TimeSinceLastTargetView = timeBeforeTargetLost;
//
//			}
//
//
//			//CheckMeleeAttack ();
//
//		}
//
//
//		if (m_TimeSinceLastTargetView <= 0.0f)
//		{
//			ForgetTarget();
//		}
//	}
//
//	public void ForgetTarget()
//	{
//		Debug.Log ("ForgetTarget");
//		//m_Animator.SetTrigger(m_HashTargetLostPara);
//		m_Target = null;
//		return;
//	}
//
//	//This is used in case where there is a delay between deciding to shoot and shoot (e.g. Spitter that have an animation before spitting)
//	//so we shoot where the target was when the animation started, not where it is when the actual shooting happen
//	public void RememberTargetPos()
//	{
//		if (m_Target == null)
//			return;
//		Debug.Log ("RememberTarget");
//
//		//m_TargetShootPosition = m_Target.transform.position;
//	}
//
////
////	//Call every frame when the enemy is in pursuit to check for range & Trigger the attack if in range
//	public void CheckMeleeAttack()
//	{
//		if (m_Target == null)
//		{//we lost the target, shouldn't shoot
//			return;
//		}
//
//		if((m_Target.transform.position - transform.position).sqrMagnitude < (meleeRange * meleeRange))
//		{
//			//m_Animator.SetTrigger(m_HashMeleeAttackPara);
//			//meleeAttackAudio.PlayRandomSound();
//			meleeAttack.gameObject.SetActive(true);
//			Debug.Log ("Attack");
//			transform.Translate (Vector2.zero);
//			if (timeBtwDamage <= 0) {
//				ajisaka.TakeDamage (damage);
//				timeBtwDamage = m_timerToattack;
//			} else {
//				timeBtwDamage -= Time.deltaTime;
//			}
//		}
//	}
////
////	//This is called when the damager get enabled (so the enemy can damage the player). Likely be called by the animation throught animation event (see the attack animation of the Chomper)
////	public void StartAttack()
////	{
////		if (m_SpriteRenderer.flipX)
////			transform.localPosition = Vector3.Scale(m_LocalDamagerPosition, new Vector3(-1, 1, 1));
////		else
////			transform.localPosition = m_LocalDamagerPosition;
////
////		//meleeDamager.EnableDamage();
////		meleeAttack.gameObject.SetActive(true);
////
////		if (attackDash)
////			m_MoveVector = new Vector2(m_SpriteForward.x * attackForce.x, attackForce.y);
////	}
////
////	public void EndAttack()
////	{
////		if (m_Target == null)
////			return;
////		Vector3 toTarget = m_Target.position - transform.position;
////
////		if (toTarget.sqrMagnitude > meleeRange * meleeRange) 
////		{
////			meleeAttack.gameObject.SetActive(false);
////		}
////	}
//
//
//	public void Patrol(){
//		transform.Translate (Vector2.left * cepatJalan * Time.deltaTime);
//		CheckGround ();
//	}
//
//
//	public void CheckGround(){
//		RaycastHit2D groundInfo = Physics2D.Raycast (GD.position, Vector2.down, 5f);
//		Debug.DrawRay (GD.position, Vector2.down, Color.red);
//
//		if (groundInfo.collider == false) {
//			if (spriteFaceLeft == false) {
////				transform.eulerAngles = new Vector2(0, -180);
//				SetFacingData(1);
//				spriteFaceLeft = true;
//			} else {
////				transform.eulerAngles = new Vector2(0, 0);
//				SetFacingData(-1);
//				spriteFaceLeft = false;
//			}
//		}
//	}
//
//
//	public void MoveToTarget(bool move){
//		if (move == true) {
//			if (player.position.x < transform.position.x) {
//
//				//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
//				rb.velocity = Vector2.left * cepatJalan;
//
//				transform.eulerAngles = new Vector2 (0, 0);
//
//				//facingRight = false;
//
//			} else {
//
//				//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
//				rb.velocity = Vector2.right * cepatJalan;
//
//				transform.eulerAngles = new Vector2 (0, -180);
//				//facingRight = true;
//			}
//
//		} else {
//			rb.velocity = Vector2.zero;
//		}
//	}
//
//
//
//		
//	#if UNITY_EDITOR
//	private void OnDrawGizmosSelected()
//	{
//		//draw the cone of view
//		Vector3 forward = spriteFaceLeft ? Vector2.left : Vector2.right;
//		forward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * forward;
//
//		if (GetComponent<SpriteRenderer>().flipX) forward.x = -forward.x;
//
//		Vector3 endpoint = transform.position + (Quaternion.Euler(0, 0, viewFov * 0.5f) * forward);
//
//		Handles.color = new Color(0, 1.0f, 0, 0.2f);
//		Handles.DrawSolidArc(transform.position, -Vector3.forward, (endpoint - transform.position).normalized, viewFov, viewDistance);
//
//		//Draw attack range
//		Handles.color = new Color(1.0f, 0,0, 0.1f);
//		Handles.DrawSolidDisc(transform.position, Vector3.back, meleeRange);
//
//		//draw the cone of view behind
//		Vector3 behind = spriteFaceLeft ? Vector2.left : Vector2.right;
//		forward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirectionBehind : viewDirectionBehind) * behind;
//
//		if (GetComponent<SpriteRenderer>().flipX) behind.x = -behind.x;
//
//		Vector3 endpoint2 = transform.position + (Quaternion.Euler(0, 0, viewBehind * 0.5f) * behind);
//
//		Handles.color = new Color(0, 1.0f, 0, 0.2f);
//		Handles.DrawSolidArc(transform.position, -Vector3.forward, (endpoint2 - transform.position).normalized, viewBehind, viewBehindDistance);
//
//
//	}
//	#endif
//}
////
//////bit hackish, to avoid to have to redefine the whole inspector, we use an attirbute and associated property drawer to 
//////display a warning above the melee range when it get over the view distance.
////public class EnemyMeleeRangeCheckAttribute : PropertyAttribute
////{
////
////}
////
////#if UNITY_EDITOR
////[CustomPropertyDrawer(typeof(EnemyMeleeRangeCheckAttribute))]
////public class EnemyMeleeRangePropertyDrawer : PropertyDrawer
////{
////
////	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
////	{
////		SerializedProperty viewRangeProp = property.serializedObject.FindProperty("viewDistance");
////		if (viewRangeProp.floatValue < property.floatValue)
////		{
////			Rect pos = position;
////			pos.height = 30;
////			EditorGUI.HelpBox(pos, "Melee range is bigger than View distance. Note enemies only attack if target is in their view range first", MessageType.Warning);
////			position.y += 30;
////		}
////
////		EditorGUI.PropertyField(position, property, label);
////	}
////
////	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
////	{
////		SerializedProperty viewRangeProp = property.serializedObject.FindProperty("viewDistance");
////		if (viewRangeProp.floatValue < property.floatValue)
////		{
////			return base.GetPropertyHeight(property, label) + 30;
////		}
////		else
////			return base.GetPropertyHeight(property, label);
////	}
////}
////#endif
}