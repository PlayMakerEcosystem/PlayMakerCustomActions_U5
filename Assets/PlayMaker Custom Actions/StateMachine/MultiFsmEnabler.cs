using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Accepts a list of fsm names, the gameobject contains the specified fsms and the corresponding bools -> fsms to be enabled or not.")]
	public class MultiFsmEnabler : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The GameObject which contains the defined fsms.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("List of FSMs to be enabled/disabled.")]
		public FsmString[] fsmNames;
		
		[Tooltip("Turn all of the defined fsms to enabled/disabled.")]
		public FsmBool toBeEnabled;
		
		private PlayMakerFSM fsmComponent;
		
		public override void Reset()
		{
			gameObject = null;
			fsmNames = null;
			toBeEnabled = null;
		}

		// Code that runs on entering the state.
		public override void OnEnter()
		{
			DoEnableFSM();

			Finish();
		}
		
		void DoEnableFSM()
		{
			GameObject go = gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;

			if (go == null) return;

			foreach (var currFsmName in fsmNames)
			{
				string fsmName = currFsmName.Value;
				if (!string.IsNullOrEmpty(fsmName))
				{
					var fsmComponents = go.GetComponents<PlayMakerFSM>();
					foreach (var component in fsmComponents)
					{
						if (component.FsmName.Equals(fsmName))
						{
							fsmComponent = component;
							break;
						}
					}
				}
				else
				{
					fsmComponent = go.GetComponent<PlayMakerFSM>();
				}
				
				if (fsmComponent == null)
				{
					return;
				}
				
				fsmComponent.enabled = toBeEnabled.Value;
			}
		}
	}
}
