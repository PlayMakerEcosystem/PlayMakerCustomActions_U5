// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: multi, multiple
// Source http://hutonggames.com/playmakerforum/index.php?topic=9989.0

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets the value of many Bool Variable.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9989.0")]
	public class SetMultiBoolValue : FsmStateAction
	{
		[CompoundArray("Count", "Bool Variable", "Value")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmBool[] boolVariable;
		public FsmBool[] values;
		public bool everyFrame;
		
		public override void Reset()
		{
			boolVariable = new FsmBool[1];
			values = new FsmBool[1];
			everyFrame = false;
		}
		

		public override void OnEnter()
		{
			DoSetBoolValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetBoolValue();
		}

		public void DoSetBoolValue()
		{
			for(int i = 0; i<boolVariable.Length;i++){
				if(!boolVariable[i].IsNone || !boolVariable[i].Value.Equals(false)) 
					boolVariable[i].Value = values[i].Value;
			}
	
		}
	}
}
