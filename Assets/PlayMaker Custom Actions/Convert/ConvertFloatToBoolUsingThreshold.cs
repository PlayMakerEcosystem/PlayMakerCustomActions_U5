// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Original action by thore: https://hutonggames.com/playmakerforum/index.php?topic=20864.msg32820;topicseen#new


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Converts a float and given threshold values, set a bool as a result.")]
    public class ConvertFloatToBoolUsingThreshold : FsmStateAction
	{
        [ActionSection("Input")]
        [Tooltip("The float to use as an input.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat floatValue;

        [ActionSection("Threshold")]
        [Tooltip("Threshold minimum.")]
        public FsmFloat thresholdMin;
        [Tooltip("Threshold maximum.")]
        public FsmFloat thresholdMax;

        public enum ThresholdRule  {  DoNotChange, SetFalse, SetTrue, FlipBool }

        [ActionSection("Rules")]
        [Tooltip("What happens to the bool result when float is within threshold?")]
        public ThresholdRule setWhenInThreshold;
        [Tooltip("What happens to the bool result when float is larger than threshold?")]
        public ThresholdRule setWhenLarger;
        [Tooltip("What happens to the bool result when float is smaller than threshold?")]
        public ThresholdRule setWhenSmaller;

        [Tooltip("What happens to the bool result when float is equal to the min threshold?")]
        public ThresholdRule setWhenEqualToMin;
        
        [Tooltip("What happens to the bool result when float is equal to the max threshold?")]
        public ThresholdRule setWhenEqualToMax;
        
        [ActionSection("Result")]
        [Tooltip("To store the resulting bool variable.")]
        [UIHint(UIHint.Variable)]
        public FsmBool storeBool;

        public bool everyFrame;

        public override void Reset()
        {
            floatValue = 0;
            thresholdMin = 0;
            thresholdMax = 0;
            setWhenInThreshold = ThresholdRule.DoNotChange;
            setWhenLarger = ThresholdRule.DoNotChange;
            setWhenSmaller = ThresholdRule.DoNotChange;
            setWhenEqualToMin = ThresholdRule.DoNotChange;
            setWhenEqualToMax = ThresholdRule.DoNotChange;
            storeBool = null;
            everyFrame = false;
        }

        public override void OnEnter()
		{
            DoFloatToBool();

            if (!everyFrame)
            {
                Finish();
            }
        }

		public override void OnUpdate()
        {
            DoFloatToBool();
            
        }

        public void DoFloatToBool()
        {
            if (floatValue.Value > thresholdMax.Value)
            {

                if (setWhenLarger == ThresholdRule.SetFalse)
                    storeBool.Value = false;

                if (setWhenLarger == ThresholdRule.SetTrue)
                    storeBool.Value = true;

                if (setWhenLarger == ThresholdRule.FlipBool)
                    storeBool.Value = !storeBool.Value;

            }

            if (floatValue.Value < thresholdMin.Value)
            {

                if (setWhenSmaller == ThresholdRule.SetFalse)
                    storeBool.Value = false;

                if (setWhenSmaller == ThresholdRule.SetTrue)
                    storeBool.Value = true;

                if (setWhenSmaller == ThresholdRule.FlipBool)
                    storeBool.Value = !storeBool.Value;

            }

            if (floatValue.Value > thresholdMin.Value && floatValue.Value < thresholdMax.Value)
            {

                if (setWhenInThreshold == ThresholdRule.SetFalse)
                    storeBool.Value = false;

                if (setWhenInThreshold == ThresholdRule.SetTrue)
                    storeBool.Value = true;

                if (setWhenInThreshold == ThresholdRule.FlipBool)
                    storeBool.Value = !storeBool.Value;

            }

            if (floatValue.Value == thresholdMin.Value)
            {
                if (setWhenEqualToMin == ThresholdRule.SetFalse)
                    storeBool.Value = false;

                if (setWhenEqualToMin == ThresholdRule.SetTrue)
                    storeBool.Value = true;

                if (setWhenEqualToMin == ThresholdRule.FlipBool)
                    storeBool.Value = !storeBool.Value;
            }
            
            if (floatValue.Value == thresholdMax.Value)
            {
                if (setWhenEqualToMax == ThresholdRule.SetFalse)
                    storeBool.Value = false;

                if (setWhenEqualToMax == ThresholdRule.SetTrue)
                    storeBool.Value = true;

                if (setWhenEqualToMax == ThresholdRule.FlipBool)
                    storeBool.Value = !storeBool.Value;
            }
        }
    }
}