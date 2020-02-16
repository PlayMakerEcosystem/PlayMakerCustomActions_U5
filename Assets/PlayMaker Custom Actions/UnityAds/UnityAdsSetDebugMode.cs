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
	[Tooltip("Sets the debug mode of UnityAds.")]
	public class UnityAdsSetDebugMode : FsmStateAction
	{
		#if UNITY_ADS
		[Tooltip("The Debug Mode")]
		public FsmBool debugMode;

		public override void Reset()
		{
			debugMode = false;
		}
		#endif

		#if UNITY_IOS || UNITY_ANDROID
			#if UNITY_ADS
			public override void OnEnter()
			{
				Advertisement.debugMode = debugMode.Value;
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