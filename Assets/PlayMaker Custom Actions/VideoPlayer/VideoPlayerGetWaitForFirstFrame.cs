﻿// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if UNITY_5_6_OR_NEWER

using UnityEngine;
using UnityEngine.Video;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VideoPlayer")]
	[Tooltip("Check whether the player will wait for the first frame to be loaded into the texture before starting playback when VideoPlayer.playOnAwake is on")]
	public class VideoPlayerGetWaitForFirstFrame : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(VideoPlayer))]
		[Tooltip("The GameObject with as VideoPlayer component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Value")]
		[UIHint(UIHint.Variable)]
		public FsmBool isWaitingForFirstFrame;

		[Tooltip("Event sent if content will wait for first frame")]
		public FsmEvent isWaitingForFirstFrameEvent;

		[Tooltip("Event sent if content will not wait for first frame")]
		public FsmEvent isNotWaitingForFirstFrameEvent;

		[Tooltip("Execute action everyframe. Events are however sent discretly, only when changes occurs")]
		public bool everyframe;

		GameObject go;

		VideoPlayer _vp;

		int _isWaitingForFirstFrame = -1;

		public override void Reset()
		{
			gameObject = null;
			isWaitingForFirstFrame = null;
			isWaitingForFirstFrameEvent = null;
			isNotWaitingForFirstFrameEvent = null;
			everyframe = false;
		}

		public override void OnEnter()
		{
			GetVideoPlayer ();

			ExecuteAction ();

			if (!everyframe)
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
			if (_vp == null)
			{
				return;
			}

			if (_vp.waitForFirstFrame)
			{
				isWaitingForFirstFrame.Value = true;
				if (_isWaitingForFirstFrame != 1)
				{
					Fsm.Event (isWaitingForFirstFrameEvent);
				}
				_isWaitingForFirstFrame = 1;
			} else
			{
				isWaitingForFirstFrame.Value = false;
				if (_isWaitingForFirstFrame != 0)
				{
					Fsm.Event (isNotWaitingForFirstFrameEvent);
				}
				_isWaitingForFirstFrame = 0;
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