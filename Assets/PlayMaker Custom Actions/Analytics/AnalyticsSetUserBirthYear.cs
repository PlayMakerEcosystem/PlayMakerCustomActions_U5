// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.Analytics;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Analytics")]
	[Tooltip("Sets the birthYear of the user. Depending on the genre of your project, creating custom segments around gender and age of your users may interest you. Whether you're receiving this information on signup of your project, or from a third-party SDK, eg: Facebook, you can send these demographics to Unity Analytics.")]
	public class AnalyticsSetUserBirthYear : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Birth year of user. Must be 4-digit year format, only.")]
		public FsmInt birthYear;

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
			birthYear = null;
			result = AnalyticsResult.AnalyticsDisabled;
			success = null;
			failure = null;
		}

		public override void OnEnter()
		{
			AnalyticsResult _result = Analytics.SetUserBirthYear(birthYear.Value);

			if (!result.IsNone) {
				result.Value = _result;
			}

			Fsm.Event(_result == AnalyticsResult.Ok?success:failure);

			Finish();		
		}

	}
}