// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)

using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;


public class PlayMakerWheelFrictionCurveClass : FsmStateAction {
	
	public FsmFloat extremumSlip;
	public FsmFloat extremumValue;
	public FsmFloat asymptoteSlip;
	public FsmFloat asymptoteValue;
	public FsmFloat stiffness;
	
	public override void Reset()
	{
		extremumSlip = new FsmFloat() {UseVariable=true};
		extremumValue = new FsmFloat() {UseVariable=true};
		asymptoteSlip = new FsmFloat() {UseVariable=true};
		asymptoteValue = new FsmFloat() {UseVariable=true};
		stiffness = new FsmFloat() {UseVariable=true};
	}
}
