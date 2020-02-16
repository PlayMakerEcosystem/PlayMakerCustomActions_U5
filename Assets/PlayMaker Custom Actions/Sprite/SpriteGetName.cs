// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
  [ActionCategory("Sprite")]
  [Tooltip("Get the name of a Unity Sprite.")]
  public class SpriteGetName : FsmStateAction
  {
    [RequiredField]
    [ObjectType(typeof(Sprite))]
    [Tooltip("The sprite to get the name of.")]
    public FsmObject sprite;

    [UIHint(UIHint.Variable)]
    [Tooltip("Store the name of the sprite in a variable.")]
    public FsmString result;

    [Tooltip("Wheter to repeat this action on every frame or only once.")]
    public FsmBool everyFrame;

    public override void Reset()
    {
      sprite = new FsmObject { UseVariable = true };
      result = null;
      everyFrame = false;
    }

    public override void OnEnter()
    {
      DoGetSpriteName();

      if(!everyFrame.Value) Finish();
    }

    public override void OnUpdate()
    {
      DoGetSpriteName();
    }

    private void DoGetSpriteName()
    {
      if(sprite == null)
      {
        Debug.LogError("Sprite is null!");
        return;
      }

      result.Value = sprite.Value.name;
    }
  }
}