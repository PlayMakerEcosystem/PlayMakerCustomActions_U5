// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.Rendering;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Sprites")]
	public class SpriteSetOrderInLayer : ComponentAction<SortingGroup>
	{

        [RequiredField]
        [CheckForComponent(typeof(SortingGroup))]
        [Tooltip("The GameObject with the SortingGroup attached.")]
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
