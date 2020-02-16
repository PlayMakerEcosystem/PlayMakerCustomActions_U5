// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Android TV

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Android")]
	[Tooltip("Check if Android is in TV mode")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11938.0")]
	public class DetectIfAndroidTv : FsmStateAction
	{
		
		[ActionSection("Output")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent trueEvent;
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event if error")]
		public FsmEvent falseEvent;
		
		public override void Reset()
		{
			trueEvent = null;
			falseEvent = null;
		}
		
		public override void OnEnter()
		{
			check();
			
			Finish();
		}
		
		
		
		void check()
		{
			
			#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass unityPlayerJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject androidActivity = unityPlayerJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass contextJavaClass = new AndroidJavaClass("android.content.Context");
			AndroidJavaObject modeServiceConst = contextJavaClass.GetStatic<AndroidJavaObject>("UI_MODE_SERVICE");
			AndroidJavaObject uiModeManager = androidActivity.Call<AndroidJavaObject>("getSystemService", modeServiceConst);
			int currentModeType = uiModeManager.Call<int>("getCurrentModeType");
			AndroidJavaClass configurationAndroidClass = new AndroidJavaClass("android.content.res.Configuration");
			int modeTypeTelevisionConst = configurationAndroidClass.GetStatic<int>("UI_MODE_TYPE_TELEVISION");
			
			if(modeTypeTelevisionConst == currentModeType)
			{
				Fsm.Event(trueEvent);		
			}
			else
			{
				Fsm.Event(falseEvent);		
			}
			#endif

			#if UNITY_EDITOR
			Fsm.Event(falseEvent);		
			#endif
		}
		
	}
}
