using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class NPC_Controller : MonoBehaviour
{
	//public string playerInteractText;	might turn into array of strings for text
	[Tooltip("TMP_Text prefab")]
	public GameObject TextBubble;
	[Tooltip("Object player will collect")]
	public GameObject collectible;
	
	private bool playerAbleToInteract;
	private GameObject instantedTextBubble;
	
	abstract public void PlayerInteract();
	
	protected void ShowTextBubble(string userText){
		TMP_Text text = TextBubble.GetComponent(typeof(TMP_Text)) as TMP_Text;
		text.SetText(userText);
		if(!instantedTextBubble){
			instantedTextBubble = Instantiate(TextBubble, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.rotation * Quaternion.Euler(0,180f,0), transform);
		} else {
			RemoveTextBubble();
		}
	}
	
	protected void RemoveTextBubble(){
		if(instantedTextBubble){
			Destroy(instantedTextBubble);
		}
	}
	
	protected void InstantiateCollectible(){
		Instantiate(collectible, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 4), transform.rotation * Quaternion.Euler(-90f,180f,0), transform);
	}
	
	private void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Player"){
			playerAbleToInteract = true;
		}
	}
	
	private void OnTriggerExit(Collider other)
    {
		if(other.tag == "Player"){
			playerAbleToInteract = false;
			RemoveTextBubble();
		}
	}
	
	public bool GetPlayerAbleToInteract(){
		return playerAbleToInteract;
	}
	
	public void SetPlayerAbleToInteract(bool playerAbleToInteract){
		this.playerAbleToInteract = playerAbleToInteract;
	}
}
