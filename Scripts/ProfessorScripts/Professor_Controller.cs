using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class Professor_Controller : NPC_Controller
{
	[Tooltip("locations that the professor moves between")]
	public Transform[] destinationPoints;
	public string TextStartGame;
	
	private int destPoint = 0;
	private NavMeshAgent agent;
	
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
		if(GetPlayerAbleToInteract()){
			StopMoving();
		} else {
			// Choose the next destination point when the agent gets
			// close to the current one.
			if(!agent.pathPending && agent.remainingDistance < 0.5f)
				GoToNextPoint();			
		}
    }
	
	public override void PlayerInteract(){		
		if(GetPlayerAbleToInteract()){
			base.ShowTextBubble(TextStartGame);
		} else {
			RemoveTextBubble();
		}
	}
	
	void GoToNextPoint() {
		
		// Returns if no points have been set up
		if(destinationPoints.Length == 0){
			Debug.Log("No destination points have been set up for professor");
			return;
		}
		
		// Set the agent to go to the currently selected destination.
        agent.destination = destinationPoints[destPoint].position;
		
		// Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = Random.Range(0, destinationPoints.Length);	
	}
	
	void StopMoving(){
		agent.destination = transform.position;
		//add in turn to player
	}
}
