// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: multi, multiple
// Source http://hutonggames.com/playmakerforum/index.php?topic=9989.0


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Sets the value of many Vector2 Variable.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9989.0")]
	public class SetMultiVector2Value : FsmStateAction
	{
		[CompoundArray("Count", "Vector2 Variable", "Value")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector2[] vector2Variable;
		public FsmVector2[] values;
		public bool everyFrame;
		
		public override void Reset()
		{
			vector2Variable = new FsmVector2[1];
			values = new FsmVector2[1];
			everyFrame = false;
		}
		

		public override void OnEnter()
		{
			DoSetVector2Value();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetVector2Value();
		}

		public void DoSetVector2Value()
		{
			for(int i = 0; i<vector2Variable.Length;i++){
				if(!vector2Variable[i].IsNone) 
					vector2Variable[i].Value = values[i].Value;
			}
		}
	}
}
