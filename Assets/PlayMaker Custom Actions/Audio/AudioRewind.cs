// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Rewind Sound Fastforward slowmotion

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Rewind the Audio Clip played by the AudioSource component on a Game Object.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11863.0")]

	public class AudioRewind : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;
		public FsmBool rewind;

		[ActionSection("Speed options")]
		[TitleAttribute("Rewind Speed")]
		public FsmFloat speed;
		public FsmBool everyFrame;

		private float previousPitch;
		private AudioSource audio;
		private float finalSpeed;


		public override void Reset()
		{
			gameObject = null;
			speed = 1f;
			rewind = false;
			everyFrame = true;
		}

		public override void OnEnter()
		{

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			audio = go.GetComponent<AudioSource>();

			previousPitch = audio.pitch;

			finalSpeed = Mathf.Abs(speed.Value)* -1;

			if (rewind.Value == true & audio.timeSamples != 0){
				audio.pitch = finalSpeed;
			}


			if (everyFrame.Value == false){
				
				Finish();
	
			}
		}

		public override void OnUpdate()
		{
			finalSpeed = Mathf.Abs(speed.Value)* -1;
            
			if (rewind.Value == true & audio.timeSamples != 0){
				audio.pitch = finalSpeed;
			}

			if (rewind.Value == false || audio.timeSamples == 0){
				audio.pitch = previousPitch;
				rewind.Value = false;
			}

			if (everyFrame.Value == false){
				
				Finish();
				
			}
		}
	}
}
