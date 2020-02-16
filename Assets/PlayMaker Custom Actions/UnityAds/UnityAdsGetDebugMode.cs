// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("UnityAds")]
	[Tooltip("Gets the debug mode of UnityAds.")]
	public class UnityAdsGetDebugMode : FsmStateAction
	{
		#if UNITY_ADS
		[Tooltip("The Debug Mode")]
		[UIHint(UIHint.Variable)]
		public FsmBool debugMode;

		[Tooltip("Event sent if debug mode is true")]
		public FsmEvent isInDebuggingModeEvent;

		[Tooltip("Event sent if debug mode is false")]
		public FsmEvent isNotInDebuggingModeEvent;

		public override void Reset()
		{
			debugMode = null;
			isInDebuggingModeEvent = null; 
			isNotInDebuggingModeEvent = null;
		}
		#endif

		#if UNITY_IOS || UNITY_ANDROID
			#if UNITY_ADS
			public override void OnEnter()
			{
				debugMode.Value = Advertisement.debugMode;

				Fsm.Event (Advertisement.debugMode ? isInDebuggingModeEvent : isNotInDebuggingModeEvent);

				Finish();

			}
			#endif
		#else
		public override void OnEnter()
		{
			Debug.Log("UnityAds only supported on IOS and Android Platforms");

			Finish();
		}
		#endif
		
	}
	
}