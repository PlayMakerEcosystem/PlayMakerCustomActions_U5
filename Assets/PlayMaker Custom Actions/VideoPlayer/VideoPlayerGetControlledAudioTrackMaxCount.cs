﻿// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if UNITY_5_6_OR_NEWER

using UnityEngine;
using UnityEngine.Video;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VideoPlayer")]
	[Tooltip("Static property. Maximum number of audio tracks that can be controlled. When playing audio from a URL, the number of audio tracks is not known in advance. It is up to the user to specify the number of controlled audio tracks through VideoPlayer.controlledAudioTrackCount. Other tracks will be ignored and silenced. In this scenario, VideoPlayer.audioTrackCount will be set to the actual number of tracks during playback, after prepration is complete. See VideoPlayer.Prepare.")]
	public class VideoPlayerGetControlledAudioTrackMaxCount : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The maximum number of audio tracks that can be controlled")]
		public FsmInt controlledAudioTrackMaxCount;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			controlledAudioTrackMaxCount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
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
			controlledAudioTrackMaxCount.Value = VideoPlayer.controlledAudioTrackMaxCount;
		}
	}
}

#endif