// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: skipadu

// Original code that was used as a reference for this action was written by Stephan-B. 
// Link: https://forum.unity.com/threads/throw-an-object-along-a-parabola.158855/#post-1087673

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory(ActionCategory.Transform)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=18571.0")]
	[Tooltip("Moves given projectile based on given angle and gravity.")]
	[Note("Projectile needs to be created before the action.")]
	public class TransformMoveProjectile : FsmStateAction {
	
		[Tooltip("Start position for the moving projectile.")]
		public FsmOwnerDefault startPosition;
		
		[Tooltip("Where the projectile is moved.")]
		[RequiredField]
		public FsmGameObject destination;
		
		[Tooltip("Angle of arc.")]
		[RequiredField]
		public FsmFloat startAngle;
		
		[Tooltip("Speed of moving.")]
		[RequiredField]
		public FsmFloat gravity;
		
		[Tooltip("Projectile that will be moving.")]
		[RequiredField]
		public FsmGameObject projectile;
		
		[Tooltip("Event to send when moving is done. (optional)")]
		public FsmEvent readyEvent;
		
		[Tooltip("Store travelled distance to variable. (optional)")]
		[UIHint(UIHint.Variable)]
		public FsmFloat distanceTravelled;
	
		public override void OnEnter()
		{
			StartCoroutine(MoveProjectile((float i) => {
				if(readyEvent != null) {
					if(!distanceTravelled.IsNone) {
						distanceTravelled.Value = i;
					}
					Fsm.Event(readyEvent);
				}
			}));
		}
		
		IEnumerator MoveProjectile(System.Action<float> callBack)
		{
			float targetDistance = Vector3.Distance(projectile.Value.transform.position, destination.Value.transform.position);
			float projectileVelocity = targetDistance / (Mathf.Sin(2 * startAngle.Value * Mathf.Deg2Rad) / gravity.Value);
			float velocityX = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(startAngle.Value * Mathf.Deg2Rad);
			float velocityY = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(startAngle.Value * Mathf.Deg2Rad);
			float flightDuration = targetDistance / velocityX;

			projectile.Value.transform.rotation = Quaternion.LookRotation(destination.Value.transform.position - projectile.Value.transform.position);

			float elapsedTime = 0;
			while (elapsedTime < flightDuration)
			{
				projectile.Value.transform.Translate(0, (velocityY - (gravity.Value * elapsedTime)) * Time.deltaTime, velocityX * Time.deltaTime);      
				elapsedTime += Time.deltaTime;


				yield return null;
			}

			callBack(targetDistance);
		}
		
		public override void Reset()
		{
			startPosition = null;
			destination = null;
			startAngle = null;
			gravity = null;
			projectile = null;
			readyEvent = null;
			distanceTravelled = new FsmFloat { UseVariable = true };
		}
	
	}
}
