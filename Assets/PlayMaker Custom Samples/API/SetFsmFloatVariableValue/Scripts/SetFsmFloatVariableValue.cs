using HutongGames.PlayMaker;
using UnityEngine;

public class SetFsmFloatVariableValue : MonoBehaviour
{

	public PlayMakerFSM Fsm;

	public string FsmFloatVariableName = "My Fsm Float";
	public float SetFsmFloatValue;

	private FsmFloat _fsmFloatVariable;
	
	void Start () {
		if (Fsm == null)
		{
			Debug.LogError("Missing Fsm reference");
			return;
		}

		_fsmFloatVariable = Fsm.Fsm.GetFsmFloat(FsmFloatVariableName);

		if (_fsmFloatVariable == null)
		{
			Debug.LogError("<"+FsmFloatVariableName+"> could not be found in Fsm");
			return;
		}

		SetValue();
	}
	
	// Update is called once per frame
	void Update ()
	{
		SetValue();
	}

	void SetValue()
	{
		if (_fsmFloatVariable != null)
		{
			_fsmFloatVariable.Value = SetFsmFloatValue;
		}
	}
}
