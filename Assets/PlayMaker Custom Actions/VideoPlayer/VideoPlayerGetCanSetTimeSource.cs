// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if UNITY_5_6_OR_NEWER

using UnityEngine;
using UnityEngine.Video;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VideoPlayer")]
	[Tooltip("Check whether the time source followed by the video player can be changed. (Read Only)")]
	public class VideoPlayerGetCanSetTimeSource : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(VideoPlayer))]
		[Tooltip("The GameObject with an VideoPlayer component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Value")]
		[UIHint(UIHint.Variable)]
		public FsmBool canSetTimeSource;

		[Tooltip("Event sent if timeSource can be set")]
		public FsmEvent canSetTimeSourceEvent;

		[Tooltip("Event sent if timeSource can not be set")]
		public FsmEvent canNotSetTimeSourceEvent;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		GameObject go;

		VideoPlayer _vp;


		public override void Reset()
		{
			gameObject = null;
			canSetTimeSource = null;
			canSetTimeSourceEvent = null;
			canNotSetTimeSourceEvent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GetVideoPlayer ();

			ExecuteAction ();

			if (!everyFrame)
			{
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			ExecuteAction ();
		}	

		void ExecuteAction()
		{
			if (_vp != null)
			{
				canSetTimeSource.Value = _vp.canSetTimeSource;
				Fsm.Event(_vp.canSetTime?canSetTimeSourceEvent:canNotSetTimeSourceEvent);
			}
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