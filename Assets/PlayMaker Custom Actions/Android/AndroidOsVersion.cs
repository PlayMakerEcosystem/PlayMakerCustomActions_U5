// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Android")]
	[Tooltip("Get the Android SDK version as an Fsm Int  (example: SDK version JELLY_BEAN - int result = 16) - alternative to SystemInfo.operatingSystem")]
	[HelpUrl("http://developer.android.com/reference/android/os/Build.VERSION_CODES.html")]
	public class AndroidOsVersion : FsmStateAction
	{
		[Tooltip("Get the Android SDK version. If 0 then no data or not android")]
		public FsmInt sdkVersion;


		public override void Reset()
		{
		
			sdkVersion = null;
		}

		public override void OnEnter()
		{
			sdkVersion.Value = GetSDKLevel();
			Finish();
		}

		public int GetSDKLevel() {

		#if UNITY_ANDROID && !UNITY_EDITOR
		var clazz = AndroidJNI.FindClass("android.os.Build$VERSION");
		var fieldID = AndroidJNI.GetStaticFieldID(clazz, "SDK_INT", "I");
		var sdkLevel = AndroidJNI.GetStaticIntField(clazz, fieldID);
		return sdkLevel;

		#else
		return 0;
		#endif

		}
	}
}
