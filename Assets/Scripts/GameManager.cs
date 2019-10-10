using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public int lifeTimes = 3;

	public bool isPlaying = false ;

	private bool _isPassedLevel = false ;
	public bool isPassedLevel {
		set{
			_isPassedLevel = value;
			if(_isPassedLevel) {
				winPanel.SetActive(true) ;
				Time.timeScale = 0.25f;

				//延时回调，方法名    时间
				Invoke("WinStep2", 0.2f) ;
			}
		}
		get{
			return _isPassedLevel;
		}
	}

	public Text liveText ;

	public GameObject winPanel ;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		instance = this ;
	}

	// Use this for initialization
	void Start () {
		Brick[] allBricks = GameObject.FindObjectsOfType<Brick>();

		foreach(var item in allBricks) {
			if(item.currentBreakType == Brick.Brick_Type.NoBreak) {
				continue ;
			}
			item.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.N)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name) ;
		}
	}

	void WinStep2() {
		Time.timeScale = 1;
		isPlaying = false ;
	}

	public bool IsLastBall{
		get{
			Ball[] allBalls = GameObject.FindObjectsOfType<Ball>();
			return allBalls.Length == 1;
		}
	}

	public void CheckLevelPassed() {
		Brick[] allBricks = GameObject.FindObjectsOfType<Brick>();
		bool tempPassedLevel = true ;

		foreach(var item in allBricks) {
			if(item.enabled == false || item.currentBreakType == Brick.Brick_Type.NoBreak) {
				continue ;
			}
			tempPassedLevel = false ;
			break;
		}
		isPassedLevel = tempPassedLevel ;
	}

	public void ResetBall() {
		isPlaying = false ;

	}

	public void GameOver() {
		if(lifeTimes == 0) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name) ;
		}else {
			ChangeLife(-1) ;
			ResetBall() ;
		}
	}

	public void ChangeLife(int life) {
		lifeTimes += life ;
		liveText.text = "Life : " + lifeTimes ;
    }

}
