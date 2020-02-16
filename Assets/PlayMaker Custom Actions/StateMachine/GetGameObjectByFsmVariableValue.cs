// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Searches for a certain fsm variable in all FSMs and returns the GameObject of the first occurrence.")]
	public class GetGameObjectByFsmVariableValue : FsmStateAction
	{
		[Tooltip("The name of the variable to search for. Leave empty to search for the name of 'Variable Value'.")]
		public FsmString variableName;

		[RequiredField]
		[HideTypeFilter]
		[UIHint(UIHint.Variable)]
		[Tooltip("The value of the variable to search for.")]
		public FsmVar variableValue;

		[Tooltip("Wheter to run every frame or only once.")]
		public FsmBool everyFrame;

		[UIHint(UIHint.Variable)]
		public FsmGameObject result;

		[Tooltip("If no FSM could be found.")]
		public FsmEvent noneFoundEvent;

		public override void Reset()
		{
			variableName = null;
			variableValue = new FsmVar();
			everyFrame = false;
			result = null;
			noneFoundEvent = null;
		}

		public override void OnEnter()
		{
			DoGetGameObjectByFsmVariableValue();

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			DoGetGameObjectByFsmVariableValue();
		}

		private void DoGetGameObjectByFsmVariableValue()
		{
			string varName = string.IsNullOrEmpty(variableName.Value)
							 || variableName.IsNone ? variableValue.variableName : variableName.Value;

			foreach(var fsm in Fsm.FsmList)
			{
				if(fsm.GameObject == Owner) continue;

				NamedVariable foundVar = fsm.Variables.FindVariable(variableValue.Type, varName);

				if(foundVar != null)
					result.Value = fsm.GameObject;
			}

			if(result.Value == null)
				Fsm.Event(noneFoundEvent);
		}
	}
}
