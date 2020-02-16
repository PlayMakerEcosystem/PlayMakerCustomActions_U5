// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: multi, multiple
// Source http://hutonggames.com/playmakerforum/index.php?topic=9989.0


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the value of many Game Object's Tag.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9989.0")]
	public class SetMultiTagValue : FsmStateAction
	{
		[CompoundArray("Count", "GameObject", "Tag")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmOwnerDefault[] gameobjectVariable;
		[UIHint(UIHint.Tag)]
		[Title("Tag")]
		public FsmString[] tag;
		public bool everyFrame;

		private GameObject go;
		
		public override void Reset()
		{
			gameobjectVariable = new FsmOwnerDefault[1];
			tag =  new FsmString[1];
			everyFrame = false;
		}
		

		public override void OnEnter()
		{
			DoSetGameObjectTagValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetGameObjectTagValue();
		}

		public void DoSetGameObjectTagValue()
		{
			for(int i = 0; i<gameobjectVariable.Length;i++){
				if(tag[i].Value != "Untagged" || tag[i].Value != "" || !tag[i].IsNone) {
	
				go = Fsm.GetOwnerDefaultTarget(gameobjectVariable[i]);
				
				if (go != null)
						go.tag = tag[i].Value;
				}
			}
		}
	}
}
