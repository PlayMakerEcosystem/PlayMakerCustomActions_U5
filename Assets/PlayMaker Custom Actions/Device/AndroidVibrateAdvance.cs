// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: vibration android

using UnityEngine;
using System.Collections;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Android - causes the device to vibrate with custom settings. Please read instruction by clicking on the action url link. If set incorrectly, you may get odd behavior as a result!")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11816.0")]
	public class AndroidVibrateAdvance : FsmStateAction
	{
		
		#if UNITY_ANDROID && !UNITY_EDITOR
		public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
		#else
		public static AndroidJavaClass unityPlayer;
		public static AndroidJavaObject currentActivity;
		public static AndroidJavaObject vibrator;
		#endif

		[ActionSection("Setup")]
		[Tooltip("0 or null = off")]
		public FsmInt milliseconds;

		[ActionSection("or Pattern Setup")]
		[Tooltip("-1 means do not repeat")]
		public FsmInt repeat;
		[Tooltip("Set milliseconds array to create a pattern. Note: Make the first index in pattern 0, followed by the rest to start immediately. The first value indicates the number of milliseconds to wait before turning the vibrator on.")]
		public FsmInt[] setPattern;

		[ActionSection("Option")]
		[Tooltip("Cancel the vibration by 're-entering' action with bool set to True. Do *not* do this immediately after calling vibrate. It may not have time to even begin vibrating!")]
		public FsmBool cancelVibrator;
		[Tooltip("The screen will never sleep if set to true as when the screen sleeps the vibration stops")]
		public FsmBool disableSleepTimeout;

		[ActionSection("Output")]
		public FsmBool notAndroid;
		[Tooltip("The event to send if it has No vibrator")]
		public FsmEvent notAndroidEvent;
		[Tooltip("If you cancel")]
		public FsmEvent vibratorCancelled;


				public override void Reset()
		{


			milliseconds = new FsmInt{UseVariable = true};
			cancelVibrator = false;
			notAndroid = false;
			notAndroidEvent = null;
			repeat = -1;
			setPattern = new FsmInt[0];
			disableSleepTimeout = true;

		}


		public override void OnEnter()
		{
			if (cancelVibrator.Value == true)
			{
				Cancel();
				Fsm.Event(vibratorCancelled);
				Finish();

			}

			if (disableSleepTimeout.Value == true)
			{

				Screen.sleepTimeout = SleepTimeout.NeverSleep;

			} 

			notAndroid.Value = isAndroid();

			if (notAndroid.Value == false)
			{
				notAndroid.Value = true;
				Fsm.Event(notAndroidEvent);
				Finish();
				
			} 

			else
			{

			if (milliseconds.Value == 0 || milliseconds.IsNone)
			{

					int length = setPattern.Length;
					long[] patternTemp = new long[length];

				for (int i = 0; i < setPattern.Length; i++)
				{

						patternTemp[i] =  (long)setPattern[i].Value;

				} 

					Vibrate(patternTemp, repeat.Value);

			} 

			else
			{

				Vibrate((long) milliseconds.Value);

			} 
			}

	

		}

		public override void OnUpdate()
		{
			
			if (cancelVibrator.Value == true)
			{
				Cancel();
				Fsm.Event(vibratorCancelled);
				Finish();
				
			}
			
		}


		public static void Vibrate()
		{
			if (isAndroid ())
				vibrator.Call ("vibrate");
			else
			{
				#if UNITY_IPHONE || UNITY_ANDROID
				Handheld.Vibrate ();
				#endif
			}
		}
		
		
		public static void Vibrate(long milliseconds)
		{
			if (isAndroid())
				vibrator.Call("vibrate", milliseconds);
		}
		
		public static void Vibrate(long[] pattern, int repeat)
		{
			if (isAndroid())
				vibrator.Call("vibrate", pattern, repeat);

		}
		
		public static bool HasVibrator()
		{
			return isAndroid();
		}
		
		public static void Cancel()
		{
			if (isAndroid())
				vibrator.Call("cancel");
			return;
		}
		
		private static bool isAndroid()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			return true;
			#else
			return false;
			#endif
		}


	}
}