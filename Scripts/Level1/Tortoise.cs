using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tortoise : EnemyController
{
	
	private Animator animator;
	
	private int animIDWalk;
	private int animIDBite;
	private bool hasAnimator;
	private float startTime;
		
    // Start is called before the first frame update
    void Start() {
		hasAnimator = TryGetComponent(out animator);
		AssignAnimationIDs();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //turn off autoBraking inbetween points, wont slow down when approaching next point
		agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update() {
		hasAnimator = TryGetComponent(out animator);
        MoveToPoints();
		if(animator.GetBool(animIDBite)){
			startTime += Time.deltaTime;
			EndAttack();
		}
    }
	
	private void AssignAnimationIDs() {
		animIDWalk = Animator.StringToHash("Walk");
		animIDBite = Animator.StringToHash("Bite");
	}
	
	public override void AttackPlayer() {
		if(hasAnimator){
			animator.SetBool(animIDBite, true);
			startTime = Time.deltaTime;
			ableToAttackPlayer = false;
		}
	}
	
	private void EndAttack(){
		if((startTime > 3)){
			player.gameObject.GetComponent<PlayerHealthController>().PlayerAttacked(AttackAmount);
			animator.SetBool(animIDBite, false);
			ableToAttackPlayer = true;
			startTime = 0;
		}
	}
	
	private void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			//PlayerEnter(other);
			AttackPlayer();
		}
	}
	
	private void OnTriggerExit(Collider other) {
		if(other.tag == "Player"){
			//PlayerExit(other);
		}
	}
}
