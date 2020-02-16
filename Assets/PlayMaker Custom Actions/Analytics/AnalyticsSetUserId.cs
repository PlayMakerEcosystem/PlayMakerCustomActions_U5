// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.Analytics;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Analytics")]
	[Tooltip("Sets the userId of the user.")]
	public class AnalyticsSetUserId : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The User Id")]
		public FsmString userId;

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
			userId = null;
			result = AnalyticsResult.AnalyticsDisabled;

			success = null;
			failure = null;
		}

		public override void OnEnter()
		{
			AnalyticsResult _result = Analytics.SetUserId(userId.Value);

			if (!result.IsNone) {
				result.Value = _result;
			}

			Fsm.Event(_result == AnalyticsResult.Ok?success:failure);

			Finish();		
		}

	}
}