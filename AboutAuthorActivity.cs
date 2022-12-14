using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UkraineDemographicIndicatorsLab
{
    [Activity(Label = "@string/aboutAuthor")]
    public class AboutAuthorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.about_author_activity);

            TextView authorInfo = FindViewById<TextView>(Resource.Id.authorInfo);
            ImageView photo = FindViewById<ImageView>(Resource.Id.photo);
            Button goBackButton = FindViewById<Button>(Resource.Id.goBackButton4);

            goBackButton.Click += (sender, e) =>
            {
                this.Finish();
            };
        }
    }
}