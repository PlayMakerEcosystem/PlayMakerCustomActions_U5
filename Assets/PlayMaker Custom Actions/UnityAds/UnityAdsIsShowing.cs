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
	[Tooltip("Get the current showing status of UnityAds.")]
	public class UnityAdsIsShowing : FsmStateAction
	{

		[Tooltip("The current ready of UnityAds.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isShowing;
		
		[Tooltip("Event sent if and when showing")]
		public FsmEvent isShowingEvent;

		[Tooltip("Event sent if and when not showing")]
		public FsmEvent isNotShowingEvent;

		[Tooltip("useful if you want to catch state changes")]
		public bool everyFrame;

		public override void Reset()
		{
			isShowing = null;

			isShowingEvent = null;
			isNotShowingEvent = null;

			everyFrame = true;
		}
		
		#if UNITY_IOS || UNITY_ANDROID
			#if UNITY_ADS
			public override void OnEnter()
			{

				checkIsShowing();

				if (!everyFrame)
				{
					Finish();
				}
			}
			
			public override void OnUpdate()
			{
				checkIsShowing();
			}

			void checkIsShowing()
			{
				bool _isShowing = Advertisement.isShowing;
				

				isShowing.Value = _isShowing;

				if (_isShowing)
				{
					Fsm.Event(isShowingEvent);
				}else{
					Fsm.Event(isNotShowingEvent);
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