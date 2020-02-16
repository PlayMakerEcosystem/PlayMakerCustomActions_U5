// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Add an GameObject to the end of a GameObject array if it's enabled. Optionally reverse the check.")]
	public class ArrayAddGameObjectIfEnabled : FsmStateAction
	{
		public enum ActiveType
		{
			ActiveInHirarchy,
			ActiveSelf
		}

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[RequiredField]
		[Tooltip("GameObject to add.")]
		public FsmGameObject gameObject;

		[Tooltip("Wheter to check if the GameObject is active in the Hirarchy " +
				 "or itself independent of any parents state.")]
		public ActiveType activeType;

		[Tooltip("Wheter to add the GameObject if it's enabled or disabled.")]
		public FsmBool ifEnabled;

		public override void Reset()
		{
			array = null;
			gameObject = null;
			activeType = ActiveType.ActiveInHirarchy;
			ifEnabled = true;
		}

		public override void OnEnter()
		{
			if(ifEnabled.Value)
			{
				switch(activeType)
				{
					case ActiveType.ActiveInHirarchy:
						if(gameObject.Value.activeInHierarchy) DoAddValue();
						break;
					case ActiveType.ActiveSelf:
						if(gameObject.Value.activeSelf) DoAddValue();
						break;
				}
			} else
			{
				switch(activeType)
				{
					case ActiveType.ActiveInHirarchy:
						if(!gameObject.Value.activeInHierarchy) DoAddValue();
						break;
					case ActiveType.ActiveSelf:
						if(!gameObject.Value.activeSelf) DoAddValue();
						break;
				}
			}
			
			Finish();
		}

		private void DoAddValue()
		{
			array.Resize(array.Length + 1);
			array.Set(array.Length - 1, gameObject.Value);
		}

	}

}

