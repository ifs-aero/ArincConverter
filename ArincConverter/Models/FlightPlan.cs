using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ArincConverter.Models
{
    public class FlightPlan
    {
        public int? Id { get; set; }

        public int AirlineId { get; set; } = 6020;

        /// <example>EXAMPLE-LFPG-KFJK</example>
        public string ThirdPartyScheduleId { get; set; }

        /// <example>XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX</example>
        public string ThirdPartyPlanId { get; set; }

        /// <example>IF-TEST</example>
        public string AircraftRegistration { get; set; }

        /// <example>738</example>
        public string AircraftType { get; set; }

        /// <example>IFS968-DEP-ARR</example>
        public string FlightLogId { get; set; }

        public bool IsPartialFlightPlan { get; set; }

        /// <example>IFS968</example>
        public string CallSign { get; set; }

        /// <example>IFS447</example>
        [StringLength(8, MinimumLength = 3)]
        public string CommercialFlightNumber { get; set; }

        public DateTime? ScheduledTimeArrival { get; set; }

        public DateTime? ScheduledTimeDeparture { get; set; }

        public DateTime? LastEditDate { get; set; }

        public int Passengers { get; set; }

        public double MaximumTakeOffMass { get; set; }

        public double Cargo { get; set; }

        public double DryOperatingWeight { get; set; }

        public double Load { get; set; }

        public double ZeroFuelWeight { get; set; }

        public double TakeoffWeight { get; set; }

        public double LandingWeight { get; set; }

        public double MaxLandingWeightOperational { get; set; }

        public double MaxLandingWeightStructural { get; set; }

        public double MaxTakeOffWeightOperational { get; set; }

        public double MaxTakeOffWeightStructural { get; set; }

        public double MaxZeroFuelWeightOperational { get; set; }

        public double MaxZeroFuelWeightStructural { get; set; }

        public TimeSpan? EstimatedElapsedTime { get; set; }

        public TimeSpan? Endurance { get; set; }

        public TimeSpan? CalculatedTakeOffTime { get; set; }

        public bool IsNATTrack { get; set; }

        public double TotalFuel { get; set; }

        public double TaxiFuel { get; set; }

        public double TripFuel { get; set; }

        public double ContingencyFuel { get; set; }

        public double FinalReserveFuel { get; set; }

        public double ETOPSFuel { get; set; }

        public double ExtraFuel { get; set; }

        public double AdditionalFuel { get; set; }

        public double TakeOffFuel { get; set; }

        public double ArrivalFuel { get; set; }

        public double MinimumFuelOnBoard { get; set; }

        public double PrimaryAlternateFuel { get; set; }

        public double SecondaryAlternateFuel { get; set; }

        public double TertiaryAlternateFuel { get; set; }

        public double QuaternaryAlternateFuel { get; set; }

        /// <example>N0460 F380 EKNUD UQ26 TUDBU DCT UPAMA DCT DEGET DCT WITRI DCT MIZOL DCT IBLIZ DCT KEKED</example>
        public string Route { get; set; }

        public string Remarks { get; set; }

        public List<Notam> Notams { get; set; }

        public string AirportCategoryMessage { get; set; }

        /// <example>ACK</example>
        public string FlightPlanStatus { get; set; }

        public int InitialAltitude { get; set; }

        public List<FlightAirport> Airports { get; set; }

        public List<RoutePoint> RoutePoints { get; set; }

        public List<Crew> Crew { get; set; }

        public FlightDocuments FlightDocuments { get; set; }

        public string LoadsheetRecipients { get; set; }
    }

    public class Notam
    {
        /// <example>A4774</example>
        public string Number { get; set; }

        public string Series { get; set; }

        public string Serial { get; set; }

        /// <example>22</example>
        public string Year { get; set; }

        /// <example>METAR LFPG 031020Z 01008KT 320V080 CAVOK 38/01 Q1005 BECMG TL1200</example>
        public string NotamText { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        /// <example>0</example>
        public string FromLevel { get; set; }

        /// <example>999</example>
        public string ToLevel { get; set; }

        public string TrafficIndicator { get; set; }

        public string Scope { get; set; }

        /// <example>instr apch proc</example>
        public string Purpose { get; set; }

        /// <example>KZNY</example>
        public string Fir { get; set; }

        /// <example>LFPG</example>
        [StringLength(4, MinimumLength = 4)]
        public string Icao { get; set; }

        public string ACode { get; set; }

        public string BCode { get; set; }

        public string CCode { get; set; }

        public string DCode { get; set; }

        public string ECode { get; set; }

        public string FCode { get; set; }

        public string GCode { get; set; }

        /// <example>QPICH</example>
        public string QCode { get; set; }

        public LocationType LocationType { get; set; }
    }


    public class Crew
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string CrewMember { get; set; }

        /// <example>109862812</example>
        public string CrewId { get; set; }

        /// <example>initials or crewId(For Azul) </example>
        public string Username { get; set; }

        /// <example>AB1234</example>
        public string LicenseNumber { get; set; }

        /// <example>C</example>
        public string Type { get; set; }

        /// <example>John Doe</example>
        public string Name { get; set; }

        /// <summary>
        /// *FlightCrew1*, *FlightCrew2*, *CabinCrew1*, *CabinCrew2*, *CabinCrew3*, *CabinCrew4*, *CabinCrew5*, *CabinCrew6*, *CabinCrew7*, *CabinCrew8*.
        /// </summary>
        /// <example>CabinCrew1</example>
        public string Duty { get; set; }

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        /// <example>CP</example>
        public string Rank { get; set; }

        /// <example>JD</example>
        public string Initials { get; set; }

        public string FullNameInitials { get; set; }

        public DateTime? DateOfBirth { get; set; }

        /// <example>Denmark</example>
        public string Nationality { get; set; }

        /// <example>Male</example>
        public string Gender { get; set; }

        /// <example>J83992648</example>
        public string Passport { get; set; }

        public bool Active { get; set; }
    }


    public class FlightDocuments
    {
        public FlightDocument FlightPlan { get; set; }
        public FlightDocument FlightPlanXml { get; set; }
        public FlightDocument Atc { get; set; }
        public FlightDocument ShortAtc { get; set; }
        public FlightDocument Messages { get; set; }
        public FlightDocument Weather { get; set; }
        public FlightDocument Notam { get; set; }
        public FlightDocument TakeOff { get; set; }
        public FlightDocument Loadsheet { get; set; }
        public FlightDocument Company { get; set; }
        public List<FlightDocument> AdditionalDocuments { get; set; }
        public List<FlightDocument> Charts { get; set; }
    }

    public class FlightDocument
    {
        /// <example>FlightPlan_IFSTest_105826.pdf</example>
        public string Filename { get; set; }

        /// <summary>
        /// Base64 encoded file.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Document MIME type.
        /// </summary>
        /// <example>application/pdf</example>
        public string ContentType { get; set; }

        public List<FlightAirport> RelevantAirports { get; set; }

        /// <example>Departure</example>>
        public string JourneyPart { get; set; }
    }

    public class FlightAirport
    {
        /// <example>Aeroport Paris Charles-De-Gaulle</example>>
        public string Name { get; set; }

        /// <example>LFPG</example>>
        [StringLength(4, MinimumLength = 4)]
        public string ICAO { get; set; }

        /// <example>CDG</example>>
        [StringLength(3, MinimumLength = 3)]
        public string IATA { get; set; }

        /// <example>A</example>>
        public string Category { get; set; }

        public LocationType Type { get; set; }
    }

    public enum LocationType
    {
        Departure = 0,
        Arrival = 1,
        ArrivalAlternatePrimary = 2,
        ArrivalAlternateSecondary = 3,
        Enroute = 4,
        EnrouteAlternate = 5,
        ContingencySaving = 6,
        DepartureAlternate = 7,
        ContingencySavingAlternate = 8,
        FlightInformationRegion = 9,
        Company = 10,
        ETOPS = 11,
        ArrivalAlternateTertiary = 12,
        ArrivalAlternateQuaternary = 13
    }

    public class RoutePoint
    {
        public int SequenceId { get; set; }

        public int Position { get; set; }

        /// <example>CDG</example>>
        public string WaypointId { get; set; }

        /// <example>LFPG</example>>
        public string WaypointName { get; set; }

        public LocationType RouteType { get; set; }

        public double Altitude { get; set; }

        public string AltitudeStatus { get; set; }

        // Coordinates are in decimal degrees
        public double Latitude { get; set; }

        // Coordinates are in decimal degrees
        public double Longitude { get; set; }

        public double WindDirection { get; set; }

        public double WindSpeed { get; set; }

        public double WindShear { get; set; }

        public double ISADeviation { get; set; }

        public TimeSpan? LegTime { get; set; }

        public double LegDistance { get; set; }

        public double FuelUsed { get; set; }

        public double EstimatedBurnOff { get; set; }

        public double AccumulatedBurnOff { get; set; }

        /// <example>04E</example>>
        public string Variation { get; set; }

        public double AccumulatedDistance { get; set; }

        public TimeSpan? AccumulatedTime { get; set; }

        /// <example>182</example>>
        public string MagneticCourse { get; set; }

        /// <example>186</example>>
        public string TrueCourse { get; set; }

        public double TrueAirSpeed { get; set; }

        public double GroundSpeed { get; set; }

        public double FuelRemaining { get; set; }

        public double DistanceRemaining { get; set; }

        public TimeSpan? TimeRemaining { get; set; }

        public double MinimumRequiredFuel { get; set; }

        public double Temperature { get; set; }

        public string TemperatureStatus { get; set; }

        /// <example>118.275</example>>
        public string Frequency { get; set; }

        public List<string> Type { get; set; }

        /// <example>LT</example>>
        public string CountryCode { get; set; }

        public string MinimumSafeAltitude { get; set; }

        /// <example>UL9</example>>
        public string Airway { get; set; }

        public string SegmentVerticalWindChange { get; set; }

        /// <example>P</example>>
        public string SegmentWindComponent { get; set; }

        public double BurnOff { get; set; }

        public double Mach { get; set; }

        public string MachStatus { get; set; }

        /// <example>000/000</example>>
        public string WindInfo { get; set; }

        public double Tropopause { get; set; }

        /// <example>EGTT</example>>
        public string FlightInformationRegion { get; set; }

        public double TrueTrack { get; set; }

        public int MinimumEnrouteAltitude { get; set; }

        public string HLAEntryExit { get; set; }
    }
}
