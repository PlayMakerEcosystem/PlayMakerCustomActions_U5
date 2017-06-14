﻿// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if UNITY_5_6_OR_NEWER

using UnityEngine;
using UnityEngine.Video;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VideoPlayer")]
	[Tooltip("Set where the video content will be drawn")]
	public class VideoPlayerSetRenderMode : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(VideoPlayer))]
		[Tooltip("The GameObject with as VideoPlayer component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("where the video content will be drawn")]
		[ObjectType(typeof(VideoRenderMode))]
		public FsmEnum renderMode;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		GameObject go;

		VideoPlayer _vp;

		public override void Reset()
		{
			gameObject = null;
			renderMode = null;
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
				_vp.renderMode = (VideoRenderMode)renderMode.Value;
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