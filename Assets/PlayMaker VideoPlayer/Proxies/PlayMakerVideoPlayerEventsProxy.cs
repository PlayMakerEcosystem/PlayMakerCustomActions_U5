using UnityEngine;
using UnityEngine.Video;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker.Ecosystem.Utils;

public class PlayMakerVideoPlayerEventsProxy : MonoBehaviour
{
	public bool debug = false;

	[ExpectComponent(typeof(VideoPlayer))]
	public Owner videoPlayer;

	public PlayMakerEventTarget eventTarget = new PlayMakerEventTarget(true);

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onStarted = new PlayMakerEvent();

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onErrorReceived = new PlayMakerEvent();

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onFrameDropped = new PlayMakerEvent();

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onFrameReady = new PlayMakerEvent();

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onLoopPointReached = new PlayMakerEvent();

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onPrepareCompleted = new PlayMakerEvent();

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onSeekCompleted = new PlayMakerEvent();


	VideoPlayer _vp;


	void OnEnable()
	{
		if (videoPlayer.gameObject != null)
		{
			_vp = videoPlayer.gameObject.GetComponent<VideoPlayer> ();
		} else
		{
			if (debug) Debug.LogError("Missing GameObject videoPlayer Target",this);
		}


		if(_vp==null){
			if (debug) Debug.LogError("missing VideoPlayer on Target: "+videoPlayer.gameObject,this);

			return;
		}
	
		if (!onStarted.isNone) _vp.started += OnStarted;
		if (!onErrorReceived.isNone) _vp.errorReceived += OnErrorReceived;
		if (!onFrameDropped.isNone) _vp.frameDropped += OnFrameDropped;
		if (!onFrameReady.isNone)
		{
			_vp.sendFrameReadyEvents = true;

			_vp.frameReady += OnFrameReady;
		}
		if (!onLoopPointReached.isNone) _vp.loopPointReached += OnLoopPointReached;
		if (!onPrepareCompleted.isNone) _vp.prepareCompleted += OnPrepareCompleted;
		if (!onSeekCompleted.isNone) _vp.seekCompleted += OnSeekCompleted;


	}

	void OnDisable()
	{
		if (_vp==null)
		{
			return;
		}
		
		if (!onStarted.isNone) _vp.started -= OnStarted;
		if (!onErrorReceived.isNone) _vp.errorReceived -= OnErrorReceived;
		if (!onFrameDropped.isNone) _vp.frameDropped -= OnFrameDropped;
		if (!onFrameReady.isNone)
		{
			_vp.sendFrameReadyEvents = false;

			_vp.frameReady -= OnFrameReady;
		}
		if (!onLoopPointReached.isNone) _vp.loopPointReached -= OnLoopPointReached;
		if (!onPrepareCompleted.isNone) _vp.prepareCompleted -= OnPrepareCompleted;
		if (!onSeekCompleted.isNone) _vp.seekCompleted -= OnSeekCompleted;

	}

	public void OnStarted (VideoPlayer source) {
		if (debug)
		{
			UnityEngine.Debug.Log("onStartedEvent "+" on "+this.gameObject.name+" sending: "+onStarted,this);
		}

		Fsm.EventData.GameObjectData = source.gameObject;
		onStarted.SendEvent(null,eventTarget);
	}

	public void OnErrorReceived (VideoPlayer source,string message) {
		if (debug)
		{
			UnityEngine.Debug.Log("OnErrorReceived "+" on "+this.gameObject.name+" sending: "+onErrorReceived,this);
		}

		Fsm.EventData.GameObjectData = source.gameObject;
		Fsm.EventData.StringData = message;
		onErrorReceived.SendEvent(null,eventTarget);
	}

	public void OnFrameDropped (VideoPlayer source) {
		if (debug)
		{
			UnityEngine.Debug.Log("OnFrameDropped "+" on "+this.gameObject.name+" sending: "+onFrameDropped,this);
		}

		Fsm.EventData.GameObjectData = source.gameObject;
		onFrameDropped.SendEvent(null,eventTarget);
	}

	public void OnFrameReady (VideoPlayer source,long frameIndex) {
		if (debug)
		{
			UnityEngine.Debug.Log("OnFrameReady "+" on "+this.gameObject.name+" sending: "+onFrameReady,this);
		}

		Fsm.EventData.GameObjectData = source.gameObject;
		Fsm.EventData.IntData = (int)frameIndex;
		onFrameReady.SendEvent(null,eventTarget);
	}

	public void OnLoopPointReached (VideoPlayer source) {
		if (debug)
		{
			UnityEngine.Debug.Log("OnLoopPointReached "+" on "+this.gameObject.name+" sending: "+onLoopPointReached,this);
		}

		Fsm.EventData.GameObjectData = source.gameObject;
		onLoopPointReached.SendEvent(null,eventTarget);
	}

	public void OnPrepareCompleted (VideoPlayer source) {
		if (debug)
		{
			UnityEngine.Debug.Log("OnPrepareCompleted "+" on "+this.gameObject.name+" sending: "+onPrepareCompleted,this);
		}

		Fsm.EventData.GameObjectData = source.gameObject;
		onPrepareCompleted.SendEvent(null,eventTarget);
	}

	public void OnSeekCompleted (VideoPlayer source) {
		if (debug)
		{
			UnityEngine.Debug.Log("OnSeekCompleted "+" on "+this.gameObject.name+" sending: "+onSeekCompleted,this);
		}

		Fsm.EventData.GameObjectData = source.gameObject;
		onSeekCompleted.SendEvent(null,eventTarget);
	}

}
