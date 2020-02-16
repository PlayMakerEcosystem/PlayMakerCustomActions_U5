// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: multi, multiple
// Source http://hutonggames.com/playmakerforum/index.php?topic=9989.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the value of many GameObject's Layer.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9989.0")]
	public class SetMultiLayerValue : FsmStateAction
	{
		[CompoundArray("Count", "GameObject", "Layer")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmOwnerDefault[] gameobjectVariable;
		[UIHint(UIHint.Layer)]
		[Title("Layer")]
		public FsmInt[] layer;
		public bool everyFrame;

		private GameObject go;
		
		public override void Reset()
		{
			gameobjectVariable = new FsmOwnerDefault[1];
			layer =  new FsmInt[1];
			everyFrame = false;
		}
		

		public override void OnEnter()
		{
			DoSetGameObjectLayerValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetGameObjectLayerValue();
		}

		public void DoSetGameObjectLayerValue()
		{
			for(int i = 0; i<gameobjectVariable.Length;i++){
				if(!layer[i].IsNone) {
	
				go = Fsm.GetOwnerDefaultTarget(gameobjectVariable[i]);
				
				if (go != null)
						go.layer = layer[i].Value;
				}
			}
		}
	}
}
