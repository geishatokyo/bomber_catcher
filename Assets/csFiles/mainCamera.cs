using UnityEngine;
using System.Collections;

public class mainCamera : MonoBehaviour {

	public float speed = 0.1f;
	private Vector3 vec;
	
	// bomb
	private bool check = false, check2 = false;
	private int i;
	private GameObject layer, layerSet;
	public GameObject[] BombPatarns;
	
	// timer
	public const float INTERVAL = 0.1f;
	public float timer = INTERVAL;
	private int count = 0;
	
	// Use this for initialization
	void Start () {
		layer = GameObject.Find("Layer");
		layerSet = GameObject.Find("LayerSet");
		layer.SetActive(false);
		layerSet.SetActive(false);
		for(i=0; i<BombPatarns.Length; i++){
			BombPatarns[i] = Instantiate(BombPatarns[i])as GameObject;
			BombPatarns[i].transform.parent = transform;
			BombPatarns[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (check2 && (Input.GetKeyUp(KeyCode.A) || Input.GetMouseButtonUp(0))){
			check2 = false;
			check = true;
			BombPatarns[count].transform.position = layerSet.transform.position;
			BombPatarns[count].SetActive(true);
			Time.timeScale = 1.0f;
		}
		else if(Input.GetKeyUp(KeyCode.A)){
			layer.SetActive(false);
			layerSet.SetActive(false);
			Time.timeScale = 1.0f;
		}
		if (Time.timeScale == 0.5f){
			if (Input.GetMouseButtonDown(0)){
				layer.SetActive(false);
				layerSet.SetActive(true);
				check2 = true;
			}
			if (Input.GetMouseButton(0)){
				vec = Input.mousePosition;
				vec.z += 5;
				layerSet.transform.position = camera.ScreenToWorldPoint(vec);
			}	
		}
	}
	
	void FixedUpdate () {
		if (check){
			timer -= Time.deltaTime;
			if(timer <= 0){
				BombPatarns[count].SetActive(false);
				count++;
				BombPatarns[count].transform.position = layerSet.transform.position;
				BombPatarns[count].SetActive(true);
				timer = INTERVAL;
			}
			if(count == BombPatarns.Length-1){
				BombPatarns[count].SetActive(false);
				count = 0;
				layer.SetActive(false);
				layerSet.SetActive(false);
				check = false;
			}
		}
		else{
			transform.position += new Vector3(speed, 0, 0);
			if (Input.GetKeyDown(KeyCode.A)){
				layer.SetActive(true);
				Time.timeScale = 0.5f;
			}
		}
	}
}