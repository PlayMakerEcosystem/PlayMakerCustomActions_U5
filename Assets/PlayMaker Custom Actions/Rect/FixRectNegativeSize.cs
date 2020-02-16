// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: rect fix negative
// Source https://answers.unity.com/questions/288438/rectcontains-cant-accept-rects-with-a-negative-wid.html#

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Rect)]
	[Tooltip("Fixes negative sizes of a rect so that width and height are always positive, moving x and y values accordingly")]
	public class FixRectNegativeSize : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmRect rect;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmRect result;
		
		public bool everyFrame;

		private Rect _rect;
		
		public override void Reset()
		{
			rect = null;
			result = null;
			everyFrame = false;
		}
		

		public override void OnEnter()
		{
			ExecuteAction();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			ExecuteAction();
		}

		public void ExecuteAction()
		{
			FixNegativeSize(rect.Value, out _rect);

			rect.Value = _rect;
		}
		
		private void FixNegativeSize (Rect originalRect, out Rect fixedRect)
		{
			fixedRect = originalRect;
			
			if (fixedRect.width < 0) {
				fixedRect.x += fixedRect.width;
				fixedRect.width = Mathf.Abs(fixedRect.width);
			}
 
			if (fixedRect.height < 0) {
				fixedRect.y += fixedRect.height;
				fixedRect.height = Mathf.Abs(fixedRect.height);
			}
 
		}
	}
}
