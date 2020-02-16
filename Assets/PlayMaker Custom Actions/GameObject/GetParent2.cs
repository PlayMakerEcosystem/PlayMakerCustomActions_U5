// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the Parent of a Game Object with option to get it everyframe for complex one state flow")]
	public class GetParent2 : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeResult;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			storeResult = null;
		}

		public override void OnEnter()
		{
			Execute();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			Execute();
		}

		public void Execute()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
				storeResult.Value = go.transform.parent == null ? null : go.transform.parent.gameObject;
			else
				storeResult.Value = null;
		}
	}
}