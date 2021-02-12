// Explanation URL:https://hutonggames.com/playmakerforum/index.php?topic=20829.0

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("You can load a scriptable object, as we've generated before, and pass the data into Playmaker, and use it here. I only use some variables contained for example purposes. Also, real actions should either get OR set, not both. Make two actions.")]
	public class GetSetScriptableObjects : FsmStateAction
	{

        // On top you define variables. Use all the other actions to see how to do this for the various types. 

        // But firsr, we want to load up the Scriptable Object we've made through ScriptableTemplate.

        public ScriptableTemplate scriptableObject; // we name the variable type and then want to get the particular object stored there.

        // Importantly, Playmaker Variables are declared like so FsmInt myInt, and when used become myInt.Value (!) 
        // let's do this for some, which correspond to the variable types in the scriptable object, but that's optional, too.

        public FsmString name;                                      // example to name it exactly as in the scriptable object, no problem.
        public FsmString someDescription;                           // but you can use other variable names if that's easier for you.
        public FsmInt healthPoints;                                 // you get the idea.
        public FsmFloat attackPower;                                    
        public ScriptableTemplate.ImpressiveCostumes myCostume;     // the system knows that ImpressiveCostumes is an enum. This acts like a path.
                                                                    // you can store enum definitions anywhere, even keep one script with all of them.
                                                                    // but you must write out the path.

        /* Notes on FsmEnum
          FsmENum also exists, but it's blanket for any enum type (can be further defined in Playmaker's variable tab).
          But because the scriptable object has specific enums, we cannot use this to SET them. But we could GET them.
          i.e. we can fill the blank FsmEnum with some specific enum and set it that way.
        */

        // All variables you use should be reset. This means that when the action runs another time, it will clear out old values first.
        // for some reason, the .Value is not used here.
        public override void Reset()
        {

            name = "";                                                     // we set it empty. 
            someDescription = "Nothing uses this for now";                 // you can default something, too.
            healthPoints = null;                                           // it's possible to set ints to null, nothing stored. 
            attackPower = null;
            myCostume = ScriptableTemplate.ImpressiveCostumes.Wizard;      // same path as above, but now we set it to some value we want.

        }

        // Code that runs on entering the state.
        public override void OnEnter()
		{
            // You could put the code inside here, but it's cleaner to make a function real quick. 
            SetScriptableExample();

            // or plug in SetScriptableExample() to write into the Scriptable Objects from Playmaker. / Not now, do this later.
            // IN A REAL SCRIPT; EITHER SET *OR* GET. This is just for demonstration.

            Finish(); // This calls the FINISH event when it's done. 

		}


        // that's how you make a simple function. Void means, it doesn't return anything, just computes. The () is empty because we do not inject arguments.
        public void GetScriptableExample()
        {
            // Easy: the variable to the left is set to the variable at the right. 
            // This is GET. So left is Playmaker, and right is Scriptable Object.

            healthPoints.Value = scriptableObject.healthPoints;    // note agian, FSM variables (here: FsmInt) need the .Value
            name.Value = scriptableObject.name;                    // try this out. Delete the .name at the end, then type . again and see the autocomplete
            someDescription.Value = scriptableObject.description;  
            attackPower.Value = scriptableObject.defensePower;     // I did this deliberately. It only cares that the target and source variable type are the same.
            myCostume = scriptableObject.favouriteCostume;         // same idea, even with enums.


        }

        // ok, the above is GETTING the variables and setting them into Playmaker. But we can also do it the other way around.
        // it's the same thing, only flipped around. Now the variable in the Scriptable Object is SET to the Playmaker variable.

        public void SetScriptableExample()
        {

            // This function is currently dormant, becasue OnEnter does not call it. To change this, simply go above and plug it into OnEnter.

            scriptableObject.name = name.Value;
            scriptableObject.healthPoints = healthPoints.Value;
            scriptableObject.description = someDescription.Value;
            scriptableObject.defensePower = attackPower.Value; // yes, that's deliberate for demonstration.
            scriptableObject.favouriteCostume = myCostume;    // no .Value, because we don't use FsmEnum here, see above.

        }

        // Looks far more complicated than it is. Once you clean out and use just what you need, it can be a short script.

         /* NOW TRY THIS:
     
         make a new scene, and add this action to the camera or something. No need for a fancy setup.
         fill out the action (create the scriptable object to store data first, see ScriptableTemplate)
         See the values FROM the scriptable object being copied over to Playmaker, where you can store them as usual.
         
         Now change GetScriptableExample() to SetScriptableExample() in the OnEnter function 
         (hint: just change the G to an S).

         It will then run the other set... function, now it will SET(store) the values FROM Playmaker TO the Scriptable Object.
         Select the Scriptable Object to see it in the inspector and hit PLAY.
         See what happens to the data.

        Now STOP playing and see what happens to the data after you stopped playing.
        You should see them being stored. That's persistent and can be loaded in a different scene again.

        */ 


    }

}
