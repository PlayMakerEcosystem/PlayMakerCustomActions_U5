// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Draw gizmo arrow in direction or towards target.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11752.0")]

	public class DebugDrawArrow : FsmStateAction 
	
	{
	public FsmOwnerDefault gameObject;
	[Tooltip("Set the distance. Min:0.1f")]
	[ActionSection("Setup")]
	[TitleAttribute("distance")]
	public FsmFloat radius;
	[Tooltip("Set the arrow head angle if needed")]
	public FsmFloat arrowHeadAngle;
	[Tooltip("Set the arrow head length if needed")]
		public FsmFloat	arrowHeadLength;
	[Tooltip("Set the spehere size")]
	public FsmFloat sphereSize;

	[ActionSection("Direction options")]
	[Tooltip("Direction of arrow or use target")]
	public FsmVector3 direction;
	[Tooltip("Target if not manually setting direction")]
	public FsmGameObject target;

	[ActionSection("Options")]
	[Tooltip("Correct gizmo when looking up or down")]
	public FsmBool using2D;
	[Tooltip("The color to use for the center debug line.")]
	public FsmColor setColor;
	public FsmColor sphereColor;
	

	private GameObject go;

			
		public override void Reset()
		{
			radius = 1f;
			direction = Vector3.forward;
			sphereSize = 1;
			using2D = false;
			setColor = Color.red;
			sphereColor = Color.yellow;
			arrowHeadAngle = 20f;
			arrowHeadLength = 0.25f;
		}


		public void OnDrawGizmos() {
				
			go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (target.Value != null){

				direction.Value = target.Value.transform.position-go.transform.position;
			}

				Gizmos.color = setColor.Value;

				Gizmos.DrawRay(go.transform.position, direction.Value.normalized*radius.Value);	
				Vector3 right = Quaternion.LookRotation(direction.Value) * Quaternion.Euler(0,180+arrowHeadAngle.Value,0) * new Vector3(0,0,1);
				Vector3 left = Quaternion.LookRotation(direction.Value) * Quaternion.Euler(0,180-arrowHeadAngle.Value,0) * new Vector3(0,0,1);
				Gizmos.DrawRay(go.transform.position + direction.Value.normalized*radius.Value, right * arrowHeadLength.Value);
				Gizmos.DrawRay(go.transform.position + direction.Value.normalized*radius.Value, left * arrowHeadLength.Value);

			if (using2D.Value == true){
				Vector3 up = Quaternion.LookRotation(direction.Value) * Quaternion.Euler(180+arrowHeadAngle.Value,0,0) * new Vector3(0,0,1);
				Vector3 down = Quaternion.LookRotation(direction.Value) * Quaternion.Euler(180-arrowHeadAngle.Value,0,0) * new Vector3(0,0,1);
				Gizmos.DrawRay(go.transform.position + direction.Value.normalized*radius.Value, up * arrowHeadLength.Value);
				Gizmos.DrawRay(go.transform.position + direction.Value.normalized*radius.Value, down * arrowHeadLength.Value);
			}
		
			Gizmos.color = sphereColor.Value;
			Gizmos.DrawSphere(go.transform.position, sphereSize.Value);
			


		}
	}
}
