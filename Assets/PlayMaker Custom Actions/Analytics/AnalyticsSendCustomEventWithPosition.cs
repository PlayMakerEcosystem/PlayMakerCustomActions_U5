// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if UNITY_5_4_OR_NEWER

using UnityEngine;

using UnityEngine.Analytics;

using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Analytics")]
	[Tooltip("Unity Analytics allows you to track specific events within your game. By configuring a series of Custom Events within your game, you can create your own Funnel Analysis to observe your players' game behavior. Good places to put custom events include: milestones, new levels, scene transitions, etc.")]
	public class AnalyticsSendCustomEventWithPosition : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the event")]
		public FsmString eventName;

		[Tooltip("The GameObject's position")]
		public FsmOwnerDefault gameObjectPosition;

		[Tooltip("Or The position as vector3")]
		public FsmVector3 OrVector3position;

		[ActionSection("Result")]

		[Tooltip("Result")]
		[ObjectType(typeof(AnalyticsResult))]
		[UIHint(UIHint.Variable)]
		public FsmEnum result;

		[Tooltip("Event Sent if execution went OK")]
		public FsmEvent success;

		[Tooltip("Event Sent if execution failed. Check result for more infos")]
		public FsmEvent failure;

		public override void Reset()
		{
			eventName = null;
			gameObjectPosition = null;
			OrVector3position = new FsmVector3 (){ UseVariable = true };

			result = AnalyticsResult.AnalyticsDisabled;
			success = null;
			failure = null;
		}

		public override void OnEnter()
		{
			Vector3 _position = Vector3.zero;

			var go = Fsm.GetOwnerDefaultTarget(gameObjectPosition);

			if (go == null) {
				_position = go.transform.position;
			} else if(!OrVector3position.IsNone){
				_position = OrVector3position.Value;
			}
				
			AnalyticsResult _result;

			if (go ==null && OrVector3position.IsNone) {
				_result = Analytics.CustomEvent (eventName.Value);
			} else {
				
				_result = Analytics.CustomEvent (eventName.Value,_position);
			}

			if (!result.IsNone) {
				result.Value = _result;
			}

			Fsm.Event(_result == AnalyticsResult.Ok?success:failure);

			Finish();		
		}

	}
}

#endif