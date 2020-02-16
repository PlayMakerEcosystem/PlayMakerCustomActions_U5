// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10490.0


using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Social")]
	[Tooltip("Share msg via android available apps - DOES NOT WORK WITH FACEBOOK/TWITTER")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10490.0")]
	public class ShareAppAndroid : FsmStateAction
	{
		[ActionSection("Setup")]
		[UIHint(UIHint.FsmString)]
		public FsmString title;
		[UIHint(UIHint.FsmString)]
		public FsmString message;

		[ActionSection("Option")]
		[Tooltip("Does not set as default app after select")]
		public FsmBool androidChooserOn;

		public override void Reset()
		{
			title = null;
			message = null;
			androidChooserOn = true;

		}

		public override void OnEnter()
		{
			DoShare();

				Finish();
		}
		

		
		void DoShare()
		{

			if (message.Value == null) return;

			#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			intentObject.Call<AndroidJavaObject>("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), title.Value);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message.Value);
			AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

			if (androidChooserOn.Value == true){

			AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
			currentActivity.Call("startActivity", jChooser);
			}

			else {
			currentActivity.Call ("startActivity", intentObject);
			}

			#endif
		}
		
	}
}

