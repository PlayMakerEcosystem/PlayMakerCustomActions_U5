// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=13542.0")]
	[Tooltip("Set the value of loop in another FSM.")]
	public class SetFsmLoop : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;
	
		[RequiredField]
        [Tooltip("Set the value of the loop.")]
		public FsmInt loopOverride;

        [Tooltip("Repeat every frame. Useful if the value is changing.")]
        public FsmBool everyFrame;

		GameObject goLastFrame;
		string fsmNameLastFrame;

		PlayMakerFSM fsm;

		public override void Reset()
		{
			gameObject = null;
			fsmName = "";
			loopOverride = 0;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetFsmBool();

			if (!everyFrame.Value)
			{
				Finish();
			}
		}

		void DoSetFsmBool()
		{
			if (loopOverride.Value == 0)
			{
				return;
			}

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
				
			if (go != goLastFrame || fsmName.Value != fsmNameLastFrame)
			{
				goLastFrame = go;
				fsmNameLastFrame = fsmName.Value;
				
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			}	

			if (fsm == null)
			{
				
				Debug.LogWarning("<color=#FF9900ff><b>Could not find FSM: </b></color>" + fsmName.Value,this.Owner);
				Finish();
			}

			fsm.Fsm.MaxLoopCountOverride = loopOverride.Value;

			if (!everyFrame.Value)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetFsmBool();
		}

	}
}
