// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Get texture height")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12348.0")]
	public class TextureGetHeight : FsmStateAction
	{

		public FsmTexture texture;
		public FsmInt getHeight;

		public override void Reset()
		{
		
			getHeight = 0;
		}

		public override void OnEnter()
		{
			getHeight.Value = texture.Value.height;
			Finish();
		}
	}
}
