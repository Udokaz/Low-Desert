using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
	[Tooltip("Amount of damage done to the player")]
	public int AttackAmount = 5;
	[Tooltip("Location the enemy will path between")]
	public Transform[] WonderPoints;	
	[Tooltip("The range to target and go after the player")]
	public float TargetRange = 0;
	[Tooltip("The range to target and go after the player")]
	public float AttackRange = 0;
	[Tooltip("Player game object")]
	public GameObject player;
	
	protected NavMeshAgent agent;
	protected bool ableToAttackPlayer = true;

	private int destPoint = 0;
	
	abstract public void AttackPlayer();
	
	
	public void MoveToPoints(){
		
		//get distance bewteen player and enemy
		float distance = Vector3.Distance(player.transform.position, transform.position);
		if(TargetRange > distance && AttackRange < distance){ //go to player
			agent.destination = player.transform.position;
			
		} else if(AttackRange > distance) {
			StopMoving();
			if(ableToAttackPlayer){
				AttackPlayer();
			}
		} else {
			// Choose the next destination point when the agent gets close to the current one.
			if(!agent.pathPending && agent.remainingDistance < 0.5f)
				GoToNextPoint();
		}
	}
	
	/*public void PlayerEnter(Collider other) {
		if(other.tag == "Player"){
			ableToAttackPlayer = true;
		}
	}
	
	public void PlayerExit(Collider other) {
		if(other.tag == "Player"){
			ableToAttackPlayer = false;
		}
	}*/
	
	private void GoToNextPoint() {
		
		// Returns if no points have been set up
		if(WonderPoints.Length == 0){
			Debug.Log("No destination points have been set up");
			return;
		}
		
		// Set the agent to go to the currently selected destination.
        agent.destination = WonderPoints[destPoint].position;
		
		// Choose the next point in the array as the destination
        destPoint = Random.Range(0, WonderPoints.Length);	
	}	
	
	private void StopMoving(){
		agent.destination = transform.position;
		//add in turn to player
	}
}
