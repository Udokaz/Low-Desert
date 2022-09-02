using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
	public TMP_Text TimerValue;
	public float timeValue = 70;
	public bool timerActive;
	public Color32 normalColor = new Color32(105, 240, 255, 255); //Ice blue
	public Color32 warningColor = new Color32(255, 255, 0, 255); //yellow
	public Color32 importantColor = new Color32(255, 0, 0, 255); //red
	
    // Start is called before the first frame update
    void Start()
    {
		//set it so the timer does not show on start up.
        //TimerValue.gameObject.SetActive(false);
		TimerValue.color = normalColor;
    }

    // Update is called once per frame
    void Update()
    {
		if(timerActive){
			int mins = (int)timeValue / 60;
			int seconds = (int)timeValue % 60;
			timeValue -= Time.deltaTime;
			
			if(seconds < 10){
				TimerValue.text = mins.ToString() + ":0" + seconds.ToString();
			} else {
				TimerValue.text = mins.ToString() + ":" + seconds.ToString();
			}
			
			if(timeValue >= 10){
				
			} else if(timeValue < 10 && timeValue > 5){
				TimerValue.color = warningColor; //set color to warning color
			} else if(timeValue <= 5 && timeValue > 0){
				
				//start of the second make the text larger
				if((timeValue % 60 - seconds) > 0.9f){
					TimerValue.fontSize = 50;
				}
				
				//start of the second make the text smaller
				if((timeValue % 60 - seconds) < 0.1f){
					TimerValue.fontSize = 36;
				}
				TimerValue.color = importantColor;//set color to 
				
			} else if(timeValue == 0){
				TimerValue.fontSize = 55;
				TimerValue.text = "00:00";				
			} else {
				EndTimer();
			}
		}
    }
	
	public void StartTimer(){
		timerActive = true;
		TimerValue.gameObject.SetActive(true); //show timer
	}
	
	public void EndTimer(){
		timerActive = false;
		TimerValue.gameObject.SetActive(false);//hide timer
	}	
	
	public void SetTimerActive(bool timerActive){
		this.timerActive = timerActive;
	}
	
	public bool GetTimerActive(){
		return timerActive;
	}
	
}
