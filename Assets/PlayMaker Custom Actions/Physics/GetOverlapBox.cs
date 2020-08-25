// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Romi Fauzi
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Physics Overlap Box
/* EcoMetaStart
{
"script dependancies":[
                        "Assets/PlayMaker Custom Actions/Physics/internal/GetOverlapBase.cs"
                    ]
}
EcoMetaEnd */

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Gets GameObject(s) using Physics.OverlapBox")]
    public class GetOverlapBox : GetOverlapBase
    {
        public FsmVector3 boxSize;

        protected override void GetOverlappedGameObject()
        {
            int targetMask = LayerMask.GetMask(LayerMask.LayerToName(layerMask.Value));

            if (boxSize.Value.sqrMagnitude <= 0)
            {
                return;
            }

            var go = Fsm.GetOwnerDefaultTarget(owner);

            int ownerMask = LayerMask.GetMask(LayerMask.LayerToName(go.layer));

            var originPos = fromPosition.IsNone ? go.transform.position : fromPosition.Value;

            Collider[] cols = Physics.OverlapBox(originPos, boxSize.Value / 2f, Quaternion.identity, targetMask);

            Collider ownerCol = go.GetComponent<Collider>();

            if (cols.Length > 0)
            {
                bool afterOwner = false;
                array.Resize(cols.Length);

                for (int i = 0; i < cols.Length; i++)
                {
                    if (!includeOwner.Value && ownerCol != null)
                    {
                        if (cols[i].gameObject == go)
                        {
                            afterOwner = true;
                            continue;
                        }
                    }

                    array.Set(afterOwner ? i - 1 : i, cols[i].gameObject);
                }

                if (!includeOwner.Value && ownerCol != null && targetMask == ownerMask)
                    array.Resize(cols.Length - 1);

                Fsm.Event(onFoundGameObject);
            }
        }

        public override void OnDrawActionGizmos()
        {
            if (!drawGizmo)
                return;

            base.OnDrawActionGizmos();
            var go = Fsm.GetOwnerDefaultTarget(owner);

            if (go == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(fromPosition.IsNone ? go.transform.position : fromPosition.Value, boxSize.Value);
        }
    }
}


