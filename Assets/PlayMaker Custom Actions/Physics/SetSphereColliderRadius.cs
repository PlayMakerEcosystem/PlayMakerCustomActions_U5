// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Set the size of a Sphere Collider")]
	[HelpUrl("")]
	public class SetSphereColliderRadius : ComponentAction<SphereCollider>
	{
		[RequiredField]
		[CheckForComponent(typeof(SphereCollider))]
        [Tooltip("The GameObject to apply the size to.")]
		public FsmOwnerDefault gameObject;
		
        [Tooltip("Sphere radius")]
		public FsmFloat radius;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			radius = null;
			everyFrame = false;
		}

       public override void OnPreprocess()
        {
            if (everyFrame)
            {
                Fsm.HandleFixedUpdate = true;
            }
        }

		public override void OnEnter()
		{
			DoChange();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}

		public override void OnFixedUpdate()
		{
			DoChange();
		}

		void DoChange()
		{
			if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
			{
				return;
			}
			
			this.cachedComponent.radius = radius.Value;
	
		}
	}
}
