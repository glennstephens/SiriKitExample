using System;
using System.Collections.Generic;
using Foundation;
using Intents;
using ObjCRuntime;

namespace MySiriKitDemoIntents
{
    [Register("IntentHandler")]
    public class IntentHandler : INExtension, IINStartWorkoutIntentHandling
    {
        protected IntentHandler(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override NSObject GetHandler(INIntent intent)
        {
            if (intent is INStartWorkoutIntent)
                return this;

            return null;
        }

        public void HandleStartWorkout(INStartWorkoutIntent intent,
            Action<INStartWorkoutIntentResponse> completion)
        {
            INStartWorkoutIntentResponse response;

            var workoutName = intent.WorkoutName.ToString().ToLower();

            // Only allow tag workouts
            var workoutOK = workoutName == "tag";
            if (workoutOK)
            {
                response = new INStartWorkoutIntentResponse(
                INStartWorkoutIntentResponseCode.ContinueInApp, new NSUserActivity("INStartWorkoutIntent"));
            } else
            {
                response = new INStartWorkoutIntentResponse(
                INStartWorkoutIntentResponseCode.FailureNoMatchingWorkout, null);
            }

            completion(response);
        }
    }
}
