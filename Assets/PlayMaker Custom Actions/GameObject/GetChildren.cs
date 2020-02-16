// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Finds multiple children of a GameObject by their names.")]
	public class GetChildren : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to search.")]
		public FsmOwnerDefault gameObject;

		[CompoundArray("Children", "Name", "Store Result")]

		[RequiredField]
		[Tooltip("The name of the child. Note, you can specify a path to the child " +
			"(e.g. \"LeftShoulder/Arm/Hand/Finger\", where \"LeftShoulder\" would be the child to start from.")]
		public FsmString[] childNames;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the current child in a GameObject variable.")]
		public FsmGameObject[] storeResult;

		public override void Reset()
		{
			gameObject = null;
			childNames = new FsmString[2];
			storeResult = new FsmGameObject[2];
		}

		public override void OnEnter()
		{
			DoFindChild();
			Finish();
		}

		void DoFindChild()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			for(int i = 0; i < childNames.Length; i++)
			{
				var transform = go.transform.Find(childNames[i].Value);
				storeResult[i].Value = transform != null ? transform.gameObject : null;
			}
		}
	}
}