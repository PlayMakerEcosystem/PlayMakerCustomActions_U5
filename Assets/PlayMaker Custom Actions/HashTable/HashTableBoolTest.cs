//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerHashTableProxy script onto a GameObject, and define a unique name for reference if several PlayMakerHashTableProxy coexists on that GameObject.
// In this Action interface, link that GameObject in "hashTableObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ArrayMaker/HashTable")]
    [Tooltip("Add int to a hashtable Variable (PlayMakerHashTableProxy)")]
    public class HashTableBoolTest : HashTableActions
    {

        [ActionSection("Set up")]

        [RequiredField]
        [Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
        [CheckForComponent(typeof(PlayMakerHashTableProxy))]
        public FsmOwnerDefault gameObject;

        [Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
        public FsmString reference;


        [RequiredField]
        [UIHint(UIHint.FsmString)]
        [Tooltip("The Key value for that hash set")]
        public FsmString key;

        [ActionSection("Data")]

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("The event to trigger when key is not found")]
        public FsmEvent KeyNotFoundEvent;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("The event to trigger when true")]
        public FsmEvent isTrue;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("The event to trigger when false")]
        public FsmEvent isFalse;

        FsmVar variable;

        public override void Reset()
        {
            gameObject = null;
            reference = null;
            key = null;
            variable = null;
        }


        public override void OnEnter()
        {
            if (SetUpHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
            {
                Get();
            }

            Finish();
        }

        public void Get()
        {

            if (!isProxyValid())
            {
                return;
            }

            if (!proxy.hashTable.ContainsKey(key.Value))
            {
                Fsm.Event(KeyNotFoundEvent);
                return;
            }

            Fsm.Event((bool)proxy.hashTable[key.Value] ? isTrue : isFalse);
        }
    }
}