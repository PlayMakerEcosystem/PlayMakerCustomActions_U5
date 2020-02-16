// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10487.0
// Keywords: getobject, unityobject

using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UnityObject)]
	[Tooltip("Get Object ID. The instance id of an object is always guaranteed to be unique.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10487.0")]
	public class GetObjectId : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The object that needs to identified")]
		public FsmObject objectValue;

		[RequiredField]
		[UIHint(UIHint.FsmInt)]
		[Tooltip("The object unique ID")]
		[TitleAttribute("Object ID")]		
		public FsmInt objectVariable;

		public override void Reset()
		{
			objectVariable = null;
			objectValue = null;
		}

		public override void OnEnter()
		{
			objectVariable.Value = objectValue.Value.GetInstanceID();

		
				Finish();
		
		}

	}
}
