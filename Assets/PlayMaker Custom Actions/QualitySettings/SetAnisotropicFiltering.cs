// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10563.0

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("QualitySettings")]
	[Tooltip("Global anisotropic filtering mode. 1 = Disable // 2 = Enable // 3 = ForceEnable")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10563.0")]
	public class SetAnisotropicFiltering : FsmStateAction
	{
		
		[UIHint(UIHint.FsmInt)]
		public FsmInt level;
		

		public override void Reset()
		{
			level = 2;
		}
		
		public override void OnEnter()
		{

			if (level.Value != 1 || level.Value != 2 || level.Value != 3){

				Debug.LogError("AnisotropicFiltering must 1 = Disable // 2 = Enable // 3 = ForceEnable");
				Finish();

			}

			if (level.Value == 1) QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
			if (level.Value == 2) QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
			if (level.Value == 3) QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;

				Finish();
			
		}

		
	}
}
