using System.Xml.Serialization;

namespace ArincConverter.Models
{
    [XmlType(AnonymousType = true, Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
    [XmlRoot(ElementName = "EFBFlightList", Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces",
        IsNullable = false)]
    public class SabreFlightList
    {
        [XmlElement("Flight")] public List<FlightList> Flight { get; set; }
    }

    [XmlType(AnonymousType = true, Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
    public class FlightList
    {
        [XmlElement("FPfx")] public string Prefix { get; set; }

        [XmlElement("FLNr")] public ushort FlightNumber { get; set; }

        [XmlElement("FLDt", DataType = "date")]
        public DateTime FlightDate { get; set; }

        [XmlElement("FlSfx")] public string Suffix { get; set; }

        [XmlElement("departureAerodrome")] public DepartureAerodrome DepartureAerodrome { get; set; }

        [XmlElement("arrivalAerodrome")] public ArrivalAerodrome ArrivalAerodrome { get; set; }

        [XmlElement("Fltleg_ID")] public uint FlightLegId { get; set; }

        [XmlElement("Flt_ID")] public uint FlightId { get; set; }

        [XmlElement] public DateTime STD { get; set; }

        [XmlElement("ACREG")] public string AircraftRegistration { get; set; }

        [XmlArrayItem("crewMember", IsNullable = false)]
        public CrewMember[] CrewMembers { get; set; }
    }

    [XmlType(AnonymousType = true, Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
    public class DepartureAerodrome
    {
        [XmlElement("iataID")] public string IATA { get; set; }

        [XmlElement("icaoID")] public string ICAO { get; set; }
    }

    [XmlType(AnonymousType = true, Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
    public class ArrivalAerodrome
    {
        [XmlElement("iataID")] public string IATA { get; set; }

        [XmlElement("icaoID")] public string ICAO { get; set; }
    }

    [XmlType(AnonymousType = true, Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
    public class CrewMember
    {
        [XmlElement("crewID")] public uint CrewId { get; set; }

        [XmlElement("function")] public string Function { get; set; }

        [XmlElement("name")] public string Name { get; set; }
    }

    [XmlType(AnonymousType = true, Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
    [XmlRoot(ElementName = "EFBBriefingSummary", Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces",
        IsNullable = false)]
    public class SabreFlightBriefing
    {
        [XmlElement("BriefingContent")] public BriefingContent Content { get; set; }

        [XmlElement("XMLProducts")] public Products Products { get; set; }

        [XmlElement(ElementName = "FlightPlan", Namespace = "http://aeec.aviation-ia.net/633")]
        public Arinc633FlightPlanning FlightPlan { get; set; }

        [XmlElement(ElementName = "Errors")] public Errors Errors { get; set; }
    }

    [XmlType(AnonymousType = true, Namespace = "http://aeec.aviation-ia.net/633")]
    [XmlRoot(ElementName = "FlightPlan", Namespace = "http://aeec.aviation-ia.net/633", IsNullable = false)]
    public class ArincFlightBriefing
    {
        [XmlAttribute(AttributeName = "creationTime")]
        public string CreationTime { get; set; }

        [XmlAttribute(AttributeName = "flightPlanId")]
        public string FlightPlanId { get; set; }

        [XmlAttribute(AttributeName = "fullPackage")]
        public bool FullPackage { get; set; }

        [XmlElement(ElementName = "M633Header")]
        public M633Header M633Header { get; set; }

        [XmlElement(ElementName = "M633SupplementaryHeader")]
        public M633SupplementaryHeader M633SupplementaryHeader { get; set; }

        [XmlElement(ElementName = "FlightInfo", Namespace = "http://aeec.aviation-ia.net/633")]
        public FlightInfo FlightInfo { get; set; }

        [XmlElement(ElementName = "Remarks", Namespace = "http://aeec.aviation-ia.net/633")]
        public RemarkList Remarks { get; set; }

        [XmlElement(ElementName = "FlightPlanHeader", Namespace = "http://aeec.aviation-ia.net/633")]
        public FlightPlanHeader FlightPlanHeader { get; set; }

        [XmlElement(ElementName = "FuelHeader", Namespace = "http://aeec.aviation-ia.net/633")]
        public FuelHeader FuelHeader { get; set; }

        [XmlElement(ElementName = "WeightHeader", Namespace = "http://aeec.aviation-ia.net/633")]
        public WeightHeader WeightHeader { get; set; }

        [XmlElement(ElementName = "Waypoints", Namespace = "http://aeec.aviation-ia.net/633")]
        public Waypoints Waypoints { get; set; }

        [XmlElement(ElementName = "FlightPlanSummary", Namespace = "http://aeec.aviation-ia.net/633")]
        public FlightPlanSummary FlightPlanSummary { get; set; }

        [XmlElement(ElementName = "NonStandardFlightPlanningType", Namespace = "http://aeec.aviation-ia.net/633")]
        public NonStandardFlightPlanningType NonStandardFlightPlanningType { get; set; }

        [XmlElement(ElementName = "ETOPSSummary", Namespace = "http://aeec.aviation-ia.net/633")]
        public ETOPSSummary ETOPSSummary { get; set; }

        [XmlElement(ElementName = "AlternateRoutes", Namespace = "http://aeec.aviation-ia.net/633")]
        public AlternateRoutes AlternateRoutes { get; set; }

        [XmlElement(ElementName = "AirportDataList", Namespace = "http://aeec.aviation-ia.net/633")]
        public AirportDataList AirportDataList { get; set; }

        [XmlElement(ElementName = "FuelStatistics", Namespace = "http://aeec.aviation-ia.net/633")]
        public FuelStatistics FuelStatistics { get; set; }

        [XmlElement(ElementName = "TankeringInfo", Namespace = "http://aeec.aviation-ia.net/633")]
        public TankeringInfo TankeringInfo { get; set; }
    }

    [XmlRoot(ElementName = "Airport", Namespace = "http://aeec.aviation-ia.net/633")]
    public class LidoAirport
    {
        [XmlElement(ElementName = "AirportIdentification", Namespace = "http://aeec.aviation-ia.net/633")]
        public AirportIdentification AirportIdentification { get; set; }

        [XmlElement(ElementName = "Runway", Namespace = "http://aeec.aviation-ia.net/633")]
        public Runway Runway { get; set; }

        [XmlElement(ElementName = "Elevation", Namespace = "http://aeec.aviation-ia.net/633")]
        public Elevation Elevation { get; set; }

        [XmlElement(ElementName = "AirportReferencePoint", Namespace = "http://aeec.aviation-ia.net/633")]
        public AirportReferencePoint AirportReferencePoint { get; set; }

        [XmlElement(ElementName = "RequiredFlightCrewQualification", Namespace = "http://aeec.aviation-ia.net/633")]
        public string RequiredFlightCrewQualification { get; set; }

        [XmlElement(ElementName = "LocalTimeOffsetToUTC", Namespace = "http://aeec.aviation-ia.net/633")]
        public LocalTimeOffset LocalTimeOffset { get; set; }
    }

    [XmlRoot(ElementName = "Elevation", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Elevation
    {
        [XmlAttribute(AttributeName = "unit")] public string Unit { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "LocalTimeOffsetToUTC", Namespace = "http://aeec.aviation-ia.net/633")]
    public class LocalTimeOffset
    {
        [XmlAttribute(AttributeName = "positive")] public bool Positive { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "AirportReferencePoint", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AirportReferencePoint
    {
        [XmlElement(ElementName = "Coordinates", Namespace = "http://aeec.aviation-ia.net/633")]
        public Coordinates Coordinates { get; set; }
    }

    [XmlRoot(ElementName = "AirportIdentification", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AirportIdentification
    {
        [XmlAttribute(AttributeName = "airportFunction")] public string AirportFunction { get; set; }

        [XmlAttribute(AttributeName = "airportName")] public string AirportName { get; set; }

        [XmlElement(ElementName = "AirportICAOCode", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirportICAOCode { get; set; }

        [XmlElement(ElementName = "AirportIATACode", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirportIATACode { get; set; }
    }

    [XmlRoot(ElementName = "Runway", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Runway
    {
        [XmlAttribute(AttributeName = "runwayIdentifier")] public string RunwayIdentifier { get; set; }

        [XmlElement(ElementName = "Approach", Namespace = "http://aeec.aviation-ia.net/633")]
        public Approach Approach { get; set; }

        [XmlElement(ElementName = "Elevation", Namespace = "http://aeec.aviation-ia.net/633")]
        public Elevation Elevation { get; set; }

        [XmlElement(ElementName = "ApprovedForRegularOperation", Namespace = "http://aeec.aviation-ia.net/633")]
        public bool ApprovedForRegularOperation { get; set; }
    }

    [XmlRoot(ElementName = "Approach", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Approach
    {
        [XmlAttribute(AttributeName = "procedureName")] public string ProcedureName { get; set; }

        [XmlElement(ElementName = "RequiredHorizontalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
        public RequiredHorizontalVisibility RequiredHorizontalVisibility { get; set; }

        [XmlElement(ElementName = "RequiredVerticalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
        public RequiredVerticalVisibility RequiredVerticalVisibility { get; set; }
    }







    [XmlRoot(ElementName = "BriefingContentProduct", Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
    public class BriefingContentProduct
    {
        [XmlAttribute(AttributeName = "prod_id")]
        public string ProductId { get; set; }

        [XmlAttribute(AttributeName = "descr")]
        public string Description { get; set; }

        [XmlAttribute(AttributeName = "prod_type")]
        public string ProductType { get; set; }

        [XmlAttribute(AttributeName = "created")]
        public string Created { get; set; }

        [XmlAttribute(AttributeName = "prod_subname")]
        public string ProductSubName { get; set; }

        [XmlAttribute(AttributeName = "FILEPATH")]
        public string FilePath { get; set; }

        [XmlAttribute(AttributeName = "mime_type")]
        public string MimeType { get; set; }
    }

    [XmlRoot(ElementName = "BriefingContent", Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
    public class BriefingContent
    {
        [XmlElement(ElementName = "BriefingContentProduct",
            Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
        public List<BriefingContentProduct> BriefingContentProduct { get; set; }
    }

    [XmlRoot(ElementName = "XMLProduct", Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
    public class Product
    {
        [XmlAttribute(AttributeName = "xmlmsgid")]
        public string MessageId { get; set; }

        [XmlAttribute(AttributeName = "created")]
        public string Created { get; set; }

        [XmlAttribute(AttributeName = "application")]
        public string Application { get; set; }

        [XmlAttribute(AttributeName = "msg_identifier")]
        public string MessageIdentifier { get; set; }
    }

    [XmlRoot(ElementName = "XMLProducts", Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
    public class Products
    {
        [XmlElement(ElementName = "XMLProduct", Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
        public Product Product { get; set; }
    }

    [XmlRoot(ElementName = "AdditionalFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AdditionalFuel
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "Duration", Namespace = "http://aeec.aviation-ia.net/633")]
        public Duration Duration { get; set; }

        [XmlAttribute(AttributeName = "reason")]
        public string Reason { get; set; }
    }

    [XmlRoot(ElementName = "AdditionalFuels", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AdditionalFuels
    {
        [XmlElement(ElementName = "AdditionalFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<AdditionalFuel> AdditionalFuel { get; set; }
    }

    [XmlRoot(ElementName = "ExtraFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ExtraFuel
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlAttribute(AttributeName = "reason")]
        public string Reason { get; set; }
    }

    [XmlRoot(ElementName = "ExtraFuels", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ExtraFuels
    {
        [XmlElement(ElementName = "ExtraFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<ExtraFuel> ExtraFuel { get; set; }
    }

    [XmlRoot(ElementName = "NonStandardFlightPlanningType", Namespace = "http://aeec.aviation-ia.net/633")]
    public class NonStandardFlightPlanningType
    {
        [XmlElement(ElementName = "ContingencySaving", Namespace = "http://aeec.aviation-ia.net/633")]
        public string ContingencySaving { get; set; }

        [XmlElement(ElementName = "ETOPS", Namespace = "http://aeec.aviation-ia.net/633")]
        public string ETOPS { get; set; }
    }

    [XmlRoot(ElementName = "Errors", Namespace = "http://www.fwz.aero/Schemas/StandardInterfaces")]
    public class Errors
    {
        [XmlAttribute(AttributeName = "Code")] public string Code { get; set; }
    }


    [XmlRoot(ElementName = "M633Header", Namespace = "http://aeec.aviation-ia.net/633")]
    public class M633Header
    {
        [XmlAttribute(AttributeName = "timestamp")]
        public string Timestamp { get; set; }

        [XmlAttribute(AttributeName = "versionNumber")]
        public string VersionNumber { get; set; }
    }

    [XmlRoot(ElementName = "FlightNumber", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FlightNumber
    {
        [XmlElement(ElementName = "CommercialFlightNumber", Namespace = "http://aeec.aviation-ia.net/633")]
        public string CommercialFlightNumber { get; set; }

        [XmlAttribute(AttributeName = "airlineIATACode")]
        public string AirlineIATACode { get; set; }

        [XmlAttribute(AttributeName = "number")]
        public string Number { get; set; }
    }

    [XmlRoot(ElementName = "FlightIdentification", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FlightIdentification
    {
        [XmlElement(ElementName = "FlightIdentifier", Namespace = "http://aeec.aviation-ia.net/633")]
        public string FlightIdentifier { get; set; }

        [XmlElement(ElementName = "FlightNumber", Namespace = "http://aeec.aviation-ia.net/633")]
        public FlightNumber FlightNumber { get; set; }
    }

    [XmlRoot(ElementName = "DepartureAirport", Namespace = "http://aeec.aviation-ia.net/633")]
    public class DepartureAirport
    {
        [XmlElement(ElementName = "AirportICAOCode", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirportICAOCode { get; set; }

        [XmlElement(ElementName = "AirportIATACode", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirportIATACode { get; set; }

        [XmlElement(ElementName = "AirportCategory", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirportCategory { get; set; }

        [XmlAttribute(AttributeName = "airportFunction")]
        public string AirportFunction { get; set; }

        [XmlAttribute(AttributeName = "airportName")]
        public string AirportName { get; set; }
    }

    [XmlRoot(ElementName = "ArrivalAirport", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ArrivalAirport
    {
        [XmlElement(ElementName = "AirportICAOCode", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirportICAOCode { get; set; }

        [XmlElement(ElementName = "AirportIATACode", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirportIATACode { get; set; }

        [XmlElement(ElementName = "AirportCategory", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirportCategory { get; set; }

        [XmlAttribute(AttributeName = "airportFunction")]
        public string AirportFunction { get; set; }

        [XmlAttribute(AttributeName = "airportName")]
        public string AirportName { get; set; }
    }

    [XmlRoot(ElementName = "Flight", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Flight
    {
        [XmlElement(ElementName = "FlightIdentification", Namespace = "http://aeec.aviation-ia.net/633")]
        public FlightIdentification FlightIdentification { get; set; }

        [XmlElement(ElementName = "DepartureAirport", Namespace = "http://aeec.aviation-ia.net/633")]
        public DepartureAirport DepartureAirport { get; set; }

        [XmlElement(ElementName = "ArrivalAirport", Namespace = "http://aeec.aviation-ia.net/633")]
        public ArrivalAirport ArrivalAirport { get; set; }

        [XmlAttribute(AttributeName = "flightOriginDate")]
        public string FlightOriginDate { get; set; }

        [XmlAttribute(AttributeName = "scheduledTimeOfDeparture")]
        public string ScheduledTimeOfDeparture { get; set; }
    }

    [XmlRoot(ElementName = "AircraftModel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AircraftModel
    {
        [XmlElement(ElementName = "AircraftICAOType", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AircraftICAOType { get; set; }

        [XmlAttribute(AttributeName = "airlineSpecificSubType")]
        public string AirlineSpecificSubType { get; set; }
    }

    [XmlRoot(ElementName = "Aircraft", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Aircraft
    {
        [XmlElement(ElementName = "AircraftModel", Namespace = "http://aeec.aviation-ia.net/633")]
        public AircraftModel AircraftModel { get; set; }

        [XmlAttribute(AttributeName = "aircraftRegistration")]
        public string AircraftRegistration { get; set; }
    }

    [XmlRoot(ElementName = "M633SupplementaryHeader", Namespace = "http://aeec.aviation-ia.net/633")]
    public class M633SupplementaryHeader
    {
        [XmlElement(ElementName = "Flight", Namespace = "http://aeec.aviation-ia.net/633")]
        public Flight Flight { get; set; }

        [XmlElement(ElementName = "Aircraft", Namespace = "http://aeec.aviation-ia.net/633")]
        public Aircraft Aircraft { get; set; }
    }

    [XmlRoot(ElementName = "FlightInfo", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FlightInfo
    {
        [XmlAttribute(AttributeName = "aTCCallsign")]
        public string ATCCallsign { get; set; }

        [XmlAttribute(AttributeName = "captain")]
        public string Captain { get; set; }
    }

    [XmlRoot(ElementName = "AuthorName", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AuthorName
    {
        [XmlAttribute(AttributeName = "authorType")]
        public string AuthorType { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "AuthorContact", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AuthorContact
    {
        [XmlElement(ElementName = "PhoneNumber", Namespace = "http://aeec.aviation-ia.net/633")]
        public string PhoneNumber { get; set; }

        [XmlElement(ElementName = "TelexAddress", Namespace = "http://aeec.aviation-ia.net/633")]
        public string TelexAddress { get; set; }

        [XmlElement(ElementName = "DispatchOffice", Namespace = "http://aeec.aviation-ia.net/633")]
        public string DispatchOffice { get; set; }
    }

    [XmlRoot(ElementName = "AuthorInformation", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AuthorInformation
    {
        [XmlElement(ElementName = "AuthorName", Namespace = "http://aeec.aviation-ia.net/633")]
        public AuthorName AuthorName { get; set; }

        [XmlElement(ElementName = "AuthorContact", Namespace = "http://aeec.aviation-ia.net/633")]
        public AuthorContact AuthorContact { get; set; }
    }

    [XmlRoot(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Value
    {
        [XmlAttribute(AttributeName = "unit")] public string Unit { get; set; }

        [XmlText] public string Text { get; set; }

        [XmlAttribute(AttributeName = "type")] public string Type { get; set; }
    }

    [XmlRoot(ElementName = "AverageFuelFlow", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AverageFuelFlow
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "TaxiFuelFlow", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TaxiFuelFlow
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "HoldingFuelFlow", Namespace = "http://aeec.aviation-ia.net/633")]
    public class HoldingFuelFlow
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "FuelFlowInformation", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FuelFlowInformation
    {
        [XmlElement(ElementName = "AverageFuelFlow", Namespace = "http://aeec.aviation-ia.net/633")]
        public AverageFuelFlow AverageFuelFlow { get; set; }

        [XmlElement(ElementName = "TaxiFuelFlow", Namespace = "http://aeec.aviation-ia.net/633")]
        public TaxiFuelFlow TaxiFuelFlow { get; set; }

        [XmlElement(ElementName = "HoldingFuelFlow", Namespace = "http://aeec.aviation-ia.net/633")]
        public HoldingFuelFlow HoldingFuelFlow { get; set; }
    }

    [XmlRoot(ElementName = "Direction", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Direction
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Speed", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Speed
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "AverageWind", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AverageWind
    {
        [XmlElement(ElementName = "Direction", Namespace = "http://aeec.aviation-ia.net/633")]
        public Direction Direction { get; set; }

        [XmlElement(ElementName = "Speed", Namespace = "http://aeec.aviation-ia.net/633")]
        public Speed Speed { get; set; }
    }

    [XmlRoot(ElementName = "AverageWindComponent", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AverageWindComponent
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "AverageTemperature", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AverageTemperature
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "AverageISADeviation", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AverageISADeviation
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "InitialAltitude", Namespace = "http://aeec.aviation-ia.net/633")]
    public class InitialAltitude
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "MachNumber", Namespace = "http://aeec.aviation-ia.net/633")]
    public class MachNumber
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public string Value { get; set; }

        [XmlElement(ElementName = "EstimatedMachNumber", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedMachNumber EstimatedMachNumber { get; set; }
    }

    [XmlRoot(ElementName = "IndicatedAirSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
    public class IndicatedAirSpeed
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }

        [XmlElement(ElementName = "EstimatedSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedSpeed EstimatedSpeed { get; set; }
    }

    [XmlRoot(ElementName = "ClimbProcedure", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ClimbProcedure
    {
        [XmlElement(ElementName = "MachNumber", Namespace = "http://aeec.aviation-ia.net/633")]
        public MachNumber MachNumber { get; set; }

        [XmlElement(ElementName = "IndicatedAirSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
        public IndicatedAirSpeed IndicatedAirSpeed { get; set; }

        [XmlAttribute(AttributeName = "procedure")]
        public string Procedure { get; set; }
    }

    [XmlRoot(ElementName = "CostIndex", Namespace = "http://aeec.aviation-ia.net/633")]
    public class CostIndex
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "CruiseProcedure", Namespace = "http://aeec.aviation-ia.net/633")]
    public class CruiseProcedure
    {
        [XmlElement(ElementName = "CostIndex", Namespace = "http://aeec.aviation-ia.net/633")]
        public CostIndex CostIndex { get; set; }

        [XmlAttribute(AttributeName = "procedure")]
        public string Procedure { get; set; }
    }

    [XmlRoot(ElementName = "DescentProcedure", Namespace = "http://aeec.aviation-ia.net/633")]
    public class DescentProcedure
    {
        [XmlElement(ElementName = "MachNumber", Namespace = "http://aeec.aviation-ia.net/633")]
        public MachNumber MachNumber { get; set; }

        [XmlElement(ElementName = "IndicatedAirSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
        public IndicatedAirSpeed IndicatedAirSpeed { get; set; }

        [XmlAttribute(AttributeName = "procedure")]
        public string Procedure { get; set; }
    }

    [XmlRoot(ElementName = "GroundDistance", Namespace = "http://aeec.aviation-ia.net/633")]
    public class GroundDistance
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "AirDistance", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AirDistance
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "GreatCircleDistance", Namespace = "http://aeec.aviation-ia.net/633")]
    public class GreatCircleDistance
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "RouteInformation", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RouteInformation
    {
        [XmlElement(ElementName = "AverageWind", Namespace = "http://aeec.aviation-ia.net/633")]
        public AverageWind AverageWind { get; set; }

        [XmlElement(ElementName = "AverageWindComponent", Namespace = "http://aeec.aviation-ia.net/633")]
        public AverageWindComponent AverageWindComponent { get; set; }

        [XmlElement(ElementName = "AverageTemperature", Namespace = "http://aeec.aviation-ia.net/633")]
        public AverageTemperature AverageTemperature { get; set; }

        [XmlElement(ElementName = "AverageISADeviation", Namespace = "http://aeec.aviation-ia.net/633")]
        public AverageISADeviation AverageISADeviation { get; set; }

        [XmlElement(ElementName = "InitialAltitude", Namespace = "http://aeec.aviation-ia.net/633")]
        public InitialAltitude InitialAltitude { get; set; }

        [XmlElement(ElementName = "ClimbProcedure", Namespace = "http://aeec.aviation-ia.net/633")]
        public ClimbProcedure ClimbProcedure { get; set; }

        [XmlElement(ElementName = "CruiseProcedure", Namespace = "http://aeec.aviation-ia.net/633")]
        public CruiseProcedure CruiseProcedure { get; set; }

        [XmlElement(ElementName = "DescentProcedure", Namespace = "http://aeec.aviation-ia.net/633")]
        public DescentProcedure DescentProcedure { get; set; }

        [XmlElement(ElementName = "RouteDescription", Namespace = "http://aeec.aviation-ia.net/633")]
        public string RouteDescription { get; set; }

        [XmlElement(ElementName = "GroundDistance", Namespace = "http://aeec.aviation-ia.net/633")]
        public GroundDistance GroundDistance { get; set; }

        [XmlElement(ElementName = "AirDistance", Namespace = "http://aeec.aviation-ia.net/633")]
        public AirDistance AirDistance { get; set; }

        [XmlElement(ElementName = "GreatCircleDistance", Namespace = "http://aeec.aviation-ia.net/633")]
        public GreatCircleDistance GreatCircleDistance { get; set; }

        [XmlAttribute(AttributeName = "routeName")]
        public string RouteName { get; set; }

        [XmlAttribute(AttributeName = "optimization")]
        public string Optimization { get; set; }
    }

    [XmlRoot(ElementName = "FlightPlanHeader", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FlightPlanHeader
    {
        [XmlElement(ElementName = "AuthorInformation", Namespace = "http://aeec.aviation-ia.net/633")]
        public AuthorInformation AuthorInformation { get; set; }

        [XmlElement(ElementName = "PerformanceFactor", Namespace = "http://aeec.aviation-ia.net/633")]
        public string PerformanceFactor { get; set; }

        [XmlElement(ElementName = "FuelFlowInformation", Namespace = "http://aeec.aviation-ia.net/633")]
        public FuelFlowInformation FuelFlowInformation { get; set; }

        [XmlElement(ElementName = "RouteInformation", Namespace = "http://aeec.aviation-ia.net/633")]
        public RouteInformation RouteInformation { get; set; }
    }

    [XmlRoot(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
    public class EstimatedWeight
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Duration", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Duration
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "TripFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TripFuel
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "Duration", Namespace = "http://aeec.aviation-ia.net/633")]
        public Duration Duration { get; set; }
    }

    [XmlRoot(ElementName = "ContingencyPolicy", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ContingencyPolicy
    {
        [XmlAttribute(AttributeName = "policyName")]
        public string PolicyName { get; set; }
    }

    [XmlRoot(ElementName = "ContingencyFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ContingencyFuel
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "Duration", Namespace = "http://aeec.aviation-ia.net/633")]
        public Duration Duration { get; set; }

        [XmlElement(ElementName = "ContingencyPolicy", Namespace = "http://aeec.aviation-ia.net/633")]
        public ContingencyPolicy ContingencyPolicy { get; set; }
    }

    [XmlRoot(ElementName = "Airport", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Airport
    {
        [XmlElement(ElementName = "AirportICAOCode", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirportICAOCode { get; set; }

        [XmlElement(ElementName = "AirportIATACode", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirportIATACode { get; set; }

        [XmlAttribute(AttributeName = "airportFunction")]
        public string AirportFunction { get; set; }

        [XmlAttribute(AttributeName = "airportName")]
        public string AirportName { get; set; }
    }

    [XmlRoot(ElementName = "FinalReserve", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FinalReserve
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "Duration", Namespace = "http://aeec.aviation-ia.net/633")]
        public Duration Duration { get; set; }
    }

    [XmlRoot(ElementName = "AlternateFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AlternateFuel
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "Duration", Namespace = "http://aeec.aviation-ia.net/633")]
        public Duration Duration { get; set; }

        [XmlElement(ElementName = "Airport", Namespace = "http://aeec.aviation-ia.net/633")]
        public Airport Airport { get; set; }

        [XmlElement(ElementName = "FinalReserve", Namespace = "http://aeec.aviation-ia.net/633")]
        public FinalReserve FinalReserve { get; set; }

        [XmlElement(ElementName = "Sequence", Namespace = "http://aeec.aviation-ia.net/633")]
        public string Sequence { get; set; }
    }

    [XmlRoot(ElementName = "AlternateFuels", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AlternateFuels
    {
        [XmlElement(ElementName = "AlternateFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<AlternateFuel> AlternateFuel { get; set; }
    }

    [XmlRoot(ElementName = "ETOPSFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ETOPSFuel
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "Duration", Namespace = "http://aeec.aviation-ia.net/633")]
        public Duration Duration { get; set; }
    }

    [XmlRoot(ElementName = "PossibleExtra", Namespace = "http://aeec.aviation-ia.net/633")]
    public class PossibleExtra
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Weight", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Weight
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Density", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Density
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "MaximumFuelWeight", Namespace = "http://aeec.aviation-ia.net/633")]
    public class MaximumFuelWeight
    {
        [XmlElement(ElementName = "Weight", Namespace = "http://aeec.aviation-ia.net/633")]
        public Weight Weight { get; set; }

        [XmlElement(ElementName = "Density", Namespace = "http://aeec.aviation-ia.net/633")]
        public Density Density { get; set; }
    }

    [XmlRoot(ElementName = "TankVolume", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TankVolume
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "PossibleExtraFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class PossibleExtraFuel
    {
        [XmlElement(ElementName = "PossibleExtra", Namespace = "http://aeec.aviation-ia.net/633")]
        public PossibleExtra PossibleExtra { get; set; }

        [XmlElement(ElementName = "MaximumFuelWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public MaximumFuelWeight MaximumFuelWeight { get; set; }

        [XmlElement(ElementName = "TankVolume", Namespace = "http://aeec.aviation-ia.net/633")]
        public TankVolume TankVolume { get; set; }

        [XmlAttribute(AttributeName = "reason")]
        public string Reason { get; set; }
    }

    [XmlRoot(ElementName = "TakeOffFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TakeOffFuel
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "Duration", Namespace = "http://aeec.aviation-ia.net/633")]
        public Duration Duration { get; set; }
    }

    [XmlRoot(ElementName = "TaxiFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TaxiFuel
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "Duration", Namespace = "http://aeec.aviation-ia.net/633")]
        public Duration Duration { get; set; }
    }

    [XmlRoot(ElementName = "BlockFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class BlockFuel
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "Duration", Namespace = "http://aeec.aviation-ia.net/633")]
        public Duration Duration { get; set; }
    }

    [XmlRoot(ElementName = "LandingFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class LandingFuel
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }
    }

    [XmlRoot(ElementName = "ArrivalFuel", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ArrivalFuel
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }
    }

    [XmlRoot(ElementName = "Adjustment", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Adjustment
    {
        [XmlAttribute(AttributeName = "unit")] public string Unit { get; set; }

        [XmlAttribute(AttributeName = "affectedItem")]
        public string AffectedItem { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "AffectedItems", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AffectedItems
    {
        [XmlElement(ElementName = "Adjustment", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<Adjustment> Adjustment { get; set; }

        [XmlAttribute(AttributeName = "deviationReasonChange")]
        public string DeviationReasonChange { get; set; }

        [XmlAttribute(AttributeName = "unit")] public string Unit { get; set; }
    }

    [XmlRoot(ElementName = "Adjustments", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Adjustments
    {
        [XmlElement(ElementName = "AffectedItems", Namespace = "http://aeec.aviation-ia.net/633")]
        public AffectedItems AffectedItems { get; set; }

        [XmlAttribute(AttributeName = "deviationReason")]
        public string DeviationReason { get; set; }
    }

    [XmlRoot(ElementName = "FuelHeader", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FuelHeader
    {
        [XmlElement(ElementName = "TripFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public TripFuel TripFuel { get; set; }

        [XmlElement(ElementName = "ContingencyFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public ContingencyFuel ContingencyFuel { get; set; }

        [XmlElement(ElementName = "AlternateFuels", Namespace = "http://aeec.aviation-ia.net/633")]
        public AlternateFuels AlternateFuels { get; set; }

        [XmlElement(ElementName = "ETOPSFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public ETOPSFuel ETOPSFuel { get; set; }

        [XmlElement(ElementName = "PossibleExtraFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public PossibleExtraFuel PossibleExtraFuel { get; set; }

        [XmlElement(ElementName = "TakeOffFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public TakeOffFuel TakeOffFuel { get; set; }

        [XmlElement(ElementName = "TaxiFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public TaxiFuel TaxiFuel { get; set; }

        [XmlElement(ElementName = "BlockFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public BlockFuel BlockFuel { get; set; }

        [XmlElement(ElementName = "LandingFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public LandingFuel LandingFuel { get; set; }

        [XmlElement(ElementName = "ArrivalFuel", Namespace = "http://aeec.aviation-ia.net/633")]
        public ArrivalFuel ArrivalFuel { get; set; }

        [XmlElement(ElementName = "FinalReserve", Namespace = "http://aeec.aviation-ia.net/633")]
        public FinalReserve FinalReserve { get; set; }

        [XmlElement(ElementName = "ExtraFuels", Namespace = "http://aeec.aviation-ia.net/633")]
        public ExtraFuels ExtraFuels { get; set; }

        [XmlElement(ElementName = "AdditionalFuels", Namespace = "http://aeec.aviation-ia.net/633")]
        public AdditionalFuels AdditionalFuels { get; set; }

        [XmlElement(ElementName = "Adjustments", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<Adjustments> Adjustments { get; set; }
    }

    [XmlRoot(ElementName = "DryOperatingWeight", Namespace = "http://aeec.aviation-ia.net/633")]
    public class DryOperatingWeight
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }
    }

    [XmlRoot(ElementName = "Load", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Load
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }
    }

    [XmlRoot(ElementName = "StructuralLimit", Namespace = "http://aeec.aviation-ia.net/633")]
    public class StructuralLimit
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "ZeroFuelWeight", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ZeroFuelWeight
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "OperationalLimit", Namespace = "http://aeec.aviation-ia.net/633")]
        public OperationalLimit OperationalLimit { get; set; }

        [XmlElement(ElementName = "StructuralLimit", Namespace = "http://aeec.aviation-ia.net/633")]
        public StructuralLimit StructuralLimit { get; set; }
    }

    [XmlRoot(ElementName = "OperationalLimit", Namespace = "http://aeec.aviation-ia.net/633")]
    public class OperationalLimit
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "TakeoffWeight", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TakeoffWeight
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "OperationalLimit", Namespace = "http://aeec.aviation-ia.net/633")]
        public OperationalLimit OperationalLimit { get; set; }

        [XmlElement(ElementName = "StructuralLimit", Namespace = "http://aeec.aviation-ia.net/633")]
        public StructuralLimit StructuralLimit { get; set; }
    }

    [XmlRoot(ElementName = "LandingWeight", Namespace = "http://aeec.aviation-ia.net/633")]
    public class LandingWeight
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }

        [XmlElement(ElementName = "OperationalLimit", Namespace = "http://aeec.aviation-ia.net/633")]
        public OperationalLimit OperationalLimit { get; set; }

        [XmlElement(ElementName = "StructuralLimit", Namespace = "http://aeec.aviation-ia.net/633")]
        public StructuralLimit StructuralLimit { get; set; }
    }

    [XmlRoot(ElementName = "StructuralLimit", Namespace = "http://www.lido.net/lsya")]
    public class StructuralLimit2
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "LCBTaxiWeightExtension", Namespace = "http://www.lido.net/lsya")]
    public class LCBTaxiWeightExtension
    {
        [XmlElement(ElementName = "StructuralLimit", Namespace = "http://www.lido.net/lsya")]
        public StructuralLimit2 StructuralLimit2 { get; set; }
    }

    [XmlRoot(ElementName = "WeightHeaderExtension", Namespace = "http://www.lido.net/lsya")]
    public class WeightHeaderExtension
    {
        [XmlElement(ElementName = "LCBTaxiWeightExtension", Namespace = "http://www.lido.net/lsya")]
        public LCBTaxiWeightExtension LCBTaxiWeightExtension { get; set; }
    }

    [XmlRoot(ElementName = "WeightHeader", Namespace = "http://aeec.aviation-ia.net/633")]
    public class WeightHeader
    {
        [XmlElement(ElementName = "DryOperatingWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public DryOperatingWeight DryOperatingWeight { get; set; }

        [XmlElement(ElementName = "Load", Namespace = "http://aeec.aviation-ia.net/633")]
        public Load Load { get; set; }

        [XmlElement(ElementName = "ZeroFuelWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public ZeroFuelWeight ZeroFuelWeight { get; set; }

        [XmlElement(ElementName = "TakeoffWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public TakeoffWeight TakeoffWeight { get; set; }

        [XmlElement(ElementName = "LandingWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public LandingWeight LandingWeight { get; set; }

        [XmlElement(ElementName = "WeightHeaderExtension", Namespace = "http://www.lido.net/lsya")]
        public WeightHeaderExtension WeightHeaderExtension { get; set; }
    }

    [XmlRoot(ElementName = "Coordinates", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Coordinates
    {
        [XmlAttribute(AttributeName = "latitude")]
        public string Latitude { get; set; }

        [XmlAttribute(AttributeName = "longitude")]
        public string Longitude { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Airway", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Airway
    {
        [XmlAttribute(AttributeName = "type")] public string Type { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "SegmentWind", Namespace = "http://aeec.aviation-ia.net/633")]
    public class SegmentWind
    {
        [XmlElement(ElementName = "Direction", Namespace = "http://aeec.aviation-ia.net/633")]
        public Direction Direction { get; set; }

        [XmlElement(ElementName = "Speed", Namespace = "http://aeec.aviation-ia.net/633")]
        public Speed Speed { get; set; }

        [XmlElement(ElementName = "Component", Namespace = "http://aeec.aviation-ia.net/633")]
        public SegmentWindComponent Component { get; set; }
    }

    [XmlRoot(ElementName = "SegmentWindComponent", Namespace = "http://aeec.aviation-ia.net/633")]
    public class SegmentWindComponent
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Tropopause", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Tropopause
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "OutboundTrack", Namespace = "http://aeec.aviation-ia.net/633")]
    public class OutboundTrack
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "SegmentTrack", Namespace = "http://aeec.aviation-ia.net/633")]
    public class SegmentTrack
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Tracks", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Tracks
    {
        [XmlElement(ElementName = "OutboundTrack", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<OutboundTrack> OutboundTrack { get; set; }

        [XmlElement(ElementName = "SegmentTrack", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<SegmentTrack> SegmentTrack { get; set; }
    }

    [XmlRoot(ElementName = "RemainingGroundDistance", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RemainingGroundDistance
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "RemainingAirDistance", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RemainingAirDistance
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
    public class EstimatedTime
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "CumulatedFlightTime", Namespace = "http://aeec.aviation-ia.net/633")]
    public class CumulatedFlightTime
    {
        [XmlElement(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedTime EstimatedTime { get; set; }
    }

    [XmlRoot(ElementName = "RemainingFlightTime", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RemainingFlightTime
    {
        [XmlElement(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedTime EstimatedTime { get; set; }
    }

    [XmlRoot(ElementName = "CumulatedBurnOff", Namespace = "http://aeec.aviation-ia.net/633")]
    public class CumulatedBurnOff
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }
    }

    [XmlRoot(ElementName = "FuelOnBoard", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FuelOnBoard
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }
    }

    [XmlRoot(ElementName = "AircraftWeight", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AircraftWeight
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }
    }

    [XmlRoot(ElementName = "MinimumFuelOnBoard", Namespace = "http://aeec.aviation-ia.net/633")]
    public class MinimumFuelOnBoard
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Waypoint", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Waypoint
    {
        [XmlElement(ElementName = "Coordinates", Namespace = "http://aeec.aviation-ia.net/633")]
        public Coordinates Coordinates { get; set; }

        [XmlElement(ElementName = "Airway", Namespace = "http://aeec.aviation-ia.net/633")]
        public Airway Airway { get; set; }

        [XmlElement(ElementName = "SegmentWind", Namespace = "http://aeec.aviation-ia.net/633")]
        public SegmentWind SegmentWind { get; set; }

        [XmlElement(ElementName = "SegmentWindComponent", Namespace = "http://aeec.aviation-ia.net/633")]
        public SegmentWindComponent SegmentWindComponent { get; set; }

        [XmlElement(ElementName = "Tropopause", Namespace = "http://aeec.aviation-ia.net/633")]
        public Tropopause Tropopause { get; set; }

        [XmlElement(ElementName = "Tracks", Namespace = "http://aeec.aviation-ia.net/633")]
        public Tracks Tracks { get; set; }

        [XmlElement(ElementName = "RemainingGroundDistance", Namespace = "http://aeec.aviation-ia.net/633")]
        public RemainingGroundDistance RemainingGroundDistance { get; set; }

        [XmlElement(ElementName = "RemainingAirDistance", Namespace = "http://aeec.aviation-ia.net/633")]
        public RemainingAirDistance RemainingAirDistance { get; set; }

        [XmlElement(ElementName = "CumulatedFlightTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public CumulatedFlightTime CumulatedFlightTime { get; set; }

        [XmlElement(ElementName = "RemainingFlightTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public RemainingFlightTime RemainingFlightTime { get; set; }

        [XmlElement(ElementName = "CumulatedBurnOff", Namespace = "http://aeec.aviation-ia.net/633")]
        public CumulatedBurnOff CumulatedBurnOff { get; set; }

        [XmlElement(ElementName = "FuelOnBoard", Namespace = "http://aeec.aviation-ia.net/633")]
        public FuelOnBoard FuelOnBoard { get; set; }

        [XmlElement(ElementName = "AircraftWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public AircraftWeight AircraftWeight { get; set; }

        [XmlElement(ElementName = "MinimumFuelOnBoard", Namespace = "http://aeec.aviation-ia.net/633")]
        public MinimumFuelOnBoard MinimumFuelOnBoard { get; set; }

        [XmlElement(ElementName = "Function", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<string> Function { get; set; }

        [XmlAttribute(AttributeName = "sequenceId")]
        public string SequenceId { get; set; }

        [XmlAttribute(AttributeName = "waypointId")]
        public string WaypointId { get; set; }

        [XmlAttribute(AttributeName = "waypointName")]
        public string WaypointName { get; set; }

        [XmlAttribute(AttributeName = "waypointLongName")]
        public string WaypointLongName { get; set; }

        [XmlAttribute(AttributeName = "countryICAOCode")]
        public string CountryICAOCode { get; set; }

        [XmlElement(ElementName = "Altitude", Namespace = "http://aeec.aviation-ia.net/633")]
        public Altitude Altitude { get; set; }

        [XmlElement(ElementName = "MinimumSafeAltitude", Namespace = "http://aeec.aviation-ia.net/633")]
        public MinimumSafeAltitude MinimumSafeAltitude { get; set; }

        [XmlElement(ElementName = "SegmentTemperature", Namespace = "http://aeec.aviation-ia.net/633")]
        public SegmentTemperature SegmentTemperature { get; set; }

        [XmlElement(ElementName = "SegmentISADeviation", Namespace = "http://aeec.aviation-ia.net/633")]
        public SegmentISADeviation SegmentISADeviation { get; set; }

        [XmlElement(ElementName = "TrueAirSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
        public TrueAirSpeed TrueAirSpeed { get; set; }

        [XmlElement(ElementName = "MachNumber", Namespace = "http://aeec.aviation-ia.net/633")]
        public MachNumber MachNumber { get; set; }

        [XmlElement(ElementName = "IndicatedAirSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
        public IndicatedAirSpeed IndicatedAirSpeed { get; set; }

        [XmlElement(ElementName = "GroundSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
        public GroundSpeed GroundSpeed { get; set; }

        [XmlElement(ElementName = "GroundDistance", Namespace = "http://aeec.aviation-ia.net/633")]
        public GroundDistance GroundDistance { get; set; }

        [XmlElement(ElementName = "AirDistance", Namespace = "http://aeec.aviation-ia.net/633")]
        public AirDistance AirDistance { get; set; }

        [XmlElement(ElementName = "TimeFromPreviousWaypoint", Namespace = "http://aeec.aviation-ia.net/633")]
        public TimeFromPreviousWaypoint TimeFromPreviousWaypoint { get; set; }

        [XmlElement(ElementName = "BurnOff", Namespace = "http://aeec.aviation-ia.net/633")]
        public BurnOff BurnOff { get; set; }

        [XmlElement(ElementName = "Airspace", Namespace = "http://aeec.aviation-ia.net/633")]
        public Airspace Airspace { get; set; }

        [XmlElement(ElementName = "Remarks", Namespace = "http://aeec.aviation-ia.net/633")]
        public Remarks Remarks { get; set; }

        [XmlElement(ElementName = "SegmentShearRate", Namespace = "http://aeec.aviation-ia.net/633")]
        public SegmentShearRate SegmentShearRate { get; set; }
    }

    [XmlRoot(ElementName = "EstimatedAltitude", Namespace = "http://aeec.aviation-ia.net/633")]
    public class EstimatedAltitude
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Altitude", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Altitude
    {
        [XmlElement(ElementName = "EstimatedAltitude", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedAltitude EstimatedAltitude { get; set; }
    }

    [XmlRoot(ElementName = "MinimumSafeAltitude", Namespace = "http://aeec.aviation-ia.net/633")]
    public class MinimumSafeAltitude
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "SegmentTemperature", Namespace = "http://aeec.aviation-ia.net/633")]
    public class SegmentTemperature
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "SegmentISADeviation", Namespace = "http://aeec.aviation-ia.net/633")]
    public class SegmentISADeviation
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "EstimatedSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
    public class EstimatedSpeed
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "TrueAirSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TrueAirSpeed
    {
        [XmlElement(ElementName = "EstimatedSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedSpeed EstimatedSpeed { get; set; }
    }

    [XmlRoot(ElementName = "EstimatedMachNumber", Namespace = "http://aeec.aviation-ia.net/633")]
    public class EstimatedMachNumber
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "GroundSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
    public class GroundSpeed
    {
        [XmlElement(ElementName = "EstimatedSpeed", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedSpeed EstimatedSpeed { get; set; }
    }

    [XmlRoot(ElementName = "TimeFromPreviousWaypoint", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TimeFromPreviousWaypoint
    {
        [XmlElement(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedTime EstimatedTime { get; set; }
    }

    [XmlRoot(ElementName = "BurnOff", Namespace = "http://aeec.aviation-ia.net/633")]
    public class BurnOff
    {
        [XmlElement(ElementName = "EstimatedWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedWeight EstimatedWeight { get; set; }
    }

    [XmlRoot(ElementName = "Airspace", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Airspace
    {
        [XmlElement(ElementName = "AirspaceName", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirspaceName { get; set; }

        [XmlElement(ElementName = "RadioFrequency", Namespace = "http://aeec.aviation-ia.net/633")]
        public string RadioFrequency { get; set; }

        [XmlAttribute(AttributeName = "airspaceICAOCode")]
        public string AirspaceICAOCode { get; set; }
    }

    [XmlRoot(ElementName = "Paragraph", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Paragraph
    {
        [XmlElement(ElementName = "Text", Namespace = "http://aeec.aviation-ia.net/633")]
        public string Text { get; set; }

        [XmlAttribute(AttributeName = "sequence")]
        public string Sequence { get; set; }
    }

    [XmlRoot(ElementName = "Remark", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Remark
    {
        [XmlElement(ElementName = "Paragraph", Namespace = "http://aeec.aviation-ia.net/633")]
        public Paragraph Paragraph { get; set; }

        [XmlAttribute(AttributeName = "remarkType")]
        public string RemarkType { get; set; }
    }

    [XmlRoot(ElementName = "Remarks", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Remarks
    {
        [XmlElement(ElementName = "Remark", Namespace = "http://aeec.aviation-ia.net/633")]
        public Remark Remark { get; set; }
    }

    [XmlRoot(ElementName = "Remarks", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RemarkList
    {
        [XmlElement(ElementName = "Remark", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<Remark> Remark { get; set; }
    }

    [XmlRoot(ElementName = "SegmentVerticalWindChange", Namespace = "http://aeec.aviation-ia.net/633")]
    public class SegmentVerticalWindChange
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "AltitudeDifference", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AltitudeDifference
    {
        [XmlAttribute(AttributeName = "unit")] public string Unit { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "SegmentShearRate", Namespace = "http://aeec.aviation-ia.net/633")]
    public class SegmentShearRate
    {
        [XmlElement(ElementName = "SegmentVerticalWindChange", Namespace = "http://aeec.aviation-ia.net/633")]
        public SegmentVerticalWindChange SegmentVerticalWindChange { get; set; }

        [XmlElement(ElementName = "AltitudeDifference", Namespace = "http://aeec.aviation-ia.net/633")]
        public AltitudeDifference AltitudeDifference { get; set; }
    }

    [XmlRoot(ElementName = "MNPSAirspaceCrossed", Namespace = "http://www.lido.net/lsya")]
    public class MNPSAirspaceCrossed
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }

        [XmlAttribute(AttributeName = "direction")]
        public string Direction { get; set; }
    }

    [XmlRoot(ElementName = "LCBNATIdentifierExtension", Namespace = "http://www.lido.net/lsya")]
    public class LCBNATIdentifierExtension
    {
        [XmlElement(ElementName = "MNPSAirspaceCrossed", Namespace = "http://www.lido.net/lsya")]
        public MNPSAirspaceCrossed MNPSAirspaceCrossed { get; set; }
    }

    [XmlRoot(ElementName = "Waypoints", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Waypoints
    {
        [XmlElement(ElementName = "Waypoint", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<Waypoint> Waypoint { get; set; }

        [XmlElement(ElementName = "LCBNATIdentifierExtension", Namespace = "http://www.lido.net/lsya")]
        public LCBNATIdentifierExtension LCBNATIdentifierExtension { get; set; }
    }

    [XmlRoot(ElementName = "OutTime", Namespace = "http://aeec.aviation-ia.net/633")]
    public class OutTime
    {
        [XmlElement(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedTime EstimatedTime { get; set; }
    }

    [XmlRoot(ElementName = "OffTime", Namespace = "http://aeec.aviation-ia.net/633")]
    public class OffTime
    {
        [XmlElement(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedTime EstimatedTime { get; set; }
    }

    [XmlRoot(ElementName = "OnTime", Namespace = "http://aeec.aviation-ia.net/633")]
    public class OnTime
    {
        [XmlElement(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedTime EstimatedTime { get; set; }
    }

    [XmlRoot(ElementName = "InTime", Namespace = "http://aeec.aviation-ia.net/633")]
    public class InTime
    {
        [XmlElement(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedTime EstimatedTime { get; set; }
    }

    [XmlRoot(ElementName = "BlockTime", Namespace = "http://aeec.aviation-ia.net/633")]
    public class BlockTime
    {
        [XmlElement(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedTime EstimatedTime { get; set; }
    }

    [XmlRoot(ElementName = "TaxiOutTime", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TaxiOutTime
    {
        [XmlElement(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedTime EstimatedTime { get; set; }
    }

    [XmlRoot(ElementName = "FlightTime", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FlightTime
    {
        [XmlElement(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedTime EstimatedTime { get; set; }
    }

    [XmlRoot(ElementName = "TaxiInTime", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TaxiInTime
    {
        [XmlElement(ElementName = "EstimatedTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public EstimatedTime EstimatedTime { get; set; }
    }

    [XmlRoot(ElementName = "FlightPlanSummary", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FlightPlanSummary
    {
        [XmlElement(ElementName = "ScheduledTimeOfArrival", Namespace = "http://aeec.aviation-ia.net/633")]
        public string ScheduledTimeOfArrival { get; set; }

        [XmlElement(ElementName = "OutTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public OutTime OutTime { get; set; }

        [XmlElement(ElementName = "OffTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public OffTime OffTime { get; set; }

        [XmlElement(ElementName = "OnTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public OnTime OnTime { get; set; }

        [XmlElement(ElementName = "InTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public InTime InTime { get; set; }

        [XmlElement(ElementName = "BlockTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public BlockTime BlockTime { get; set; }

        [XmlElement(ElementName = "TaxiOutTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public TaxiOutTime TaxiOutTime { get; set; }

        [XmlElement(ElementName = "FlightTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public FlightTime FlightTime { get; set; }

        [XmlElement(ElementName = "TaxiInTime", Namespace = "http://aeec.aviation-ia.net/633")]
        public TaxiInTime TaxiInTime { get; set; }
    }

    [XmlRoot(ElementName = "AdequateAirport", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AdequateAirport
    {
        [XmlElement(ElementName = "AirportICAOCode", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AirportICAOCode { get; set; }

        [XmlAttribute(AttributeName = "airportFunction")]
        public string AirportFunction { get; set; }
    }

    [XmlRoot(ElementName = "AdequateAirports", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AdequateAirports
    {
        [XmlElement(ElementName = "AdequateAirport", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<AdequateAirport> AdequateAirport { get; set; }
    }

    [XmlRoot(ElementName = "ETOPSSummary", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ETOPSSummary
    {
        [XmlElement(ElementName = "AdequateAirports", Namespace = "http://aeec.aviation-ia.net/633")]
        public AdequateAirports AdequateAirports { get; set; }

        [XmlAttribute(AttributeName = "borderTime")]
        public string BorderTime { get; set; }

        [XmlAttribute(AttributeName = "ruleTime")]
        public string RuleTime { get; set; }
    }

    [XmlRoot(ElementName = "AlternateRoute", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AlternateRoute
    {
        [XmlElement(ElementName = "Waypoints", Namespace = "http://aeec.aviation-ia.net/633")]
        public Waypoints Waypoints { get; set; }

        [XmlElement(ElementName = "Airport", Namespace = "http://aeec.aviation-ia.net/633")]
        public Airport Airport { get; set; }

        [XmlElement(ElementName = "RouteInformation", Namespace = "http://aeec.aviation-ia.net/633")]
        public RouteInformation RouteInformation { get; set; }
    }

    [XmlRoot(ElementName = "AlternateRoutes", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AlternateRoutes
    {
        [XmlElement(ElementName = "AlternateRoute", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<AlternateRoute> AlternateRoute { get; set; }
    }

    [XmlRoot(ElementName = "TerminalProcedure", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TerminalProcedure
    {
        [XmlAttribute(AttributeName = "procedureType")]
        public string ProcedureType { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "SuitablePeriod", Namespace = "http://aeec.aviation-ia.net/633")]
    public class SuitablePeriod
    {
        [XmlAttribute(AttributeName = "from")] public string From { get; set; }

        [XmlAttribute(AttributeName = "until")]
        public string Until { get; set; }
    }

    [XmlRoot(ElementName = "RequiredHorizontalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RequiredHorizontalVisibility
    {
        [XmlAttribute(AttributeName = "unit")] public string Unit { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "RequiredVerticalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RequiredVerticalVisibility
    {
        [XmlAttribute(AttributeName = "unit")] public string Unit { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "RequiredWeatherCondition", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RequiredWeatherCondition
    {
        [XmlElement(ElementName = "RequiredHorizontalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
        public RequiredHorizontalVisibility RequiredHorizontalVisibility { get; set; }

        [XmlElement(ElementName = "RequiredVerticalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
        public RequiredVerticalVisibility RequiredVerticalVisibility { get; set; }
    }

    [XmlRoot(ElementName = "ConsideredHorizontalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ConsideredHorizontalVisibility
    {
        [XmlAttribute(AttributeName = "unit")] public string Unit { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "ConsideredVerticalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ConsideredVerticalVisibility
    {
        [XmlAttribute(AttributeName = "unit")] public string Unit { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "ConsideredWeatherCondition", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ConsideredWeatherCondition
    {
        [XmlElement(ElementName = "ConsideredHorizontalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
        public ConsideredHorizontalVisibility ConsideredHorizontalVisibility { get; set; }

        [XmlElement(ElementName = "ConsideredVerticalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
        public ConsideredVerticalVisibility ConsideredVerticalVisibility { get; set; }
    }

    [XmlRoot(ElementName = "AirportData", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AirportData
    {
        [XmlElement(ElementName = "Airport", Namespace = "http://aeec.aviation-ia.net/633")]
        public Airport Airport { get; set; }

        [XmlElement(ElementName = "PlannedRunway", Namespace = "http://aeec.aviation-ia.net/633")]
        public string PlannedRunway { get; set; }

        [XmlElement(ElementName = "TerminalProcedure", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<TerminalProcedure> TerminalProcedure { get; set; }

        [XmlElement(ElementName = "SuitablePeriod", Namespace = "http://aeec.aviation-ia.net/633")]
        public SuitablePeriod SuitablePeriod { get; set; }

        [XmlElement(ElementName = "RequiredWeatherCondition", Namespace = "http://aeec.aviation-ia.net/633")]
        public RequiredWeatherCondition RequiredWeatherCondition { get; set; }

        [XmlElement(ElementName = "ConsideredWeatherCondition", Namespace = "http://aeec.aviation-ia.net/633")]
        public ConsideredWeatherCondition ConsideredWeatherCondition { get; set; }
    }

    [XmlRoot(ElementName = "AirportDataList", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AirportDataList
    {
        [XmlElement(ElementName = "AirportData", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<AirportData> AirportData { get; set; }
    }

    [XmlRoot(ElementName = "AnalysedFuelValue", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AnalysedFuelValue
    {
        [XmlAttribute(AttributeName = "elapsedTime")]
        public string ElapsedTime { get; set; }

        [XmlAttribute(AttributeName = "unit")] public string Unit { get; set; }

        [XmlAttribute(AttributeName = "analysedFuelText")]
        public string AnalysedFuelText { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "FuelStatistic", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FuelStatistic
    {
        [XmlElement(ElementName = "AnalysedFuelCategory", Namespace = "http://aeec.aviation-ia.net/633")]
        public string AnalysedFuelCategory { get; set; }

        [XmlElement(ElementName = "NumberOfConsideredFlights", Namespace = "http://aeec.aviation-ia.net/633")]
        public string NumberOfConsideredFlights { get; set; }

        [XmlElement(ElementName = "AnalysedFuelValue", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<AnalysedFuelValue> AnalysedFuelValue { get; set; }

        [XmlElement(ElementName = "Remark", Namespace = "http://aeec.aviation-ia.net/633")]
        public Remark Remark { get; set; }
    }

    [XmlRoot(ElementName = "FuelStatistics", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FuelStatistics
    {
        [XmlElement(ElementName = "FuelStatistic", Namespace = "http://aeec.aviation-ia.net/633")]
        public FuelStatistic FuelStatistic { get; set; }
    }

    [XmlRoot(ElementName = "TankeringAdvice", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TankeringAdvice
    {
        [XmlAttribute(AttributeName = "priority")]
        public string Priority { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "TankeringWeight", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TankeringWeight
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "TankeringProfit", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TankeringProfit
    {
        [XmlAttribute(AttributeName = "unit")] public string Unit { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "TankeringInfo", Namespace = "http://aeec.aviation-ia.net/633")]
    public class TankeringInfo
    {
        [XmlElement(ElementName = "TankeringAdvice", Namespace = "http://aeec.aviation-ia.net/633")]
        public TankeringAdvice TankeringAdvice { get; set; }

        [XmlElement(ElementName = "TankeringWeight", Namespace = "http://aeec.aviation-ia.net/633")]
        public TankeringWeight TankeringWeight { get; set; }

        [XmlElement(ElementName = "TankeringProfit", Namespace = "http://aeec.aviation-ia.net/633")]
        public TankeringProfit TankeringProfit { get; set; }
    }

    [XmlRoot(ElementName = "FlightPlan", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Arinc633FlightPlanning
    {
        [XmlElement(ElementName = "M633Header", Namespace = "http://aeec.aviation-ia.net/633")]
        public M633Header M633Header { get; set; }

        [XmlElement(ElementName = "M633SupplementaryHeader", Namespace = "http://aeec.aviation-ia.net/633")]
        public M633SupplementaryHeader M633SupplementaryHeader { get; set; }

        [XmlElement(ElementName = "FlightInfo", Namespace = "http://aeec.aviation-ia.net/633")]
        public FlightInfo FlightInfo { get; set; }

        [XmlElement(ElementName = "Remarks", Namespace = "http://aeec.aviation-ia.net/633")]
        public RemarkList Remarks { get; set; }

        [XmlElement(ElementName = "FlightPlanHeader", Namespace = "http://aeec.aviation-ia.net/633")]
        public FlightPlanHeader FlightPlanHeader { get; set; }

        [XmlElement(ElementName = "FuelHeader", Namespace = "http://aeec.aviation-ia.net/633")]
        public FuelHeader FuelHeader { get; set; }

        [XmlElement(ElementName = "WeightHeader", Namespace = "http://aeec.aviation-ia.net/633")]
        public WeightHeader WeightHeader { get; set; }

        [XmlElement(ElementName = "Waypoints", Namespace = "http://aeec.aviation-ia.net/633")]
        public Waypoints Waypoints { get; set; }

        [XmlElement(ElementName = "FlightPlanSummary", Namespace = "http://aeec.aviation-ia.net/633")]
        public FlightPlanSummary FlightPlanSummary { get; set; }

        [XmlElement(ElementName = "NonStandardFlightPlanningType", Namespace = "http://aeec.aviation-ia.net/633")]
        public NonStandardFlightPlanningType NonStandardFlightPlanningType { get; set; }

        [XmlElement(ElementName = "ETOPSSummary", Namespace = "http://aeec.aviation-ia.net/633")]
        public ETOPSSummary ETOPSSummary { get; set; }

        [XmlElement(ElementName = "AlternateRoutes", Namespace = "http://aeec.aviation-ia.net/633")]
        public AlternateRoutes AlternateRoutes { get; set; }

        [XmlElement(ElementName = "AirportDataList", Namespace = "http://aeec.aviation-ia.net/633")]
        public AirportDataList AirportDataList { get; set; }

        [XmlElement(ElementName = "FuelStatistics", Namespace = "http://aeec.aviation-ia.net/633")]
        public FuelStatistics FuelStatistics { get; set; }

        [XmlElement(ElementName = "TankeringInfo", Namespace = "http://aeec.aviation-ia.net/633")]
        public TankeringInfo TankeringInfo { get; set; }

        [XmlAttribute(AttributeName = "flightPlanId")]
        public string FlightPlanId { get; set; }

        [XmlAttribute(AttributeName = "category")]
        public string Category { get; set; }

        [XmlAttribute(AttributeName = "computedTime")]
        public string ComputedTime { get; set; }
    }

    [XmlRoot(ElementName = "NOTAMSubjects", Namespace = "http://aeec.aviation-ia.net/633")]
    public class NOTAMSubjects
    {
        [XmlElement(ElementName = "NOTAMSubject", Namespace = "http://aeec.aviation-ia.net/633")]
        public string NOTAMSubject { get; set; }
    }

    [XmlRoot(ElementName = "NOTAMText", Namespace = "http://aeec.aviation-ia.net/633")]
    public class NOTAMText
    {
        [XmlElement(ElementName = "Paragraph", Namespace = "http://aeec.aviation-ia.net/633")]
        public Paragraph Paragraph { get; set; }
    }

    [XmlRoot(ElementName = "Airports", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Airports
    {
        [XmlElement(ElementName = "Airport", Namespace = "http://aeec.aviation-ia.net/633")]
        public Airport Airport { get; set; }
    }

    [XmlRoot(ElementName = "Keys", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Keys
    {
        [XmlElement(ElementName = "Airports", Namespace = "http://aeec.aviation-ia.net/633")]
        public Airports Airports { get; set; }

        [XmlElement(ElementName = "Airspaces", Namespace = "http://aeec.aviation-ia.net/633")]
        public Airspaces Airspaces { get; set; }
    }

    //[XmlRoot(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
    //public class Value
    //{
    //  [XmlAttribute(AttributeName = "unit")]
    //  public string Unit { get; set; }
    //  [XmlText]
    //  public string Text { get; set; }
    //}

    [XmlRoot(ElementName = "Upper", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Upper
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Lower", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Lower
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Altitudes", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Altitudes
    {
        [XmlElement(ElementName = "Upper", Namespace = "http://aeec.aviation-ia.net/633")]
        public Upper Upper { get; set; }

        [XmlElement(ElementName = "Lower", Namespace = "http://aeec.aviation-ia.net/633")]
        public Lower Lower { get; set; }
    }

    [XmlRoot(ElementName = "ICAONOTAMInformation", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ICAONOTAMInformation
    {
        [XmlElement(ElementName = "ItemA", Namespace = "http://aeec.aviation-ia.net/633")]
        public string ItemA { get; set; }

        [XmlElement(ElementName = "ItemB", Namespace = "http://aeec.aviation-ia.net/633")]
        public string ItemB { get; set; }

        [XmlElement(ElementName = "ItemC", Namespace = "http://aeec.aviation-ia.net/633")]
        public string ItemC { get; set; }

        [XmlAttribute(AttributeName = "qcode1")]
        public string QCode1 { get; set; }

        [XmlAttribute(AttributeName = "qcode2")]
        public string QCode2 { get; set; }

        [XmlAttribute(AttributeName = "trafficIndicator")]
        public string TrafficIndicator { get; set; }

        [XmlAttribute(AttributeName = "purpose")]
        public string Purpose { get; set; }

        [XmlAttribute(AttributeName = "scope")]
        public string Scope { get; set; }

        [XmlAttribute(AttributeName = "lowerAlt")]
        public string LowerAlt { get; set; }

        [XmlAttribute(AttributeName = "upperAlt")]
        public string UpperAlt { get; set; }

        [XmlAttribute(AttributeName = "fIR")] public string Fir { get; set; }

        [XmlElement(ElementName = "ItemD", Namespace = "http://aeec.aviation-ia.net/633")]
        public string ItemD { get; set; }

        [XmlElement(ElementName = "ItemF", Namespace = "http://aeec.aviation-ia.net/633")]
        public string ItemF { get; set; }

        [XmlElement(ElementName = "ItemG", Namespace = "http://aeec.aviation-ia.net/633")]
        public string ItemG { get; set; }
    }

    //[XmlRoot(ElementName = "Coordinates", Namespace = "http://aeec.aviation-ia.net/633")]
    //public class Coordinates
    //{
    //  [XmlAttribute(AttributeName = "latitude")]
    //  public string Latitude { get; set; }
    //  [XmlAttribute(AttributeName = "longitude")]
    //  public string Longitude { get; set; }
    //}

    [XmlRoot(ElementName = "Radius", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Radius
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Spot", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Spot
    {
        [XmlElement(ElementName = "Coordinates", Namespace = "http://aeec.aviation-ia.net/633")]
        public Coordinates Coordinates { get; set; }

        [XmlElement(ElementName = "Radius", Namespace = "http://aeec.aviation-ia.net/633")]
        public Radius Radius { get; set; }
    }

    [XmlRoot(ElementName = "Geography", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Geography
    {
        [XmlElement(ElementName = "Spot", Namespace = "http://aeec.aviation-ia.net/633")]
        public Spot Spot { get; set; }
    }

    [XmlRoot(ElementName = "Location", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Location
    {
        [XmlElement(ElementName = "Geography", Namespace = "http://aeec.aviation-ia.net/633")]
        public Geography Geography { get; set; }

        [XmlElement(ElementName = "Airspaces", Namespace = "http://aeec.aviation-ia.net/633")]
        public Airspaces Airspaces { get; set; }
    }

    [XmlRoot(ElementName = "NOTAM", Namespace = "http://aeec.aviation-ia.net/633")]
    public class NOTAM
    {
        [XmlElement(ElementName = "NOTAMSubjects", Namespace = "http://aeec.aviation-ia.net/633")]
        public NOTAMSubjects NOTAMSubjects { get; set; }

        [XmlElement(ElementName = "NOTAMText", Namespace = "http://aeec.aviation-ia.net/633")]
        public NOTAMText NOTAMText { get; set; }

        [XmlElement(ElementName = "Keys", Namespace = "http://aeec.aviation-ia.net/633")]
        public Keys Keys { get; set; }

        [XmlElement(ElementName = "Altitudes", Namespace = "http://aeec.aviation-ia.net/633")]
        public Altitudes Altitudes { get; set; }

        [XmlElement(ElementName = "ICAONOTAMInformation", Namespace = "http://aeec.aviation-ia.net/633")]
        public ICAONOTAMInformation ICAONOTAMInformation { get; set; }

        [XmlElement(ElementName = "Location", Namespace = "http://aeec.aviation-ia.net/633")]
        public Location Location { get; set; }

        [XmlAttribute(AttributeName = "source")]
        public string Source { get; set; }

        [XmlAttribute(AttributeName = "series")]
        public string Series { get; set; }

        [XmlAttribute(AttributeName = "year")] public string Year { get; set; }

        [XmlAttribute(AttributeName = "startValidTime")]
        public string StartValidTime { get; set; }

        [XmlAttribute(AttributeName = "endValidTime")]
        public string EndValidTime { get; set; }

        [XmlAttribute(AttributeName = "creationTime")]
        public string CreationTime { get; set; }

        [XmlAttribute(AttributeName = "revisionTime")]
        public string RevisionTime { get; set; }

        [XmlAttribute(AttributeName = "consideredInFlightPlan")]
        public string ConsideredInFlightPlan { get; set; }

        [XmlAttribute(AttributeName = "issuer")]
        public string Issuer { get; set; }

        [XmlAttribute(AttributeName = "serial")]
        public string Serial { get; set; }

        [XmlElement(ElementName = "Remark", Namespace = "http://aeec.aviation-ia.net/633")]
        public Remark Remark { get; set; }

        [XmlElement(ElementName = "OtherNotamInformation", Namespace = "http://www.lido.net/lsya")]
        public OtherNotamInformation OtherNotamInformation { get; set; }
    }

    [XmlRoot(ElementName = "OtherNotamInformation", Namespace = "http://www.lido.net/lsya")]
    public class OtherNotamInformation
    {
        [XmlAttribute(AttributeName = "notamClass")]
        public string NotamClass { get; set; }

        [XmlElement(ElementName = "BriefingSection", Namespace = "http://www.lido.net/lsya")]
        public BriefingSection BriefingSection { get; set; }

        [XmlElement(ElementName = "ChartNotam", Namespace = "http://www.lido.net/lsya")]
        public ChartNotam ChartNotam { get; set; }
    }

    [XmlRoot(ElementName = "BriefingSection", Namespace = "http://www.lido.net/lsya")]
    public class BriefingSection
    {
        [XmlAttribute(AttributeName = "sectionName")]
        public string SectionName { get; set; }
    }

    [XmlRoot(ElementName = "ChartNotam", Namespace = "http://www.lido.net/lsya")]
    public class ChartNotam
    {
        [XmlAttribute(AttributeName = "chart")]
        public bool Chart { get; set; }
    }

    [XmlRoot(ElementName = "Airspaces", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Airspaces
    {
        [XmlElement(ElementName = "Airspace", Namespace = "http://aeec.aviation-ia.net/633")]
        public Airspace Airspace { get; set; }
    }

    [XmlRoot(ElementName = "NOTAMs", Namespace = "http://aeec.aviation-ia.net/633")]
    public class NOTAMs
    {
        [XmlElement(ElementName = "NOTAM", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<NOTAM> NOTAM { get; set; }
    }

    [XmlRoot(ElementName = "NOTAMBriefing", Namespace = "http://aeec.aviation-ia.net/633")]
    public class NOTAMBriefing
    {
        [XmlElement(ElementName = "M633Header", Namespace = "http://aeec.aviation-ia.net/633")]
        public M633Header M633Header { get; set; }

        [XmlElement(ElementName = "M633SupplementaryHeader", Namespace = "http://aeec.aviation-ia.net/633")]
        public M633SupplementaryHeader M633SupplementaryHeader { get; set; }

        [XmlElement(ElementName = "NOTAMs", Namespace = "http://aeec.aviation-ia.net/633")]
        public NOTAMs NOTAMs { get; set; }

        [XmlAttribute(AttributeName = "briefingType")]
        public string BriefingType { get; set; }

        [XmlAttribute(AttributeName = "creationTime")]
        public string CreationTime { get; set; }

        [XmlAttribute(AttributeName = "fullPackage")]
        public string FullPackage { get; set; }
    }

    [XmlRoot(ElementName = "Directions", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Directions
    {
        [XmlElement(ElementName = "Direction", Namespace = "http://aeec.aviation-ia.net/633")]
        public Direction Direction { get; set; }
        [XmlElement(ElementName = "Variable", Namespace = "http://aeec.aviation-ia.net/633")]
        public string Variable { get; set; }
    }


    [XmlRoot(ElementName = "Speeds", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Speeds
    {
        [XmlElement(ElementName = "Speed", Namespace = "http://aeec.aviation-ia.net/633")]
        public Speed Speed { get; set; }
        [XmlElement(ElementName = "Calm", Namespace = "http://aeec.aviation-ia.net/633")]
        public string Calm { get; set; }
        [XmlElement(ElementName = "Gusts", Namespace = "http://aeec.aviation-ia.net/633")]
        public Gusts Gusts { get; set; }
    }

    [XmlRoot(ElementName = "SurfaceWind", Namespace = "http://aeec.aviation-ia.net/633")]
    public class SurfaceWind
    {
        [XmlElement(ElementName = "Directions", Namespace = "http://aeec.aviation-ia.net/633")]
        public Directions Directions { get; set; }
        [XmlElement(ElementName = "Speeds", Namespace = "http://aeec.aviation-ia.net/633")]
        public Speeds Speeds { get; set; }
    }

    [XmlRoot(ElementName = "SurfaceWinds", Namespace = "http://aeec.aviation-ia.net/633")]
    public class SurfaceWinds
    {
        [XmlElement(ElementName = "SurfaceWind", Namespace = "http://aeec.aviation-ia.net/633")]
        public SurfaceWind SurfaceWind { get; set; }
    }

    [XmlRoot(ElementName = "PrevailingVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
    public class PrevailingVisibility
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Visibilities", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Visibilities
    {
        [XmlElement(ElementName = "PrevailingVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
        public PrevailingVisibility PrevailingVisibility { get; set; }
    }

    [XmlRoot(ElementName = "VerticalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
    public class VerticalVisibility
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Ceiling", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Ceiling
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "CloudDescription", Namespace = "http://aeec.aviation-ia.net/633")]
    public class CloudDescription
    {
        [XmlElement(ElementName = "Ceiling", Namespace = "http://aeec.aviation-ia.net/633")]
        public Ceiling Ceiling { get; set; }
        [XmlAttribute(AttributeName = "cloudCover")]
        public string CloudCover { get; set; }
        [XmlAttribute(AttributeName = "cloudType")]
        public string CloudType { get; set; }
    }

    [XmlRoot(ElementName = "CloudDescriptions", Namespace = "http://aeec.aviation-ia.net/633")]
    public class CloudDescriptions
    {
        [XmlElement(ElementName = "CloudDescription", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<CloudDescription> CloudDescription { get; set; }
    }

    [XmlRoot(ElementName = "Clouds", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Clouds
    {
        [XmlElement(ElementName = "VerticalVisibility", Namespace = "http://aeec.aviation-ia.net/633")]
        public VerticalVisibility VerticalVisibility { get; set; }
        [XmlElement(ElementName = "CloudDescriptions", Namespace = "http://aeec.aviation-ia.net/633")]
        public CloudDescriptions CloudDescriptions { get; set; }
        [XmlElement(ElementName = "NilSignificantClouds", Namespace = "http://aeec.aviation-ia.net/633")]
        public string NilSignificantClouds { get; set; }
    }

    [XmlRoot(ElementName = "Weather", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Weather
    {
        [XmlAttribute(AttributeName = "intensity")]
        public string Intensity { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "VisibilitiesAndWeather", Namespace = "http://aeec.aviation-ia.net/633")]
    public class VisibilitiesAndWeather
    {
        [XmlElement(ElementName = "Visibilities", Namespace = "http://aeec.aviation-ia.net/633")]
        public Visibilities Visibilities { get; set; }
        [XmlElement(ElementName = "Clouds", Namespace = "http://aeec.aviation-ia.net/633")]
        public Clouds Clouds { get; set; }
        [XmlElement(ElementName = "Weather", Namespace = "http://aeec.aviation-ia.net/633")]
        public Weather Weather { get; set; }
    }

    [XmlRoot(ElementName = "AirTemperature", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AirTemperature
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "DewPointTemperature", Namespace = "http://aeec.aviation-ia.net/633")]
    public class DewPointTemperature
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Temperatures", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Temperatures
    {
        [XmlElement(ElementName = "AirTemperature", Namespace = "http://aeec.aviation-ia.net/633")]
        public AirTemperature AirTemperature { get; set; }
        [XmlElement(ElementName = "DewPointTemperature", Namespace = "http://aeec.aviation-ia.net/633")]
        public DewPointTemperature DewPointTemperature { get; set; }
    }

    [XmlRoot(ElementName = "QNH", Namespace = "http://aeec.aviation-ia.net/633")]
    public class QNH
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "Pressures", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Pressures
    {
        [XmlElement(ElementName = "QNH", Namespace = "http://aeec.aviation-ia.net/633")]
        public QNH QNH { get; set; }
    }

    [XmlRoot(ElementName = "ObservationDetails", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ObservationDetails
    {
        [XmlElement(ElementName = "SurfaceWinds", Namespace = "http://aeec.aviation-ia.net/633")]
        public SurfaceWinds SurfaceWinds { get; set; }
        [XmlElement(ElementName = "VisibilitiesAndWeather", Namespace = "http://aeec.aviation-ia.net/633")]
        public VisibilitiesAndWeather VisibilitiesAndWeather { get; set; }
        [XmlElement(ElementName = "Temperatures", Namespace = "http://aeec.aviation-ia.net/633")]
        public Temperatures Temperatures { get; set; }
        [XmlElement(ElementName = "Pressures", Namespace = "http://aeec.aviation-ia.net/633")]
        public Pressures Pressures { get; set; }
    }

    [XmlRoot(ElementName = "ObservationText", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ObservationText
    {
        [XmlElement(ElementName = "Paragraph", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<Paragraph> Paragraph { get; set; }
    }

    [XmlRoot(ElementName = "Observation", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Observation
    {
        [XmlElement(ElementName = "ObservationDetails", Namespace = "http://aeec.aviation-ia.net/633")]
        public ObservationDetails ObservationDetails { get; set; }

        [XmlElement(ElementName = "ObservationText", Namespace = "http://aeec.aviation-ia.net/633")]
        public ObservationText ObservationText { get; set; }

        [XmlAttribute(AttributeName = "observationTime")]
        public string ObservationTime { get; set; }

        [XmlAttribute(AttributeName = "observationType")]
        public string ObservationType { get; set; }
    }

    [XmlRoot(ElementName = "WeatherBulletin", Namespace = "http://aeec.aviation-ia.net/633")]
    public class WeatherBulletin
    {
        [XmlElement(ElementName = "Airport", Namespace = "http://aeec.aviation-ia.net/633")]
        public Airport Airport { get; set; }

        [XmlElement(ElementName = "Observation", Namespace = "http://aeec.aviation-ia.net/633")]
        public Observation Observation { get; set; }

        [XmlAttribute(AttributeName = "sequence")]
        public string Sequence { get; set; }

        [XmlElement(ElementName = "Forecast", Namespace = "http://aeec.aviation-ia.net/633")]
        public Forecast Forecast { get; set; }
    }

    [XmlRoot(ElementName = "ForecastGeneral", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ForecastGeneral
    {
        [XmlElement(ElementName = "SurfaceWind", Namespace = "http://aeec.aviation-ia.net/633")]
        public SurfaceWind SurfaceWind { get; set; }
        [XmlElement(ElementName = "VisibilitiesAndWeather", Namespace = "http://aeec.aviation-ia.net/633")]
        public VisibilitiesAndWeather VisibilitiesAndWeather { get; set; }
    }

    [XmlRoot(ElementName = "WeatherChange", Namespace = "http://aeec.aviation-ia.net/633")]
    public class WeatherChange
    {
        [XmlElement(ElementName = "SurfaceWind", Namespace = "http://aeec.aviation-ia.net/633")]
        public SurfaceWind SurfaceWind { get; set; }
        [XmlElement(ElementName = "VisibilitiesAndWeather", Namespace = "http://aeec.aviation-ia.net/633")]
        public VisibilitiesAndWeather VisibilitiesAndWeather { get; set; }
        [XmlAttribute(AttributeName = "becoming")]
        public string Becoming { get; set; }
        [XmlAttribute(AttributeName = "startTime")]
        public string StartTime { get; set; }
        [XmlAttribute(AttributeName = "endTime")]
        public string EndTime { get; set; }
        [XmlAttribute(AttributeName = "from")]
        public string From { get; set; }
    }

    [XmlRoot(ElementName = "WeatherChanges", Namespace = "http://aeec.aviation-ia.net/633")]
    public class WeatherChanges
    {
        [XmlElement(ElementName = "WeatherChange", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<WeatherChange> WeatherChange { get; set; }
    }

    [XmlRoot(ElementName = "ForecastDetails", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ForecastDetails
    {
        [XmlElement(ElementName = "ForecastGeneral", Namespace = "http://aeec.aviation-ia.net/633")]
        public ForecastGeneral ForecastGeneral { get; set; }
        [XmlElement(ElementName = "WeatherChanges", Namespace = "http://aeec.aviation-ia.net/633")]
        public WeatherChanges WeatherChanges { get; set; }
    }

    [XmlRoot(ElementName = "ForecastText", Namespace = "http://aeec.aviation-ia.net/633")]
    public class ForecastText
    {
        [XmlElement(ElementName = "Paragraph", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<Paragraph> Paragraph { get; set; }
    }

    [XmlRoot(ElementName = "Forecast", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Forecast
    {
        [XmlElement(ElementName = "ForecastDetails", Namespace = "http://aeec.aviation-ia.net/633")]
        public ForecastDetails ForecastDetails { get; set; }
        [XmlElement(ElementName = "ForecastText", Namespace = "http://aeec.aviation-ia.net/633")]
        public ForecastText ForecastText { get; set; }
        [XmlAttribute(AttributeName = "forecastTime")]
        public string ForecastTime { get; set; }
        [XmlAttribute(AttributeName = "forecastType")]
        public string ForecastType { get; set; }
        [XmlAttribute(AttributeName = "forecastStartTime")]
        public string ForecastStartTime { get; set; }
        [XmlAttribute(AttributeName = "forecastEndTime")]
        public string ForecastEndTime { get; set; }
    }

    [XmlRoot(ElementName = "Gusts", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Gusts
    {
        [XmlElement(ElementName = "Value", Namespace = "http://aeec.aviation-ia.net/633")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "WeatherBulletins", Namespace = "http://aeec.aviation-ia.net/633")]
    public class WeatherBulletins
    {
        [XmlElement(ElementName = "WeatherBulletin", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<WeatherBulletin> WeatherBulletin { get; set; }
    }

    [XmlRoot(ElementName = "AirportWeather", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AirportWeather
    {
        [XmlElement(ElementName = "M633Header", Namespace = "http://aeec.aviation-ia.net/633")]
        public M633Header M633Header { get; set; }

        [XmlElement(ElementName = "M633SupplementaryHeader", Namespace = "http://aeec.aviation-ia.net/633")]
        public M633SupplementaryHeader M633SupplementaryHeader { get; set; }

        [XmlElement(ElementName = "WeatherBulletins", Namespace = "http://aeec.aviation-ia.net/633")]
        public WeatherBulletins WeatherBulletins { get; set; }

        [XmlAttribute(AttributeName = "creationTime")]
        public string CreationTime { get; set; }

        [XmlAttribute(AttributeName = "fullPackage")]
        public string FullPackage { get; set; }
    }

    [XmlRoot(ElementName = "RegionWeatherBriefing", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RegionWeatherBriefing
    {
        [XmlElement(ElementName = "M633Header", Namespace = "http://aeec.aviation-ia.net/633")]
        public M633Header M633Header { get; set; }

        [XmlElement(ElementName = "M633SupplementaryHeader", Namespace = "http://aeec.aviation-ia.net/633")]
        public M633SupplementaryHeader M633SupplementaryHeader { get; set; }

        [XmlElement(ElementName = "RegionWeathers", Namespace = "http://aeec.aviation-ia.net/633")]
        public RegionWeathers RegionWeathers { get; set; }

        [XmlAttribute(AttributeName = "creationTime")]
        public string CreationTime { get; set; }

        [XmlAttribute(AttributeName = "fullPackage")]
        public string FullPackage { get; set; }
    }

    [XmlRoot(ElementName = "RegionWeathers", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RegionWeathers
    {
        [XmlElement(ElementName = "RegionWeather", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<RegionWeather> RegionWeather { get; set; }
    }

    [XmlRoot(ElementName = "RegionWeather", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RegionWeather
    {
        [XmlElement(ElementName = "RegionWeatherText", Namespace = "http://aeec.aviation-ia.net/633")]
        public RegionWeatherText RegionWeatherText { get; set; }

        [XmlElement(ElementName = "Location", Namespace = "http://aeec.aviation-ia.net/633")]
        public Location Location { get; set; }

        [XmlAttribute(AttributeName = "issuer")]
        public string Issuer { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "sequence")]
        public string Sequence { get; set; }

        [XmlAttribute(AttributeName = "startValidTime")]
        public string StartValidTime { get; set; }

        [XmlAttribute(AttributeName = "endValidTime")]
        public string EndValidTime { get; set; }

        [XmlAttribute(AttributeName = "observationTime")]
        public string ObservationTime { get; set; }
    }

    [XmlRoot(ElementName = "RegionWeatherText", Namespace = "http://aeec.aviation-ia.net/633")]
    public class RegionWeatherText
    {
        [XmlElement(ElementName = "Paragraph", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<Paragraph> Paragraph { get; set; }
    }

    [XmlRoot(ElementName = "FlightPlanAtcIcao", Namespace = "http://aeec.aviation-ia.net/633")]
    public class FlightPlanAtcIcao
    {
        [XmlElement(ElementName = "M633Header", Namespace = "http://aeec.aviation-ia.net/633")]
        public M633Header M633Header { get; set; }

        [XmlElement(ElementName = "M633SupplementaryHeader", Namespace = "http://aeec.aviation-ia.net/633")]
        public M633SupplementaryHeader M633SupplementaryHeader { get; set; }

        [XmlElement(ElementName = "AtcMessageText", Namespace = "http://aeec.aviation-ia.net/633")]
        public AtcMessageText AtcMessageText { get; set; }

        [XmlElement(ElementName = "AtcMessageDetails", Namespace = "http://aeec.aviation-ia.net/633")]
        public AtcMessageDetails AtcMessageDetails { get; set; }

        [XmlAttribute(AttributeName = "flightPlanId")]
        public string FlightPlanId { get; set; }
    }

    [XmlRoot(ElementName = "AtcMessageText", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AtcMessageText
    {
        [XmlElement(ElementName = "Paragraph", Namespace = "http://aeec.aviation-ia.net/633")]
        public List<Paragraph> Paragraph { get; set; }
    }

    [XmlRoot(ElementName = "AtcMessageDetails", Namespace = "http://aeec.aviation-ia.net/633")]
    public class AtcMessageDetails
    {
        [XmlAttribute(AttributeName = "messageType")]
        public string MessageType { get; set; }

        [XmlAttribute(AttributeName = "messagePriority")]
        public string MessagePriority { get; set; }

        [XmlElement(ElementName = "Addressees", Namespace = "http://aeec.aviation-ia.net/633")]
        public Addressees Addressees { get; set; }

        [XmlElement(ElementName = "Originator", Namespace = "http://aeec.aviation-ia.net/633")]
        public Originator Originator { get; set; }

        [XmlElement(ElementName = "Items", Namespace = "http://aeec.aviation-ia.net/633")]
        public Items Items { get; set; }
    }

    [XmlRoot(ElementName = "Addressees", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Addressees
    {
        [XmlAttribute(AttributeName = "addresses")]
        public string Addresses { get; set; }
    }

    [XmlRoot(ElementName = "Originator", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Originator
    {
        [XmlAttribute(AttributeName = "address")]
        public string Address { get; set; }
    }

    [XmlRoot(ElementName = "Items", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Items
    {
        [XmlElement(ElementName = "Item7", Namespace = "http://aeec.aviation-ia.net/633")]
        public Item7 Item7 { get; set; }

        [XmlElement(ElementName = "Item8", Namespace = "http://aeec.aviation-ia.net/633")]
        public Item8 Item8 { get; set; }

        [XmlElement(ElementName = "Item9", Namespace = "http://aeec.aviation-ia.net/633")]
        public Item9 Item9 { get; set; }

        [XmlElement(ElementName = "Item10", Namespace = "http://aeec.aviation-ia.net/633")]
        public Item10 Item10 { get; set; }

        [XmlElement(ElementName = "Item13", Namespace = "http://aeec.aviation-ia.net/633")]
        public Item13 Item13 { get; set; }

        [XmlElement(ElementName = "Item15", Namespace = "http://aeec.aviation-ia.net/633")]
        public Item15 Item15 { get; set; }

        [XmlElement(ElementName = "Item16", Namespace = "http://aeec.aviation-ia.net/633")]
        public Item16 Item16 { get; set; }

        [XmlElement(ElementName = "Item18", Namespace = "http://aeec.aviation-ia.net/633")]
        public Item18 Item18 { get; set; }

        [XmlElement(ElementName = "Item19", Namespace = "http://aeec.aviation-ia.net/633")]
        public Item19 Item19 { get; set; }
    }

    [XmlRoot(ElementName = "Item7", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Item7
    {
        [XmlAttribute(AttributeName = "aTCCallsign")]
        public string Callsign { get; set; }
    }

    [XmlRoot(ElementName = "Item8", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Item8
    {
        [XmlAttribute(AttributeName = "flightRules")]
        public string FlightRules { get; set; }

        [XmlAttribute(AttributeName = "typeOfFlight")]
        public string FlightType { get; set; }
    }

    [XmlRoot(ElementName = "Item9", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Item9
    {
        [XmlAttribute(AttributeName = "typeOfAircraft")]
        public string AircraftType { get; set; }

        [XmlAttribute(AttributeName = "wakeTurbulence")]
        public string WakeTurbulence { get; set; }
    }

    [XmlRoot(ElementName = "Item10", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Item10
    {
        [XmlAttribute(AttributeName = "aircraftEquipment")]
        public string AircraftEquipment { get; set; }
    }

    [XmlRoot(ElementName = "Item13", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Item13
    {
        [XmlAttribute(AttributeName = "departureAirport")]
        public string DepartureAirport { get; set; }

        [XmlAttribute(AttributeName = "departureTime")]
        public string DepartureTime { get; set; }
    }

    [XmlRoot(ElementName = "Item15", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Item15
    {
        [XmlAttribute(AttributeName = "cruisingSpeed")]
        public string CruisingSpeed { get; set; }

        [XmlAttribute(AttributeName = "cruisingLevel")]
        public string CruisingLevel { get; set; }

        [XmlAttribute(AttributeName = "route")]
        public string Route { get; set; }
    }

    [XmlRoot(ElementName = "Item16", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Item16
    {
        [XmlAttribute(AttributeName = "arrivalAirport")]
        public string ArrivalAirport { get; set; }

        [XmlAttribute(AttributeName = "estimatedFlightTime")]
        public string EstimatedFlightTime { get; set; }

        [XmlAttribute(AttributeName = "alternateAirport")]
        public string AlternateAirport { get; set; }
    }

    [XmlRoot(ElementName = "Item18", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Item18
    {
        [XmlElement(ElementName = "EET", Namespace = "http://aeec.aviation-ia.net/633")]
        public string EET { get; set; }

        [XmlElement(ElementName = "REG", Namespace = "http://aeec.aviation-ia.net/633")]
        public string REG { get; set; }

        [XmlElement(ElementName = "SEL", Namespace = "http://aeec.aviation-ia.net/633")]
        public string SEL { get; set; }

        [XmlElement(ElementName = "OPR", Namespace = "http://aeec.aviation-ia.net/633")]
        public string OPR { get; set; }

        [XmlElement(ElementName = "DOF", Namespace = "http://aeec.aviation-ia.net/633")]
        public string DOF { get; set; }

        [XmlElement(ElementName = "PER", Namespace = "http://aeec.aviation-ia.net/633")]
        public string PER { get; set; }

        [XmlElement(ElementName = "CODE", Namespace = "http://aeec.aviation-ia.net/633")]
        public string CODE { get; set; }

        [XmlElement(ElementName = "RMK", Namespace = "http://aeec.aviation-ia.net/633")]
        public string RMK { get; set; }
    }

    [XmlRoot(ElementName = "Item19", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Item19
    {
        [XmlElement(ElementName = "E", Namespace = "http://aeec.aviation-ia.net/633")]
        public string E { get; set; }

        [XmlElement(ElementName = "P", Namespace = "http://aeec.aviation-ia.net/633")]
        public string P { get; set; }

        [XmlElement(ElementName = "R", Namespace = "http://aeec.aviation-ia.net/633")]
        public string R { get; set; }

        [XmlElement(ElementName = "S", Namespace = "http://aeec.aviation-ia.net/633")]
        public string S { get; set; }

        [XmlElement(ElementName = "J", Namespace = "http://aeec.aviation-ia.net/633")]
        public string J { get; set; }

        [XmlElement(ElementName = "D", Namespace = "http://aeec.aviation-ia.net/633")]
        public string D { get; set; }

        [XmlElement(ElementName = "A", Namespace = "http://aeec.aviation-ia.net/633")]
        public string A { get; set; }

        [XmlElement(ElementName = "C", Namespace = "http://aeec.aviation-ia.net/633")]
        public string C { get; set; }
    }

    [XmlRoot(ElementName = "EFUSUB", Namespace = "http://aeec.aviation-ia.net/633")]
    public class EFUSUB
    {
        [XmlAttribute(AttributeName = "fullPackage")]
        public bool FullPackage { get; set; }

        [XmlElement(ElementName = "M633Header")]
        public M633Header M633Header { get; set; }

        [XmlElement(ElementName = "M633SupplementaryHeader")]
        public M633SupplementaryHeader M633SupplementaryHeader { get; set; }

        [XmlElement(ElementName = "SubFolder")]
        public SubFolder SubFolder { get; set; }
    }

    [XmlRoot(ElementName = "SubFolder", Namespace = "http://aeec.aviation-ia.net/633")]
    public class SubFolder
    {
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "SubFolder")]
        public List<SubFolder> SubFolders { get; set; }

        [XmlElement(ElementName = "Topic")]
        public List<Topic> Topics { get; set; }

        [XmlElement(ElementName = "Document")]
        public List<Document> Documents { get; set; }
    }

    [XmlRoot(ElementName = "Topic", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Topic
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "Document", Namespace = "http://aeec.aviation-ia.net/633")]
    public class Document
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "updateDateTime")]
        public string UpdateDateTime { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "file")]
        public string File { get; set; }

        [XmlElement(ElementName = "Topic")]
        public Topic Topic { get; set; }
    }
}
