// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.VR;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VR")]
	[Tooltip("Get Type of VR device that is currently in use.")]
	public class VRSettingsGetVrDeviceType : FsmStateAction
	{

		[Tooltip("Type of VR device currently in use")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(VRDeviceType))]
		public FsmEnum deviceType;

		[Tooltip("No VR Device Event")]
		public FsmEvent noDevice;

		[Tooltip("Stereo 3D via D3D11 or OpenGL.")]
		public FsmEvent isStereo;

		[Tooltip("Split screen stereo 3D (the left and right cameras are rendered side by side).")]
		public FsmEvent isSplit;

		[Tooltip("Oculus family of VR devices.")]
		public FsmEvent isOculus;

		[Tooltip("Sony's PlayStation VR device for Playstation 4 (formerly called Project Morpheus VR).")]
		public FsmEvent isPlayStationVR;

		public override void Reset()
		{
			deviceType = null;

			noDevice = null;
			isStereo = null;
			isSplit = null;
			isOculus = null;
			isPlayStationVR = null;
		}

		public override void OnEnter()
		{
			deviceType.Value = VRSettings.loadedDevice;

			if (noDevice != null && VRSettings.loadedDevice == VRDeviceType.None ) {
				Fsm.Event (noDevice);
			}

			if (isStereo != null && VRSettings.loadedDevice == VRDeviceType.Stereo ) {
				Fsm.Event (isStereo);
			}

			if (isPlayStationVR != null && VRSettings.loadedDevice == VRDeviceType.PlayStationVR ) {
				Fsm.Event (isPlayStationVR);
			}

			if (isOculus != null && VRSettings.loadedDevice == VRDeviceType.Oculus ) {
				Fsm.Event (isOculus);
			}


			Finish ();
		}

	}
}