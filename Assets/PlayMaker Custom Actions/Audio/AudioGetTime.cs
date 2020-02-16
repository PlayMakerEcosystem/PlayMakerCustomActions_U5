// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Sound Time

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Read current playback time. On a compressed audio track position does not necessary reflect the actual time in the track ")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=13667.0")]

	public class AudioGetTime : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;

		public FsmBool everyframe;

		[ActionSection("Output")]
		public FsmInt timeSample;
        [Tooltip("Time in seconds")]
		public FsmFloat time;

		private AudioSource audio;


		public override void Reset()
		{
			gameObject = null;
			timeSample = 0;
			time = 0f;
			everyframe = false;
		}

		public override void OnEnter()
		{

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			audio = go.GetComponent<AudioSource>();

		
			time.Value = audio.time;
			timeSample.Value = audio.timeSamples;
				

			if (everyframe.Value == false)
				Finish();
	

		}

		public override void OnUpdate()
		{
			
			time.Value = audio.time;
			timeSample.Value = audio.timeSamples;


			if (everyframe.Value == false)
				Finish();
		}
	}
}

