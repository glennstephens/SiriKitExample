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

        public override bool ContinueUserActivity(UIApplication application,
            NSUserActivity userActivity,
            UIApplicationRestorationHandler completionHandler)
        {
            // Get the intent in the right format
            var workoutIntent = userActivity.GetInteraction().Intent as INStartWorkoutIntent;

            // Notify the app that it is there
            NewWorkoutOperationManager.Notify(workoutIntent);

            // Don't forget the completion handler (bad things happen otherwise)
            completionHandler(new NSObject[] { });

            return true;
        }
    }
}

