using ArincConverter.Models;

namespace ArincConverter.Helpers
{
    public static class Constants
    {
        public static class DateTimeFormat
        {
            public const string Default = "yyyy-MM-dd HH:mm:ss";
            public const string DefaultDate = "yyyy-MM-dd";
            public const string CDISC = "dd-MMM-yyyy";
            public const string Identifier = "yyMMddHHmm";
            public const string IdentifierJepp = "ddMMyyHHmm";
        }

        public static class ContentType
        {
            public static class Application
            {
                public const string Pdf = "application/pdf";
                public const string Json = "application/json";
                public const string Xml = "application/xml";
                public const string Tar = "application/x-tar";
                public const string Gzip = "application/gzip";
                public const string X7z = "application/x-7z-compressed";
                public const string Rar = "application/x-rar-compressed";
                public const string ZipCompressed = "application/x-zip-compressed";
                public const string Zip = "application/zip";
                public const string Bin = "application/octet-stream";
            }
        }

        public static class RoutePoints
        {
            public static LocationType[] DefaultAirports = new LocationType[]
            {
                LocationType.Arrival,
                LocationType.ArrivalAlternatePrimary,
                LocationType.ArrivalAlternateSecondary,
            };
        }

        public static class Document
        {
            public static class Type
            {
                public const string MET = "MET";
                public const string NOTAM = "NOTAM";
                public const string RAIM = "RAIM";
                public const string Wind = "WIND";
                public const string Track = "TRACK";
                public const string Graph = "GRAPH";
                public const string OFP = "OFP";
                public const string NOTAMD = "NOTAMD";
                public const string SIG = "SIG";
                public const string Company = "CMP";
                public const string VolcanicAsh = "VolcanicAsh";
                public const string TropicalCyclone = "TropicalCyclone";
                public const string RaimOutages = "RaimOutages";
                public const string AirportWeather = "AirportWeather";
                public const string RegionWeather = "RegionWeather";
                public const string ATCFlightPlan = "ATCFlightPlan";
                public const string ATCPlan = "ATC";
                public const string METAR = "METAR";
                public const string TAF = "TAF_FC";
                public const string UpperAirData = "UAD";
                public const string SIGMET = "SIGMET";
                public const string AIRMET = "AIRMET";
                public const string PIREP = "PIREP";
                public const string EFF = "EFF";
                public const string AdditionalDocuments = "EFBADDITIONALDOCS";
                public const string ATC = "EFBATC";
                public const string FlightLog = "EFBFlightLog";
                public const string Messages = "EFBFltMsg";
                public const string NOTAMs = "EFBNotams";
                public const string Charts = "EFBGW";
                public const string Weather = "EFBWX";
                public const string FlightPlan = "Arinc633";
                public const string NOTAMText = "Arinc633Ntm";
                public const string WeatherText = "Arinc633Wx";
                public const string NFP = "ifsnoc_integration";
            }
        }

        public static class Sabre
        {
            public static class AlternateAirport
            {
                public const string DepartureAirport = "DepartureAirport";
                public const string ArrivalAirport = "ArrivalAirport";
                public const string PrimaryArrivalAlternateAirport = "PrimaryArrivalAlternateAirport";
                public const string SecondaryArrivalAlternateAirport = "ArrivalAlternateAirport";
                public const string DepartureAlternateAirport = "DepartureAlternateAirport";
                public const string TakeOffAlternate = "TakeOffAlternate";
                public const string EnrouteAlternateAirport = "EnRouteAlternateAirport";
                public const string PrimaryContingencySavingAlternate = "PrimaryContingencySavingAlternate";
                public const string ContingencySavingAirport = "ContingencySavingAirport";
                public const string FlightInformationRegion = "FlightInformationRegion";
                public const string ETOPSAirport = "ETOPSSuitableAirport";
                public const string ETOPSAdequateAirport = "ETOPSAdequateAirport";
            }
        }
    }
}
