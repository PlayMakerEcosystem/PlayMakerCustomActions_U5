// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using System.Globalization;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Adds seconds to date and time and stores it in a string variable. An optional format string gives you a lot of control over the formatting (see online docs for format syntax).")]
	public class AddToDateTime : FsmStateAction
	{
	
        [RequiredField]
        [Tooltip("The time and date to add seconds to, following the format described in 'dateFormat'")]
        public FsmString startDate;

		[Tooltip("format string. E.g., MM/dd/yyyy HH:mm:ss")]
		public FsmString dateFormat;
		
        [RequiredField]
        [Tooltip("Integer seconds to add to date.")]
        public FsmInt add;
		
		[ActionSection("Result")]
        [UIHint(UIHint.Variable)]
		[Tooltip("Store result date and time as a string.")]
		public FsmString storeString;
		
		CultureInfo provider = CultureInfo.InvariantCulture;
		
		public override void Reset()
		{
            startDate = null;
            storeString = null;
            dateFormat = "MM/dd/yyyy HH:mm:ss";
            add = null;
		}

		public override void OnEnter()
		{
            
            DateTime _startDate = DateTime.ParseExact(startDate.Value, dateFormat.Value, provider);

            _startDate = _startDate.Add(System.TimeSpan.FromSeconds((int)add.Value));

            storeString.Value = _startDate.ToString(dateFormat.Value);
			
			Finish();
      	}

#if UNITY_EDITOR
	    public override string AutoName()
	    {
            return ActionHelpers.AutoName(this, startDate, add, storeString, dateFormat);
	    }
#endif

	}
}