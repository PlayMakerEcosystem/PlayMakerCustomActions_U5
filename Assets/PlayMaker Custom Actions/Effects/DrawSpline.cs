// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

//DrawSpline.cs v1.1.0
//Curve Drawing code from: https://en.wikibooks.org/wiki/Cg_Programming/Unity/B%C3%A9zier_Curves
//Other elements from: DrawLine.cs by AndrewRaphaelLukasik@live.com http://hutonggames.com/playmakerforum/index.php?topic=3943.0
// made by holyfingers : http://hutonggames.com/playmakerforum/index.php?topic=11193.msg52832#msg52832



using UnityEngine;
namespace HutongGames.PlayMaker.Actions

{
	[ActionCategory(ActionCategory.Effects)]
	[Tooltip("Draws a Spline between Game Objects using Unity's Line Renderer.")]
	public class DrawSpline : FsmStateAction
	
	{
		[RequiredField]
		[Tooltip("Game Objects used as Control Points. Use a minimum of 5. Start and End should occupy the first TWO and last TWO slots respectivley.")]
		[CompoundArray("Control Points", "Control Point", "Label")]
		public FsmGameObject[] controlPoints;
		public FsmString[] labels;

		[Tooltip("Curve Material (If set to None will automatically use 'Particles/Additive'.")]
		public FsmMaterial material;

		[Tooltip("Curve Colour at Start.")]
		public FsmColor startColour;
		
		[Tooltip("Curve Colour at End.")]
		public FsmColor endColour;

		[Tooltip("Curve Width at Start.")]
		public FsmFloat startWidth = 0.0f;

		[Tooltip("Curve Width at End.")]
		public FsmFloat endWidth = 1.0f;

		[Tooltip("Number of points with which the Curve is drawn.")]
		public FsmInt numberOfPoints = 10;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		[Tooltip("Destroys the Spline on exiting the state.")]
		public bool destroyOnExit;

		private FsmGameObject goLineRenderer;
		private FsmGameObject startObjectPosition;
		private FsmGameObject middleObjectPosition;
		private FsmGameObject endObjectPosition;
			
		public override void Reset()
		
		{
			material = null;
			startColour = Color.clear;
			endColour = Color.white;
			numberOfPoints = 10;
			startWidth = 0.0f;
			endWidth = 1.0f;
			goLineRenderer = null;
			everyFrame = false;
			destroyOnExit = false;
		}

		public override void OnEnter()

		{	
			// create line renderer
			goLineRenderer = new GameObject("FSM draw line");
			goLineRenderer.Value.AddComponent<LineRenderer>();
			goLineRenderer.Value.GetComponent<LineRenderer>().material = material.Value;
			if (material.Value == null) {goLineRenderer.Value.GetComponent<LineRenderer>().material.shader = Shader.Find ("Particles/Additive");}

			// update line renderer
			goLineRenderer.Value.GetComponent<LineRenderer>().startColor = startColour.Value;
			goLineRenderer.Value.GetComponent<LineRenderer>().endColor = endColour.Value;
			goLineRenderer.Value.GetComponent<LineRenderer> ().startWidth = startWidth.Value;
			goLineRenderer.Value.GetComponent<LineRenderer> ().endWidth = endWidth.Value;


			if (numberOfPoints.Value < 2)
			{
				numberOfPoints.Value = 2;
			}

			goLineRenderer.Value.GetComponent<LineRenderer>().positionCount = (numberOfPoints.Value * 
			                            (controlPoints.Length - 2));

			// loop over segments of spline
			Vector3 p0;
			Vector3 p1;
			Vector3 p2;

			//Control Points
			for (int j = 0; j < controlPoints.Length - 2; j++)
			
			{
				//check control points
				if (controlPoints[j] == null || 
				    controlPoints[j + 1] == null ||
				    controlPoints[j + 2] == null)
					
				{
					Debug.Log("returning");
					return;
				}
				
				// determine control points of segment
				p0 = 0.5f * (controlPoints[j].Value.transform.position 
				             + controlPoints[j + 1].Value.transform.position);
				p1 = controlPoints[j + 1].Value.transform.position;
				p2 = 0.5f * (controlPoints[j + 1].Value.transform.position 
				             + controlPoints[j + 2].Value.transform.position);
				
				
				// set points of quadratic Bezier curve
				Vector3 position;
				float t; 
				float pointStep = 1.0f / numberOfPoints.Value;
				if (j == controlPoints.Length - 3)
				{
					pointStep = 1.0f / (numberOfPoints.Value - 1.0f);
					// last point of last segment should reach p2
				}  
				for(int i = 0; i < numberOfPoints.Value; i++) 
				{
					t = i * pointStep;
					position = (1.0f - t) * (1.0f - t) * p0 
						+ 2.0f * (1.0f - t) * t * p1
							+ t * t * p2;
					goLineRenderer.Value.GetComponent<LineRenderer>().SetPosition(i + j * numberOfPoints.Value, position);
				}
			}

			if (!everyFrame)
				
			{
				Finish();
			}


	}

		public override void OnUpdate()

		{
			
			// update line renderer
			goLineRenderer.Value.GetComponent<LineRenderer>().startColor = startColour.Value;
			goLineRenderer.Value.GetComponent<LineRenderer>().endColor = endColour.Value;
			
			if (numberOfPoints.Value < 2)
			{
				numberOfPoints.Value = 2;
			}
			
			goLineRenderer.Value.GetComponent<LineRenderer>().positionCount = (numberOfPoints.Value * (controlPoints.Length - 2));
			
			// loop over segments of spline
			Vector3 p0;
			Vector3 p1;
			Vector3 p2;
			
			for (int j = 0; j < controlPoints.Length - 2; j++)
				
			{
				//check control points
				if (controlPoints[j] == null || 
				    controlPoints[j + 1] == null ||
				    controlPoints[j + 2] == null)
					
				{
					Debug.Log("returning");
					return;
				}
				
				// determine control points of segment
				p0 = 0.5f * (controlPoints[j].Value.transform.position 
				             + controlPoints[j + 1].Value.transform.position);
				p1 = controlPoints[j + 1].Value.transform.position;
				p2 = 0.5f * (controlPoints[j + 1].Value.transform.position 
				             + controlPoints[j + 2].Value.transform.position);
				
				
				// set points of quadratic Bezier curve
				Vector3 position;
				float t; 
				float pointStep = 1.0f / numberOfPoints.Value;
				if (j == controlPoints.Length - 3)
				{
					pointStep = 1.0f / (numberOfPoints.Value - 1.0f);
					// last point of last segment should reach p2
				}  
				for(int i = 0; i < numberOfPoints.Value; i++) 
				{
					t = i * pointStep;
					position = (1.0f - t) * (1.0f - t) * p0 
						+ 2.0f * (1.0f - t) * t * p1
							+ t * t * p2;
					goLineRenderer.Value.GetComponent<LineRenderer>().SetPosition(i + j * numberOfPoints.Value, 
					                                                              position);
				}
			}

		}

				public override void OnExit()

				{

						if (destroyOnExit) 

						{
								Object.Destroy (goLineRenderer.Value);	
						}		
				}



}
}
