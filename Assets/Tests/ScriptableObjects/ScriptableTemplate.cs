// Explanation URL:https://hutonggames.com/playmakerforum/index.php?topic=20829.0


using UnityEngine; // When adding special code snippets you may need to add something here too. If greyed out, you can delete the entry.

// comments can be deleted without consequence.

/// <summary>
/// Write three slashes and you get this, for longer comments.
/// </summary>

// this is for naming the object, and here you can define the menu structure (just try it out).
[CreateAssetMenu(fileName = "New ScriptableObject", menuName = "MyScriptables/ScriptableObjectExample")]

// the class name must be the same as the file name, e.g. class <name> must be the same as <name>.cs 
public class ScriptableTemplate : ScriptableObject
{

    [Header("Header")]                              // adds a headline, optional
    public new string name = "some name";           // 'name' string is a special case, because it exists internally has the 'new' keyword.
    public string description;                      // standard string, and would be NULL. You can also set it empty by writing = "";

    [Space]                                         // optional, to add spacing in the inspector.

    [Tooltip("My tooltip")]                         // optional, just goes above the variable you want to have a tooltip for.
    public int healthPoints = 12;                   // this is how you write variables with a default value.
                                                    // spacing things out here is irrelevant, but makes it more readible here.
    [Header("New Section")]                         
    public float attackPower = 1f;                  // the f at the end indicates clearly that this is a float value.
    [Range(0f, 1f)]                                 // adds a slider, with minimum and maximum value. You can also use it on ints etc.
    public float defensePower = 0.33f;                    
    [Space]
    private int _secretNumber = 23;                 // will not show up & cannot be accessed from outside the script, but still there for computation.
    int _anotherSecretNumber = 42;                  // when not stated, it assumes private. The underscore is a common convention for private variables.
    [SerializeField] int stealth;                   // this also cannot be accessed from outside the script, but can be configured in the inspector.

    public enum ImpressiveCostumes                   // here we define a dropdown list of values (called Enum).
    {                                               // Also see Playmaker > Addons > Tools > Enum Creator Wizard.
        Warrior,
        CombatOrthopaedist,
        Wizard, Sorcerer, Mage,                  // code doesn't care about line breaks or spaces here. Separate enums with commas.
        Janitor = 99,                               // enums are internally treated as ints, and assigned by order (Warrior is 0, CombatOrthopaedist is 1 etc).
        Daughter = 0,                               // if you want to ensure they always get assigned the same value, set an int value like this.  Generally do this.
                                                    // Important: it first assignes 0 to warrior, and later also sets Daugther to 0. In a real script, assign ints to all entries.
    }

    public ImpressiveCostumes favouriteCostume;     // you see right above how we invented this variable type. This makes the actual field to say WHICH of the costumes we picked.

    /* typically this would be 'public ImpressiveCostume impressiveCostume' -- case sensitive! You might see e.g. Rigidbody rigidbody etc.
    this is confusing for newbies, but makes sense, as you know then that myObscureEnum is holding a variable from MyObscureEnum (the enum list).
    but in this case, we use a more clearly different variable name. */



}

/* HOW TO TEST THIS:
Okay, now you made a "template" of what your scriptable object contains. Now you need to actualy create one such object.

STEP 1: In your project folder, right-click, and you should see the entry in the big context menu that you defined above.

STEP 2: Select the newly created object and fill in some values.

STEP 3: Make a new scene, add an FSM to the camera, and add the action GetSetScriptableObject that came with this script.

STEP 4: you need to plug in the scriptable object you've made in step 1. 


Later on, make your own scriptable object types. And create as many scriptable objects from as you need. 
For example, you can define different characters, or even just individual variables (e.g. a custom int variable type,
or more specific, a custom health point type)

The cool thing is that you can store and read those values across scenes, and also nest them into each other, e.g.
the Character object contains a slot for character stats etc. or health point scriptable objects etc.

It's done exactly as it's done in the example action. 
public ScriptName variableName, in this case e.g. 'public ScriptableTemplate slotToPlugIt;' 


 Enjoy. */


