// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10559.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Controls the game sound volume (0.0 to 1.0).")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10559.0")]
	public class AudioListenerSetVolume : FsmStateAction
	{
		
		[UIHint(UIHint.FsmFloat)]
		[HasFloatSlider(0.00f,1.0f)]
		public FsmFloat volume;
		

		public override void Reset()
		{
			volume = 1.0f;
		}
		
		public override void OnEnter()
		{

			if (volume.Value > 1f){

				Debug.LogWarning("Volume is too high. Auto Reset to 1. Please correct");
				volume.Value = 1.0f;

			}

			if (volume.Value < 0f){
				
				Debug.LogWarning("Volume is too low. Auto Reset to 0. Please correct");
				volume.Value = 0.0f;
				
			}


			AudioListener.volume = volume.Value;
			
		
				Finish();
			
		}

		
	}
}
