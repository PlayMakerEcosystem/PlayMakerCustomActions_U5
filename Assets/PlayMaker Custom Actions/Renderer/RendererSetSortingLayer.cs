// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Sprites")]
	public class RendererSetSortingLayer : ComponentAction<Renderer>
	{

        [RequiredField]
        [CheckForComponent(typeof(Renderer))]
        [Tooltip("The GameObject with the SortingGroup attached.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The Layer Name to set as Sorting Layer.")]
        [UIHint(UIHint.SortingLayer)]
        public FsmString sortingLayerName;

		[Tooltip("The Layer Id to set as Sorting Layer. If set, SortingLayerName is ignored")]
		public FsmInt orSortingLayerID;
	

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		

        public override void Reset()
        {
            gameObject = null;
	        orSortingLayerID = new FsmInt(){UseVariable = true};
		    sortingLayerName = new FsmString(){UseVariable = true};
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
		        if (!sortingLayerName.IsNone)
		        {
			        this.cachedComponent.sortingLayerName = sortingLayerName.Value;
		        }
		        
		        if (!orSortingLayerID.IsNone)
		        {
			        this.cachedComponent.sortingLayerID = orSortingLayerID.Value;
		        }
	        }
        } // method


		public override string ErrorCheck()
		{
			if (!sortingLayerName.IsNone && !orSortingLayerID.IsNone)
			{
				return "sortingLayerName will be ignored, because orSortingLayerID is set";
			}
			
			return base.ErrorCheck();
		}
	} // class
 } // namespace
