// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Finds all child GameObject's with the matching name and/or tag and stores them in an FsmArray.")]
	public class FindChildren : FsmStateAction
	{
    [Tooltip("The name of the GameObject to find. You can leave this empty if you specify a Tag.")]
		public FsmString name;

		[UIHint(UIHint.Tag)]
    [Tooltip("Find a GameObject with this tag. If a name was specified then both the name and tag must match.")]
		public FsmString tag;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject)]
        [Tooltip("Store the result in a FsmArray variable of type GameObject.")]
		public FsmArray result;

		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if any children could be found.")]
		public FsmEvent successEvent;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger when no child matches the given criteria.")]
		public FsmEvent failedEvent;

		private List<GameObject> children = new List<GameObject>();

		public override void Reset()
		{
			name = "";
			tag = "Untagged";
			result = null;
			successEvent = null;
			failedEvent = null;
		}

		public override void OnEnter()
		{
			FindChildrenRecursive(Owner);

			if (children.Count != 0)
			{
				result.Resize(children.Count);

				int i = 0;
				foreach (var child in children)
				{
					result.Set(i, child);
					i++;
				}
				
				result.SaveChanges();
				
				Fsm.Event(successEvent);
			}
			else Fsm.Event(failedEvent);
			
			Finish();
		}
		
		public override string ErrorCheck()
		{
			if (string.IsNullOrEmpty(name.Value) && string.IsNullOrEmpty(tag.Value))
			{
			  return "Specify name, tag, or both.";
			}

			return null;
		}
		
		/// <summary>
		/// Returns a list of all children and children's children of a GameObject.
		/// </summary>
		/// <returns>List of all (sub-)children.</returns>
		private void FindChildrenRecursive(GameObject go)
		{
			var tmpChildren = go.GetComponentsInChildren<Transform>(true);
			
			foreach(var child in tmpChildren)
			{
				//skip children that don't have the specified name
				if(!string.IsNullOrEmpty(name.Value) && child.name != name.Value) continue;
				//skip children that don't have the specified tag
				if(!string.IsNullOrEmpty(tag.Value)
				   && tag.Value != "Untagged" && child.CompareTag(tag.Value)) continue;
				
				children.Add(child.gameObject);
			}
		}
	}
}