using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthController : MonoBehaviour
{	
	[Tooltip("Text of the Total health of the player")]
	public TMP_Text totalHealth;
	[Tooltip("Slider bar for the players health")]
	public Slider healthSlider;	
		
	private int health = 100;
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health;		
    }
	
	private void OnTriggerEnter(Collider other)
    {		
		if(other.gameObject.tag == "EnviromentDamage"){
			PlayerAttacked(other.gameObject.GetComponent<DamageIndicator>().GetDamage());			
		}
			
    }
	
	private void die(){
		Debug.Log("You Have Died");
	}
	
	public void PlayerAttacked(int damage){
		health -= damage;
		totalHealth.text = health.ToString();
			
		if(health <= 0){
			die();
		}
	}
}
