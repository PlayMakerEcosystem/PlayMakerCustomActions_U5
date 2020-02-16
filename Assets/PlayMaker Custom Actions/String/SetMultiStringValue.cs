// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: multi, multiple
// Source http://hutonggames.com/playmakerforum/index.php?topic=9989.0


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Sets the value of many String Variable.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9989.0")]
	public class SetMultiStringValue : FsmStateAction
	{
		[CompoundArray("Count", "String Variable", "Value")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString[] stringVariable;
		public FsmString[] values;
		public bool everyFrame;
		
		public override void Reset()
		{
			stringVariable = new FsmString[1];
			values = new FsmString[1];
			everyFrame = false;
		}
		

		public override void OnEnter()
		{
			DoSetStringValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetStringValue();
		}

		public void DoSetStringValue()
		{
			for(int i = 0; i<stringVariable.Length;i++){
				if(!stringVariable[i].IsNone || !stringVariable[i].Value.Equals("")) 
					stringVariable[i].Value = values[i].IsNone ? "": values[i].Value;
			}
		}
	}
}
