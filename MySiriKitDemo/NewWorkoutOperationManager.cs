using System;
using Intents;

namespace MySiriKitDemo
{
    /// <summary>
    /// A nice friendly singleton-ish object so that the Intent (like the start workout) can be received by other
    /// objects.
    /// </summary>
    public class NewSiriWorkoutOperationManager
    {
        // Start workout intent
        public static void NotifyStartWorkout(INStartWorkoutIntent workout)
        {
            if (OnStartNewWorkout != null)
            {
                OnStartNewWorkout(workout);
            }
        }

        public static Action<INStartWorkoutIntent> OnStartNewWorkout;

        // Pause workout intent
        public static void NotifyPauseWorkout(INPauseWorkoutIntent workout)
        {
            if (OnPauseWorkout != null)
            {
                OnPauseWorkout(workout);
            }
        }

        public static Action<INPauseWorkoutIntent> OnPauseWorkout;

        // Resume workout intent
        public static void NotifyResumeWorkout(INResumeWorkoutIntent workout)
        {
            if (OnResumeWorkout != null)
            {
                OnResumeWorkout(workout);
            }
        }

        public static Action<INResumeWorkoutIntent> OnResumeWorkout;

        // Cancel workout intent
        public static void NotifyCancelWorkout(INCancelWorkoutIntent workout)
        {
            if (OnCancelWorkout != null)
            {
                OnCancelWorkout(workout);
            }
        }

        public static Action<INCancelWorkoutIntent> OnCancelWorkout;
    }
}