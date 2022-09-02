using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChickenRacerController : NPC_Controller
{
	
	public string TextPreQuest = "Welcome to the Chicken Race. Where racers ride chickens around the course. Select a chicken to start a race.";
	public string TextDuringQuest = "Select a chicken to start a race.";
	public string TextQuestFinished = "Wow! You were so fast. Here take this as a reward.";
	public string TextAfterQuest = "Would you like to beat your fastest time?";
	public Timer timer;
	public float time;
	public float timeToBeat = 300;
	
	private bool questActive;
	private bool questComplete;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	public override void PlayerInteract(){
		
		if(GetPlayerAbleToInteract()){
			if(!questActive && !questComplete){
				//quest has not started yet. start quest
				SetQuestActive(true);
				ShowTextBubble(TextPreQuest);
			} else if(questActive && !questComplete){
				//quest has started but is not finished.
				if(time < timeToBeat){
					//player won the race, show finish quest text
					ShowTextBubble(TextQuestFinished);
					GiveReward();
				} else {
					//player did not win the race
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
}

