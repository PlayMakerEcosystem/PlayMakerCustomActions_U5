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
	[Tooltip("Sets the debug level of UnityAds.")]
	#if UNITY_5_6_OR_NEWER
	[Obsolete("Use UnityAdsSetDebugMode instead")]
	#endif
	public class UnityAdsSetDebugLevel : FsmStateAction
	{
		#if UNITY_ADS
		[Tooltip("The Debug Level")]
		public Advertisement.DebugLevel  debugLevel;


		public override void Reset()
		{
			debugLevel = Advertisement.DebugLevel.None;
		}
		#endif

		#if UNITY_IOS || UNITY_ANDROID
			#if UNITY_ADS
			public override void OnEnter()
			{
			 	Advertisement.debugLevel = debugLevel;
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