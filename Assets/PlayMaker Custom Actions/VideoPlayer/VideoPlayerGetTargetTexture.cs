﻿// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if UNITY_5_6_OR_NEWER

using UnityEngine;
using UnityEngine.Video;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VideoPlayer")]
	[Tooltip("Get the RenderTexture to draw to when VideoPlayer.renderMode is set to Video.VideoTarget.RenderTexture.")]
	public class VideoPlayerGetTargetTexture : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(VideoPlayer))]
		[Tooltip("The GameObject with as VideoPlayer component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The RenderTexture")]
		public FsmTexture targetTexture;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		GameObject go;

		VideoPlayer _vp;


		public override void Reset()
		{
			gameObject = null;
			targetTexture = null;
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
				targetTexture.Value = _vp.targetTexture;
				return;
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