using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
	
	[Tooltip("Text that will showup at the spot the player is damaged")]
	public GameObject DamageIndicatorText;
	[Tooltip("Color of text when player takes damage")]
	public Color32 DamageColor = new Color32(255, 0, 0, 255); //default red
	[Tooltip("Color of text when player get health")]
	public Color32 RecoveryColor = new Color32(0, 255, 0, 255); //default green
	[Tooltip("Check if the damage text is showing")]
	public bool isTextShowing;
	[Tooltip("Time of how long the text should show")]
	public float CountDown;
	[Tooltip("Amount of damage that will be dealt to the player")]
	public int Damage;
	
	private GameObject instantedTextBubble;
	private float CountDownTimer;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTextShowing){
			CountDownTimer -= Time.deltaTime;
			if(CountDownTimer <= 0f){
				RemoveTextBubble();
			}
		}
    }
	
	private void OnTriggerEnter(Collider other)
    {		
		if(other.gameObject.tag == "Player"){
			ShowDamageText(other);
		}
			
    }	
	
	private void ShowDamageText(Collider other){
		if(isTextShowing)
			RemoveTextBubble();
		isTextShowing = true;
		TMP_Text dmgText = DamageIndicatorText.GetComponent(typeof(TMP_Text)) as TMP_Text;
		dmgText.text = Damage.ToString();
		dmgText.color = DamageColor;
		CountDownTimer = CountDown;
		if(!instantedTextBubble){
			instantedTextBubble = Instantiate(DamageIndicatorText, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), other.gameObject.transform.rotation * Quaternion.Euler(0f,0f,0f), transform);
		}
	}
	
	private void RemoveTextBubble(){
		if(instantedTextBubble){
			Destroy(instantedTextBubble);
			isTextShowing = false;
		}
	}
	
	public int GetDamage(){
		return Damage;
	}
}
