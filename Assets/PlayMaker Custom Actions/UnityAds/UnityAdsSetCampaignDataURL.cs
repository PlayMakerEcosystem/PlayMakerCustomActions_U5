// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections.Generic;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("UnityAds")]
	[Tooltip("Get the current status of UnityAds.")]
	public class UnityAdsSetCampaignDataURL : FsmStateAction
	{
		[Tooltip("The Campaign data Url")]
		public FsmString url;

		public override void Reset()
		{
			url = "";

		}
		
		#if UNITY_IOS || UNITY_ANDROID
			#if UNITY_ADS
			public override void OnEnter()
			{

				Advertisement.SetCampaignDataURL(url.Value);

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