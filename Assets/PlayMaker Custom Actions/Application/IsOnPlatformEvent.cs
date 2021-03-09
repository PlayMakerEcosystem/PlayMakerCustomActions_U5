// (c) Copyright HutongGames, LLC 2010-2021. All rights reserved. 
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek
// Additions and modifications : BrokenStylus

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Sends an Event if on the specified Platforms or not. Detects the targeted build platform.")]
	//[Note("Stadia and Cloud Rendering not yet supported.")]

	public class IsOnPlatformEvent : FsmStateAction
	{
		[UIHint(UIHint.Description)]
		public string desc = "Warning: Stadia and Cloud Rendering not yet supported.";

		public enum PlatformDependentFlags
		{
			UNITY_EDITOR,
			UNITY_EDITOR_WIN,
			UNITY_EDITOR_OSX,
			UNITY_EDITOR_LINUX, //added March 2021
			UNITY_STANDALONE,
			UNITY_STANDALONE_WIN,
			UNITY_STANDALONE_OSX,
			UNITY_STANDALONE_LINUX, //added March 2021
			UNITY_WSA, //added March 2021
			NETFX_CORE, //added March 2021
			UNITY_WSA_10_0, //added March 2021
			WINDOWS_UWP, //added March 2021
			UNITY_WINRT,
			UNITY_WEBPLAYER,
			UNITY_WEBGL,
			UNITY_FACEBOOK, //added March 2021
			UNITY_FLASH,
			UNITY_NACL,
			UNITY_WII,
			UNITY_SWITCH_Experimental, //added March 2021
			UNITY_PS3,
			UNITY_PS4,
			UNITY_XBOX360,
			UNITY_XBOXONE,
			UNITY_LUMIN, //added March 2021
			UNITY_STADIA_Unsupported, //added March 2021 - no code found
			UNITY_TVOS,
			UNITY_IOS,
			UNITY_IPHONE,
			UNITY_ANDROID,
			UNITY_TIZEN,
			UNITY_METRO,
			UNITY_WP8,
			UNITY_BLACKBERRY,
			UNITY_ASSERTIONS, //added March 2021
			UNITY_64, //added March 2021
			UNITY_ANALYTICS, //added March 2021
			UNITY_CLOUD_RENDERING_Unsupported, //added March 2021 - no code found
			UNITY_DASHBOARD_WIDGET,
		}

		[RequiredField]
		[Tooltip("The platforms")]
		public PlatformDependentFlags[] platforms;

		[Tooltip("The event to send IF on any of the specified platforms. Note: Gets send on the first platform it finds it is on.")]
		public FsmEvent onEvent;

		[Tooltip("The event to send IF NOT on any of the specified platforms.")]
		public FsmEvent notOnEvent;

		public static RuntimePlatform platform;

		private bool isOnEditor = false;
		private bool isOnEditorWin = false;
		private bool isOnEditorOSX = false;
		private bool isOnEditorLinux = false; //added March 2021

		private bool isOnStandalone = false;
		private bool isOnStandaloneWin = false;
		private bool isOnStandaloneOSX = false;
		private bool isOnStandaloneLinux = false;

		private bool isOnWSA = false;  //added March 2021
		private bool isOnNetFXCore = false; //added March 2021
		private bool isOnWSA1010 = false; //added March 2021
		private bool isOnWindowsUWP = false; //added March 2021
		private bool isOnWinRT = false;

		private bool isOnWebplayer = false;
		private bool isOnWebGL = false;
		private bool isOnFacebook = false; //added March 2021
		private bool isOnFlash = false;
		private bool isOnNACL = false;

		private bool isOnWii = false;
		private bool isOnSwitch = false; //added March 2021
		private bool isOnPS3 = false;
		private bool isOnPS4 = false;
		private bool isOnXBOX360 = false;
		private bool isOnXBoxOne = false;

		private bool isOnLumin = false; //added March 2021
		private bool isOnStadia = false; //added March 2021
		private bool isOnTVOS = false;

		private bool isOnIOS = false;
		private bool isOnIPhone = false;
		private bool isOnAndroid = false;
		private bool isOnTizen = false;
		private bool isOnMetro = false;
		private bool isOnWP8 = false;
		private bool isOnBlackberry = false;

		private bool isOnAssertions = false; //added March 2021
		private bool isOn64 = false; //added March 2021
		private bool isOnUnityAnalytics = false; //added March 2021
		private bool isOnCloudRendering = false; //added March 2021
		private bool isOnDashboardWidget = false;


		public override void Reset()
		{
			platforms = new PlatformDependentFlags[1];
			platforms[0] = PlatformDependentFlags.UNITY_WEBPLAYER;
			onEvent = null;
			notOnEvent = null;
		}

		public override void OnEnter()
		{

			foreach (PlatformDependentFlags _flag in platforms)
			{

				//Unity Editor
#if UNITY_EDITOR
				if (_flag == PlatformDependentFlags.UNITY_EDITOR) isOnEditor = true;
#endif

#if UNITY_EDITOR_WIN
				if (_flag == PlatformDependentFlags.UNITY_EDITOR_WIN) isOnEditorWin = true;
#endif

#if UNITY_EDITOR_OSX
					if(_flag == PlatformDependentFlags.UNITY_EDITOR_OSX )  isOnEditorOSX = true;
#endif

#if UNITY_EDITOR_LINUX
					if(_flag == PlatformDependentFlags.UNITY_EDITOR_LINUX )  isOnEditorLinux = true;
#endif


				//Unity Standalone
#if UNITY_STANDALONE
				if (_flag == PlatformDependentFlags.UNITY_STANDALONE) isOnStandalone = true;
#endif

#if UNITY_STANDALONE_WIN
				if (_flag == PlatformDependentFlags.UNITY_STANDALONE_WIN) isOnStandaloneWin = true;
#endif

#if UNITY_STANDALONE_OSX
					if(_flag == PlatformDependentFlags.UNITY_STANDALONE_OSX )  isOnStandaloneOSX = true;
#endif

#if UNITY_STANDALONE_LINUX
					if(_flag == PlatformDependentFlags.UNITY_STANDALONE_LINUX )  isOnStandaloneLinux = true;
#endif


				//More Microsoft
#if UNITY_WSA
					if(_flag == PlatformDependentFlags.UNITY_WSA )  isOnWSA = true;
#endif

#if NETFX_CORE
					if(_flag == PlatformDependentFlags.NETFX_CORE )  isOnNetFXCore = true;
#endif

#if UNITY_WSA_10_10
					if(_flag == PlatformDependentFlags.UNITY_WSA_10_10 )  isOnWSA1010 = true;
#endif

#if WINDOWS_UWP
					if(_flag == PlatformDependentFlags.WINDOWS_UWP )  isOnWindowsUWP = true;
#endif

#if UNITY_WINRT
					if(_flag == PlatformDependentFlags.UNITY_WINRT )  isOnWinRT = true;
#endif


				//Web
#if UNITY_WEBPLAYER
					if(_flag == PlatformDependentFlags.UNITY_WEBPLAYER )  isOnWebplayer = true;
#endif

#if UNITY_WEBGL
					if(_flag == PlatformDependentFlags.UNITY_WEBGL )  isOnWebGL = true;
#endif

#if UNITY_FACEBOOK
					if(_flag == PlatformDependentFlags.UNITY_FACEBOOK )  isOnFacebook = true;
#endif

#if UNITY_FLASH
					if(_flag == PlatformDependentFlags.UNITY_FLASH )  isOnFlash = true;
#endif

#if UNITY_NACL
					if(_flag == PlatformDependentFlags.UNITY_NACL )  isOnNACL = true;
#endif


				//Video Game Consoles
#if UNITY_WII
					if(_flag == PlatformDependentFlags.UNITY_WII )  isOnWii = true;
#endif

				//Nintendo Switch
				if (Application.platform == RuntimePlatform.Switch && _flag == PlatformDependentFlags.UNITY_SWITCH_Experimental) isOnSwitch = true;


#if UNITY_PS3
					if(_flag == PlatformDependentFlags.UNITY_PS3 )  isOnPS3 = true;
#endif

#if UNITY_PS4
					if(_flag == PlatformDependentFlags.UNITY_PS4 )  isOnPS4 = true;
#endif

#if UNITY_XBOX360
					if(_flag == PlatformDependentFlags.UNITY_XBOX360 )  isOnXBOX360 = true;
#endif

#if UNITY_XBOXONE
					if(_flag == PlatformDependentFlags.UNITY_XBOXONE )  isOnXBoxOne = true;
#endif


				//AR, VR, TV and Cloud Platforms
#if UNITY_LUMIN
					if(_flag == PlatformDependentFlags.UNITY_LUMIN )  isOnLumin = true;
#endif

#if UNITY_STADIA_Unsupported
				//Google Stadia                   
				if (Application.platform == RuntimePlatform.Stadia && _flag == PlatformDependentFlags.UNITY_STADIA_Unsupported) isOnStadia = true;
#endif

#if UNITY_TVOS
					if (_flag == PlatformDependentFlags.UNITY_TVOS )  isOnTVOS = true;
#endif


				//Smartphones and Tablets
				//1st group: original code block
				/*#if UNITY_IPHONE || UNITY_IOS
									if (Enum.Equals(platforms[i],PlatformDependentFlags.UNITY_IPHONE ) )
									{
										UnityEngine.Debug.Log("---------- WE FIRE "+onEvent.Name);
										 isOnIPhone = true;
										return;
									}
									if( Enum.Equals(platforms[i],PlatformDependentFlags.UNITY_IOS ) )
									{
										UnityEngine.Debug.Log("---------- WE FIRE "+onEvent.Name);
										 isOnIOS = true;
										return;
									}
#endif*/
				//new block by Alex Chouls (March 2021)
#if UNITY_IPHONE || UNITY_IOS
					if(_flag == PlatformDependentFlags.UNITY_IPHONE )
					{
						UnityEngine.Debug.Log("---------- WE FIRE "+onEvent.Name);
						 isOnIPhone = true;
						return;
					}
					if(_flag == PlatformDependentFlags.UNITY_IOS )
					{
						UnityEngine.Debug.Log("---------- WE FIRE "+onEvent.Name);
						 isOnIOS = true;
						return;
					}
#endif

				//version with separate checks,
				/*#if UNITY_IOS
									if (_flag == PlatformDependentFlags.UNITY_IOS )	 isOnIOS = true;
				#endif

				#if UNITY_IPHONE
									if(_flag == PlatformDependentFlags.UNITY_IPHONE )	isOnIPhone = true;
				#endif*/

				//could need an iPad OS check here with UNITY_IPADOS and isOnIpadOS
				//if not, then a separate iPhoneOS check is necessary so users can check if it's on iPhone or not
				//assuming Unity translates iPadOs as iOS and doesn't trigger iPhoneOS too

#if UNITY_ANDROID
					if(_flag == PlatformDependentFlags.UNITY_ANDROID )  isOnAndroid = true;
#endif

#if UNITY_TIZEN
					if(_flag == PlatformDependentFlags.UNITY_TIZEN )  isOnTizen = true;
#endif

#if UNITY_METRO
					if(_flag == PlatformDependentFlags.UNITY_METRO )  isOnMetro = true;
#endif

#if UNITY_WP8
					if(_flag == PlatformDependentFlags.UNITY_WP8 )  isOnWP8 = true;
#endif

#if UNITY_BLACKBERRY
					if(_flag == PlatformDependentFlags.UNITY_BLACKBERRY )  isOnBlackberry = true;
#endif


				//Miscellaneous
#if UNITY_ASSERTIONS
				if (_flag == PlatformDependentFlags.UNITY_ASSERTIONS) isOnAssertions = true;
#endif

#if UNITY_64
					if(_flag == PlatformDependentFlags.UNITY_64 )  isOn64 = true;
#endif

#if UNITY_ANALYTICS
				if (_flag == PlatformDependentFlags.UNITY_ANALYTICS) isOnUnityAnalytics = true;
#endif

#if UNITY_STADIA_Unsupported
				//Cloud Rendering
				if (Application.platform == RuntimePlatform.CloudRendering && _flag == PlatformDependentFlags.UNITY_CLOUD_RENDERING_Unsupported) isOnCloudRendering = true;
#endif

#if UNITY_DASHBOARD_WIDGET
					if(_flag == PlatformDependentFlags.UNITY_DASHBOARD_WIDGET )  isOnDashboardWidget = true;
#endif


			}

			if (isOnEditor || isOnEditorWin || isOnEditorOSX || isOnEditorLinux || isOnStandalone || isOnStandaloneOSX || isOnStandaloneWin || isOnStandaloneLinux || isOnWSA || isOnNetFXCore || isOnWSA1010 || isOnWindowsUWP || isOnWinRT || isOnWebplayer || isOnWebGL || isOnFacebook || isOnFlash || isOnNACL || isOnWii || isOnSwitch || isOnPS3 || isOnPS4 || isOnXBOX360 || isOnXBoxOne || isOnLumin || isOnStadia || isOnTVOS || isOnIOS || isOnIPhone || isOnAndroid || isOnTizen || isOnMetro || isOnWP8 || isOnBlackberry || isOnAssertions || isOn64 || isOnUnityAnalytics || isOnCloudRendering || isOnDashboardWidget)
			{
				Fsm.Event(onEvent);
			}
			else
			{
				Fsm.Event(notOnEvent);
			}

			Finish();
		}

	}
}