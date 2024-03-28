using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage;
    public float speed;
    public GameObject explosion;
    public GameObject explosionTwo;
    private Animator camAnim;
	private Transform boss;
	private Vector2 target;
	Collider2D range;
	private float waktuDelayMenembak;
	public float waktuMulaiDelayMenembak;

	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss").transform;
		range = GetComponent<Collider2D> ();
		target = new Vector2 (boss.position.x, boss.position.y);


	}

    private void Update()
    {   
		transform.Translate (Vector2.up * speed * Time.deltaTime);
		if (transform.position.x == target.x && transform.position.y == target.y) {
		}

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the boss some damage + spawn particle effects + screen shake
        if (other.CompareTag("Boss")) {
            camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
            camAnim.SetTrigger("shake");
			other.GetComponent<BossScriptdewata>().health -= damage;
			DestroyPeluru ();
            
        }
    }

	public void DestroyPeluru(){

		Instantiate(explosion, transform.position, Quaternion.identity);
		Instantiate(explosionTwo, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
