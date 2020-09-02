using System;
using System.Collections.Generic;
using Foundation;
using Intents;
using ObjCRuntime;

namespace MySiriKitDemoIntents
{
    [Register("IntentHandler")]
    public class IntentHandler : INExtension,
        IINStartWorkoutIntentHandling,
        IINPauseWorkoutIntentHandling,
        IINResumeWorkoutIntentHandling,
        IINCancelWorkoutIntentHandling
    {
        protected IntentHandler(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override NSObject GetHandler(INIntent intent)
        {
            if (intent is INStartWorkoutIntent)
                return this;
            if (intent is INPauseWorkoutIntent)
                return this;
            if (intent is INResumeWorkoutIntent)
                return this;
            if (intent is INCancelWorkoutIntent)
                return this;

            return null;
        }

        string GetWorkoutName(INSpeakableString workout)
        {
            if (workout == null)
                return "";
            else
                return workout.ToString().ToLower();
        }

        bool IsWorkoutOK(string workoutType)
        {
            Console.WriteLine("Workout Type: " + workoutType);
            return true;
            return workoutType == "tag";
        }

        public void HandleStartWorkout(INStartWorkoutIntent intent,
            Action<INStartWorkoutIntentResponse> completion)
        {
            INStartWorkoutIntentResponse response;

            var workoutName = GetWorkoutName(intent.WorkoutName);

            // Only allow tag workouts
            if (IsWorkoutOK(workoutName))
            {
                response = new INStartWorkoutIntentResponse(
                INStartWorkoutIntentResponseCode.HandleInApp,
                    new NSUserActivity("INStartWorkoutIntent"));
            } else
            {
                response = new INStartWorkoutIntentResponse(
                INStartWorkoutIntentResponseCode.FailureNoMatchingWorkout, null);
            }

            completion(response);
        }

        public void HandlePauseWorkout(INPauseWorkoutIntent intent,
            Action<INPauseWorkoutIntentResponse> completion)
        {
            INPauseWorkoutIntentResponse response;

            var workoutName = GetWorkoutName(intent.WorkoutName);

            if (IsWorkoutOK(workoutName))
            {
                response = new INPauseWorkoutIntentResponse(
                    INPauseWorkoutIntentResponseCode.ContinueInApp,
                    new NSUserActivity("INPauseWorkoutIntent"));
            }
            else
            {
                response = new INPauseWorkoutIntentResponse(
                    INPauseWorkoutIntentResponseCode.FailureNoMatchingWorkout, null);
            }

            completion(response);
        }

        public void HandleCancelWorkout(INCancelWorkoutIntent intent,
            Action<INCancelWorkoutIntentResponse> completion)
        {
            INCancelWorkoutIntentResponse response;

            var workoutName = GetWorkoutName(intent.WorkoutName);

            if (IsWorkoutOK(workoutName))
            {
                response = new INCancelWorkoutIntentResponse(
                    INCancelWorkoutIntentResponseCode.ContinueInApp,
                    new NSUserActivity("INCancelWorkoutIntent"));
            }
            else
            {
                response = new INCancelWorkoutIntentResponse(
                    INCancelWorkoutIntentResponseCode.FailureNoMatchingWorkout, null);
            }

            completion(response);
        }

        public void HandleResumeWorkout(INResumeWorkoutIntent intent,
            Action<INResumeWorkoutIntentResponse> completion)
        {
            INResumeWorkoutIntentResponse response;

            var workoutName = GetWorkoutName(intent.WorkoutName);

            if (IsWorkoutOK(workoutName))
            {
                response = new INResumeWorkoutIntentResponse(
                    INResumeWorkoutIntentResponseCode.ContinueInApp,
                    new NSUserActivity("INResumeWorkoutIntent"));
            }
            else
            {
                response = new INResumeWorkoutIntentResponse(
                    INResumeWorkoutIntentResponseCode.FailureNoMatchingWorkout, null);
            }

            completion(response);
        }
    }
}
