// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Gets the name of an Object and stores it in a String Variable.")]
	public class GetObjectName : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Object to get the name of.")]
		public FsmObject specifyObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Name of the specified Object.")]
		public FsmString storeName;

		public bool everyFrame;

		public override void Reset()
		{
			specifyObject = new FsmObject { UseVariable = true };
			storeName = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetObjectName();

			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetObjectName();
		}

		void DoGetObjectName()
		{
			if(!specifyObject.Value)
			{
				LogError("Object is null!");
			}

			var obj = specifyObject.Value;

			storeName.Value = obj.name;
		}
	}
}
