using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventStorer : MonoBehaviour
{
	public UnityEvent storedEvent;

	public void CallStoredEvent(){
		storedEvent.Invoke();
	}

	public void CallStoredEventAfterSomeTime(float waitTime){
		Invoke("CallStoredEvent", waitTime);                      
	}
}