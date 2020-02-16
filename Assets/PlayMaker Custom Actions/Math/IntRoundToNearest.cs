// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Rounds an Int Variable to the specified base up or down. If the given Int is 36 and the Base is 10, then the result is 40. Useful for HealthManager or consticted Slider.")]
	public class IntRoundToNearest : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Variable that should be rounded.")]
		public FsmInt intVariable;

		[RequiredField]
		[Tooltip("The Base Value to round from.")]
		public FsmInt baseValue;

		[RequiredField]
		[Tooltip("The rounded int.")]
		public FsmInt result;

		[Tooltip("If it should repeat every frame. (Only useful if the Int Variable changes over time)")]
		public bool everyFrame;

		public override void Reset()
		{
			intVariable = null;
			baseValue = null;
			result = new FsmInt() { UseVariable = true };
			everyFrame = false;
		}

		// Code that runs on entering the state.
		public override void OnEnter()
		{
			DoRound();
			if(!everyFrame)
			{
				Finish();
			}
		}

		// Code that runs every frame.
		public override void OnUpdate()
		{
			DoRound();
		}

		public void DoRound()
		{
			double d = (double)intVariable.Value;
			result.Value = (int)Math.Round(d / baseValue.Value) * baseValue.Value;
		}

	}

}
