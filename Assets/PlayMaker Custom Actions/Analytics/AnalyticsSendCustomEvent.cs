// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.Analytics;

using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Analytics")]
	[Tooltip("Unity Analytics allows you to track specific events within your game. By configuring a series of Custom Events within your game, you can create your own Funnel Analysis to observe your players' game behavior. Good places to put custom events include: milestones, new levels, scene transitions, etc.")]
	public class AnalyticsSendCustomEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the event")]
		public FsmString eventName;

		[CompoundArray("EventData", "Key", "Value")]
		public FsmString[] keys;
		public FsmVar[] values;

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
			keys = null;
			values = null;
			result = AnalyticsResult.AnalyticsDisabled;
			success = null;
			failure = null;
		}

		public override void OnEnter()
		{
			AnalyticsResult _result;

			if (keys.Length == 0) {
				_result = Analytics.CustomEvent(eventName.Value,null);
			} else {
				Dictionary<string,object> _data = new Dictionary<string, object> ();

				for (int i = 0; i < keys.Length; i++) {
					
					values[i].UpdateValue ();

					_data.Add (keys [i].Value, values [i].GetValue());
				}

				_result = Analytics.CustomEvent (eventName.Value,_data);
			}

			if (!result.IsNone) {
				result.Value = _result;
			}

			Fsm.Event(_result == AnalyticsResult.Ok?success:failure);

			Finish();		
		}

	}
}