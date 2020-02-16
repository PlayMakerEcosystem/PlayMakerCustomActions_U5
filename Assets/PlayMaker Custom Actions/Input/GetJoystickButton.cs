// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Joystick

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Set bool of joystick button down/up")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11951.0")]
	public class GetJoystickButton : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The name of the button axis. Set in the Unity Input Manager.")]
        public FsmString buttonName;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a bool variable.")]
        public FsmBool store;

        [Tooltip("Repeat every frame. Typically this would be set to True.")]
		public bool everyFrame;

		private float temp;

		public override void Reset()
		{
			buttonName = "";
			store = null;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			DoGetAxis();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetAxis();
		}

		void DoGetAxis()
		{
			var axisValue = Input.GetAxis(buttonName.Value);

			temp = axisValue;

			if (temp == 1 || temp == -1){
				store.Value = true;

			}

			else {
				store.Value = false;
			}
		}
	}
}

