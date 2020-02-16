// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Color)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Compares one color against multiple and sends Events based on the result.")]
	public class CompareColors : FsmStateAction
	{
		public enum CompareColorBy
		{
			R,
			G,
			B,
			A,
			RG,
			GB,
			RB,
			RA,
			GA,
			BA,
			RGA,
			GBA,
			RBA,
			RGB,
			RGBA
		}

		[RequiredField]
		[Tooltip("The color variable to compare.")]
		public FsmColor mainColor;

		[CompoundArray("Amount", "Compare to", "Compare Event")]

		[Tooltip("Color to compare to the main color.")]
		public FsmColor[] compareTos;

		[Tooltip("Event to raise on match.")]
		public FsmEvent[] compareEvents;

		[Tooltip("Event to raise if no matches are found.")]
		public FsmEvent noMatchEvent;

		[Tooltip("Defines what part of the colors should be compared (R = Red, G = Green, B = Blue, A = Alpha).")]
		public CompareColorBy compareBy;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of the check in a bool variable (true if equal, false if not equal).")]
		public FsmBool storeResult;

		[Tooltip("Repeat every frame. Useful if you're waiting for a true or false result.")]
		public bool everyFrame;

		public override void Reset()
		{
			mainColor = Color.white;
			compareTos = null;
			compareEvents = null;
			noMatchEvent = null;
			compareBy = CompareColorBy.RGBA;
			storeResult = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoCompareColors();

			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoCompareColors();
		}

		void DoCompareColors()
		{
			Color col1 = mainColor.Value;
			Color col2;

			for(int i = 0; i < compareTos.Length; i++)
			{
				if(compareTos[i].IsNone)
				{
					continue;
				}

				col2 = compareTos[i].Value;

				switch(compareBy)
				{
					case CompareColorBy.R:
						if(col1.r == col2.r) IsEqual(i);
						break;
					case CompareColorBy.G:
						if(col1.g == col2.g) IsEqual(i);
						break;
					case CompareColorBy.B:
						if(col1.b == col2.b) IsEqual(i);
						break;
					case CompareColorBy.A:
						if(col1.a == col2.a) IsEqual(i);
						break;
					case CompareColorBy.RG:
						if(col1.r == col2.r && col1.g == col2.g) IsEqual(i);
						break;
					case CompareColorBy.GB:
						if(col1.g == col2.g && col1.b == col2.b) IsEqual(i);
						break;
					case CompareColorBy.RB:
						if(col1.r == col2.r && col1.b == col2.b) IsEqual(i);
						break;
					case CompareColorBy.RA:
						if(col1.r == col2.r && col1.a == col2.a) IsEqual(i);
						break;
					case CompareColorBy.GA:
						if(col1.g == col2.g && col1.a == col2.a) IsEqual(i);
						break;
					case CompareColorBy.BA:
						if(col1.b == col2.b && col1.a == col2.a) IsEqual(i);
						break;
					case CompareColorBy.RGA:
						if(col1.r == col2.r && col1.g == col2.g && col1.a == col2.a) IsEqual(i);
						break;
					case CompareColorBy.GBA:
						if(col1.g == col2.g && col1.b == col2.b && col1.a == col2.a) IsEqual(i);
						break;
					case CompareColorBy.RBA:
						if(col1.r == col2.r && col1.b == col2.b && col1.a == col2.a) IsEqual(i);
						break;
					case CompareColorBy.RGB:
						if(col1.r == col2.r && col1.g == col2.g && col1.b == col2.b) IsEqual(i);
						break;
					case CompareColorBy.RGBA:
						if(col1 == col2) IsEqual(i);
						break;
				}
			}

			//nothing found, so fire the No-Match-Event
			if(noMatchEvent != null && storeResult.Value == false)
			{
				Fsm.Event(noMatchEvent);
			}
		}

		void IsEqual(int i)
		{
			Fsm.Event(compareEvents[i]);
			storeResult.Value = true;
		}
	}
}
