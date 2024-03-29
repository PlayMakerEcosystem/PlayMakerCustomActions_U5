﻿// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// keywords: VideoPlayer

#if UNITY_5_6_OR_NEWER

using UnityEngine;
using UnityEngine.Video;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VideoClip")]
	[Tooltip("Get the number of audio tracks in a videoClip. (ReadOnly)")]
	public class VideoClipGetAudioTrackCount : FsmStateAction
	{
		[CheckForComponent(typeof(VideoPlayer))]
		[Tooltip("The GameObject with as VideoPlayer component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Or The video clip of the VideoPlayer. Leave to none, else gameObject is ignored")]
		public FsmObject orVideoClip;

		[UIHint(UIHint.Variable)]
		[Tooltip("the number of audio tracks")]
		public FsmInt audioTrackCount;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		GameObject go;

		VideoPlayer _vp;
		VideoClip _vc;


		public override void Reset()
		{
			gameObject = null;
			orVideoClip = new FsmObject() {UseVariable=true};

			audioTrackCount = null;

			everyFrame = false;
		}

		public override void OnEnter()
		{
			GetVideoClip ();

			ExecuteAction ();

			if (!everyFrame)
			{
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			GetVideoClip ();

			ExecuteAction ();
		}

		void ExecuteAction()
		{
			if (_vc != null)
			{
				audioTrackCount.Value = _vc.audioTrackCount;
			}
		}

		void GetVideoClip()
		{
			if (orVideoClip.IsNone)
			{
				go = Fsm.GetOwnerDefaultTarget (gameObject);
				if (go != null)
				{
					_vp = go.GetComponent<VideoPlayer> ();
					if (_vp != null)
					{
						_vc = _vp.clip;
					}
				}
			} else
			{
				_vc = orVideoClip.Value as VideoClip;
			}
		}
	}
}

#endif