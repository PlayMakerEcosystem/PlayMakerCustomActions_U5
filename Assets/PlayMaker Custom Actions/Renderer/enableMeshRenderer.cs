// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Renderer)]
    public class enableMeshRenderer : FsmStateAction
    {
        public FsmOwnerDefault owner;
        public FsmBool enable;
        MeshRenderer theMesh;

        public override void Reset()
        {
            enable = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(owner);
            theMesh = go.GetComponent<MeshRenderer>();
            theMesh.enabled = enable.Value;
            Finish();
        }
    }




}