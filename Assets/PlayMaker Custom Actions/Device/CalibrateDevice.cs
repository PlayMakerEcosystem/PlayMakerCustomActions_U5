// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Romi Fauzi
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Device Accelerometer Acceleration Calibrate Calibration

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Calibrate the Device Accelerometer Start Rotation.")]
	public class CalibrateDevice : FsmStateAction
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
		public FsmQuaternion calibrationQuaternion;
		
		public override void Reset()
		{
		}
		
		public override void OnEnter()
		{
			CalibrateAccelerometer ();
			Finish();
		}
		
		void CalibrateAccelerometer () {
			Vector3 accelerationSnapshot = Input.acceleration;
			Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
			calibrationQuaternion.Value = Quaternion.Inverse (rotateQuaternion);
		}
		
	}
}