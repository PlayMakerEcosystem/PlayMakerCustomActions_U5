// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
//License: Attribution 4.0 International (CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Like 'Random Wait' but also randomizes the min and max values by a random offset to make the result even more unpredictable.")]
	public class RandomWaitAroundOffset : FsmStateAction
	{

		[RequiredField]
		[Tooltip("Minimum amount of time to wait.")]
		public FsmFloat min;

		[RequiredField]
		[Tooltip("Maximum amount of time to wait.")]
		public FsmFloat max;

		public FsmFloat offset;

		[Tooltip("Event to send when timer is finished.")]
		public FsmEvent finishEvent;

		[Tooltip("Ignore time scale.")]
		public bool realTime;


		private float startTime;
		private float timer;
		private float time;

		public override void Reset()
		{
			min = 0f;
			max = 1f;
			offset = 1f;
			finishEvent = null;
			realTime = false;
		}

		public override void OnEnter()
		{
			float n = max.Value;
			float o = offset.Value;
			float m = min.Value;

			float minFrom = m - offset.Value;
			float minTo = m + o > n - o
						? n - o : m + o;

			float maxFrom = n - o < m + o
						  ? m + o : n - o;
			float maxTo = n + o;

			m = Random.Range(minFrom, minTo);
			n = Random.Range(maxFrom, maxTo);

			if(m < 0) m = 0;
			if(n < 0) n = 0;
			if(n < m) n = m;

			time = Random.Range(m, n);

			if(time <= 0)
			{
				Fsm.Event(finishEvent);
				Finish();
				return;
			}

			startTime = FsmTime.RealtimeSinceStartup;
			timer = 0f;
		}

		public override void OnUpdate()
		{
			if(realTime) timer = FsmTime.RealtimeSinceStartup - startTime;
			else timer += Time.deltaTime;

			if(timer >= time)
			{
				Finish();
				if(finishEvent != null) Fsm.Event(finishEvent);
			}
		}

	}
}
