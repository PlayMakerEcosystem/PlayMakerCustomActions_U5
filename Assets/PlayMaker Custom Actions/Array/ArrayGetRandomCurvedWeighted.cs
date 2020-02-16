// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Get a Random item from an Array weighted along a curve")]
	public class ArrayGetRandomCurvedWeighted : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array to use.")]
		public FsmArray array;

		public FsmAnimationCurve weightCurve;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the value in a variable.")]
		[MatchElementType("array")]
		public FsmVar storeValue;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the index of the pick in a variable.")]
		[MatchElementType("array")]
		public FsmInt storeIndex;

		[Tooltip("Can return the same item twice in a row.")]
		public FsmBool repeat;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		int prevItem;

		float[] _weights;
		
		public override void Reset()
		{
			array = null;
			weightCurve = new FsmAnimationCurve();
			
			storeValue = null;
			storeIndex = null;
			repeat = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			_weights = GetWeightsFromCurve(weightCurve.curve,array.Length);
			
			DoGetRandomValue();

			if(!everyFrame) Finish();

		}

		public override void OnUpdate()
		{
			DoGetRandomValue();

		}

		private void DoGetRandomValue()
		{
			if(storeValue.IsNone) return;

			int currItem = GetRandomWeightedIndex(_weights);

			if(!repeat.Value)
			{
				int _repeat = 0;
				while(currItem == prevItem)
				{
					currItem = Mathf.Max(0,GetRandomWeightedIndex(_weights));
					_repeat++;

					if (_repeat > 20)
					{
						prevItem = -1;
					}
				}
			}
			storeIndex.Value = currItem;
			storeValue.SetValue(array.Get(currItem));
			
			
			prevItem = currItem;
		}

		public float[] GetWeightsFromCurve(AnimationCurve curve, int sampleCount)
		{
			float _totalTime = curve.keys[curve.keys.Length - 1].time;

			sampleCount = Mathf.Max(2, sampleCount);
			
			float[] weights = new float[sampleCount];
			
			float _interval = _totalTime / (sampleCount - 1);
			
			
			for (int i = 0; i < sampleCount; i++)
			{
				weights[i] = Mathf.Max(0,curve.Evaluate(_interval * i));
			}

			return weights;
		}
		
		/// <summary>
		/// Given an array of weights, returns a randomly selected index. 
		/// </summary>
		public int GetRandomWeightedIndex(float[] weights)
		{
			float totalWeight = 0;

			foreach (var t in weights)
			{
				totalWeight += t;
			}

			var random = Random.Range(0, totalWeight);

			for (var i = 0; i < weights.Length; i++)
			{
				if (random < weights[i])
				{
					return i;
				}

				random -= weights[i];
			}

			return -1;
		}
		

	}
}

