// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10564.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("QualitySettings")]
	[Tooltip("The number of VSyncs that should pass between each frame. Value must be 0, 1 2 3 o 4")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10564.0")]
	public class SetVSyncCount : FsmStateAction
	{
		[UIHint(UIHint.FsmInt)]
		public FsmInt level;
		
		public override void Reset()
		{
			level = 2;
		}
		
		public override void OnEnter()
		{

			if (level.Value >=0 && level.Value<=4){

				Debug.LogError("Value must be in between 0 and 4");
				Finish();

			}
			QualitySettings.vSyncCount = level.Value;
			Finish();
			
		}
		
	}
}
