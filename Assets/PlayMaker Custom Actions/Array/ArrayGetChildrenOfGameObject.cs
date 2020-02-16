// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Original Action by DudeBxl 

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Transform")]
	[Tooltip("Store all direct childs of GameObject (active and/or inactive) from a parent.")]
	public class ArrayGetChildrenOfGameObject : FsmStateAction
	{

		[Tooltip("The parent gameObject")]
		[RequiredField]
		public FsmOwnerDefault parent;
		
		[ActionSection("Option")]
		public FsmBool includeInactive;

		public FsmBool includeParent;

		[RequiredField]
		[VariableType(VariableType.GameObject)]
		[UIHint(UIHint.Variable)]
		public FsmArray storeChildren;
		
		
		private GameObject go;
		private Transform[] childs;
		private List<GameObject> list;
		
		public override void Reset()
		{
			storeChildren = new FsmArray();
			parent = null;
			includeInactive = true;
			includeParent = false;
		}
		
		
		public override void OnEnter()
		{
			GetAllChilds(Fsm.GetOwnerDefaultTarget(parent));
			
			Finish();
		}
		
		
		public void GetAllChilds(GameObject parent)
		{
			
			childs = parent.GetComponentsInChildren<Transform>(includeInactive.Value);

			
			list = new List<GameObject>();
			foreach(Transform trans in childs) {
				if ( !includeParent.Value && trans.gameObject == parent)
				{
					continue;
				}

                if (trans.parent == parent.transform)
                {
	                list.Add(trans.gameObject);
                }
			}

			storeChildren.objectReferences = list.ToArray();
			storeChildren.SaveChanges();
		}
	}
}
