// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends an event if a touch input does not hit a gui element.")]
	public class TouchEventNotOnUGui : FsmStateAction
	{
		[Tooltip("Event to send when the touch is over an uGui object.")]
		public FsmEvent pointerOverUI;
		
		[Tooltip("Event to send when the touch is NOT over an uGui object.")]
		public FsmEvent pointerNotOverUI;

		[UIHint(UIHint.Variable)]
		public FsmBool isPointerOverUI;


		public override void Reset()
		{
			pointerOverUI = null;
			pointerNotOverUI = null;
			isPointerOverUI = null;
		}
		
		public override void OnLateUpdate()
		{
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				
				if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch (0).fingerId)) {
					isPointerOverUI.Value = false;
					Fsm.Event(pointerNotOverUI);
					
				} 
				
				else {
					isPointerOverUI.Value = true;
					Fsm.Event(pointerOverUI);
							
				}
			}
			
		}
	}
}
