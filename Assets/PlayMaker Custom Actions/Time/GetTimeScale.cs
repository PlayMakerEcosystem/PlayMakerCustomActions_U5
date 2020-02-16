// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the current TimeScale (the time at wich the game operates at).")]
	public class GetTimeScale : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the current GameTime (1 by default).")]
		public FsmFloat currentTimeScale;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the intervall (in s) at wich physics and other fixed frame rate operates.")]
		public FsmFloat fixedDeltaTime;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			currentTimeScale = new FsmFloat() { UseVariable = true };
			fixedDeltaTime = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoTimeScale();

			if(!everyFrame)
			{
				Finish();
			}
		}
		public override void OnUpdate()
		{
			DoTimeScale();
		}

		void DoTimeScale()
		{
			currentTimeScale.Value = Time.timeScale;
			fixedDeltaTime.Value = Time.fixedDeltaTime;
		}
	}
}
