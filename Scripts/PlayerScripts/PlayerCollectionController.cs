using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerCollectionController : MonoBehaviour
{
	public TMP_Text CatchemCounter;
	public IceCreamSellerController iceCreamSellerController;
	public PlayerInput chickenInput;
	
	private int totalCollectedCatcems = 0;	
	private GameObject _mainCamera;
	private int totalCatchems;
	
	
	private void Awake()
	{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
	}
	
    // Start is called before the first frame update
    void Start()
    {
		totalCatchems = GameObject.FindGameObjectsWithTag("Coin").Length;
        
    }

    // Update is called once per frame
    void Update()
    {		
		if(totalCatchems == totalCollectedCatcems)
			Debug.Log("Yeaaah you did it. You collected them all");
    }
	
	void FixedUpdate() {
		
	}
	
	public void Interact(){
		RaycastHit hit;
		
		//used to pick up objects
		if(Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, Mathf.Infinity, LayerMask.GetMask("Collections"))){
			//check that the object is close enough to pick up
			if(hit.distance < 5.5){
				//hit.collider.gameObject.GetComponent<CollectionController>().PlayerInteract();
				if(hit.collider.gameObject.name == "Ice"){
					if(!iceCreamSellerController.GetPlayerHasIce() && iceCreamSellerController.GetQuestActive() && !iceCreamSellerController.GetQuestComplete()){
						//add icon for ice to UI
						iceCreamSellerController.SetPlayerHasIce(true);
						Debug.Log("Ice icon added");
					}
				}
			}
		}
		
		//used to interact with NPCs
		if(Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, Mathf.Infinity, LayerMask.GetMask("NPCs"))){
			//check that the NPC is close enough to talk to
			if(hit.distance < 5.5){
				hit.collider.gameObject.GetComponent<NPC_Controller>().PlayerInteract();
			} else {
				
			}
			
		}
	}
	
	private void CheckEnemy(){
		RaycastHit hit;
		
		//used for interacting with enemys
		LayerMask EnemyLayerMask = LayerMask.GetMask("Enemys");
		if(Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, Mathf.Infinity, EnemyLayerMask)){
			//check that the enemy is close enough to hit
			if(hit.distance < 5.5){
				Debug.Log("Enemey Hit");
			}
		}
	}
	
	
	private void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.tag == "Coin"){
			Debug.Log("Coin: " + other.gameObject.tag);
			Destroy(other.gameObject, 0);
			totalCollectedCatcems++;
			CatchemCounter.text = totalCollectedCatcems.ToString();
			
		}		
    }
}
