// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: multi, multiple
// Source http://hutonggames.com/playmakerforum/index.php?topic=9989.0


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Sets the value of many Vector3 Variable.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9989.0")]
	public class SetMultiVector3Value : FsmStateAction
	{
		[CompoundArray("Count", "Vector3 Variable", "Value")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3[] vector3Variable;
		public FsmVector3[] values;
		public bool everyFrame;
		
		public override void Reset()
		{
			vector3Variable = new FsmVector3[1];
			values = new FsmVector3[1];
			everyFrame = false;
		}
		

		public override void OnEnter()
		{
			DoSetVector3Value();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetVector3Value();
		}

		public void DoSetVector3Value()
		{
			for(int i = 0; i<vector3Variable.Length;i++){
				if(!vector3Variable[i].IsNone) 
					vector3Variable[i].Value = values[i].Value;
			}
		}
	}
}
