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
	[Tooltip("Show a UnityAds Advert. Can get feedback on result to reward user for example if ad was watched.")]
	public class UnityAdsShowAd : FsmStateAction
	{
		[Tooltip("ZoneId can be defined in the ads editor setup. LEave to none for default behavior")]
		public FsmString zoneId;
	
		[Tooltip("Using this property, you can pass an user identifier for server-to-server item redeem callbacks.")]
		public FsmString gamerSid;

		[ActionSection("Result")]

		[Tooltip("The current status of UnityAds.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isReady;

		[Tooltip("If set, you'll get this event fired if not ready. Else, the action will wait when ready and show the ad")]
		public FsmEvent isNotReadyEvent;

		[Tooltip("Event Sent if ad was properly watched")]
		public FsmEvent successEvent;

		[Tooltip("Event Sent if ad was skipped by the user")]
		public FsmEvent skippedEvent;

		[Tooltip("Event Sent if ad failed showing to the user")]
		public FsmEvent failedEvent;

		public override void Reset()
		{
			zoneId = new FsmString(){UseVariable=true};
			gamerSid = new FsmString(){UseVariable=true};

			successEvent = null;
			skippedEvent = null;
			failedEvent = null;
			isNotReadyEvent = null;
		}
	

		#if UNITY_IOS || UNITY_ANDROID
			#if UNITY_ADS
			public override void OnEnter()
			{

				if (!isAdReady() && isNotReadyEvent==null)
				{
					Fsm.Event(isNotReadyEvent);
					Finish();
				}


			}

			public override void OnUpdate()
			{

				if (isAdReady())
				{
					ShowAd();
				}

			}

			void ShowAd()
			{
				ShowOptions options = new ShowOptions { resultCallback = HandleShowResult, gamerSid = string.IsNullOrEmpty(gamerSid.Value)?null:gamerSid.Value};

				if (zoneId.IsNone)
				{
					Advertisement.Show(null, options);
				}else{
					Advertisement.Show(zoneId.Value, options);
				}


				if (successEvent==null && skippedEvent == null && failedEvent ==null)
				{
					Finish();
				}
			}

			bool isAdReady()
			{
				bool _isReady = false;
				if (zoneId.IsNone)
				{
					_isReady = Advertisement.IsReady();
				}else{
					_isReady = Advertisement.IsReady(zoneId.Value);
				}

				isReady.Value = _isReady;
				return _isReady;
			}

			private void HandleShowResult(ShowResult result)
			{
				switch (result)
				{
				case ShowResult.Finished:
					//Debug.Log("The ad was successfully shown.");
					Fsm.Event(successEvent);
					break;
				case ShowResult.Skipped:
					//Debug.Log("The ad was skipped before reaching the end.");
					Fsm.Event(skippedEvent);
					break;
				case ShowResult.Failed:
					//Debug.LogError("The ad failed to be shown.");
					Fsm.Event(failedEvent);

					break;
				}

				Finish();
			}
			#endif
		#else
			public override void OnEnter()
			{
				Debug.Log("UnityAds only supported on IOS and Android Platforms");
				Fsm.Event(failedEvent);

				Finish();
			}
		#endif

	}

}