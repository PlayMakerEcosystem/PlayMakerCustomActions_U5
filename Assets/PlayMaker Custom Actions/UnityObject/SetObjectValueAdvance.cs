// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: setobject, unityobject

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UnityObject)]
	[Tooltip("Sets the value from a script/behaviour/component/etc as an Object Variable.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11807.0")]
	public class SetObjectValueAdvance : FsmStateAction
	{

		[ActionSection("Input")]
		public FsmOwnerDefault gameObject;
		[Tooltip("The name of the script/behaviour/component/etc to set as an object. Note: No space in script name.")]
		[TitleAttribute("Object Name")]
		public FsmString objectName;

		[ActionSection("Output")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmObject objectVariable;

		[ActionSection("Options")]
		public FsmBool debugType;
		public FsmBool everyFrame;

		UnityEngine.Object componentTarget;
		private GameObject go;

		public override void Reset()
		{
			objectVariable = null;
			objectName = null;
			everyFrame = false;
			debugType = false;
		}

		public override void OnEnter()
		{
			if (objectName.Value.Contains(" "))
			{
				Debug.LogWarning ("<color=#5F9EA0>Should not be any space. <i>Example: </i>'Sprite Renderer' must be 'SpriteRenderer' & Capital letters are mandatory.</color>",this.Owner);
			}

			doAction(Fsm.GetOwnerDefaultTarget(gameObject));

			if (!everyFrame.Value)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			doAction(Fsm.GetOwnerDefaultTarget(gameObject));

			if (!everyFrame.Value)
			{
				Finish();
			}
		}

		void doAction(GameObject gos)
		{


			if (!string.IsNullOrEmpty(objectName.Value))
			{

				componentTarget = gos.gameObject.GetComponent(objectName.Value) as UnityEngine.Object;

				if (debugType.Value == true)
				{
					doDebug();
				}

				objectVariable.Value = componentTarget;
			}

			else {
	
				Debug.LogWarning ("<color=#A62A2A>Object Name is empty. Please correct</color>",this.Owner);
			}


			return;
		}

		public void doDebug()
		{
		

			Debug.Log ("<color=#A62A2A>Type Input: </color>"+componentTarget.GetType().ToString()+"<color=#5F9EA0> Type Output: </color>"+objectVariable.TypeName, this.Owner);
		
			return;
		}

	}
}
