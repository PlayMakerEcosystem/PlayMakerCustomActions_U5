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
	public class UnityAdsIsReady : FsmStateAction
	{
		[Tooltip("ZoneId can be defined in the ads editor setup. Leave to none for default behavior")]
		public FsmString zoneId;

		[ActionSection("Result")]
		
		[Tooltip("The current ready of UnityAds.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isReady;
		
		[Tooltip("Event sent if and when ready")]
		public FsmEvent isReadyEvent;

		[Tooltip("Event sent if and when not ready")]
		public FsmEvent isNotReadyEvent;

		[Tooltip("useful if you want to catch when UnityAds will become ready")]
		public bool everyFrame;

		public override void Reset()
		{
			zoneId = new FsmString(){UseVariable=true};

			isReadyEvent = null;
			isNotReadyEvent = null;

			everyFrame = true;
		}
		
		#if UNITY_IOS || UNITY_ANDROID
			#if UNITY_ADS
			public override void OnEnter()
			{

				checkIsReady();

				if (!everyFrame)
				{
					Finish();
				}
			}
			
			public override void OnUpdate()
			{
				checkIsReady();
			}

			void checkIsReady()
			{
				bool _isReady = false;
				if (zoneId.IsNone)
				{
					_isReady = Advertisement.IsReady();
				}else{
					_isReady = Advertisement.IsReady(zoneId.Value);
				}

				isReady.Value = _isReady;

				if (_isReady)
				{
					Fsm.Event(isReadyEvent);
				}else{
					Fsm.Event(isNotReadyEvent);
				}
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