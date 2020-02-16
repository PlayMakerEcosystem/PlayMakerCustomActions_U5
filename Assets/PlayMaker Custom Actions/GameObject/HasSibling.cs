// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Finds the sibling of a GameObject by Name and/or Tag. Sends events based on result (found or not found). NOTE: This action will search recursively through all siblingren of the GameObjects parent and return the first match.")]
	public class HasSibling : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to search from.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The name of the sibling to search for.")]
		public FsmString siblingName;

		[UIHint(UIHint.Tag)]
		[Tooltip("The Tag to search for. If sibling-name is set, both name and Tag need to match.")]
		public FsmString withTag;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a GameObject variable.")]
		public FsmGameObject storeResult;

		[UIHint(UIHint.Variable)]
		[Tooltip("True if sibling was found")]
		public FsmBool found;

		public FsmEvent foundEvent;
		public FsmEvent notFoundEvent;

		private GameObject go;

		public override void Reset()
		{
			gameObject = null;
			siblingName = "";
			withTag = "Untagged";
			storeResult = null;
			found = null;
			foundEvent = null;
			notFoundEvent = null;
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);

			if(!go)
				LogError("GameObject is null!");

			storeResult.Value = DoGetSiblingByName(go, siblingName.Value, withTag.Value);

			found.Value = storeResult.Value != null;
			Fsm.Event(found.Value ? foundEvent : notFoundEvent);

			Finish();
		}

		static GameObject DoGetSiblingByName(GameObject root, string name, string tag)
		{
			if(root == null)
				return null;

			if(root.transform == null)
				return null;

			if(root.transform.parent == null)
				return null;

			foreach(Transform sibling in root.transform.parent)
			{
				//skip if own go
				if(sibling == root.transform)
					continue;

				if(!string.IsNullOrEmpty(name))
				{
					if(sibling.name == name)
					{
						if(!string.IsNullOrEmpty(tag))
						{
							if(sibling.tag.Equals(tag))
								return sibling.gameObject;
						} else
							return sibling.gameObject;
					}
				} else if(!string.IsNullOrEmpty((tag)))
				{
					if(sibling.tag == tag)
						return sibling.gameObject;
				}

				// search recursively
				var returnObject = DoGetSiblingByName(sibling.gameObject, name, tag);

				if(returnObject != null)
					return returnObject;
			}

			return null;
		}

		public override string ErrorCheck()
		{
			if(string.IsNullOrEmpty(siblingName.Value) && string.IsNullOrEmpty(withTag.Value))
				return "Specify sibling-name, tag, or both.";

			return null;
		}

	}
}
