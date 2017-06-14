// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if UNITY_5_6_OR_NEWER

using UnityEngine;
using UnityEngine.Video;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VideoPlayer")]
	[Tooltip("Sets Time source followed by the VideoPlayer when reading content.")]
	public class VideoPlayerSetTimeSource : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(VideoPlayer))]
		[Tooltip("The GameObject with an VideoPlayer component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The timeSource Value")]
		[ObjectType(typeof(VideoTimeSource))]
		public FsmEnum timeSource;

		[Tooltip("Event sent if time can not be set")]
		public FsmEvent canNotSetTime;


		GameObject go;

		VideoPlayer _vp;


		public override void Reset()
		{
			gameObject = null;
			timeSource = VideoTimeSource.AudioDSPTimeSource;
			canNotSetTime = null;
		}

		public override void OnEnter()
		{
			GetVideoPlayer ();

			if (_vp != null && !_vp.canSetTime)
			{
				Fsm.Event (canNotSetTime);
			} else
			{
				ExecuteAction ();
			}

			Finish ();

		}

		void ExecuteAction()
		{
			if (_vp != null && _vp.canSetTime)
			{
				_vp.timeSource = (VideoTimeSource)timeSource.Value;
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