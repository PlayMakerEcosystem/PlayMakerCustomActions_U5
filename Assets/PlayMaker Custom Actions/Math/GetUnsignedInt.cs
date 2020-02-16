// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Returns the absolute value of an Int variable and optionally sets it to that.")]
	public class GetUnsignedInt : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Int variable.")]
		public FsmInt intVariable;

		[UIHint(UIHint.Variable)]
		[Tooltip("The unsigned Int.")]
		public FsmInt result;

		[Tooltip("Apply the unsigned value to the int.")]
		public FsmBool applyToOriginal;

		[Tooltip("Repeat every frame. Useful if the Int variable is changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			intVariable = new FsmInt() { UseVariable = true };
			result = null;
			applyToOriginal = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoIntAbs();

			if(!everyFrame) Finish();
		}

		public override void OnUpdate()
		{
			DoIntAbs();
		}

		void DoIntAbs()
		{
			result.Value = Mathf.Abs(intVariable.Value);

			if(applyToOriginal.Value)
				intVariable.Value = result.Value;
		}
	}
}