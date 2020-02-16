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
	[Tooltip("Gets mousePosition from CrossPlatformInput Manager. Requires Standard Assets CrossPlatformInput.")]
	public class CrossPlatformGetMousePosition : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the mouse position in a vector3")]
		public FsmVector3 mousePosition;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the mouse position in a vector2")]
		public FsmVector2 mousePosition2d;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the mouse X position")]
		public FsmFloat mousePositionX;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the mouse Y position")]
		public FsmFloat mousePositionY;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the mouse Z position")]
		public FsmFloat mousePositionZ;

		public bool normalize;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			mousePosition = null;
			mousePosition2d = null;
			mousePositionX = null;
			mousePositionY = null;
			mousePositionZ = null;

			normalize = false;

			everyFrame = true;
		}

		public override string ErrorCheck()
		{
			#if !CROSS_PLATFORM_INPUT
			return "Missing Cross Platform Input Asset:\nImport from Assets/Import Package/CrossPlatformInput";
			#endif
			
			return "";
		}
		
		public override void OnEnter()
		{
			DoGetMousePosition();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetMousePosition();
		}
		
		void DoGetMousePosition()
		{
			Vector3 _position = Vector3.zero;

			#if CROSS_PLATFORM_INPUT
			_position = CrossPlatformInputManager.mousePosition;
			#endif

			if (normalize) {
				_position.x /= Screen.width;
				_position.y /= Screen.height;
			}

			if(!mousePosition.IsNone) mousePosition.Value = _position;
			if(!mousePosition2d.IsNone) mousePosition2d.Value = new Vector2 (_position.x, _position.y);
			if(!mousePositionX.IsNone) mousePositionX.Value = _position.x;
			if(!mousePositionY.IsNone) mousePositionY.Value = _position.y;
			if(!mousePositionZ.IsNone) mousePositionZ.Value = _position.z;
		}
	}
}

