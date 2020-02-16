// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Finds a Game Object by type")]
	public class FindGameObjectByType : FsmStateAction
	{

        [Tooltip("The Type. Should be a component type.")]
        public string type;

		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the result in a GameObject variable.")]
		public FsmGameObject store;

        [Tooltip("Event sent if this type was found in the hierarchy")]
        public FsmEvent found;

        [Tooltip("Event sent if this type was not found in the hierarchy")]
        public FsmEvent notFound;

		public override void Reset()
		{
            type = "UnityEngine.Camera, UnityEngine";
            store = null;
            found = null;
            notFound = null;
		}

		public override void OnEnter()
		{
			Find();
			Finish();
		}

		void Find()
		{
            Type _type = Type.GetType(type);

            var _result =  UnityEngine.Object.FindObjectOfType(_type);
            Component _c = _result as Component;

            if (_c != null)
            {
                store.Value = _c.gameObject;
                Fsm.Event(found);
            }
            else{
                Fsm.Event(notFound);
            }

		}


	}
}