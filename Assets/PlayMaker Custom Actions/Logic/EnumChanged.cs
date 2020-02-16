// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: skipadu

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=18567.0")]
	[Tooltip("Tests if the value of a Enum Variable has changed")]
	public class EnumChanged : FsmStateAction 
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Enum variable to watch for changes.")]
		public FsmEnum enumVariable;

		[MatchFieldType("enumVariable")]
		[Tooltip("If wanted to check only for certain enum's state. If not given, we will look changes to whatever possible value.")]
		public FsmEnum wantedEnumValue;

		[Tooltip("Event to send if the variable changes.")]
		public FsmEvent changedEvent;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Set to True if changed.")]
		public FsmBool storeResult;
		
		private Enum previousValue;
		
		public override void Reset() {
			enumVariable = new FsmEnum { UseVariable = true };
			wantedEnumValue = new FsmEnum { UseVariable = true };
			changedEvent = null;
			storeResult = null;
			previousValue = null;
		}

		public override void OnEnter()
		{
			if(enumVariable.IsNone) {
				Finish();
				return;
			}
			previousValue = enumVariable.Value;
		}
		
		public override void OnUpdate() {
			storeResult.Value = false;
			
			if(!enumVariable.Value.Equals(previousValue)) {
				if(!wantedEnumValue.IsNone) {
					if( enumVariable.Value.Equals(wantedEnumValue.Value) ) {
						EnumWasChanged();
					}
				}
				else {
					EnumWasChanged();
				}
			}
		}
		
		void EnumWasChanged() {
			storeResult.Value = true;
			Fsm.Event(changedEvent);
		}

	}

}
