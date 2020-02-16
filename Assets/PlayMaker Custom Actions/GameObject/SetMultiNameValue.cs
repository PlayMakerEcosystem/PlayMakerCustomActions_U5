// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: multi, multiple
// Source http://hutonggames.com/playmakerforum/index.php?topic=9989.0


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the value of many GameObject's name.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9989.0")]
	public class SetMultiNameValue : FsmStateAction
	{
		[CompoundArray("Count", "GameObject", "Name")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmOwnerDefault[] gameobjectVariable;
		public FsmString[] name;
		public bool everyFrame;

		private GameObject go;
		
		public override void Reset()
		{
			gameobjectVariable = new FsmOwnerDefault[1];
			name =  new FsmString[1];
			everyFrame = false;
		}
		

		public override void OnEnter()
		{
			DoSetGameObjectnameValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetGameObjectnameValue();
		}

		public void DoSetGameObjectnameValue()
		{
			for(int i = 0; i<gameobjectVariable.Length;i++){
				if(name[i].Value != "" || !name[i].IsNone) {
	
				go = Fsm.GetOwnerDefaultTarget(gameobjectVariable[i]);
				
				if (go != null)
						go.name = name[i].Value;
				}
			}
		}
	}
}
