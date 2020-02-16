// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.Analytics;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Analytics")]
	[Tooltip("Sets the gender of the user. Depending on the genre of your project, creating custom segments around gender and age of your users may interest you. Whether you're receiving this information on signup of your project, or from a third-party SDK, eg: Facebook, you can send these demographics to Unity Analytics.")]
	public class AnalyticsSetUserGender : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Gender of the user")]
		[ObjectType(typeof(Gender))]
		public FsmEnum gender;

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
			gender = null;
			result = AnalyticsResult.AnalyticsDisabled;
			success = null;
			failure = null;
		}

		public override void OnEnter()
		{
			AnalyticsResult _result = Analytics.SetUserGender((Gender)gender.Value);

			if (!result.IsNone) {
				result.Value = _result;
			}

			Fsm.Event(_result == AnalyticsResult.Ok?success:failure);

			Finish();		
		}

	}
}