// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=11220.0


using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
[ActionCategory(ActionCategory.GameObject)]
[Tooltip("Create a 2d grid")]
[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11220.0")]




	public class grid2d : FsmStateAction
{
		public FsmGameObject gameObject;
		public FsmInt width;
		public FsmInt height;
		[Tooltip("Space between objects in grid. 1 = no space")]
		public FsmFloat space;


		[ActionSection("options")]
		public FsmBool verticalSpawn;

		public override void Reset()
		{
			width = 10;
			height = 10;
			space = 1.0f;
			verticalSpawn = false;
		}
	
	public override void OnEnter()
	{
			if (verticalSpawn.Value){
			for (int x =0; x < width.Value; x++) {
				for (int y =0; y < height.Value; y++){
					GameObject gridPlane = (GameObject)Object.Instantiate(gameObject.Value);
					gridPlane.transform.position = new Vector2(gridPlane.transform.position.x + x, 
					                                           gridPlane.transform.position.y + y) * space.Value;
				}
			}
			}

			else{
			for (int y =0; y < width.Value; y++) {
				for (int x =0; x < height.Value; x++){
					GameObject gridPlane = (GameObject)Object.Instantiate(gameObject.Value);
					gridPlane.transform.position = new Vector2(gridPlane.transform.position.x + x, 
					                                           gridPlane.transform.position.y + y) * space.Value;
					}
			}
			
			}
	Finish();
		}

	

}
}

