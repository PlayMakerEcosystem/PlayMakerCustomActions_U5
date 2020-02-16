// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: NPC fov
//v1.1

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("FOV")]
	[Tooltip("Send event or set bool if target is inside gameobject 2D FOV. Does NOT use physics. Set FOV direction manually or auto to target")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11753.0")]
	
	public class SetEnemyFov2d : FsmStateAction 
		
	{
		public FsmOwnerDefault gameObject;
		[Tooltip("Set the distance. Min:0.1f")]
		[ActionSection("FOV Setup")]
		public FsmFloat radius;
		[Tooltip("Set the FOV. Min:1f")]
		[Range(1.0f, 360f)]
		public FsmInt fov;
		
		[ActionSection("Direction options")]
		[Tooltip("Manually set the FOV direction")]
		public FsmVector2 direction;
		[Tooltip("Set rotation default gameobject rotation")]
		public FsmBool autoRotate;
		[Tooltip("Manually set the FOV rotation. Leave it at 0 to ignore.")]
		public FsmFloat rotation;
		
		[ActionSection("Options")]
		[Tooltip("Auto set the FOV direction towards the target object")]
		[TitleAttribute("Auto Target")]
		public FsmBool autoOn;
		[Tooltip("use Late Update")]
		public FsmBool useLateUpdate;
		
		[ActionSection("Target")]
		[Tooltip("Your target - player or npc")]	
		public FsmGameObject target;
		
		[ActionSection("Output")]
		[Tooltip("Will set bool to true or false")]
		public FsmBool insideFov;
		public FsmEvent insideEvent;
		public FsmEvent outsideEvent;
		
		[ActionSection("Debug")]
		[Tooltip("Draw the FOV in scene")]
		public FsmBool drawGizmo;
		[Tooltip("The color to use for the center debug line.")]
		public FsmColor centerColor;
		[Tooltip("The color to use for the angle debug line.")]
		public FsmColor angleColor;
		
		public FsmBool everyFrame;
		
		private Vector2 leftLineFOV;
		private Vector2 rightLineFOV;
		private GameObject go;
		private float tempdata;
		
		
		public override void Reset()
		{
			radius = 1f;
			fov = 90;
			direction = Vector2.up;
			target = null;
			everyFrame = true;
			insideEvent = null;
			outsideEvent = null;
			insideFov = null;
			autoOn = false;
			drawGizmo = true;
			useLateUpdate = false;
			centerColor = Color.green;
			angleColor = Color.yellow;
			rotation= 90;
			autoRotate= true;
			
		}
		
		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			if (useLateUpdate.Value == true)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
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
			
			
			if (autoOn.Value == true)
			{
				autoRotate.Value = false;

				Vector3 diff = target.Value.transform.position - go.transform.position;
				diff.Normalize();
				

				direction.Value = new Vector2(diff.x,diff.y);


				float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
				go.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

				var temp_rot = go.transform.eulerAngles;
				rotation.Value = temp_rot.z;

				tempdata = rotation.Value;
			
			}
			
			if (autoRotate.Value == false){
				
				if (rotation.Value != tempdata){

					
					direction.Value = Vector2FromAngle(rotation.Value);
					tempdata = rotation.Value;
				}
				
			}
			
			if (autoRotate.Value == true) {
				
				

				var temp_rot = go.transform.eulerAngles;
				rotation.Value = temp_rot.z;
				direction.Value = Vector2FromAngle(rotation.Value);

				tempdata = rotation.Value;
			}
			
			
			
			rightLineFOV = RotatePointAroundTransform(direction.Value.normalized*radius.Value, -fov.Value/2);
			leftLineFOV = RotatePointAroundTransform(direction.Value.normalized*radius.Value, fov.Value/2);
			insideFov.Value = InsideFOV(new Vector2(target.Value.transform.position.x, target.Value.transform.position.y));
			
			exitAction();
		}
		
		
		public Vector2 Vector2FromAngle(float a)
		{
			a *= Mathf.Deg2Rad;
			return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
		}
		
		
		public bool InsideFOV(Vector2 playerPos) {
			float squaredDistance = ((playerPos.x - go.transform.position.x)*(playerPos.x - go.transform.position.x)) + ((playerPos.y-go.transform.position.y)*(playerPos.y-go.transform.position.y));
			
			if(radius.Value * radius.Value >= squaredDistance) {
				float signLeftLine = (leftLineFOV.x) * (playerPos.y - go.transform.position.y) - (leftLineFOV.y) * (playerPos.x-go.transform.position.x);
				float signRightLine = (rightLineFOV.x) * (playerPos.y - go.transform.position.y) - (rightLineFOV.y) * (playerPos.x-go.transform.position.x);
				if(fov.Value <= 180) {
					
					if(signLeftLine <= 0 && signRightLine >= 0)
						return true;
				} else {
					if(!(signLeftLine >= 0 && signRightLine <= 0))
						return true;
				}
			}
			return false;
		}
		
		
		private Vector2 RotatePointAroundTransform(Vector2 p, float angles) {
			return new Vector2(Mathf.Cos((angles)  * Mathf.Deg2Rad) * (p.x) - Mathf.Sin((angles) * Mathf.Deg2Rad) * (p.y),
			                   Mathf.Sin((angles)  * Mathf.Deg2Rad) * (p.x) + Mathf.Cos((angles) * Mathf.Deg2Rad) * (p.y));
		}
		
		
		public void OnDrawGizmos() {
			
			if (drawGizmo.Value == true & go !=null){
				Gizmos.color = centerColor.Value;
				Gizmos.DrawRay(go.transform.position, direction.Value.normalized*radius.Value);
				
				Gizmos.color = angleColor.Value;
				Gizmos.DrawRay(go.transform.position, rightLineFOV);
				Gizmos.DrawRay(go.transform.position, leftLineFOV);
				
				Vector2 p = rightLineFOV;
				for(int i = 1; i <= 20; i++) {
					float step = fov.Value/20;
					Vector2 p1 = RotatePointAroundTransform(direction.Value.normalized*radius.Value, -fov.Value/2 + step*(i));
					Gizmos.DrawRay(new Vector2(go.transform.position.x, go.transform.position.y) + p, p1-p);
					p = p1;
				}
				Gizmos.DrawRay(new Vector2(go.transform.position.x, go.transform.position.y) + p, leftLineFOV - p);
			}
		}
	}
}
