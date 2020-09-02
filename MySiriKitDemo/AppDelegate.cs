using Foundation;
using UIKit;
using Intents;
using System;
using System.Threading;
using UserNotifications;
using ObjCRuntime;

namespace MySiriKitDemo
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            this.Window = new UIWindow(UIScreen.MainScreen.Bounds);
            this.Window.RootViewController = new ViewController();
            this.Window.MakeKeyAndVisible();

            // Request access to Siri
            INPreferences.RequestSiriAuthorization((INSiriAuthorizationStatus status) => {
                // Respond to returned status
                switch (status)
                {
                    case INSiriAuthorizationStatus.Authorized:
                        SetupVocabulary();
                        break;
                    case INSiriAuthorizationStatus.Denied:
                        break;
                    case INSiriAuthorizationStatus.NotDetermined:
                        break;
                    case INSiriAuthorizationStatus.Restricted:
                        break;
                }
            });

            return true;
        }

        public NSOrderedSet<NSString> GetCustomWorkoutNames()
        {
            var workoutNames = new NSMutableOrderedSet<NSString>();

            foreach (string workoutName in new string[] { "Tag", "Jumping", "Hoops" })
            {
                workoutNames.Add(new NSString(workoutName));
            }

            return new NSOrderedSet<NSString>(workoutNames.AsSet());
        }

        void SetupVocabulary()
        {
            // Clear any existing vocabulary
            INVocabulary.SharedVocabulary.RemoveAllVocabularyStrings();

            // Register new vocabulary
            INVocabulary.SharedVocabulary.SetVocabularyStrings(
                GetCustomWorkoutNames(), INVocabularyStringType.WorkoutActivityName);
        }

        void DoStartWorkout(INStartWorkoutIntent intent)
        {
            // Notify the app that it is there
            NewSiriWorkoutOperationManager.NotifyStartWorkout(intent);
        }

        void DoPauseWorkout(INPauseWorkoutIntent intent)
        {
            NewSiriWorkoutOperationManager.NotifyPauseWorkout(intent);
        }

        void DoCancelWorkout(INCancelWorkoutIntent intent)
        {
            NewSiriWorkoutOperationManager.NotifyCancelWorkout(intent);
        }

        void DoResumeWorkout(INResumeWorkoutIntent intent)
        {
            NewSiriWorkoutOperationManager.NotifyResumeWorkout(intent);
        }

        public override bool ContinueUserActivity(UIApplication application,
            NSUserActivity userActivity,
            UIApplicationRestorationHandler completionHandler)
        {
            // Get the intent in the right format
            var intent = userActivity.GetInteraction().Intent;
            if (intent is INStartWorkoutIntent)
            {
                DoStartWorkout(userActivity.GetInteraction().Intent as INStartWorkoutIntent);
            } else if (intent is INPauseWorkoutIntent)
            {
                DoPauseWorkout(userActivity.GetInteraction().Intent as INPauseWorkoutIntent);
            } else if (intent is INResumeWorkoutIntent)
            {
                DoResumeWorkout(userActivity.GetInteraction().Intent as INResumeWorkoutIntent);
            } else if (intent is INCancelWorkoutIntent)
            {
                DoCancelWorkout(userActivity.GetInteraction().Intent as INCancelWorkoutIntent);
            }

            // Don't forget the completion handler (bad things happen otherwise)
            completionHandler(new NSObject[] { });

            return true;
        }
    }
}

