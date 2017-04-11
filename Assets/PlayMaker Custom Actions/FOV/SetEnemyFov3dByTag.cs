// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: NPC fov
// v1.0

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("FOV")]
	[Tooltip("Send event or set bool if target is inside gameobject FOV. Does NOT use physics. Set FOV direction manually or auto to target")]
	[HelpUrl("")]

	public class SetEnemyFov3dByTag : FsmStateAction 
	
	{
	public FsmOwnerDefault gameObject;
	[Tooltip("Set the distance. Min:0.1f")]
	[ActionSection("FOV Setup")]
	[TitleAttribute("distance")]
	public FsmFloat radius;
	[Tooltip("Set the FOV. Min:1f")]
	[Range(1.0f, 360f)]
	public FsmInt fov;

	[ActionSection("Direction options")]
	[Tooltip("Manually set the FOV direction")]
	public FsmVector3 direction;
	[Tooltip("Set rotation default gameobject rotation")]
	public FsmBool autoRotate;
	[Tooltip("Manually set the FOV rotation. Leave it at 0 to ignore.")]
	public FsmVector3 rotation;
	
	[ActionSection("Options")]
	[Tooltip("use Late Update")]
	public FsmBool useLateUpdate;

	[ActionSection("Target")]
	[Tooltip("Your agent - player or npc by tag if target object is empty")]
	[UIHint(UIHint.Tag)]
	[TitleAttribute("by Tag")]
	public FsmString tag;

	[ActionSection("Output")]
	[Tooltip("Will set bool to true or false")]
	public FsmBool insideFov;
	public FsmEvent insideEvent;
	public FsmEvent outsideEvent;

	[ActionSection("Debug")]
	[Tooltip("Draw the FOV in scene")]
	public FsmBool drawGizmo;
	[Tooltip("Correct gizmo when looking up or down")]
	public FsmBool flipOn;
	[Tooltip("The color to use for the center debug line.")]
	public FsmColor centerColor;
	[Tooltip("The color to use for the angle debug line.")]
	public FsmColor angleColor;

	public FsmBool everyFrame;
		
	private Vector3 leftLineFOV;
	private Vector3 rightLineFOV;
	private GameObject go;
	private Vector3 tempdata;
	private Quaternion leftRayRotation;
	private Quaternion rightRayRotation;
	private List<GameObject> tagList;
	private GameObject target;

		public override void Reset()
		{
			radius = 1f;
			fov = 90;
			direction = Vector3.forward;
			target = null;
			everyFrame = true;
			insideEvent = null;
			outsideEvent = null;
			insideFov = null;
			drawGizmo = true;
			useLateUpdate = false;
			centerColor = Color.green;
			angleColor = Color.yellow;
			rotation= null;
			autoRotate= true;
			tagList = new List<GameObject>();
		}


		public override void OnEnter()
		{
			tempdata = rotation.Value;
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			DirectionSetup();


		}





		public override void OnUpdate(){
		

			if (everyFrame.Value == true && useLateUpdate.Value == false){
				
			

					DirectionSetup();
					

			}
		}

		public override void OnLateUpdate(){


			if (everyFrame.Value == true && useLateUpdate.Value == true){

			

					DirectionSetup();

			}

		}

		void exitAction(){

			if(insideEvent != null && insideFov.Value  == true){
				Fsm.Event(insideEvent);
			}

			if(outsideEvent != null && insideFov.Value  == false){
				Fsm.Event(outsideEvent);
			}

			if (everyFrame.Value == false){
				Finish();
			}

		}

		void DirectionSetup()
		{
			tagList = new List<GameObject>();
			tagList.AddRange(GameObject.FindGameObjectsWithTag(tag.Value));

			if (autoRotate.Value == false){

				if (rotation.Value != tempdata){
				direction.Value  = Quaternion.Euler(rotation.Value) * Vector3.forward;
				
				
				}

			}

			if (autoRotate.Value == true) {


					
					rotation.Value = go.transform.eulerAngles;
					direction.Value = Quaternion.Euler(rotation.Value) * Vector3.forward; 
					tempdata = rotation.Value;
	
			}





			FsmFloat floatVariable = fov.Value;


			for (int i = 0; i < tagList.Count; i++)
			{

				target = tagList[i];
				insideFov.Value = InsideFOV(target,go,floatVariable.Value, radius.Value);
			
				if (insideFov.Value == true)
					break;

			}

			exitAction();
		}


		
		public bool InsideFOV(GameObject targetTemp, GameObject goTemp, float angleTemp, float distanceTemp )
		{
			Vector3 distanceToPlayer =  targetTemp.transform.position - goTemp.transform.position;
			float angleToPlayer = Vector3.Angle(distanceToPlayer, direction.Value.normalized);
			float finalDistanceToPlayer = distanceToPlayer.magnitude;
			
			if (angleToPlayer <= angleTemp/2 & finalDistanceToPlayer <= distanceTemp)
				return true;
			
			return false;
		}

		
	
		

		public void OnDrawGizmos() {
				
			if (drawGizmo.Value == true & go !=null){
				Gizmos.color = centerColor.Value;
				float arrowHeadLength = 0.25f;
				float arrowHeadAngle = 20.0f;

				Gizmos.DrawRay(go.transform.position, direction.Value.normalized*radius.Value);	
				Vector3 right = Quaternion.LookRotation(direction.Value) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
				Vector3 left = Quaternion.LookRotation(direction.Value) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
				Gizmos.DrawRay(go.transform.position + direction.Value.normalized*radius.Value, right * arrowHeadLength);
				Gizmos.DrawRay(go.transform.position + direction.Value.normalized*radius.Value, left * arrowHeadLength);


				Gizmos.color = angleColor.Value;

				if (flipOn.Value == false){
				 leftRayRotation = Quaternion.AngleAxis( -fov.Value/2, Vector3.up );
				 rightRayRotation = Quaternion.AngleAxis( fov.Value/2, Vector3.up );
				}

				if (flipOn.Value == true){
					 leftRayRotation = Quaternion.AngleAxis( -fov.Value/2, Vector3.forward );
					 rightRayRotation = Quaternion.AngleAxis( fov.Value/2, Vector3.forward );
				}


				Vector3 leftRayDirection = leftRayRotation * direction.Value;
				Vector3 rightRayDirection = rightRayRotation * direction.Value;
				Gizmos.DrawRay( go.transform.position, leftRayDirection * radius.Value );
				Gizmos.DrawRay( go.transform.position, rightRayDirection * radius.Value );
		


			}


		}
	}
}

