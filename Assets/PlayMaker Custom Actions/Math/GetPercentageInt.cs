// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
  [ActionCategory(ActionCategory.Math)]
  [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
  [Tooltip("Returns the percentage of a base Integer and the current value by dividing the value by the base and multiplying it by 100.")]
  public class GetPercentageInt : FsmStateAction
  {
    [RequiredField]
    public FsmInt _value;
		
    [RequiredField]
    public FsmInt _base;
		
    [RequiredField]
    [UIHint(UIHint.Variable)]
    public FsmInt storeResult;
		
    public bool everyFrame;

    public override void Reset()
    {
      _value = null;
      _base = null;
      storeResult = null;
      everyFrame = false;
    }
		
    public override void OnEnter()
    {
      DoGetPercentage();
			
      if (!everyFrame)
        Finish();
    }
		
    public override void OnUpdate()
    {
      DoGetPercentage();
    }
		
    void DoGetPercentage()
    {
      storeResult.Value = Mathf.RoundToInt(_value.Value / _base.Value * 100);
    }
  }
}