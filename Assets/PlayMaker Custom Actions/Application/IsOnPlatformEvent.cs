// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("The counterpart to 'PlatformDependentEvents'. Sends an Event if or if not on the specified Platforms.")]
	public class IsOnPlatformEvent : FsmStateAction
	{
		public enum PlatformDependentFlags
		{
			UNITY_EDITOR,
			UNITY_EDITOR_WIN,
			UNITY_EDITOR_OSX,
			UNITY_STANDALONE_OSX,
			UNITY_DASHBOARD_WIDGET,
			UNITY_STANDALONE_WIN,
			UNITY_STANDALONE_LINUX,
			UNITY_STANDALONE,
			UNITY_WEBPLAYER,
			UNITY_WII,
			UNITY_IPHONE,
			UNITY_ANDROID,
			UNITY_PS3,
			UNITY_XBOX360,
			UNITY_NACL,
			UNITY_FLASH,
			UNITY_BLACKBERRY,
			UNITY_WP8,
			UNITY_METRO,
			UNITY_WINRT,
			UNITY_IOS,
			UNITY_PS4,
			UNITY_XBOXONE,
			UNITY_TIZEN,
			UNITY_WEBGGL
		}

		[RequiredField]
		[Tooltip("The platforms")]
		public PlatformDependentFlags[] platforms;

		[Tooltip("The event to send IF on any of the specified platforms. Note: Gets send on the first platform it finds it is on.")]
		public FsmEvent onEvent;

		[Tooltip("The event to send IF NOT on any of the specified platforms.")]
		public FsmEvent notOnEvent;

		private bool isOnEditor = false;
		private bool isOnEditorWin = false;
		private bool isOnEditorOSX = false;
		private bool isOnStandaloneOSX = false;
		private bool isOnDashboardWidget = false;
		private bool isOnStandaloneWin = false;
		private bool isOnStandaloneLinux = false;
		private bool isOnStandalone = false;
		private bool isOnWebplayer = false;
		private bool isOnWii = false;
		private bool isOnIPhone = false;
		private bool isOnAndroid = false;
		private bool isOnPS3 = false;
		private bool isOnXBOX360 = false;
		private bool isOnNACL = false;
		private bool isOnFlash = false;
		private bool isOnBlackberry = false;
		private bool isOnWP8 = false;
		private bool isOnMetro = false;
		private bool isOnWinRT = false;
		private bool isOnIOS = false;
		private bool isOnPS4 = false;
		private bool isOnXBoxOne = false;
		private bool isOnTizen = false;
		private bool isOnWebGL = false;

		public override void Reset()
		{
			platforms = new PlatformDependentFlags[1];
			platforms[0] = PlatformDependentFlags.UNITY_WEBPLAYER;
			onEvent = null;
			notOnEvent = null;
		}

		public override void OnEnter()
		{

			foreach(PlatformDependentFlags _flag in platforms)
			{
#if UNITY_EDITOR
				if(_flag == PlatformDependentFlags.UNITY_EDITOR) isOnEditor = true;
#endif

#if UNITY_EDITOR_WIN
				if(_flag == PlatformDependentFlags.UNITY_EDITOR_WIN) isOnEditorWin = true;
#endif

#if UNITY_EDITOR_OSX
					if(_flag == PlatformDependentFlags.UNITY_EDITOR_OSX )  isOnEditorOSX = true;
#endif

#if UNITY_STANDALONE_OSX
					if(_flag == PlatformDependentFlags.UNITY_STANDALONE_OSX )  isOnStandaloneOSX = true;
#endif

#if UNITY_DASHBOARD_WIDGET
					if(_flag == PlatformDependentFlags.UNITY_DASHBOARD_WIDGET )  isOnDashboardWidget = true;
#endif

#if UNITY_STANDALONE_WIN
				if(_flag == PlatformDependentFlags.UNITY_STANDALONE_WIN) isOnStandaloneWin = true;
#endif

#if UNITY_STANDALONE_LINUX
					if(_flag == PlatformDependentFlags.UNITY_STANDALONE_LINUX )  isOnStandaloneLinux = true;
#endif

#if UNITY_STANDALONE
				if(_flag == PlatformDependentFlags.UNITY_STANDALONE) isOnStandalone = true;
#endif

#if UNITY_WEBPLAYER
					if(_flag == PlatformDependentFlags.UNITY_WEBPLAYER )  isOnWebplayer = true;
#endif

#if UNITY_WII
					if(_flag == PlatformDependentFlags.UNITY_WII )  isOnWii = true;
#endif

#if UNITY_IPHONE || UNITY_IOS
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
#endif

#if UNITY_ANDROID
					if(_flag == PlatformDependentFlags.UNITY_ANDROID )  isOnAndroid = true;
#endif

#if UNITY_PS3
					if(_flag == PlatformDependentFlags.UNITY_PS3 )  isOnPS3 = true;
#endif

#if UNITY_XBOX360
					if(_flag == PlatformDependentFlags.UNITY_XBOX360 )  isOnXBOX360 = true;
#endif

#if UNITY_NACL
					if(_flag == PlatformDependentFlags.UNITY_NACL )  isOnNACL = true;
#endif

#if UNITY_FLASH
					if(_flag == PlatformDependentFlags.UNITY_FLASH )  isOnFlash = true;
#endif

#if UNITY_BLACKBERRY
					if(_flag == PlatformDependentFlags.UNITY_BLACKBERRY )  isOnBlackberry = true;
#endif

#if UNITY_WP8
					if(_flag == PlatformDependentFlags.UNITY_WP8 )  isOnWP8 = true;
#endif

#if UNITY_METRO
					if(_flag == PlatformDependentFlags.UNITY_METRO )  isOnMetro = true;
#endif

#if UNITY_WINRT
					if(_flag == PlatformDependentFlags.UNITY_WINRT )  isOnWinRT = true;
#endif

#if UNITY_PS4
					if(_flag == PlatformDependentFlags.UNITY_PS4 )  isOnPS4 = true;
#endif

#if UNITY_XBOXONE
					if(_flag == PlatformDependentFlags.UNITY_XBOXONE )  isOnXBoxOne = true;
#endif

#if UNITY_TIZEN
					if(_flag == PlatformDependentFlags.UNITY_TIZEN )  isOnTizen = true;
#endif

#if UNITY_WEBGL
					if(_flag == PlatformDependentFlags.UNITY_WEBGL )  isOn = true;
#endif
			}

			if(isOnEditor || isOnEditorWin || isOnEditorOSX || isOnStandalone || isOnStandaloneOSX || isOnStandaloneWin || isOnStandaloneLinux || isOnDashboardWidget || isOnWebplayer || isOnWii || isOnIPhone || isOnIOS || isOnPS3 || isOnPS4 || isOnAndroid || isOnXBOX360 || isOnXBoxOne || isOnNACL || isOnFlash || isOnBlackberry || isOnWP8 || isOnMetro || isOnWinRT || isOnTizen || isOnWebGL)
			{
				Fsm.Event(onEvent);
			} else
			{
				Fsm.Event(notOnEvent);
			}

			Finish();
		}

	}
}
