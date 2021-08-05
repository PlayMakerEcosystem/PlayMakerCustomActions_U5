namespace HutongGames.PlayMaker.Actions
{
    /// <summary>
    /// Author is Istvan Nemeth. <see cref="https://github.com/PlayMakerEcosystem/PlayMakerCustomActions_U5"/>
    /// </summary>
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip(
        "Accepts a list of fsm names, the gameobject contains the specified fsms and the corresponding bools -> fsms to be enabled or not.")]
    public class MultiFsmEnabler : FsmStateAction
    {
        [RequiredField] [Tooltip("The GameObject which contains the defined fsms.")]
        public FsmOwnerDefault FsmGameObject;

        [Tooltip("List of FSMs to be enabled/disabled.")] 
        [CompoundArray("Tag switches", "Fsm Name", "Enable/Disable")]
        public FsmString[] FsmNames;

        [Tooltip("Turn all of the defined fsms to enabled/disabled.")]
        public FsmBool[] ToBeEnabled;

        private PlayMakerFSM _fsmComponent;

        public override void Reset()
        {
            FsmGameObject = null;
            FsmNames = null;
            ToBeEnabled = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            DoEnableFsm();

            Finish();
        }

        private void DoEnableFsm()
        {
            var ownerGameObject = FsmGameObject.OwnerOption == OwnerDefaultOption.UseOwner
                ? Owner
                : FsmGameObject.GameObject.Value;
            
            if (ownerGameObject == null) return;

            var i = 0;
            foreach (var currentFsmName in FsmNames)
            {
                var fsmName = currentFsmName.Value;
                if (!string.IsNullOrEmpty(fsmName))
                {
                    var fsmComponents = ownerGameObject.GetComponents<PlayMakerFSM>();
                    foreach (var component in fsmComponents)
                    {
                        if (component.FsmName.Equals(fsmName))
                        {
                            _fsmComponent = component;
                            _fsmComponent.enabled = ToBeEnabled[i].Value;
                            break;
                        }
                    }
                }
                else
                {
                    _fsmComponent = ownerGameObject.GetComponent<PlayMakerFSM>();
                }

                
                if (_fsmComponent == null) return;

                i++;
            }
        }
    }
}