// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Retrieve the progress (in %) of the specified AudioSource. Optionally sends an Event when past the specified percentage.")]
	public class GetAudioProgress : ComponentAction<AudioSource>
	{
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with the AudioSource component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("The progress of currently playing clip.")]
		public FsmFloat currentProgress;

		[ActionSection("Optional")]

		[HasFloatSlider(0, 100)]
		[Tooltip("Send the Event when this percentage is reached. Must be above 0, otherwise ignores 'Send Event'.")]
		public FsmFloat onPercentage;

		[Tooltip("The Event to send when the progress reached the 'On Percentage' amount.")]
		public FsmEvent sendEvent;

		private float time;
		private float length;
		private AudioClip _audioClip;

		public override void Reset()
		{
			gameObject = null;
			currentProgress = 0;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if(UpdateCache(go))
			{
				_audioClip = audio.clip as AudioClip;
				length = _audioClip.length;
			}
		}

		public override void OnUpdate()
		{
			time = audio.time;
			currentProgress.Value = (time / length) * 100;

			if(onPercentage.Value > 0)
			{
				if(currentProgress.Value > onPercentage.Value)
				{
					Fsm.Event(sendEvent);
					Finish();
				}
			}
		}
	}
}
