// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
#if CROSS_PLATFORM_INPUT 
using UnityStandardAssets.CrossPlatformInput;
#endif

#pragma warning disable 0162

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("CrossPlatformInput")]
	[Tooltip("Sets the value of the virtual MousePosition. Requires Standard Assets CrossPlatformInput.")]
	public class CrossPlatformSetVirtualMousePosition : FsmStateAction
	{

		[Tooltip("Set the mouse position in a vector3")]
		public FsmVector3 mousePosition;


		[Tooltip("Set the mouse position in a vector2")]
		public FsmVector2 mousePosition2d;


		[Tooltip("Set the mouse X position")]
		public FsmFloat mousePositionX;


		[Tooltip("Set the mouse Y position")]
		public FsmFloat mousePositionY;


		[Tooltip("Set the mouse Z position")]
		public FsmFloat mousePositionZ;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			mousePosition = new FsmVector3 (){ UseVariable = true };
			mousePosition2d = new FsmVector2 (){ UseVariable = true };
			mousePositionX = new FsmFloat (){ UseVariable = true };
			mousePositionY = new FsmFloat (){ UseVariable = true };
			mousePositionZ = new FsmFloat (){ UseVariable = true };

			everyFrame = true;
		}

		
		public override void OnEnter()
		{
			DoSetMousePosition();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetMousePosition();
		}
		
		void DoSetMousePosition()
		{
			#if CROSS_PLATFORM_INPUT
			Vector3 _pos = CrossPlatformInputManager.mousePosition;

			if (!mousePosition.IsNone)
			{
				_pos = mousePosition.Value;
			}
			if (!mousePosition2d.IsNone)
			{
				_pos.x = mousePosition2d.Value.x;
				_pos.y = mousePosition2d.Value.y;
			}
			if (!mousePositionX.IsNone)
			{
				_pos.x = mousePositionX.Value;
			}
			if (!mousePositionY.IsNone)
			{
				_pos.y = mousePositionY.Value;
			}
			if (!mousePositionZ.IsNone)
			{
				_pos.z = mousePositionZ.Value;
			}

			CrossPlatformInputManager.SetVirtualMousePositionX(_pos.x);
			CrossPlatformInputManager.SetVirtualMousePositionY(_pos.y);
			CrossPlatformInputManager.SetVirtualMousePositionZ(_pos.z);

			#endif

		}
	}
}

