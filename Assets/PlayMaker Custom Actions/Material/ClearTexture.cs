// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Clear the texture")]
	public class ClearTexture : FsmStateAction
	{

		public FsmTexture texture;


		public override void Reset()
		{
		
			texture = null;
		}

		public override void OnEnter()
		{
			texture.Value = null;
			Finish();
		}
	}
}
