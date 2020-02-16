// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: multi, multiple
// Source http://hutonggames.com/playmakerforum/index.php?topic=9989.0

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Rect)]
	[Tooltip("Sets the value of many Rect Variable.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9989.0")]
	public class SetMultiRectValue : FsmStateAction
	{
		[CompoundArray("Count", "Rect Variable", "Value")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmRect[] rectVariable;
		public FsmRect[] values;
		public bool everyFrame;
		
		public override void Reset()
		{
			rectVariable = new FsmRect[1];
			values = new FsmRect[1];
			everyFrame = false;
		}
		

		public override void OnEnter()
		{
			DoSetRectValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetRectValue();
		}

		public void DoSetRectValue()
		{
			for(int i = 0; i<rectVariable.Length;i++){
				if(!rectVariable[i].IsNone) 
					rectVariable[i].Value = values[i].Value;
			}
		}
	}
}
