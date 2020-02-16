// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Mesh")]
	[Tooltip("Create a wave effect on a mesh")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12103.0")]
	public class WaveMeshFx : FsmStateAction {

		public FsmOwnerDefault gameObject;

		[ActionSection("Setup")]
		public FsmVector3 waveSource;
		public FsmFloat freq;
		public FsmFloat amp;
		public FsmFloat waveLength;
	
		[ActionSection("Options")]
		public FsmBool forceQuit;
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent failureEvent;

		private Vector3[] vertices;
		private Mesh mesh;

	public override void Reset()
	{
			waveSource = new Vector3 (2.0f, 0.0f, 2.0f);
			freq = 0.1f;
			amp = 0.01f;
			waveLength = 0.05f;
			mesh = null;
			forceQuit = false;
			failureEvent = null;
	}

	
		public override void OnEnter() {

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			MeshFilter mf = go.GetComponent<MeshFilter>();

			if(mf == null)
			{
				Debug.LogWarning("<b>[WaveMeshFx]</b><color=#FF9900ff>  Please review!</color>", this.Owner);
				Fsm.Event(failureEvent);
				Finish();
			}

			mesh = mf.mesh;
			vertices = mesh.vertices;
	}
	

		public override void OnUpdate() {

			if (forceQuit.Value == false){
			WaveAction();
			}

			else{
				Finish();
			}
	}

		void WaveAction()
		{
			for(int i = 0; i < vertices.Length; i++)
			{
				Vector3 v = vertices[i];
				v.y = 0.0f;
				float dist = Vector3.Distance(v, waveSource.Value);
				dist = (dist % waveLength.Value) / waveLength.Value;
				v.y = amp.Value * Mathf.Sin(Time.time * Mathf.PI * 2.0f * freq.Value
				                       + (Mathf.PI * 2.0f * dist));
				vertices[i] = v;
			}
			mesh.vertices = vertices;
		}




}
}

