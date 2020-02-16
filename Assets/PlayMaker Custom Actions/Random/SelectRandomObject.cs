// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Random")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Selects a Random Object from an array of Objects.")]
	public class SelectRandomObject : FsmStateAction
	{
		[CompoundArray("Objects", "Object", "Weight")]
		public FsmObject[] objects;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmObject storeObject;

		public override void Reset()
		{
			objects = new FsmObject[3];
			weights = new FsmFloat[] { 1, 1, 1 };
			storeObject = null;
		}

		public override void OnEnter()
		{
			DoSelectRandomObject();
			Finish();
		}

		void DoSelectRandomObject()
		{
			if(objects == null) return;
			if(objects.Length == 0) return;
			if(storeObject == null) return;

			int randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);

			if(randomIndex != -1)
			{
				storeObject.Value = objects[randomIndex].Value;
			}

		}
	}
}
