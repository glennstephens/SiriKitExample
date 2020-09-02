using Foundation;
using UIKit;
using Intents;

namespace MySiriKitDemo
{
    public partial class ViewController : UIViewController
    {
        public ViewController() : base()
        {
        }

        UILabel details;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            details = new UILabel(this.View.Frame);
            details.TextAlignment = UITextAlignment.Center;
            details.TextColor = UIColor.Black;
            details.LineBreakMode = UILineBreakMode.WordWrap;

            details.Text = "Say 'start a tag workout'";

            Add(details);

            NewSiriWorkoutOperationManager.OnStartNewWorkout += StartANewWorkout;
            NewSiriWorkoutOperationManager.OnPauseWorkout += PauseTheWorkout;
            NewSiriWorkoutOperationManager.OnResumeWorkout += ResumeTheWorkout;
            NewSiriWorkoutOperationManager.OnCancelWorkout += CancelTheWorkout;
        }

        private void StartANewWorkout(INStartWorkoutIntent workout)
        {
            bool hasAGoal = workout.GoalValue == null;

            if (hasAGoal)
            {
                details.Text = "Doing a " + workout.WorkoutName + " workout";
            } else
            {
                details.Text = "Doing a " + workout.WorkoutName + " workout for " + workout.GoalValue + " seconds";
            }
        }

        void PauseTheWorkout(INPauseWorkoutIntent intent)
        {
            details.Text = "Paused workout";
        }

        void ResumeTheWorkout(INResumeWorkoutIntent intent)
        {
            details.Text = "Resumed workout";
        }

        void CancelTheWorkout(INCancelWorkoutIntent intent)
        {
            details.Text = "Workout cancelled";
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}