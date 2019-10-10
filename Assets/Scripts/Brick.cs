using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public enum Brick_Type{
		Normal,
		NoBreak,
		Prop_Life,
		Prob_Many
	}

	//声音
	public AudioSource brickAudio ;

	//生命道具的预制体
	public GameObject itemPrefab ;

	public GameObject brickParticlePrefab ;

	public Brick_Type currentBreakType ;
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
		if(currentBreakType == Brick_Type.NoBreak) {
			return ;
		}else if(currentBreakType == Brick_Type.Prop_Life || currentBreakType == Brick_Type.Prob_Many) {
			GameObject item = Instantiate(itemPrefab) ;
			Vector3 pos = transform.position ;
			pos.z -= 0.5f ;
			item.transform.position = pos ;
		}
		GameObject particle = Instantiate(brickParticlePrefab);
		particle.transform.position = transform.position ;

		brickAudio.Play();
		this.enabled = false ;
		GameManager.instance.CheckLevelPassed() ;
		Destroy(gameObject);
	}
}
