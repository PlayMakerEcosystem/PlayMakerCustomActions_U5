// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Map, Longitude, Latitude, Geo Coordinates


using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Get distance between two sets of longitude/latitude")]
	public class MapGetDistanceGeoCoordinates : FsmStateAction
	{
		[ActionSection("From")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat latitudeFrom;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat longitudeFrom;

		[ActionSection("To")]
		[UIHint(UIHint.Variable)]
		public FsmFloat latitudeTo;
		[UIHint(UIHint.Variable)]
		public FsmFloat longitudeTo;

		[ActionSection("result")]
		public FsmFloat result;

		[ActionSection("Options")]
		private double unitTypeSelect;

		public enum Mode{
			Kilometers,
			Nautical_Miles,
			Miles,
		}
		public Mode unitSelect;
		public FsmBool everyFrame;

		public override void Reset()
		{
			longitudeFrom = null;
			latitudeFrom = null;
			longitudeTo = null;
			latitudeTo = null;
			unitSelect = Mode.Kilometers;
			result = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			switch (unitSelect)
			{
			case Mode.Kilometers:
				unitTypeSelect = 1.609344;
				break;
			case Mode.Nautical_Miles:
				unitTypeSelect = 0.8684;
				break;
			case Mode.Miles:
				unitTypeSelect = 0;
				break;
			}

			result.Value = (float)DistanceTo((double)latitudeFrom.Value,(double)longitudeFrom.Value,(double)latitudeTo.Value,(double)longitudeTo.Value,unitTypeSelect);
			
			if (!everyFrame.Value)
				Finish();
		}


		public override void OnUpdate()
		{
			result.Value = (float)DistanceTo((double)latitudeFrom.Value,(double)longitudeFrom.Value,(double)latitudeTo.Value,(double)longitudeTo.Value,unitTypeSelect);
		}
		
		public double DistanceTo(double lat1, double lon1, double lat2, double lon2, double unitTypeSelect)
		{
			double rlat1 = Math.PI*lat1/180;
			double rlat2 = Math.PI*lat2/180;
			double theta = lon1 - lon2;
			double rtheta = Math.PI*theta/180;
			double dist = 
				Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * 
					Math.Cos(rlat2) * Math.Cos(rtheta);
			dist = Math.Acos(dist);
			dist = dist*180/Math.PI;
			dist = dist*60*1.1515;

			dist = dist*unitTypeSelect;

			return dist;
		}
		
	}
}
