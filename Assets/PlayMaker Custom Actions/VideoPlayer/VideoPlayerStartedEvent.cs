﻿// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if UNITY_5_6_OR_NEWER

using UnityEngine;
using UnityEngine.Video;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VideoPlayer")]
	[Tooltip("Send the started event from a VideoPlayer.")]
	public class VideoPlayerStartedEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(VideoPlayer))]
		[Tooltip("The GameObject with as VideoPlayer component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("event sent when VideoPlayer started")]
		public FsmEvent onStartedEvent;

		GameObject go;

		VideoPlayer _vp;


		public override void Reset()
		{
			gameObject = null;
			onStartedEvent = null;
		}

		public override void OnEnter()
		{
			GetVideoPlayer ();

			if (_vp != null)
			{
				_vp.started += OnStarted;
			}
		}

		public override void OnExit()
		{
			if (_vp != null)
			{
				_vp.started -= OnStarted;
			}
		}

		void OnStarted(VideoPlayer source)
		{
			Fsm.EventData.GameObjectData = source.gameObject;
			Fsm.Event (onStartedEvent);
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