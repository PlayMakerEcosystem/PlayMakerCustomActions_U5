// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: multi, multiple
// Source http://hutonggames.com/playmakerforum/index.php?topic=9989.0


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets the value of many Float Variable.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9989.0")]
	public class SetMultiFloatValue : FsmStateAction
	{
		[CompoundArray("Count", "Float Variable", "Value")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat[] floatVariable;
		public FsmFloat[] values;
		public bool everyFrame;
		
		public override void Reset()
		{
			floatVariable = new FsmFloat[1];
			values = new FsmFloat[1];
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetFloatValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetFloatValue();
		}
		
		public void DoSetFloatValue()
		{
			for(int i = 0; i<floatVariable.Length;i++){
				if(!floatVariable[i].IsNone || !floatVariable[i].Value.Equals("")) 
					floatVariable[i].Value = values[i].IsNone ? 0f : values[i].Value;
			}
		}
	}
}
