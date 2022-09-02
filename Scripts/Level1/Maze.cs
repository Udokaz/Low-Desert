using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
	private bool inMaze = false;
	public GameObject exitBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void EnterMaze(){
		inMaze = true;
		Collider collider = exitBox.GetComponent(typeof(BoxCollider)) as BoxCollider;
		collider.isTrigger = true;
	}
	
	public void ExitMaze(){
		inMaze = false;
		Collider collider = exitBox.GetComponent(typeof(BoxCollider)) as BoxCollider;
		collider.isTrigger = false;
	}
	
	public bool GetInMaze(){
		return inMaze;
	}
	
	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.name == "PlayerArmature"){
			if(inMaze){
				ExitMaze();
			} else {
				EnterMaze();
			}
		}
	}
}
