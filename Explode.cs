using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explode : MonoBehaviour {

	public float cubeSize = 0.2f;
	public int cubesInRow = 5;

	float cubesPivotDistance;
	Vector3 cubesPivot;

	public float explosionRadius = 2f;
	public float explosionForce = 500f;
	public float explosionUpwards = 1f;

	public Text clickText;

	void Start () {
		cubesPivotDistance = cubeSize * cubesInRow / 2;
		cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

		Stop();
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Play();
		}
	}

	private void OnTriggerEnter(Collider other){
		if(other.CompareTag("Floor")){
			Exploder();
		}
	}

	public void Exploder(){
		gameObject.SetActive(false);

		for(int x = 0; x < cubesInRow; x++){
			for(int y = 0; y < cubesInRow; y++){
				for(int z = 0; z < cubesInRow; z++){
					CreatePiece(x, y, z);
				}
			}
		}

		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

		foreach(Collider hit in colliders){
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if(rb != null){
				rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpwards);
			}
		}
	}

	private void CreatePiece(int x, int y, int z){
		GameObject piece;
		piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

		piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
		piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

		piece.AddComponent<Rigidbody>();
		piece.GetComponent<Rigidbody>().mass = 0.2f;
	}

	private void Play(){
		Time.timeScale = 1;

		clickText.text = "";
	}

	private void Stop(){
		Time.timeScale = 0;

		clickText.text = "LEFT CLICK TO DROP CUBE!";
	}

}