// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
					]
}
EcoMetaEnd
// Keywords: ABS invert
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets a Float variable to its absolute value.")]
	public class FloatAbsAdvanced : FsmStateActionAdvanced
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The Float variable.")]
		public FsmFloat floatVariable;

		[Tooltip("The multiplier. Set to -1 to force the abs result to be negative for example")]
		public FsmFloat multiplier;
		

		public override void Reset()
		{
			base.Reset();
			floatVariable = null;
			multiplier = new FsmFloat(){UseVariable = true};
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoFloatAbs();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoFloatAbs();
		}
		
		void DoFloatAbs()
		{
			floatVariable.Value = Mathf.Abs(floatVariable.Value) * (multiplier.IsNone?1f:multiplier.Value);
		}
	}
}