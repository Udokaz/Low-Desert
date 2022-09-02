using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StopWatch : MonoBehaviour
{
	public TMP_Text StopWatchTime;
	public TMP_Text Lap1;
	public TMP_Text Lap2;
	public TMP_Text Lap3;
	public TMP_Text FastestLap;
	public bool StopWatchActive;
	//public Color32 normalColor = new Color32(105, 240, 255, 255); //Ice blue
	//public Color32 warningColor = new Color32(255, 255, 0, 255); //yellow
	//public Color32 importantColor = new Color32(255, 0, 0, 255); //red
	
	private float timeValue = 0f;
	private float Lap1Time;
	private float Lap2Time;
	private float Lap3Time;
	private float FastestLapTime;
	private float CurrentLapTime;
	
    // Start is called before the first frame update
    void Start()
    {
		//set it so the timer does not show on start up.
        StopWatchTime.gameObject.SetActive(false);
		Lap1.gameObject.SetActive(false);
		Lap2.gameObject.SetActive(false);
		Lap3.gameObject.SetActive(false);
		FastestLap.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if(StopWatchActive){
			timeValue += Time.deltaTime;
			CurrentLapTime = timeValue;
			StopWatchTime.text = TimeToString(timeValue);
		}
    }
	
	public void StartTimer(){
		StopWatchActive = true;
		StopWatchTime.gameObject.SetActive(true); //show timer
	}
	
	public void EndTimer(){
		StopWatchActive = false;
		StopWatchTime.gameObject.SetActive(false);//hide timer
	}
	
	public bool GetStopWatchTime(){
		return StopWatchTime;
	}
	
	public void ResetTimer(){
		timeValue = 0f;
	}
	
	public float GetTime(){
		return timeValue;
	}
	
	public void SetLap(){
		if(Lap1Time == 0){//there is no laps yet
			Lap1Time = CurrentLapTime;
			Lap1.text = TimeToString(Lap1Time);
		} else if(Lap2Time == 0){//only lap1 has be set
			Lap2Time = CurrentLapTime;
			Lap2.text = TimeToString(Lap2Time);
		} else if(Lap3Time == 0){//laps1 and 2 have been set
			Lap3Time = CurrentLapTime;
			Lap3.text = TimeToString(Lap3Time);
		} else {//all laps have been set. remove the first and shift the others 
			Lap1Time = Lap2Time;
			Lap2Time = Lap3Time;
			Lap3Time = CurrentLapTime;
			Lap1.text = TimeToString(Lap1Time);
			Lap2.text = TimeToString(Lap2Time);
			Lap3.text = TimeToString(Lap3Time);
		}
		
		if(CurrentLapTime < FastestLapTime)
			FastestLapTime = CurrentLapTime;
		
		CurrentLapTime = 0f;
	}
	
	private string TimeToString(float time){
		int mins = (int)time / 60;
		int seconds = (int)time % 60;
		if(seconds < 10){
			return mins.ToString() + ":0" + seconds.ToString();
		} else {
			return mins.ToString() + ":" + seconds.ToString();
		}
	}
	
}

