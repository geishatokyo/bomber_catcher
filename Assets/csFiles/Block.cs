using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block: MonoBehaviour {
	
	// floor
	public GameObject Original;
	private GameObject Clone;
	private float f, move = 1.0f;
	
	// BlockPatarns is
	// * Created of 10*10 size
	// * until 1 child
	public GameObject[] BlockPatarns;
	private GameObject temp;
	private int i;
	public int stageLength = 200;
	
	// bomb
	private Vector2 Me, Enemy;
	private List<GameObject> allBlocks = new List<GameObject>();
	private List<GameObject> destroyBlocks = new List<GameObject>();
	private bool mouseUpDown = true;
	
	// Use this for initialization
	void Start () {
		for(f=0; f<stageLength; f+=move){
			Clone = Instantiate(Original, new Vector3(transform.localPosition.x+f, transform.localPosition.y, 0), transform.rotation)as GameObject;
			Clone.transform.parent = transform;
			Clone = Instantiate(Original, new Vector3(transform.localPosition.x+f, transform.localPosition.y-1.0f, 0), transform.rotation)as GameObject;
			Clone.transform.parent = transform;
			Clone = Instantiate(Original, new Vector3(transform.localPosition.x+f, transform.localPosition.y+11.0f, 0), transform.rotation)as GameObject;
			Clone.transform.parent = transform;
		}
		for(i=11; i<stageLength; i++){
			if(i%10 == 0){
				temp = Instantiate(BlockPatarns[Random.Range(0, BlockPatarns.Length)], new Vector3(i, 0, 0), transform.rotation)as GameObject;
				temp.transform.parent = transform;
				allBlocks.Add(temp);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.A) && Input.GetMouseButtonUp(0) && mouseUpDown){
			mouseUpDown = false;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			foreach(var block in allBlocks){
				RaycastHit raycastHit;
				for(i=0; i < block.transform.childCount; i++){
					bool hit = block.transform.GetChild(i).GetComponent<BoxCollider>().Raycast(ray,out raycastHit,100);
					if(hit){
						Me = new Vector2(block.transform.GetChild(i).localPosition.x, block.transform.GetChild(i).localPosition.y);
						for(i=0; i<block.transform.childCount; i++){
							Enemy = new Vector2(block.transform.GetChild(i).localPosition.x, block.transform.GetChild(i).localPosition.y);
							if(Me.x+2.5f >= Enemy.x && Me.x-2.5f <= Enemy.x && Me.y+2.5f >= Enemy.y && Me.y-2.5f <= Enemy.y){
								destroyBlocks.Add(block.transform.GetChild(i).gameObject);
							}
							else if(Me.x+3.0f >= Enemy.x && Me.x-3.0f <= Enemy.x && Me.y+2.0f >= Enemy.y && Me.y-2.0f <= Enemy.y){
								destroyBlocks.Add(block.transform.GetChild(i).gameObject);
							}
							else if(Me.x+2.0f >= Enemy.x && Me.x-2.0f <= Enemy.x && Me.y+3.0f >= Enemy.y && Me.y-3.0f <= Enemy.y){
								destroyBlocks.Add(block.transform.GetChild(i).gameObject);
							}
						}
					}
				}
			}
			
			foreach(var dustbin in destroyBlocks){
				Destroy(dustbin);
			}
		}
		else if(Input.GetKeyUp(KeyCode.A)){
			mouseUpDown = true;
		}
	}
}
