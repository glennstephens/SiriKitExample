using System;
using Intents;

namespace MySiriKitDemo
{
    /// <summary>
    /// A nice friendly singleton-ish object so that the Intent (like the start workout) can be received by other
    /// objects.
    /// </summary>
    public class NewWorkoutOperationManager
    {
        public static void Notify(INStartWorkoutIntent workout)
        {
            if (OnStartWorkoutNewWorkout != null)
            {
                OnStartWorkoutNewWorkout(workout);
            }
        }

        public static Action<INStartWorkoutIntent> OnStartWorkoutNewWorkout;
    }
}