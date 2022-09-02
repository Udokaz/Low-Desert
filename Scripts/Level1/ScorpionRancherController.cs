using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorpionRancherController : NPC_Controller
{
	public ScorpionController[] scorpionControllers;
	public string TextPreQuest;
	public string TextDuringQuest;
	public string TextQuestFinished;
	public string TextAfterQuest;
	public float turnSpeed = 5;
	
	private bool questActive;
	private bool questComplete;
	
	
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		//look at player
		if(GetPlayerAbleToInteract()){
			Vector3 relativeVector = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativeVector);
			Quaternion current = transform.localRotation;
			
			transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * turnSpeed);
			
		} else {
			if(transform.rotation != Quaternion.Euler(0,0,0)){
				Vector3 relativeVector = GameObject.Find("barrel (3)").transform.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation(relativeVector);
				Quaternion current = transform.localRotation;
				
				transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * turnSpeed);
			}
		}
    }
	
	public override void PlayerInteract(){
		
		if(GetPlayerAbleToInteract()){
			if(questActive){//quest is active
			
				//if at lest one scorpion is not homw quest is not complete and break loop
				for(int i = 0; i < scorpionControllers.Length; i++){
					if(!scorpionControllers[i].GetIsHome()){
						questComplete = false;
						break;
					} else 
						questComplete = true;
				}
				
				if(questComplete){
					ShowTextBubble(TextQuestFinished);
					GiveReward();
				} else {
					ShowTextBubble(TextDuringQuest);
				}
				
			} else if(questComplete) { //quest is complete
				ShowTextBubble(TextAfterQuest);
			} else {//quest has not started yet
				ShowTextBubble(TextPreQuest);
				SetQuestActive(true);
			}
		} else {
			RemoveTextBubble();
		}
	}
	
	private void GiveReward() {
		SetQuestActive(false);
		InstantiateCollectible();
	}
	
	private void SetQuestActive(bool questActive){
		for(int i = 0; i < scorpionControllers.Length; i++){
			scorpionControllers[i].SetQuestActive(questActive);
			this.questActive = questActive;
		}
	}	
	
	public bool GetQuestActive(){
		return questActive;
	}
		
}
