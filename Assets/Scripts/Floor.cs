using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

	public AudioSource deathAudio ;
	public GameObject deathParticlePrefab ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		Item item = other.gameObject.GetComponent<Item>();
		if(item != null) {
			Destroy(other.gameObject);
		}else {
			if(GameManager.instance.IsLastBall) {
				deathAudio.Play() ;
				GameManager.instance.GameOver() ;
			}else {
				GameObject particle = Instantiate(deathParticlePrefab) ;
				particle.transform.position = other.transform.position ;
				Destroy(other.gameObject) ;
			}
			
		}
	}
}
