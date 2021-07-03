// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Romi Fauzi
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Device Accelerometer Acceleration

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets the last measured linear acceleration of a device and stores it in a Vector3 Variable. Also support additional Quaternion retrieved from Calibration.")]
	public class GetDeviceAccelerationV2 : FsmStateAction
	{
		// TODO: Figure out some nice mapping options for common use cases.
/*		public enum MappingOptions
		{
			Flat,
			Vertical
		}
		
		[Tooltip("Flat is god for marble rolling games, vertical is good for Doodle Jump type games.")]
		public MappingOptions mappingOptions;
*/

		[UIHint(UIHint.Variable)]
		public FsmVector3 storeVector;
		[UIHint(UIHint.Variable)]
		public FsmFloat storeX;
		[UIHint(UIHint.Variable)]
		public FsmFloat storeY;
		[UIHint(UIHint.Variable)]
		public FsmFloat storeZ;
		public FsmFloat multiplier;
		public bool everyFrame;
		public bool useCalibrateQuat;
		public FsmQuaternion calibrationQuaternion;
		
		public override void Reset()
		{
			storeVector = null;
			storeX = null;
			storeY = null;
			storeZ = null;
			multiplier = 1;
			everyFrame = false;
			useCalibrateQuat = false;
		}
		
		public override void OnEnter()
		{
			DoGetDeviceAcceleration();
			
			if (!everyFrame)
				Finish();
		}
		

		public override void OnUpdate()
		{
			DoGetDeviceAcceleration();
		}
		
		void DoGetDeviceAcceleration()
		{
/*			var dir = Vector3.zero;
			
			switch (mappingOptions) 
			{
			case MappingOptions.Flat:
				
				dir.x = Input.acceleration.x;
				dir.y = Input.acceleration.z;
				dir.z = Input.acceleration.y;
				break;
					
				
			case MappingOptions.Vertical:
				dir.x = Input.acceleration.x;
				dir.y = Input.acceleration.y;
				dir.z = Input.acceleration.x;
				break;
			}
*/
			var rawDir = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
			var dir = new Vector3();

			if (useCalibrateQuat)
			{
				dir = calibrationQuaternion.Value * rawDir;
			} else {
				dir = rawDir;
			}
			
			if (!multiplier.IsNone)
			{
				dir *= multiplier.Value;
			}
			
			storeVector.Value = dir;
			storeX.Value = dir.x;
			storeY.Value = dir.y;
			storeZ.Value = dir.z;
		}
		
	}
}