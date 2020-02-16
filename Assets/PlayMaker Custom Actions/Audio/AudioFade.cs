// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Made By : DjayDino
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Audio)]
    [Tooltip("Fades out the Volume of the Audio Clip played by the AudioSource component on a Game Object.")]
    public class AudioFade : ComponentAction<AudioSource>
    {
        [RequiredField]
        [CheckForComponent(typeof(AudioSource))]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [HasFloatSlider(0, 1)]
        [Tooltip("The volume to reach")]
        public FsmFloat fadeTo;

        [RequiredField]
        [HasFloatSlider(0, 10)]
        [Tooltip("Fade in time in seconds.")]
        public FsmFloat time;

        [Tooltip("Interpolation mode: Linear or EaseInOut.")]
        public InterpolationType mode;

        [Tooltip("Event to send when finished.")]
        public FsmEvent finishEvent;

        [Tooltip("Ignore TimeScale. Useful if the game is paused.")]
        public bool realTime;

        private float startTime;
        private float currentTime;


        // for the linear equation;
        private float startingVolume;
        public override void Reset()
        {
            fadeTo = 1f;
            gameObject = null;
            time = 1.0f;
            finishEvent = null;

        }

        public override void OnEnter()
        {

            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                startingVolume = audio.volume;
            }

            startTime = FsmTime.RealtimeSinceStartup;
            currentTime = 0f;

        }

        public override void OnUpdate()
        {
            // update time

            if (realTime)
            {
                currentTime = FsmTime.RealtimeSinceStartup - startTime;
            }
            else
            {
                currentTime += Time.deltaTime;
            }

            var lerpTime = currentTime / time.Value;

            switch (mode)
            {

                case InterpolationType.Linear:

                    audio.volume = Mathf.Lerp(startingVolume, fadeTo.Value, lerpTime);
                    Debug.Log(audio.volume);

                    break;

                case InterpolationType.EaseInOut:

                    audio.volume = Mathf.SmoothStep(startingVolume, fadeTo.Value, lerpTime);

                    break;
            }

            if (lerpTime > 1)
            {
                if (finishEvent != null)
                {
                    Fsm.Event(finishEvent);
                }

                Finish();
            }
        }
    }
}
