// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Retrieve the length of the AudioClip inside the specified AudioSource or from the given object.")]
	public class GetAudioClipLength : ComponentAction<AudioSource>
	{

		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with the AudioSource component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Supply a clip directly.")]
		public FsmObject audioClip;

		[UIHint(UIHint.Variable)]
		[Tooltip("The length of the clip.")]
		public FsmFloat length;

		private AudioClip _audioClip;

		public override void Reset()
		{
			gameObject = null;
			audioClip = new FsmObject() { UseVariable = true };
			length = 0;
		}

		public override void OnEnter()
		{

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if(UpdateCache(go))
			{
				_audioClip = audio.clip as AudioClip;
				length.Value = _audioClip.length;
			} else
			{
				_audioClip = audioClip.Value as AudioClip;
				length.Value = _audioClip.length;
			}

			Finish();
		}
	}
}
