using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	private Rigidbody rb ;
	// Use this for initialization

	public float speed;

	public Paddle paddle;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		paddle = GameObject.FindObjectOfType<Paddle>();
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.instance.isPlaying) {
			Vector3 pos = transform.position ;
			pos = paddle.transform.position ;
			pos.y += 0.5f;
			transform.position = pos ;
		}
		if(GameManager.instance.isPassedLevel) {
			return ;
		}
		//按下鼠标左键
		if(Input.GetMouseButtonDown(0)){
			if(!GameManager.instance.isPlaying) {
				GameManager.instance.isPlaying = true ;
				StartMove() ;
			}
			
		}
		if(Input.GetKey(KeyCode.Keypad0)) {
			Vector3 pos = paddle.transform.position;
			pos.x = transform.position.x;
			paddle.transform.position = pos ;
		}

	}

	public void StartMove() {
		int angle = getRandomAngle() ;

		//更平滑
		Vector3 speedNormalized = new Vector3(1.0f, Mathf.Tan(angle * Mathf.Deg2Rad), 0).normalized ;
		rb.velocity = speedNormalized * speed;
	}

	int getRandomAngle() {
		int angle = Random.Range(10, 170) ;
		if(angle > 80 && angle < 100) {
			angle = getRandomAngle() ;
		}
		return angle ;
	}

	//因为球可能同同时接触两个物体，则受到反弹力会叠加影响而改变速度，这里处理一下
	/// <summary>
	/// OnCollisionExit is called when this collider/rigidbody has
	/// stopped touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionExit(Collision other)
	{
		if(GameManager.instance.isPlaying) {
			Vector3 sp = rb.velocity.normalized;
			float angle = Mathf.Asin(sp.y / 1) * Mathf.Rad2Deg ;

			if(angle >= 0 && angle < 10) {
				if(sp.x > 0) {
					Vector3 newVelocity = new Vector3(1f, Mathf.Tan(10 * Mathf.Deg2Rad), 0).normalized ;
					sp = newVelocity ;
				}else {
					Vector3 newVelocity = new Vector3(-1f, Mathf.Tan(10 * Mathf.Deg2Rad), 0).normalized ;
					sp = newVelocity ;
				}
			}else if(angle > 80 && angle <= 90) {
				if(sp.x > 0) {
					Vector3 newVelocity = new Vector3(1f, Mathf.Tan(80 * Mathf.Deg2Rad), 0).normalized ;
					sp = newVelocity ;
				}else {
					Vector3 newVelocity = new Vector3(-1f, Mathf.Tan(80 * Mathf.Deg2Rad), 0).normalized ;
					sp = newVelocity ;
				}
			}else if(angle <= 0 && angle > -10) {
				if(sp.x > 0) {
					Vector3 newVelocity = new Vector3(1f, Mathf.Tan(-10 * Mathf.Deg2Rad), 0).normalized ;
					sp = newVelocity ;
				}else {
					Vector3 newVelocity = new Vector3(-1f, Mathf.Tan(-10 * Mathf.Deg2Rad), 0).normalized ;
					sp = newVelocity ;
				}
			}else if(angle < -80 && angle >= -90) {
				if(sp.x > 0) {
					Vector3 newVelocity = new Vector3(1f, Mathf.Tan(-80 * Mathf.Deg2Rad), 0).normalized ;
					sp = newVelocity ;
				}else {
					Vector3 newVelocity = new Vector3(-1f, Mathf.Tan(-80 * Mathf.Deg2Rad), 0).normalized ;
					sp = newVelocity ;
				}
			}
			
			rb.velocity = sp * speed ;
		}
	}
}
