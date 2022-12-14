using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace UkraineDemographicIndicatorsLab
{
    [Activity(Label = "@string/demographic_indicators", Theme = "@style/AppTheme")]
    public class DBActivity : Activity
    {
        private static Database _database;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.db_activity);

            Button getYearsButton = FindViewById<Button>(Resource.Id.getYearsButton);
            Button getMaxChangeYearButton = FindViewById<Button>(Resource.Id.getMaxChangeYearButton);
            Button goBackButton = FindViewById<Button>(Resource.Id.goBackButton1);

            InitializeDatabase();
            DisplayData();

            getYearsButton.Click += (sender, e) =>
            {
                var yearsList = _database.GetYearsByPopDelta(0.5);
                string years = "";

                if (yearsList.Count != 0)
                {
                    foreach (var y in yearsList)
                    {
                        years += y + " ";
                    }
                }
                else
                    years = "Немає такіх років.";

                Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(this);
                alertDialog.SetTitle("Результат");
                alertDialog.SetMessage(years);
                alertDialog.SetNeutralButton("OK", delegate
                {
                    alertDialog.Dispose();
                });
                alertDialog.Show();
            };

            getMaxChangeYearButton.Click += (sender, e) =>
            {
                var year = _database.GetMaxChangeYear();

                Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(this);
                alertDialog.SetTitle("Результат");
                alertDialog.SetMessage(year);
                alertDialog.SetNeutralButton("OK", delegate
                {
                    alertDialog.Dispose();
                });
                alertDialog.Show();
            };

            goBackButton.Click += (sender, e) =>
            {
                this.Finish();
            };
        }

        private void InitializeDatabase()
        {
            _database = new Database(this);
        }

        private void DisplayData()
        {
            var table = FindViewById<TableLayout>(Resource.Id.tableLayout1);
            var addedData = _database.GetAllData();

            //Add data
            foreach (var model in addedData)
            {
                //New row
                TableRow newRow = new TableRow(this);
                newRow.SetGravity(GravityFlags.CenterHorizontal);

                //Set cells
                TextView yearView = new TextView(this);
                yearView.Text = model.Year;
                yearView.Gravity = GravityFlags.Center;
                yearView.SetTypeface(Typeface.Serif, TypefaceStyle.Normal);

                TextView urbanView = new TextView(this);
                urbanView.Text = model.UrbanPopulation.ToString();
                urbanView.Gravity = GravityFlags.Center;
                urbanView.SetTypeface(Typeface.Serif, TypefaceStyle.Normal);

                TextView ruralView = new TextView(this);
                ruralView.Text = model.RuralPopulation.ToString();
                ruralView.Gravity = GravityFlags.Center;
                ruralView.SetTypeface(Typeface.Serif, TypefaceStyle.Normal);

                TextView totalView = new TextView(this);
                totalView.Text = model.Total.ToString();
                totalView.Gravity = GravityFlags.Center;
                totalView.SetTypeface(Typeface.Serif, TypefaceStyle.Normal);

                //Add cells to row
                newRow.AddView(yearView);
                newRow.AddView(urbanView);
                newRow.AddView(ruralView);
                newRow.AddView(totalView);

                //Add row to table
                table.AddView(newRow);
            }
        }
    }
}