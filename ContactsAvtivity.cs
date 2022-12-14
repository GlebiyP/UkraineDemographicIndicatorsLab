using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using System;
using System.Collections.Generic;

namespace UkraineDemographicIndicatorsLab
{
    [Activity(Label = "@string/contacts")]
    public class ContactsAvtivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.contacts_activity);

            Button goBackButton = FindViewById<Button>(Resource.Id.goBackButton2);

            goBackButton.Click += (sender, e) =>
            {
                this.Finish();
            };

            CheckPermission("android.permission.READ_CONTACTS", 100);

            GetContacts();
        }

        private void GetContacts()
        {
            var whereNameParams = new String[] { ContactsContract.CommonDataKinds.StructuredPostal.ContentItemType };
            var whereQuery = ContactsContract.Data.InterfaceConsts.Mimetype + " = ? AND " + ContactsContract.CommonDataKinds.StructuredPostal.FormattedAddress + " LIKE '%Kyiv%'";
            var cursor = ContentResolver.Query(ContactsContract.Data.ContentUri, new string[] { ContactsContract.CommonDataKinds.StructuredName.DisplayName }, whereQuery, whereNameParams, null);
            var contacts = new List<string>();

            while (cursor.MoveToNext())
            {
                var name = cursor.GetString(cursor.GetColumnIndex(ContactsContract.CommonDataKinds.StructuredName.DisplayName));
                contacts.Add(name);
            }

            var list = FindViewById<ListView>(Resource.Id.contacts_list);
            var arrayAdapter = new ArrayAdapter<string>(this, Resource.Layout.listView_activity,
                Resource.Id.listTextView, contacts);
            list.Adapter = arrayAdapter;
        }

        public void CheckPermission(String permission, int requestCode)
        {
            if (ContextCompat.CheckSelfPermission(this, permission) == Android.Content.PM.Permission.Denied)
            {
                ActivityCompat.RequestPermissions(this, new String[] { permission }, requestCode);
            }
            else
            {
                Toast.MakeText(this, "Permission already granted", ToastLength.Short).Show();
            }
        }
    }
}