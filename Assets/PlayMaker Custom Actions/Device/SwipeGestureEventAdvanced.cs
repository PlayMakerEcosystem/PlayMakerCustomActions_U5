// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Sends an event when a swipe is detected. Advanced: Store the speed, distance and duration of the swipe (inedepent of 'Min Swipe Distance').")]
	public class SwipeGestureEventAdvanced : FsmStateAction
	{
		[Tooltip("How far a touch has to travel to be considered a swipe. Uses normalized distance (e.g. 1 = 1 screen diagonal distance). Should generally be a very small number.")]
		public FsmFloat minSwipeDistance;
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the speed of the swipe (distance / time).")]
		public FsmFloat getSwipeSpeed;
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the distance the swipe traveled.")]
		public FsmFloat getSwipeDistance;
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the duration between the swipe start and end.")]
		public FsmFloat getSwipeDuration;
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the force of the swipe.")]
		public FsmVector2 getForce;

		[Tooltip("Event to send when swipe left detected.")]
		public FsmEvent swipeLeftEvent;
		[Tooltip("Event to send when swipe right detected.")]
		public FsmEvent swipeRightEvent;
		[Tooltip("Event to send when swipe up detected.")]
		public FsmEvent swipeUpEvent;
		[Tooltip("Event to send when swipe down detected.")]
		public FsmEvent swipeDownEvent;

		float screenDiagonalSize = 0.0f;
		float minSwipeDistancePixels = 0.0f;
		bool touchHasStarted = false;
		Vector2 touchStartPos;
		float touchStartTime = 0.0f;

		public override void Reset()
		{
			minSwipeDistance = 0.1f;
			getSwipeSpeed = null;
			getSwipeDistance = null;
			getSwipeDuration = null;
			getForce = Vector2.zero;
			swipeLeftEvent = null;
			swipeRightEvent = null;
			swipeUpEvent = null;
			swipeDownEvent = null;
		}

		public override void OnEnter()
		{
			screenDiagonalSize = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
			minSwipeDistancePixels = minSwipeDistance.Value * screenDiagonalSize;
		}

		public override void OnUpdate()
		{
			foreach(Touch touch in Input.touches)
			{
				switch(touch.phase)
				{
					case TouchPhase.Began:

						touchHasStarted = true;
						touchStartPos = touch.position;
						touchStartTime = FsmTime.RealtimeSinceStartup;

						break;

					case TouchPhase.Ended:

						Vector2 touchEndPos = Input.mousePosition;
						touchEndPos = Camera.main.ScreenToWorldPoint(touchEndPos);

						var force = touchEndPos - touchStartPos;
						force /= (Time.time - touchStartTime);
						getForce.Value = force;

						if(touchHasStarted)
						{
							TestForSwipeGesture(touch);
							touchHasStarted = false;
						}

						break;

					case TouchPhase.Canceled:

						touchHasStarted = false;

						break;

					case TouchPhase.Stationary:

						if(touchHasStarted)
						{
							touchStartPos = touch.position;
							touchStartTime = FsmTime.RealtimeSinceStartup;
						}

						break;

					case TouchPhase.Moved:

						break;
				}
			}
		}

		void TestForSwipeGesture(Touch touch)
		{
			var lastPos = touch.position;

			getSwipeDistance.Value = Vector2.Distance(touchStartPos, lastPos);
			getSwipeDuration.Value = FsmTime.RealtimeSinceStartup - touchStartTime;
			getSwipeSpeed.Value = getSwipeDistance.Value / getSwipeDuration.Value;

			if(getSwipeDistance.Value > minSwipeDistancePixels)
			{
				float dy = lastPos.y - touchStartPos.y;
				float dx = lastPos.x - touchStartPos.x;

				float angle = Mathf.Rad2Deg * Mathf.Atan2(dx, dy);

				angle = (360 + angle - 45) % 360;

				if(angle < 90)
				{
					Fsm.Event(swipeRightEvent);
				} else if(angle < 180)
				{
					Fsm.Event(swipeDownEvent);
				} else if(angle < 270)
				{
					Fsm.Event(swipeLeftEvent);
				} else
				{
					Fsm.Event(swipeUpEvent);
				}
			}
		}

	}
}
