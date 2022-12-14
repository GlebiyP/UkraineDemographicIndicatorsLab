using Android.Content;
using System.Collections.Generic;
using Android.Database.Sqlite;
using Android.Provider;

namespace UkraineDemographicIndicatorsLab
{
    public class Database : SQLiteOpenHelper
    {
        private const int _databaseVersion = 1;
        private const string _databaseName = "UADemographicIndicators.db";
        private const string _tableCreationScript = "CREATE TABLE " + DataContract.TableName + " (" +
            IBaseColumns.Id + " INTEGER PRIMARY KEY, " +
            DataContract.Year + " TEXT, " +
            DataContract.UrbanPopulation + " REAL, " +
            DataContract.RuralPopulation + " REAL, " +
            DataContract.Total + " REAL)";
        private const string _tableDeletionScript = "DROP TABLE IF EXISTS " + DataContract.TableName;
        /*private const string _getYearsByPopDelta = "SELECT p1.Year as year FROM " + DataEx.TableName + 
            " as p1 JOIN " + DataEx.TableName + " as p2 ON p1.Year = p2.Year + 1 WHERE " +
            "((ABS (p1.Total - p2.Total) * 1.0)) / (p2.Total * 1.0) * 100 < ?)";*/

        private const string _getMaxChangeYear = "SELECT year FROM (SELECT p1.Year as year, " +
            "ABS(p1.Total - p2.Total) as delta FROM " + DataContract.TableName + " as p1 JOIN " + DataContract.TableName +
            " as p2 WHERE p1.Year = p2.Year + 1 ORDER BY delta DESC) LIMIT 1";

        public Database(Context context)
            : base(context, _databaseName, null, _databaseVersion)
        {

        }

        public List<DataModel> GetAllData()
        {
            var projection = new string[]
            {
                IBaseColumns.Id,
                DataContract.Year,
                DataContract.UrbanPopulation,
                DataContract.RuralPopulation,
                DataContract.Total,
            };

            var cursor = ReadableDatabase.Query(
                DataContract.TableName,
                projection,
                null,
                null,
                null,
                null,
                null);

            var allData = new List<DataModel>();

            while (cursor.MoveToNext())
            {
                allData.Add(new DataModel
                {
                    Id = cursor.GetInt(cursor.GetColumnIndexOrThrow(IBaseColumns.Id)),
                    Year = cursor.GetString(cursor.GetColumnIndexOrThrow(DataContract.Year)),
                    UrbanPopulation = cursor.GetInt(cursor.GetColumnIndexOrThrow(DataContract.UrbanPopulation)),
                    RuralPopulation = cursor.GetInt(cursor.GetColumnIndexOrThrow(DataContract.RuralPopulation)),
                    Total = cursor.GetDouble(cursor.GetColumnIndexOrThrow(DataContract.Total)),
                });
            }

            cursor.Close();
            return allData;
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(_tableCreationScript);

            foreach (var d in DataContract.Data)
            {
                var values = new ContentValues();
                values.Put(DataContract.Year, d.Year);
                values.Put(DataContract.UrbanPopulation, d.UrbanPopulation);
                values.Put(DataContract.RuralPopulation, d.RuralPopulation);
                values.Put(DataContract.Total, d.Total);
                db.Insert(DataContract.TableName, null, values);
            }
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL(_tableDeletionScript);
            OnCreate(db);
        }

        public override void OnDowngrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            OnUpgrade(db, oldVersion, newVersion);
        }

        public List<string> GetYearsByPopDelta(double perc)
        {
            string _getYearsByPopDelta = "SELECT p1.Year as year FROM " + DataContract.TableName +
            " as p1 JOIN " + DataContract.TableName + " as p2 ON p1.Year = p2.Year + 1 WHERE " +
            $"((ABS (p1.Total - p2.Total) * 1.0) / (p2.Total * 1.0) * 100 < {perc})";

            var years = new List<string>();

            var cursor = ReadableDatabase.RawQuery(_getYearsByPopDelta, null);

            while (cursor.MoveToNext())
            {
                years.Add(cursor.GetString(cursor.GetColumnIndexOrThrow("year")));
            }

            cursor.Close();
            return years;
        }

        public string GetMaxChangeYear()
        {
            string res = "";

            var cursor = ReadableDatabase.RawQuery(_getMaxChangeYear, null);

            while (cursor.MoveToNext())
            {
                res = cursor.GetString(cursor.GetColumnIndexOrThrow("year"));
            }

            cursor.Close();
            return res;
        }
    }
}