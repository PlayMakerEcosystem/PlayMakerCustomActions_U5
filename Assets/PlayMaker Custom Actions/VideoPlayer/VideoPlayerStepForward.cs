// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if UNITY_5_6_OR_NEWER

using UnityEngine;
using UnityEngine.Video;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VideoPlayer")]
	[Tooltip("Advances the current time by one frame immediately of a VideoPlayer.")]
	public class VideoPlayerStepForward : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(VideoPlayer))]
		[Tooltip("The GameObject with as VideoPlayer component.")]
		public FsmOwnerDefault gameObject;

		GameObject go;

		VideoPlayer _vp;


		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnEnter()
		{
			GetVideoPlayer ();

			if (_vp != null)
			{
				_vp.StepForward();
				return;
			}
			Finish();
		}

		void GetVideoPlayer()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_vp = go.GetComponent<VideoPlayer>();
			}
		}
	}
}

#endif