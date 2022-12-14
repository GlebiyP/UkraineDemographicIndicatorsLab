using Android.Provider;

namespace UkraineDemographicIndicatorsLab
{
    public class DataContract : IBaseColumns
    {
        public const string TableName = "UADemographicIndicators";
        public const string Model = "Model";
        public const string Year = "Year";
        public const string UrbanPopulation = "UrbanPopulation";
        public const string RuralPopulation = "RuralPopulation";
        public const string Total = "Total";

        public static readonly DataModel[] Data = new DataModel[]
        {
            new DataModel("1992", 17052690, 34364740),
            new DataModel("1993", 16999700, 34317940),
            new DataModel("1994", 16924160, 34225380),
            new DataModel("1995", 16823730, 34081950),
            new DataModel("1996", 16697280, 33885180),
            new DataModel("1997", 16548090, 33641230),
            new DataModel("1998", 16383510, 33365070),
            new DataModel("1999", 16213680, 33077110),
            new DataModel("2000", 16046510, 32793570),
            new DataModel("2001", 15884190, 32518700),
            new DataModel("2002", 15697690, 32281900),
            new DataModel("2003", 15498320, 32082240),
            new DataModel("2004", 15299600, 31916370),
            new DataModel("2005", 15103970, 31788200),
            new DataModel("2006", 14931770, 31684620),
            new DataModel("2007", 14775380, 31610970),
            new DataModel("2008", 14629480, 31556950),
            new DataModel("2009", 14487120, 31506900),
            new DataModel("2010", 14380840, 31411660),
            new DataModel("2011", 14270410, 31305900),
            new DataModel("2012", 14157020, 31192310),
            new DataModel("2013", 14042200, 31073590),
            new DataModel("2014", 13928190, 30955240),
            new DataModel("2015", 13816730, 30840970),
            new DataModel("2016", 13707750, 30730870),
            new DataModel("2017", 13600410, 30622540),
            new DataModel("2018", 13487740, 30521470),
            new DataModel("2019", 13369270, 30425950),
            new DataModel("2020", 13244600, 30334630),
            new DataModel("2021", 13113810, 30247160)
        };
    }
}