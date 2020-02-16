// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
//Author: thore
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.Rendering;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Sprites")]
	[HelpUrl("https://hutonggames.com/playmakerforum/index.php?topic=20545.msg90056#msg90056")]
	public class SetSortingGroupProperties : ComponentAction<SortingGroup>
	{

        [RequiredField]
        [CheckForComponent(typeof(SortingGroup))]
        [Tooltip("The GameObject with the SortingGroup attached.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The Layer Name to set as Sorting Layer.")]
        [UIHint(UIHint.SortingLayer)]
        public FsmString sortingLayerName;

		[Tooltip("The Layer Id to set as Sorting Layer. If set, SortingLayerName is ignored")]
		public FsmInt orSortingLayerID;
		
        [Tooltip("Provide Int to set Order in Layer.")]
        public FsmInt OrderInLayer;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		

        public override void Reset()
        {
            gameObject = null;
	        orSortingLayerID = new FsmInt(){UseVariable = true};
		    sortingLayerName = new FsmString(){UseVariable = true};
            OrderInLayer = null;
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
		        if (!OrderInLayer.IsNone)
		        {
			        this.cachedComponent.sortingOrder = OrderInLayer.Value;
		        }

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
