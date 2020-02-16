// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("WebCam")]
	[Tooltip("Count the amount of connected camera")]
	public class WebcamCount : FsmStateAction
	{
		[ActionSection("Output")]
		[Tooltip("Name of front facing camera")]
		public FsmInt count;

		private WebCamTexture webCamTexture;


		public override void Reset()
		{
			count = null;
		
		}

		public override void OnEnter()
		{
			WebCamDevice[] devices = WebCamTexture.devices;
			
			count.Value = devices.Length;
			
			Finish ();
		}
	}
}
