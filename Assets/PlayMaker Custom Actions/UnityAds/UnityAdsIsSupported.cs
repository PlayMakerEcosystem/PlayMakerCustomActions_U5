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
	[Tooltip("Get the supported status of UnityAds for the current platform.")]
	public class UnityAdsIsSupported : FsmStateAction
	{

		[Tooltip("The suppported statux of UnityAds.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isSupported;
		
		[Tooltip("Event sent if and when supported")]
		public FsmEvent isSupportedEvent;

		[Tooltip("Event sent if and when not supported")]
		public FsmEvent isNotSupporedEvent;


		public override void Reset()
		{
			isSupported = null;
			isSupportedEvent = null;
			isNotSupporedEvent = null;

		}

		public override void OnEnter()
		{

			bool _isSupported = false;
			#if UNITY_ADS
			_isSupported = Advertisement.isSupported;
			#endif

			isSupported.Value = _isSupported;
			
			if (_isSupported)
			{
				Fsm.Event(isSupportedEvent);
			}else{
				Fsm.Event(isNotSupporedEvent);
			}
			Finish();

		}

		
	}
	
}