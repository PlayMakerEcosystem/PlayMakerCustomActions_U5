// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
  [ActionCategory(ActionCategory.Physics2D)]
  [Tooltip("Gets The degree to which this object is affected by gravity.  NOTE: Game object must have a rigidbody 2D.")]
  public class GetGravity2dScale : ComponentAction<Rigidbody2D>
  {
    [RequiredField]
    [CheckForComponent(typeof(Rigidbody2D))]
    [Tooltip("The GameObject with a Rigidbody 2d attached")]
    public FsmOwnerDefault gameObject;

    [UIHint(UIHint.Variable)]
    [RequiredField]
    [Tooltip("The gravity scale effect")]
    public FsmFloat result;

    public FsmBool everyFrame;
    
    public override void Reset()
    {
      gameObject = null;
      result = null;
      everyFrame = false;
    }
		
    public override void OnEnter()
    {
      DoGetGravityScale();
      
      if(!everyFrame.Value) Finish();
    }

    public override void OnUpdate()
    {
      DoGetGravityScale();
    }
		
    void DoGetGravityScale()
    {
      var go = Fsm.GetOwnerDefaultTarget(gameObject);
      if (!UpdateCache(go)) return;
			
      result.Value = rigidbody2d.gravityScale;
    }
  }
}