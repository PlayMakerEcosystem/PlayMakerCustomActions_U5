﻿// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if UNITY_5_6_OR_NEWER

using UnityEngine;
using UnityEngine.Video;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VideoPlayer")]
	[Tooltip("Defines Destination for the audio embedded in the video.")]
	public class VideoPlayerSetAudioOutputMode : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(VideoPlayer))]
		[Tooltip("The GameObject with an VideoPlayer component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The AudioOutputMode type")]
		[ObjectType(typeof(VideoAudioOutputMode))]
		public FsmEnum audioOutputMode;

		GameObject go;

		VideoPlayer _vp;


		public override void Reset()
		{
			gameObject = null;
			audioOutputMode = VideoAudioOutputMode.AudioSource;
		}

		public override void OnEnter()
		{
			GetVideoPlayer ();

			ExecuteAction ();
		
			Finish ();
		}


		void ExecuteAction()
		{
			if (_vp != null)
			{
				_vp.audioOutputMode = (VideoAudioOutputMode)audioOutputMode.Value;
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