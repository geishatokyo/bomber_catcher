using UnityEngine;
using System.Collections;

public class titlePlay : MonoBehaviour {

	private GUIStyle style;
	
	void Start(){
		style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Italic;
	}
	void OnGUI(){
		Rect rect = new Rect(220, 110, 400, 300);
		GUI.Label(rect, "Play !!", style);
		rect = new Rect(230, 250, 400, 300);
		GUI.Label(rect, "Score", style);
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			bool hit = transform.GetComponent<BoxCollider>().Raycast(ray,out raycastHit,100);
			if(hit){
				Application.LoadLevel("mainScene");
			}
		}
	}
}
