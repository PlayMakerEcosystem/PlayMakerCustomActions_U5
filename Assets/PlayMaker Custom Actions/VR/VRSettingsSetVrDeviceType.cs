// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.VR;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VR")]
	[Tooltip("Set Type of VR to the desired VR device and it will be loaded. Note that if a VR device was already loaded, a restart may be forced.")]
	public class VRSettingsSetVrDeviceType : FsmStateAction
	{

		[Tooltip("Type of VR device currently in use")]
		[ObjectType(typeof(VRDeviceType))]
		public FsmEnum deviceType;

		public override void Reset()
		{
			deviceType = VRDeviceType.None;
		}

		public override void OnEnter()
		{
			
			VRSettings.loadedDevice = (VRDeviceType)deviceType.Value;

			Finish ();
		}

	}
}