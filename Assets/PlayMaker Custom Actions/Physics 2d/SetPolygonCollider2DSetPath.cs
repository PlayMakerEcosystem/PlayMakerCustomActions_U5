// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Sets the path defining the polygon for a PolygonCollider2D")]
    public class SetPolygonCollider2DSetPath : ComponentAction<PolygonCollider2D>
    {
        [RequiredField]
        [CheckForComponent(typeof(PolygonCollider2D))]
        public FsmOwnerDefault gameObject;

        public FsmInt pathIndex;

        public FsmVector2[] path;

        Vector2[] _array;

        public override void Reset()
        {
            gameObject = null;
            path = null;
            pathIndex = null; 
        }

        public override void OnEnter()
        {
            if (this.UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {
                _array = new Vector2[path.Length];

                int i = 0;
                foreach (FsmVector2 _v in path)
                {
                    _array[i] = path[i].Value;
                    i++;
                }

                this.cachedComponent.SetPath(pathIndex.Value, _array);
                
            }
            Finish();
        }
    }
}