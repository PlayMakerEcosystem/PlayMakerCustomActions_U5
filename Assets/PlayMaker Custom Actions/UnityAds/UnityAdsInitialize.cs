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
	[Tooltip("Manually Initializes UnityAds.\n" +
		"Normally, this is done from Editor and you should only call this method if you have disabled UnityAds from the Editor Settings in the Connect Window ")]	
	public class UnityAdsInitialize : FsmStateAction
	{


		#if UNITY_IOS
		[ActionSection("You are currently targeting IOS")]
		[RequiredField]
		#endif
		public FsmString IosGameId;

		#if UNITY_ANDROID
		[ActionSection("You are currently targeting Android")]
		[RequiredField]
		#endif
		public FsmString AndroidGameId;

		public FsmBool testMode;

		public override void Reset()
		{
			IosGameId = null;
			AndroidGameId = null;
			testMode = false;
		}

		public override void OnEnter()
		{
			#if UNITY_ADS
				#if UNITY_IOS
				Advertisement.Initialize(IosGameId.Value,testMode.Value);
				#elif UNITY_ANDROID
				Advertisement.Initialize(AndroidGameId.Value,testMode.Value);
				#endif
			#endif
			Finish();
		}


	}
}