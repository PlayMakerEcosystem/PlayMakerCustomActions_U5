using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallbackTest : MonoBehaviour
{

	public static CallbackTest Instance;
	
	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TriggerSomething(UnityAction<string> doneCallback)
	{
		Debug.Log("TriggerSomething");
		_callbackAction = doneCallback;
		
		Invoke("Callback",3f);
	}

	private UnityAction<string> _callbackAction;
	
	void Callback()
	{
		Debug.Log("Callback");
		_callbackAction.Invoke("hello");
	}
	
}
