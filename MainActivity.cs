using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace UkraineDemographicIndicatorsLab
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Button dbButton = FindViewById<Button>(Resource.Id.dbButton);
            Button contactsButton = FindViewById<Button>(Resource.Id.contactsButton);
            Button geoServicesButton = FindViewById<Button>(Resource.Id.geoServicesButton);
            Button aboutAuthorButton = FindViewById<Button>(Resource.Id.aboutAuthorButton);

            dbButton.Click += (sender, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(DBActivity));
                StartActivity(nextActivity);
            };

            contactsButton.Click += (sender, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(ContactsAvtivity));
                StartActivity(nextActivity);
            };

            /*geoServicesButton.Click += (sender, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(GeoservicesActivity));
                StartActivity(nextActivity);
            };*/

            aboutAuthorButton.Click += (sender, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(AboutAuthorActivity));
                StartActivity(nextActivity);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}