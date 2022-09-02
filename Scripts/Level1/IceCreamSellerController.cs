using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IceCreamSellerController : NPC_Controller
{
	
	public string TextPreQuest;
	public string TextDuringQuest;
	public string TextQuestFinished;
	public string TextAfterQuest;
	public bool playerHasIce;	
	public Timer timer;
	public float turnSpeed = 5;
	
	private bool questActive;
	private bool questComplete;
	private Transform originalRotationValue;
	
    // Start is called before the first frame update
    void Start()
    {
		originalRotationValue = transform;
    }

    // Update is called once per frame
    void Update()
    {
		
		//Player has held ice to long and it has melted. 
		if(GetPlayerHasIce() && !timer.GetTimerActive()){
			SetPlayerHasIce(false);
		}
		
		//look at player
		if(GetPlayerAbleToInteract()){
			Vector3 relativeVector = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativeVector);
			Quaternion current = transform.localRotation;
			
			transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * turnSpeed);
			
		} else {
			if(transform.rotation != Quaternion.Euler(0,-90,0)){
				Vector3 relativeVector = GameObject.Find("IceCreamCart").transform.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation(relativeVector);
				Quaternion current = transform.localRotation;
				
				transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * turnSpeed);
			}
		}
    }
	
	public override void PlayerInteract(){		
		if(GetPlayerAbleToInteract()){
			if(!questActive && !questComplete){
				//quest has not started yet. start quest
				SetQuestActive(true);
				ShowTextBubble(TextPreQuest);
			} else if(questActive && !questComplete){
				//quest has started but is not finished.
				if(playerHasIce){
					//player has ice, show finish quest text					
					playerHasIce = false;//remove the flag for ice.
					ShowTextBubble(TextQuestFinished);
					GiveReward();
				} else {
					//player does not have ice, show still needing ice text
					ShowTextBubble(TextDuringQuest);
				}
			} else if(!questActive && questComplete){
				//quest is complete
				ShowTextBubble(TextAfterQuest);
			} else {
				//Not possible
				Debug.Log("Should not be able to get here");
			}
		} else {
			RemoveTextBubble();
		}
	}
	
	private void GiveReward() {
		
		SetQuestActive(false);
		SetQuestComplete(true);
		InstantiateCollectible();//create a new colletible
		timer.EndTimer();//stop the timer
	}
	
	public void SetQuestActive(bool questActive){
		this.questActive = questActive;
	}
	
	public bool GetQuestActive(){
		return questActive;
	}
	
	public void SetQuestComplete(bool questComplete){
		this.questComplete = questComplete;
	}
	
	public bool GetQuestComplete(){
		return questComplete;
	}
	
	public void SetPlayerHasIce(bool playerHasIce){
		this.playerHasIce = playerHasIce;
		timer.StartTimer();//when player picks up ice start countdown.
	}
	
	public bool GetPlayerHasIce(){
		return playerHasIce;
	}
}
