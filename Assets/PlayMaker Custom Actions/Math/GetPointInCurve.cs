// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: curve

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Get a point within the curve(from the horizontal axis in the curve graph). Or get a random point in curve")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11866.0")]

	public class GetPointInCurve : FsmStateAction
	{
		[ActionSection("Curve setup")]
		[RequiredField]
		[Tooltip("The curve to use.")]
		[TitleAttribute("Curve")]
		public FsmAnimationCurve animCurve;

		[ActionSection("Options")]
		[Tooltip("Get a point in curve")]
		[TitleAttribute("Get curve point")]
		public FsmFloat curvePoint;
		[Tooltip("Get a point using time - time since start of scene")]
		public FsmBool useTime;
		[TitleAttribute("Convert Time to minutes")]
		public FsmBool useTimeMinutes;

		[ActionSection("Random Options")]
		[Tooltip("Get random if curve is 0 -> 1")]
		public FsmBool getRandomValue;
		[Tooltip("Get random Auto")]
		public FsmBool getRandomNormal;

		[ActionSection("Random Options advance")]
		[Tooltip("Get random with a set Min/Max")]
		public FsmBool getRandomAdvance;
		public FsmFloat setMin;
		public FsmFloat setMax;

		[ActionSection("Output")]
		public FsmFloat storeResult;

		public FsmBool everyFrame;

		private bool[] boolarray = new bool[5];
		private bool returnedBool;
		private int boolCount;
		private int length;
		private float min;
		private float max;
		private float time;

		public override void Reset()
		{
			animCurve = null;
			curvePoint = new FsmFloat{UseVariable=true};
			useTime = false;
			everyFrame = false;
			getRandomValue = false;
			getRandomNormal = false;
			getRandomAdvance = false;
			storeResult = null;
			setMax = new FsmFloat{UseVariable=true};
			setMin = new FsmFloat{UseVariable=true};
			useTimeMinutes = null;
		}

		public override void OnEnter()
		{
			getPoint();

			if (everyFrame.Value == false){
				
				Finish();
				
			}
		}

		public override void OnUpdate()
		{
		
			getPoint();

			if (everyFrame.Value == false){
				
				Finish();
				
			}
		}

		void getPoint(){


			boolarray[0] = useTime.Value;
			boolarray[1] = getRandomValue.Value;
			boolarray[2] = getRandomNormal.Value;
			boolarray[3] = getRandomAdvance.Value;
			returnedBool = false;

			if (CheckBoolArrays(0,3))
			{
				returnedBool = true;
				Debug.Log ("More than one option selected - please correct", this.Owner);
			}
				
			if (useTimeMinutes.Value == true & useTime.Value == true){

				time = Time.time / 60f;
			}

			else if (useTime.Value == true) {

				time = Time.time;
			}

			length = animCurve.curve.length;
			min = animCurve.curve.keys[0].time;
			max = animCurve.curve.keys[length-1].time;

			if (returnedBool == false && curvePoint.Value != 0f || !curvePoint.IsNone)	{
				if (curvePoint.Value < min  || curvePoint.Value > max){
					Debug.LogWarning ("Your 'Get curve point' is above or below the curve min/max horizontal axis");	
				}

				storeResult.Value = animCurve.curve.Evaluate(curvePoint.Value);

			}


			if (useTime.Value == true)	{
				
				storeResult.Value = animCurve.curve.Evaluate(time);
					
			}

			if (getRandomValue.Value == true)	{
				
				storeResult.Value = animCurve.curve.Evaluate(Random.value);
				
			}



			if (getRandomNormal.Value == true)	{

	
				storeResult.Value = animCurve.curve.Evaluate(Random.Range(min, max));
				
			}

				                                            
			if (getRandomAdvance.Value == true) {

				if (setMin.Value < min  || setMax.Value > max){
					Debug.LogWarning ("Your min/max is above or below the curve min/max horizontal axis");	
				}

				storeResult.Value = animCurve.curve.Evaluate(Random.Range(setMin.Value, setMax.Value));
				
			}

		}
			 
		bool CheckBoolArrays(int lowVal, int highVal)
			{
			boolCount = 0;
			for(int i = lowVal; i <= highVal; i++)
				{

				if(boolarray[i] == true){
					boolCount++;	}
				}

			if (boolCount <= 1 ){
				return false;
			}
					
				return true;


			}

	}
}
