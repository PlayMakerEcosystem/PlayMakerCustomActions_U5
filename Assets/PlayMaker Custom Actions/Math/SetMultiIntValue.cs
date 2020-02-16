// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: multi, multiple
// Source http://hutonggames.com/playmakerforum/index.php?topic=9989.0


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets the value of many Int Variable.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9989.0")]
	public class SetMultiIntValue : FsmStateAction
	{
		[CompoundArray("Count", "Int Variable", "Value")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt[] intVariable;
		public FsmInt[] values;
		public bool everyFrame;
		
		public override void Reset()
		{
			intVariable = new FsmInt[1];
			values = new FsmInt[1];
		}

		public override void OnEnter()
		{
			DoSetIntValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetIntValue();
		}

		public void DoSetIntValue()
		{
			for(int i = 0; i<intVariable.Length;i++){
				if(!intVariable[i].IsNone || !intVariable[i].Value.Equals("")) 
					intVariable[i].Value = values[i].IsNone ? 0: values[i].Value;
			}
		}
	}
}
