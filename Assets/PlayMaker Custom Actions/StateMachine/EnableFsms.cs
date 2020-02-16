// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Enables/Disables multiple FSM components on different GameObjects.")]
	public class EnableFsms : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM component.")]
		public FsmGameObject[] gameObjects;

		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on GameObject. Useful if you have more than one FSM on a GameObject.")]
		public FsmString fsmName;

		[Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enable;

		[Tooltip("Reset the initial enabled state when exiting the state.")]
		public FsmBool resetOnExit;

		private PlayMakerFSM fsmComponent;
		private bool doReset;

		public override void Reset()
		{
			gameObjects = new FsmGameObject[0];
			fsmName = null;
			enable = true;
			resetOnExit = false;
		}

		public override void OnEnter()
		{
			doReset = false;
			DoEnableFSM();

			Finish();
		}

		void DoEnableFSM()
		{
			foreach(var gameObject in gameObjects)
			{
				GameObject go = gameObject.Value;
				if(go == null) continue;

				if(!string.IsNullOrEmpty(fsmName.Value))
				{
					var fsmComponents = go.GetComponents<PlayMakerFSM>();
					foreach(var component in fsmComponents)
					{
						if(component.FsmName == fsmName.Value)
						{
							fsmComponent = component;
							break;
						}
					}
				} else
					fsmComponent = go.GetComponent<PlayMakerFSM>();

				if(fsmComponent == null)
				{
					LogError("Missing FsmComponent!");
					return;
				}

				if(doReset)
					fsmComponent.enabled = !enable.Value;
				else
					fsmComponent.enabled = enable.Value;
			}
		}

		public override void OnExit()
		{
			if(resetOnExit.Value)
			{
				doReset = true;
				DoEnableFSM();
			}
		}
	}
}