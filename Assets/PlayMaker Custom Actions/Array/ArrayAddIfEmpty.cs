// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Add an item to the end of an Array if it's null or empty. Optionally reverse the check.")]
	public class ArrayAddIfEmpty : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[RequiredField]
		[MatchElementType("array")]
		[Tooltip("Item to add.")]
		public FsmVar value;

		[Tooltip("Adds if empty, otherwise if not empty.")]
		public FsmBool ifEmpty;

		public override void Reset()
		{
			array = null;
			value = null;
			ifEmpty = true;
		}

		public override void OnEnter()
		{
			if(ifEmpty.Value)
			{
				if(value.IsNone && value.GetValue() == null) DoAddValue();
			} else
			{
				if(!value.IsNone && value.GetValue() != null) DoAddValue();
			}

			Finish();
		}

		private void DoAddValue()
		{
			//incorporate the check for empty or not empty if the value is of type string
			if(value.Type == VariableType.String)
			{
				if(ifEmpty.Value)
				{
					if(!string.IsNullOrEmpty(value.stringValue)) return;
				} else
				{
					if(string.IsNullOrEmpty(value.stringValue)) return;
				}
			}

			array.Resize(array.Length + 1);
			value.UpdateValue();
			array.Set(array.Length - 1, value.GetValue());
		}
	}
}

