// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.VR;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VR")]
	[Tooltip("Center tracking on the current pose.")]
	public class InputTrackingRecenter : FsmStateAction
	{

		public override void OnEnter()
		{
			InputTracking.Recenter();
			Finish();		
		}

	}
}