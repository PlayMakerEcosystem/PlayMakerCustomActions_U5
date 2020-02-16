// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the FSM component on the specified GameObject. Optionally send an event if the " +
			 "GameObject doesn't have any FSM component attached.")]
	public class GetFsmComponent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to get the FSM from.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Optionally search by FSM name.")]
		public FsmString fsmName;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[CheckForComponent(typeof(PlayMakerFSM))]
		[Tooltip("The FSM component.")]
		public FsmObject result;

		[Tooltip("If the GameObject doesn't have any FSM component with the given name or any at all.")]
		public FsmEvent noneFoundEvent;

		[Tooltip("Wheter to run every frame or only once.")]
		public FsmBool everyFrame;

		private GameObject go;

		public override void Reset()
		{
			gameObject = null;
			fsmName = null;
			result = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetFsmComponent();

			if(!everyFrame.Value)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetFsmComponent();
		}

		private void DoGetFsmComponent()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			if(!go)
				LogError("GameObject is null!");

			PlayMakerFSM[] allFSMs = go.GetComponents<PlayMakerFSM>();

			if(allFSMs.Length == 0)
				Fsm.Event(noneFoundEvent);

			if(string.IsNullOrEmpty(fsmName.Value))
			{
				result.Value = allFSMs[0];
			} else
			{
				for(int i = 0; i < allFSMs.Length; i++)
				{
					if(allFSMs[i].Fsm.Name == fsmName.Value)
						result.Value = allFSMs[i];
				}
				Fsm.Event(noneFoundEvent);
			}
		}
	}
}
