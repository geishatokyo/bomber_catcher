using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	// score
	private float score = 0.0f;
	private GUIStyle style;
	
	// timer
	public const float INTERVAL = 0.1f;
	public float timer = INTERVAL;
	
	// animation
	private int count = 0;
	public GameObject[] characterFigures;
	
	// jump
	public float force = 5.0f;
	
	// collision, itween
//	private bool comeBack = true;
	
	void Start () {
		style = new GUIStyle();
		style.fontSize = 30;
		for(int i=0; i<characterFigures.Length; i++){
			characterFigures[i] = Instantiate(characterFigures[i])as GameObject;
			characterFigures[i].transform.parent = transform;
			characterFigures[i].transform.localPosition = Vector3.zero;
			characterFigures[i].transform.localScale = Vector3.one;
			characterFigures[i].SetActive(false);
		}
		characterFigures[0].SetActive(true);
	}
	
	void Update(){
		timer -= Time.deltaTime;
		if(timer <= 0){
			characterFigures[count].SetActive(false);
			count = (count + 1) % characterFigures.Length;
			characterFigures[count].SetActive(true);
			timer = INTERVAL;
		}
		if(transform.localPosition.x < -7.5){
			Application.LoadLevel("titleScene");
		}
	}
	
	void FixedUpdate(){
		score += 0.01f;
		if(Input.GetKeyDown(KeyCode.Z)){
			transform.rigidbody.velocity = new Vector3(0, force, 0);
		}
/*		if(comeBack && (transform.localPosition.x > -5 || transform.localPosition.x < -7)){
			iTween.MoveTo(transform.gameObject , iTween.Hash("x", -6, "islocal", true), 3.0f);
		}
*/	}
	
	void OnGUI(){
		Rect rect = new Rect(10, 10, 400, 300);
		GUI.Label(rect, "score : " + score, style);
	}
}