// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/



using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("WebCam")]
	[Tooltip("Checks if camera faces the same direction as screen.")]
	public class WebcamIsFrontFacing : FsmStateAction
	{
		[ActionSection("Output")]
		[Tooltip("Name of front facing camera")]
		public FsmString webcamName;

        [Tooltip("Event to send if a webcam is front facing was found.")]
		public FsmEvent webcamIsFrontFacing;

		[Tooltip("True if front facing webcam was found.")]
		public FsmBool isFrontFacing; 

		private string camName;
		private WebCamTexture webCamTexture;


		public override void Reset()
		{
			webcamName = null;
			webcamIsFrontFacing = null;
			isFrontFacing = false;
		}

		public override void OnEnter()
		{
			WebCamDevice[] devices = WebCamTexture.devices;
			
			for(int i = 0; i < devices.Length; i++)  
			{  

				if (devices[i].isFrontFacing) {
					webcamName.Value = WebCamTexture.devices[i].name;
					isFrontFacing.Value = true;
					Fsm.Event(webcamIsFrontFacing);
					Finish ();
					
				}

			} 

			isFrontFacing.Value = false;

			Finish ();
		}
	}
}

