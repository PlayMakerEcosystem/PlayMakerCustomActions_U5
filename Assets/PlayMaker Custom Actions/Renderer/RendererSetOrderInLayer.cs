// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Renderer")]
	public class RendererSetOrderInLayer : ComponentAction<Renderer>
	{

        [RequiredField]
        [CheckForComponent(typeof(Renderer))]
        [Tooltip("The GameObject with the Renderer attached.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Provide Int to set Order in Layer.")]
        public FsmInt orderInLayer;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		

        public override void Reset()
        {
            gameObject = null;
	        orderInLayer = null;
	        everyFrame = false;
        }

        public override void OnEnter()
		{
             DoSetSortingGroup();
           
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetSortingGroup();
		}

		public void DoSetSortingGroup()
        {
	        if (UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
	        {
		        if (!orderInLayer.IsNone)
		        {
			        this.cachedComponent.sortingOrder = orderInLayer.Value;
		        }
	        }
        } // method

	} // class
 } // namespace
