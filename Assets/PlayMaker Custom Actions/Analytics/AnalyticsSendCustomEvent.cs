// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.Analytics;

using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Analytics")]
	[Tooltip("Sets the gender of the user. Depending on the genre of your project, creating custom segments around gender and age of your users may interest you. Whether you're receiving this information on signup of your project, or from a third-party SDK, eg: Facebook, you can send these demographics to Unity Analytics.")]
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