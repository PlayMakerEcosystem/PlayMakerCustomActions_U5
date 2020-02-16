// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Mesh")]
	[Tooltip("Sets the Text Mech attached to a Game Object.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11285.0")]
	public class SetTextMech : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(TextMesh))]
		public FsmOwnerDefault gameObject;
		public FsmString text;
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			text = "";
		}

		public override void OnEnter()
		{
			DoSetText();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate()
		{
			DoSetText();
		}
		
		void DoSetText()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    
		    
			go.GetComponent<TextMesh>().text = text.Value;
		    
		}
	}
}
