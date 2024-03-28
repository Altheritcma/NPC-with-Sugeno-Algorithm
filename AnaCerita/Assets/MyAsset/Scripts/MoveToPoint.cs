using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Collider2D))]

public class MoveToPoint : MonoBehaviour {

//	[Header("Facing Sprite")]
//	public bool spriteFaceLeft = true;
//	public bool playerInRange;
//
//	[Header("Movement")]
//	public float speed;
//	public float gravity = 10.0f;
//
//	[Header("Scanning settings")]
//	public Transform MyTarget;
//	[Tooltip("The angle of the forward of the view cone. 0 is forward of the sprite, 90 is up, 180 behind etc.")]
//	[Range(0.0f,360.0f)]
//	public float viewDirection = 0.0f;
//	[Range(0.0f, 360.0f)]
//	public float viewFov;
//	public float viewDistance;
//	[Tooltip("Time in seconds without the target in the view cone before the target is considered lost from sight")]
//	public float timeBeforeTargetLost = 3.0f;
//
//	[Header("Melee Attack Data")]
//	//[EnemyMeleeRangeCheck]
//	public float meleeRange = 3.0f;
//	//public Damager meleeDamager;
//	//public Damager contactDamager;
//	[Tooltip("if true, the enemy will jump/dash forward when it melee attack")]
//	public bool attackDash;
//	[Tooltip("The force used by the dash")]
//	public Vector2 attackForce;
//
//	[Header("Range Attack Data")]
//	[Tooltip("From where the projectile are spawned")]
//	public Transform shootingOrigin;
//
//
//	protected SpriteRenderer m_SpriteRenderer;
//	protected AjiSakaScript m_CharacterController2D;
//	protected ButtonHandler CheckButton;
//	protected Animator m_Animator;
//	protected Collider2D m_Collider;
//
//	protected Vector3 m_MoveVector;
//	protected Transform m_Target;
//	protected float m_TimeSinceLastTargetView;
//	protected Vector3 m_TargetShootPosition;
//
//	protected float m_FireTimer = 0.0f;
//
//	//as we flip the sprite instead of rotating/scaling the object, this give the forward vector according to the sprite orientation
//	protected Vector2 m_SpriteForward;
//	protected Bounds m_LocalBounds;
//
//	protected ContactFilter2D m_Filter;
//
//	protected Color m_OriginalColor;
//
//	protected bool m_Dead = false;
//
//
//	private void Awake(){
//		MyTarget = GameObject.FindGameObjectWithTag("Player").transform;
//
//		m_CharacterController2D = GetComponent<AjiSakaScript>();
//		CheckButton = GetComponent<ButtonHandler> ();
//
//
//		m_Collider = GetComponent<Collider2D>();
//		m_Animator = GetComponent<Animator>();
//		m_SpriteRenderer = GetComponent<SpriteRenderer>();
//
//		m_OriginalColor = m_SpriteRenderer.color;
//
////		if(projectilePrefab != null)
////			m_BulletPool = BulletPool.GetObjectPool(projectilePrefab.gameObject, 8);
//
//		m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
//		if (m_SpriteRenderer.flipX) m_SpriteForward = -m_SpriteForward;
//
////		if(meleeDamager != null)
////			EndAttack();
//	}
//
//	private void Start(){
//		m_Dead = false;
//		m_Collider.enabled = true;
//		playerInRange = false;
//
//
//	}
//
//	private void FixedUpdate(){
//		if (m_Dead)
//			return;
//
//		m_MoveVector.y = Mathf.Max(m_MoveVector.y - gravity * Time.deltaTime, - gravity);
//		UpdateTimers();
//
//		if(MyTarget.position.x > transform.position.x - viewDistance){
//			SetFacingData (1);
//		}
//		else if(MyTarget.position.x < transform.position.x - viewDistance){
//			SetFacingData (-1);
//		}
//		if (MyTarget.hasChanged)
//		ScanForPlayer ();
//		MyTarget.hasChanged = false;
//	}
//
//	void UpdateTimers()
//	{
//		if (m_TimeSinceLastTargetView > 0.0f)
//			m_TimeSinceLastTargetView -= Time.deltaTime;
//
//		if (m_FireTimer > 0.0f)
//			m_FireTimer -= Time.deltaTime;
//	}
//
//	public bool CheckForObstacle(float forwardDistance)
//	{
//		//we circle cast with a size sligly small than the collider height. That avoid to collide with very small bump on the ground
//		if (Physics2D.CircleCast(m_Collider.bounds.center, m_Collider.bounds.extents.y - 0.2f, m_SpriteForward, forwardDistance, m_Filter.layerMask.value))
//		{
//			return true;
//		}
//
//		Vector3 castingPosition = (Vector2)(transform.position + m_LocalBounds.center) + m_SpriteForward * m_LocalBounds.extents.x;
//		Debug.DrawLine(castingPosition, castingPosition + Vector3.down * (m_LocalBounds.extents.y + 0.2f), Color.red);
//
//		//if (!Physics2D.CircleCast(castingPosition, 0.1f, Vector2.down, m_LocalBounds.extents.y + 0.2f, m_CharacterController2D.groundedLayerMask.value))
//		{
//			return true;
//		}
//
//		return false;
//	}
//
//	public void SetFacingData(int facing)
//	{
//		if (facing == -1)
//		{
//			m_SpriteRenderer.flipX = !spriteFaceLeft;
//			m_SpriteForward = spriteFaceLeft ? Vector2.right : Vector2.left;
//		}
//		else if (facing == 1)
//		{
//			m_SpriteRenderer.flipX = spriteFaceLeft;
//			m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
//		}
//	}
//
//	public void SetMoveVector(Vector2 newMoveVector)
//	{
//		m_MoveVector = newMoveVector;
//	}
//
//	public void UpdateFacing()
//	{
//		bool faceLeft = m_MoveVector.x < 0f;
//		bool faceRight = m_MoveVector.x > 0f;
//
//		if (faceLeft)
//		{
//			SetFacingData(-1);
//		}
//		else if (faceRight)
//		{
//			SetFacingData(1);
//		}
//	}
//
//	public void ScanForPlayer()
//	{
////		//If the player don't have control, they can't react, so do not pursue them
////		if( MyTarget.hasChanged )
////			return;
//
//		Vector3 dir = MyTarget.position - transform.position;
//		if (dir.sqrMagnitude > viewDistance * viewDistance)
//		{
//			Debug.Log ("get");
//			return;
//		}
//
//		Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? Mathf.Sign(m_SpriteForward.x) * - viewDirection : Mathf.Sign(m_SpriteForward.x) * viewDirection) * m_SpriteForward;
//
//
//		float angle = Vector3.Angle(testForward, dir);
//
//		if (angle > viewFov * 0.5f)
//		{
//			return;
//		}
//
//		//m_Target = ;
//		m_TimeSinceLastTargetView = timeBeforeTargetLost;
////
////		//m_Animator.SetTrigger(m_HashSpottedPara);
//	}
//
//	public void OrientToTarget()
//	{
//		if (m_Target == null)
//			return;
//
//		Vector3 toTarget = m_Target.position - transform.position;
//
//		if (Vector2.Dot(toTarget, m_SpriteForward) < 0)
//		{
//			SetFacingData(Mathf.RoundToInt(-m_SpriteForward.x));
//		}
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
//			Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * m_SpriteForward;
//			if (m_SpriteRenderer.flipX) testForward.x = -testForward.x;
//
//			float angle = Vector3.Angle(testForward, toTarget);
//
//			if (angle <= viewFov * 0.5f)
//			{
//				//we reset the timer if the target is at viewing distance.
//				m_TimeSinceLastTargetView = timeBeforeTargetLost;
//			}    
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
//		//m_Animator.SetTrigger(m_HashTargetLostPara);
//		m_Target = null;
//	}
//
//	//This is used in case where there is a delay between deciding to shoot and shoot (e.g. Spitter that have an animation before spitting)
//	//so we shoot where the target was when the animation started, not where it is when the actual shooting happen
//	public void RememberTargetPos()
//	{
//		if (m_Target == null)
//			return;
//
//		m_TargetShootPosition = m_Target.transform.position;
//	}
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