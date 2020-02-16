// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Sets the number of path path defining the polygon for a PolygonCollider2D")]
    public class SetPolygonCollider2DSetPathCount : ComponentAction<PolygonCollider2D>
    {
        [RequiredField]
        [CheckForComponent(typeof(PolygonCollider2D))]
        public FsmOwnerDefault gameObject;

        public FsmInt pathCount;


        public override void Reset()
        {
            gameObject = null;
            pathCount = null;
  
        }

        public override void OnEnter()
        {
            if (this.UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {

                this.cachedComponent.pathCount = pathCount.Value;


            }
            Finish();
        }
    }
}