using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
	public Transform target;
	public float speed = 6f, rotationSpeed = 120f, dustWait = .05f;
	public GameObject missilePrefab;
	public Text best;
	Vector2 direction;
	void Start()
    {
		StartCoroutine(missileSpawner());
		best.text = "Best\n" + Mathf.FloorToInt(PlayerPrefs.GetFloat("score")).ToString() + " Seconds";
	}

	void FixedUpdate()
	{
		//rb.velocity = transform.up * speed;
		if (target != null)
		{
			target.transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-8f, 8f));
		}
	}
	public void startGame()
    {
		SceneManager.LoadScene("Main");
	}

	IEnumerator missileSpawner()
	{
		while (true)
		{
			int j = 10;
			int i = 8;			
			Vector3 spawnPosition = target.position + new Vector3(Random.Range(j, i), Random.Range(j, i), 0f);
			GameObject missileTemp = Instantiate(missilePrefab, spawnPosition, missilePrefab.transform.rotation);
			missileTemp.GetComponent<MissileBehaviour>().target = target;
			yield return new WaitForSeconds(Random.Range(0.5f, 3f));
		}
	}
}
