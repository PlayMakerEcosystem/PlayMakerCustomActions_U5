// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10566.0
// Keywords: Set Master Texture Limit

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("QualitySettings")]
	[Tooltip("A texture size limit applied to all textures. Tip: 0 is Off - 1 is half - 2 is a quarter - etc.. ")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10566.0")]
	public class SetMasterTextureLimit : FsmStateAction
	{
		
		[UIHint(UIHint.FsmInt)]
		public FsmInt level;
		

		public override void Reset()
		{
			level = 0;
		}
		
		public override void OnEnter()
		{


			QualitySettings.masterTextureLimit = level.Value;
			
		
				Finish();
			
		}

		
	}
}
