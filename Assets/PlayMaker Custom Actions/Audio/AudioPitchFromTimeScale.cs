// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Sound Fastforward slowmotion

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Sets the Pitch of the Audio Clip to TimeScale played by the AudioSource component on a Game Object. Will create a rewind or fastforward sound FX deping on your TimeScale")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11862.0")]
    public class AudioPitchFromTimeScale : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;

		[ActionSection("Pitch options")]
		public FsmFloat minPitch;
		public FsmFloat maxPitch;

		[ActionSection("Speed options")]
		public FsmFloat lerpSpeed;

		private float previousRealTime;
		private AudioSource audio;
		
		public override void Reset()
		{
			gameObject = null;
			minPitch = 0f;
			maxPitch = 2f;
			lerpSpeed = 3f;
		}

		public override void OnEnter()
		{

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			audio = go.GetComponent<AudioSource>();
		
		}
				
		public override void OnUpdate ()
		{
		
			float currentTime = Time.realtimeSinceStartup;
			float timeDelta = currentTime - previousRealTime;

			float targetPitch = Mathf.Clamp(Time.timeScale, minPitch.Value, maxPitch.Value);


			audio.pitch = Mathf.Lerp(audio.pitch, targetPitch, lerpSpeed.Value * timeDelta);
			

			previousRealTime = currentTime;



		}	
		
	
	}
}
