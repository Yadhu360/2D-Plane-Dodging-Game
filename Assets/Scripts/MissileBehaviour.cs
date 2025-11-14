using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour {

	Rigidbody2D rb;
	public Transform target;
	public GameObject dust;
	public GameObject explosionEffect; 
	public float speed = 6f, rotationSpeed = 120f, dustWait = .05f;

	public float bestScore;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(makingDust());
	}
	
	void FixedUpdate(){
		rb.linearVelocity = transform.up * speed;
		if(target != null){
			Vector2 direction = (Vector2) target.position - rb.position;
			direction.Normalize();
			float angle = Vector3.Cross(direction, transform.up).z;
			rb.angularVelocity = -rotationSpeed * angle;
		}
	}

	IEnumerator makingDust(){
		while(gameObject){
			yield return new WaitForSeconds(dustWait);
			GameObject dustTemp = Instantiate(dust, transform.position, dust.transform.rotation);
			Destroy(dustTemp,4f);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		string tag = other.gameObject.tag;
		if(tag.Equals("Player")){
			blowUpPlane(other.gameObject.transform);
		}
		if(tag.Equals("missile")){
			blowUpSelf();
		}
	}

	void blowUpPlane(Transform plane){
		bestScore = PlayerPrefs.GetFloat("score");
		if (timeManger.timeToDisplay > bestScore)
		{
			PlayerPrefs.SetFloat("score", timeManger.timeToDisplay);
			bestScore = timeManger.timeToDisplay;
		}
		blowUpSelf();
		plane.parent.GetComponent<PlaneBehaviour>().GameOver(transform);
	}

	void blowUpSelf(){
			GameObject tempExplosion = Instantiate(explosionEffect, transform.position, dust.transform.rotation);
			Destroy(tempExplosion,1.2f);
			Destroy(gameObject);
	}

}
