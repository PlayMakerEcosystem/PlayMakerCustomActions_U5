// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Get texture width")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12348.0")]
	public class TextureGetWidth : FsmStateAction
	{

		public FsmTexture texture;
		public FsmInt getWidth;

		public override void Reset()
		{
		
			getWidth = 0;
		}

		public override void OnEnter()
		{
			getWidth.Value = texture.Value.width;
			Finish();
		}
	}
}
