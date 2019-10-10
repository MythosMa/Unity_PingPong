using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour {
	public float timeDestroy ;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, timeDestroy);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
