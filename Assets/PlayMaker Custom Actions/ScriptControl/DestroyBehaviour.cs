// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Destroy a Behaviour on a GameObject.")]
	public class DestroyBehaviour : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The GameObject that owns the Behaviour.")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Behaviour)]
        [Tooltip("The name of the Behaviour to destroy.")]
		public FsmString behaviour;
		
		[Tooltip("Optionally drag a component directly into this field (behavior name will be ignored).")]
		public Component component;
	
		public override void Reset()
		{
			gameObject = null;
			behaviour = null;
			component = null;

		}

		Behaviour componentTarget;

		public override void OnEnter()
		{
			DoEnableBehaviour(Fsm.GetOwnerDefaultTarget(gameObject));
			
			Finish();
		}

		void DoEnableBehaviour(GameObject go)
		{
			if (go == null)
			{
				return;
			}

			if (component != null)
			{
				componentTarget = component as Behaviour;
			}
			else
			{
				componentTarget = go.GetComponent(ReflectionUtils.GetGlobalType(behaviour.Value)) as Behaviour;
			}

			if (componentTarget == null)
			{
				LogWarning(" " + go.name + " missing behaviour: " + behaviour.Value);
				return;
			}

           GameObject.Destroy(componentTarget);

		}


	    public override string ErrorCheck()
	    {
	        var go = Fsm.GetOwnerDefaultTarget(gameObject);

	        if (go == null || component != null || behaviour.IsNone || string.IsNullOrEmpty(behaviour.Value))
	        {
	            return null;
	        }

	        var comp = go.GetComponent(ReflectionUtils.GetGlobalType(behaviour.Value)) as Behaviour;
	        return comp != null ? null : "Behaviour missing";
	    }
	}
}