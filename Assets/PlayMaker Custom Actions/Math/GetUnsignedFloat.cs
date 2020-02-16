// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Returns the absolute value of an Float variable and optionally sets it to that.")]
	public class GetUnsignedFloat : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Float variable.")]
		public FsmFloat floatVariable;

		[UIHint(UIHint.Variable)]
		[Tooltip("The unsigned Float.")]
		public FsmFloat result;

		[Tooltip("Apply the unsigned value to the float.")]
		public FsmBool applyToOriginal;

		[Tooltip("Repeat every frame. Useful if the Float variable is changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			floatVariable = new FsmFloat() { UseVariable = true };
			result = null;
			applyToOriginal = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoFloatAbs();

			if(!everyFrame) Finish();
		}

		public override void OnUpdate()
		{
			DoFloatAbs();
		}

		void DoFloatAbs()
		{
			result.Value = Mathf.Abs(floatVariable.Value);

			if(applyToOriginal.Value)
				floatVariable.Value = result.Value;
		}
	}
}