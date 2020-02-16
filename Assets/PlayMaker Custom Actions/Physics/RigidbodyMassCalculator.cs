// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Rigidbody Mass

using UnityEngine;
using HutongGames.PlayMaker;


namespace HutongGames.PlayMaker.Actions
{
[ActionCategory(ActionCategory.Physics)]
[Tooltip("Used to approximate a proper mass value from all the colliders in a given Rigidbody (m = d * v).")]
[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11901.0")]

public class RigidbodyMassCalculator : FsmStateAction
{
	[RequiredField]
	public FsmOwnerDefault gameObject;
	[ActionSection("Setup")]
	public FsmFloat density;
	
	[ActionSection("Options")]
	[Tooltip("Only for 2D Box, Circle & Polygon collider")]
	public FsmBool is2D;

	private GameObject go;
	private Rigidbody rb;
	private Rigidbody2D rb2d;
	private float volume;

	public override void Reset()
	{
			gameObject = null;
			density = 1f;
			is2D = false;
	}



	public override void OnEnter()
	{
			go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (is2D.Value == false){
			rb = go.GetComponent<Rigidbody>();
				if (rb == null){ 
					Debug.Log ("<color=#6B8E23ff> Missing 3D rigibody</color>",this.Owner);
					Finish ();}
				else{
					RecalculateMass();}	}

			else {

			rb2d = go.GetComponent<Rigidbody2D>();
				if (rb2d == null){ 
					Debug.Log ("<color=#6B8E23ff> Missing 2D rigibody</color>",this.Owner);
					Finish ();}
				else {
					RecalculateMass2D();	}
			}
				
		
			Finish ();
	}

		public void RecalculateMass() {
			Collider[] cols = go.GetComponentsInChildren<Collider>();

			for(var i = 0; i<cols.Length; i++){

					volume +=  GetVolume( cols[i], cols[i].transform.localScale );
			
			}

			rb.mass = volume * density.Value;
		}

		public void RecalculateMass2D() {
			Collider2D[] cols = go.GetComponentsInChildren<Collider2D>();
			
			for(var i = 0; i<cols.Length; i++){
				
				volume +=  GetVolume2D( cols[i], cols[i].transform.localScale );
				
			}
			
			rb2d.mass = volume * density.Value;
		}


		static float GetVolume( Collider c, Vector3 scale ) {
			if( c is BoxCollider )
				return BoxVolume( Vector3.Scale(( c as BoxCollider ).size, scale) );
			
			if( c is MeshCollider )
				return BoxVolume( Vector3.Scale( c.bounds.extents * 2, scale ) );
			
			float uniScale = scale.x * scale.y * scale.z;
			
			if( c is SphereCollider )
				return SphereVolume( ( c as SphereCollider ).radius * uniScale );
			
			if( c is CapsuleCollider ) {
				CapsuleCollider cc = c as CapsuleCollider;
				return CapsuleVolume( cc.height * scale[cc.direction], cc.radius * uniScale );
			}
			
			if( c is CharacterController ) {
				CharacterController chc = c as CharacterController;
				return CapsuleVolume( chc.height * scale.y, chc.radius * uniScale );
			}
			
			Debug.Log( "Invalid attempt to get volume of " + c.GetType().Name, c.gameObject );
			return 0f;
		}

		static float GetVolume2D( Collider2D c, Vector2 scale ) {
			if( c is BoxCollider2D )
				return Box2DVolume( Vector2.Scale(( c as BoxCollider2D ).size, scale) );
			
			if( c is PolygonCollider2D )
				return Box2DVolume( Vector2.Scale( c.bounds.extents * 2, scale ) );

			float uniScale = scale.x * scale.y;
			
			if( c is CircleCollider2D )
				return Sphere2DVolume( ( c as CircleCollider2D ).radius * uniScale );

			
			Debug.Log( "Invalid attempt to get volume of " + c.GetType().Name, c.gameObject );
			return 0f;
		}
//3D -->
		
		static float BoxVolume( Vector3 size ) {
			return size.x * size.y * size.z;
		}
		
		static float SphereVolume( float r ) {
			return r * r * r * Mathf.PI * ( 4f / 3f );
		}
		
		static float CylinderVolume( float h, float r ) {
			return h * r * r * Mathf.PI;
		}
		
		static float CapsuleVolume( float h, float r ) {
			float cylHeight = h - 2 * r;
			float sphereVol = SphereVolume( r );
			// If radius is twice as large or bigger than h - it degenerates into a sphere
			if( cylHeight <= 0 )
				return sphereVol;
			float cylVol = CylinderVolume( cylHeight, r );
			return sphereVol + cylVol;
		}
//2D -->
		static float Box2DVolume( Vector2 size ) {
			return size.x * size.y;
		}
		
		static float Sphere2DVolume( float r ) {
			return r * r * Mathf.PI * ( 4f / 3f );
		}


}
}
