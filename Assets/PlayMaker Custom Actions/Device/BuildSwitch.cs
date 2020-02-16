// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=11240.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Send event based on build.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11240.0")]
	public class BuildSwitch : FsmStateAction
	{
		public FsmEvent sendEventUnityEditor;
		public FsmEvent sendEventIos;
		public FsmEvent sendEventAndroid;
		public FsmEvent sendEventEditorWin;
		public FsmEvent sendEventEditorOsx;
		public FsmEvent sendEventStandaloneOsx;
		public FsmEvent sendEventStandaloneWin;
		public FsmEvent sendEventStandaloneLinux;
		public FsmEvent sendEventStandalone;
		public FsmEvent sendEventWebplayer;
		public FsmEvent sendEventIphone;
		public FsmEvent sendEventWebGl;
		public FsmEvent sendEventTizen;

		public override void Reset()
		{
			sendEventUnityEditor = null;
			sendEventIos= null;
			sendEventAndroid= null;
			sendEventEditorWin= null;
			sendEventEditorOsx= null;
			sendEventStandaloneOsx= null;
			sendEventStandaloneWin= null;
			sendEventStandaloneLinux= null;
			sendEventStandalone= null;
			sendEventWebplayer= null;
			sendEventIphone= null;
			sendEventWebGl= null;
			sendEventTizen= null;
		}

		public override void OnEnter()
		{
	

			#if UNITY_EDITOR
			Fsm.Event(sendEventUnityEditor);
			#endif


			#if UNITY_IOS
			Fsm.Event(sendEventIos);
			#endif

			#if UNITY_ANDROID
			Fsm.Event(sendEventAndroid);
			#endif

			#if UNITY_EDITOR_WIN
			Fsm.Event(sendEventEditorWin);
			#endif

			#if UNITY_EDITOR_OSX
			Fsm.Event(sendEventEditorOsx);
			#endif

			#if UNITY_STANDALONE_OSX
			Fsm.Event(sendEventStandaloneOsx);
			#endif

			#if UNITY_STANDALONE_WIN
			Fsm.Event(sendEventStandaloneWin);
			#endif

			#if UNITY_STANDALONE_LINUX
			Fsm.Event(sendEventStandaloneLinux);
			#endif

			#if UNITY_STANDALONE
			Fsm.Event(sendEventStandalone);
			#endif

			#if UNITY_WEBPLAYER
			Fsm.Event(sendEventWebplayer);
			#endif

			#if UNITY_IPHONE
			Fsm.Event(sendEventIphone);
			#endif

			#if UNITY_WEBGL
			Fsm.Event(sendEventWebGl);
			#endif

			#if UNITY_TIZEN
			Fsm.Event(sendEventTizen);
			#endif

			Finish();
		}
	


	}
}

