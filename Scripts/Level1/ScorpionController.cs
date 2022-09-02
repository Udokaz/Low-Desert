using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScorpionController : NPC_Controller
{
	public Transform[] destinationPoints;
	public bool questActive;
	public Transform ScorpionHomeDestination;
	public float TooFarDistance;
	public float TooCloseDistance;
	public float ScorpionHomeDistance;
	
	private GameObject player;
	private int destPoint = 0;
	private NavMeshAgent agent;
	private bool followPlayer;
	public bool isHome;
	
	// Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //turn off autoBraking inbetween points, wont slow down when approaching next point
		agent.autoBraking = false; 
    }    
	
	// Update is called once per frame
    void Update()
    {
		if(followPlayer){
			//Check if player has been set up yet
			if(player){
				//get the distance between the player and the scorpion
				float dist = Vector3.Distance(player.transform.position, transform.position);
				if(dist > TooFarDistance){//scorpion is to far and will stop waiting for the player to come back
					StopMoving();
				} else if (dist < TooCloseDistance) { // scorpion is getting to close and will stop/slowdown
					StopMoving();
				} else {//follows player
					agent.destination = player.transform.position; //if scorpion is not too far or too close go to player
				}
			}
		} else if(isHome && ScorpionHomeDistance >= Vector3.Distance(agent.destination, ScorpionHomeDestination.position)) {
			agent.destination = ScorpionHomeDestination.position;
		} else {
			if(GetPlayerAbleToInteract()){
				StopMoving();
			} else {
				// Choose the next destination point when the agent gets close to the current one.
				if(!agent.pathPending && agent.remainingDistance < 0.5f)
					GoToNextPoint();
			}
		}
    }

	public override void PlayerInteract(){
		if(GetPlayerAbleToInteract() && questActive){
			agent.destination = player.transform.position;
			followPlayer = true;
		}
	}
	
	public bool GetIsHome(){
		return isHome;
	}
	
	public void SetQuestActive(bool questActive){
		this.questActive = questActive;
	}
	
	private void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Player"){
			SetPlayerAbleToInteract(true);
			player = other.gameObject;
		}
		
		if(other.gameObject.name == "ScorpionHome"){
			GoToHome();
		}
	}
	
	void GoToNextPoint() {
		
		// Returns if no points have been set up
		if(destinationPoints.Length == 0){
			Debug.Log("No destination points have been set up for scorpion");
			return;
		}
		
		// Set the agent to go to the currently selected destination.
        agent.destination = destinationPoints[destPoint].position;
		
		// Choose the next point in the array as the destination
        destPoint = Random.Range(0, destinationPoints.Length);	
	}

	void StopMoving(){
		agent.destination = transform.position;
		//add in turn to player
	}	
	
	void GoToHome(){
		followPlayer = false;
		isHome = true;
		agent.destination = ScorpionHomeDestination.position;
	}
}
