using System.Xml.Serialization;
using Ionic.Zip;
using ArincConverter.Contracts;
using ArincConverter.Models;
using ArincConverter.Helpers;
using System.Text;
using static ArincConverter.Helpers.Constants;

namespace ArincConverter.Services
{
    public class ArincService : IArincService
    {
        private readonly IPdfService _pdfService;
        private User _user;

        public ArincService(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public FlightPlan GetFlightPlan(byte[] flightPlanData, User user)
        {
            var flightPlan = new FlightPlan();
            ZipFile unzippedDatFolder = null;
            _user = user;

            using (Stream stream = new MemoryStream(flightPlanData))
            {
                using (ZipFile zip = ZipFile.Read(stream))
                {
                    if (!zip.Any(x => x.FileName.EndsWith(".dat", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        throw new Exception($"The specified compressed file could not be uploaded.\nIt should contain a file with extension dat");
                    }

                    var datZipFile = zip.FirstOrDefault(f => f.FileName.EndsWith(".dat", StringComparison.InvariantCultureIgnoreCase));
                    using (var datStream = new MemoryStream())
                    {
                        datZipFile.Extract(datStream);
                        datStream.Seek(0, SeekOrigin.Begin);
                        unzippedDatFolder = ZipFile.Read(datStream);

                        if (unzippedDatFolder != null)
                        {
                            var flightPlanXmlFile = unzippedDatFolder.FirstOrDefault(x => x.FileName.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase) && x.FileName.Contains("-std", StringComparison.InvariantCultureIgnoreCase));
                            flightPlan = TransformFlightPlan(flightPlanXmlFile);
                            var log = GetFlightPlanDocument(unzippedDatFolder.FirstOrDefault(x => x.FileName.EndsWith(".pdf", StringComparison.InvariantCultureIgnoreCase)));
                            var wx = GetWeatherDocument(unzippedDatFolder.Where(x => x.FileName.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase) && new string[] { Constants.Document.Type.AirportWeather, Constants.Document.Type.RegionWeather }.Any(x.FileName.Contains)).ToList());
                            var charts = GetChartDocuments(flightPlan.ThirdPartyScheduleId, unzippedDatFolder.Where(x => new string[] { ".jpeg", ".gif" }.Any(x.FileName.ToLower().EndsWith)).ToList());
                            var notam = GetNotamDocument(unzippedDatFolder.FirstOrDefault(x => x.FileName.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase) && x.FileName.Contains(Constants.Document.Type.NOTAM, StringComparison.InvariantCultureIgnoreCase)));
                            var shortAtc = GetShortAtcDocument(unzippedDatFolder.FirstOrDefault(x => x.FileName.Contains(Constants.Document.Type.ATCFlightPlan, StringComparison.InvariantCultureIgnoreCase)));
                            var additionalDocuments = GetAdditionalDocuments(unzippedDatFolder.Where(x => x.FileName.EndsWith(".txt", StringComparison.InvariantCultureIgnoreCase)));

                            flightPlan.Notams = TransformNotams(unzippedDatFolder.FirstOrDefault(x => x.FileName.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase) && x.FileName.Contains(Constants.Document.Type.NOTAM, StringComparison.InvariantCultureIgnoreCase)), flightPlanXmlFile, unzippedDatFolder.FirstOrDefault(x => x.FileName.Contains(Constants.Document.Type.AirportWeather, StringComparison.InvariantCultureIgnoreCase)));
                            flightPlan.FlightDocuments = new FlightDocuments
                            {
                                FlightPlan = log,
                                Weather = wx,
                                Notam = notam,
                                ShortAtc = shortAtc,
                                Charts = charts,
                                AdditionalDocuments = additionalDocuments
                            };
                        }
                    }
                }
            }

            return flightPlan;
        }

        private FlightPlan TransformFlightPlan(ZipEntry file)
        {
            var flightPlan = new FlightPlan();

            var arincPlan = TransformXmlFile<ArincFlightBriefing>(file, new XmlSerializer(typeof(ArincFlightBriefing)));
            if (arincPlan == null)
                return flightPlan;

            var primaryArrivalAlternateAirport = arincPlan.AirportDataList?.AirportData?.FirstOrDefault(x => x.Airport.AirportFunction == Sabre.AlternateAirport.PrimaryArrivalAlternateAirport)?.Airport;
            var secondaryAlternateArrivalAirport = arincPlan.AirportDataList?.AirportData?.FirstOrDefault(x => x.Airport.AirportFunction == Sabre.AlternateAirport.SecondaryArrivalAlternateAirport)?.Airport;
            var tertiaryAlternateArrivalAirport = arincPlan.AirportDataList?.AirportData?.Where(x => x.Airport.AirportFunction == Sabre.AlternateAirport.SecondaryArrivalAlternateAirport)?.ElementAtOrDefault(1)?.Airport;
            var quaternaryAlternateArrivalAirport = arincPlan.AirportDataList?.AirportData?.Where(x => x.Airport.AirportFunction == Sabre.AlternateAirport.SecondaryArrivalAlternateAirport)?.ElementAtOrDefault(2)?.Airport;
            var alternateDepartureAirport = arincPlan.AirportDataList?.AirportData?.FirstOrDefault(x => x.Airport.AirportFunction == Sabre.AlternateAirport.DepartureAlternateAirport || x.Airport.AirportFunction == Sabre.AlternateAirport.TakeOffAlternate)?.Airport;
            var enrouteAlternateAirport = arincPlan.AirportDataList?.AirportData?.FirstOrDefault(x => x.Airport.AirportFunction == Sabre.AlternateAirport.EnrouteAlternateAirport)?.Airport;
            var contingencySavingAirport = arincPlan.AirportDataList?.AirportData?.FirstOrDefault(x => x.Airport.AirportFunction == Sabre.AlternateAirport.ContingencySavingAirport)?.Airport;
            var primaryContingencySavingAlternate = arincPlan.AirportDataList?.AirportData?.FirstOrDefault(x => x.Airport.AirportFunction == Sabre.AlternateAirport.PrimaryContingencySavingAlternate)?.Airport;
            var flightIdentification = arincPlan.M633SupplementaryHeader?.Flight?.FlightIdentification?.FlightNumber?.CommercialFlightNumber;
            var callSign = arincPlan.FlightInfo?.ATCCallsign;
            var dep = arincPlan.M633SupplementaryHeader?.Flight?.DepartureAirport?.AirportICAOCode;
            var arr = arincPlan.M633SupplementaryHeader?.Flight?.ArrivalAirport?.AirportICAOCode;
            var flightNumber = arincPlan.M633SupplementaryHeader?.Flight?.FlightIdentification?.FlightNumber?.Number;
            var flightLogId = callSign != nameof(flightIdentification)
                    ? $"{flightIdentification}-{flightNumber}-({callSign})-{dep}-{arr}"
                    : $"{flightIdentification}-{flightNumber}-{dep}-{arr}";
            var airline = arincPlan.M633SupplementaryHeader?.Flight?.FlightIdentification?.FlightNumber?.AirlineIATACode;
            var std = arincPlan.M633SupplementaryHeader?.Flight?.ScheduledTimeOfDeparture?.ToUniversalTime(DateTimeFormat.Default);
            var primaryAlternateFuel = arincPlan.FuelHeader?.AlternateFuels?.AlternateFuel?.ElementAtOrDefault(0);
            var altFuel = Convert.ToDouble(primaryAlternateFuel?.EstimatedWeight?.Value?.Text);
            var alt2Fuel = Convert.ToDouble(arincPlan.FuelHeader?.AlternateFuels?.AlternateFuel?.ElementAtOrDefault(1)?.EstimatedWeight?.Value?.Text);
            var alt3Fuel = Convert.ToDouble(arincPlan.FuelHeader?.AlternateFuels?.AlternateFuel?.ElementAtOrDefault(2)?.EstimatedWeight?.Value?.Text);
            var alt4Fuel = Convert.ToDouble(arincPlan.FuelHeader?.AlternateFuels?.AlternateFuel?.ElementAtOrDefault(3)?.EstimatedWeight?.Value?.Text);
            var remarks = arincPlan.Remarks?.Remark?.FirstOrDefault(x => x.RemarkType == "general")?.Paragraph?.Text;
            var extraFuel = (Airline)_user.AirlineId == Airline.EnterAir
                ? (arincPlan.FuelHeader?.AdditionalFuels?.AdditionalFuel?.Sum(x => Convert.ToDouble(x.EstimatedWeight?.Value?.Text))
                    + arincPlan.FuelHeader?.ExtraFuels?.ExtraFuel?.Sum(x => Convert.ToDouble(x.EstimatedWeight?.Value?.Text))) ?? 0
                : arincPlan.FuelHeader?.ExtraFuels?.ExtraFuel?.Sum(x => Convert.ToDouble(x.EstimatedWeight?.Value?.Text)) ?? 0;
            var flightId = Guid.NewGuid().ToString();

            flightPlan = new FlightPlan
            {
                AirlineId = _user.AirlineId,
                Remarks = remarks,
                ThirdPartyPlanId = arincPlan.FlightPlanId,
                ThirdPartyScheduleId = flightId,
                FlightLogId = flightLogId,
                CommercialFlightNumber = flightIdentification,
                AircraftRegistration = arincPlan.M633SupplementaryHeader?.Aircraft?.AircraftRegistration,
                ScheduledTimeDeparture = std,
                ScheduledTimeArrival = arincPlan.FlightPlanSummary?.ScheduledTimeOfArrival?.ToUniversalTime(DateTimeFormat.Default),
                TotalFuel = Convert.ToDouble(arincPlan.FuelHeader?.BlockFuel?.EstimatedWeight?.Value?.Text),
                TaxiFuel = Convert.ToDouble(arincPlan.FuelHeader?.TaxiFuel?.EstimatedWeight?.Value?.Text),
                TripFuel = Convert.ToDouble(arincPlan.FuelHeader?.TripFuel?.EstimatedWeight?.Value?.Text),
                ContingencyFuel = Convert.ToDouble(arincPlan.FuelHeader?.ContingencyFuel?.EstimatedWeight?.Value?.Text),
                FinalReserveFuel = Convert.ToDouble(arincPlan.FuelHeader?.FinalReserve?.EstimatedWeight?.Value?.Text ?? primaryAlternateFuel?.FinalReserve?.EstimatedWeight?.Value?.Text),
                ETOPSFuel = Convert.ToDouble(arincPlan.FuelHeader?.ETOPSFuel?.EstimatedWeight?.Value?.Text),
                ExtraFuel = arincPlan.FuelHeader?.ExtraFuels?.ExtraFuel?.Sum(x => Convert.ToDouble(x.EstimatedWeight?.Value?.Text)) ?? 0,
                TakeOffFuel = Convert.ToDouble(arincPlan.FuelHeader?.TakeOffFuel?.EstimatedWeight?.Value?.Text),
                ArrivalFuel = Convert.ToDouble(arincPlan.FuelHeader?.ArrivalFuel?.EstimatedWeight?.Value?.Text),
                AdditionalFuel = arincPlan.FuelHeader?.AdditionalFuels?.AdditionalFuel?.Sum(x => Convert.ToDouble(x.EstimatedWeight?.Value?.Text)) ?? 0,
                PrimaryAlternateFuel = altFuel,
                SecondaryAlternateFuel = altFuel == alt2Fuel ? 0 : alt2Fuel,
                TertiaryAlternateFuel = alt3Fuel == alt2Fuel ? 0 : alt3Fuel,
                QuaternaryAlternateFuel = alt4Fuel == alt3Fuel ? 0 : alt4Fuel,
                MaximumTakeOffMass = Convert.ToDouble(arincPlan.WeightHeader?.TakeoffWeight?.StructuralLimit?.Value?.Text),
                Cargo = Convert.ToDouble(arincPlan.WeightHeader?.Load?.EstimatedWeight?.Value?.Text),
                LastEditDate = arincPlan.M633Header?.Timestamp?.ToUniversalTime(DateTimeFormat.Default),
                RoutePoints = TransformRoutePoints(arincPlan),
                Route = arincPlan.FlightPlanHeader?.RouteInformation?.RouteDescription,
                AircraftType = arincPlan.M633SupplementaryHeader?.Aircraft?.AircraftModel?.AircraftICAOType,
                EstimatedElapsedTime = arincPlan.FuelHeader?.TripFuel?.Duration?.Value?.ToTimeSpan(),
                Endurance = arincPlan.FuelHeader?.TakeOffFuel?.Duration?.Value?.ToTimeSpan(),
                CallSign = callSign,
                DryOperatingWeight = Convert.ToDouble(arincPlan.WeightHeader?.DryOperatingWeight?.EstimatedWeight?.Value?.Text),
                Load = Convert.ToDouble(arincPlan.WeightHeader?.Load?.EstimatedWeight?.Value?.Text),
                ZeroFuelWeight = Convert.ToDouble(arincPlan.WeightHeader?.ZeroFuelWeight?.EstimatedWeight?.Value?.Text),
                TakeoffWeight = Convert.ToDouble(arincPlan.WeightHeader?.TakeoffWeight?.EstimatedWeight?.Value?.Text),
                LandingWeight = Convert.ToDouble(arincPlan.WeightHeader?.LandingWeight?.EstimatedWeight?.Value?.Text),
                MaxZeroFuelWeightStructural = Convert.ToDouble(arincPlan.WeightHeader?.ZeroFuelWeight?.StructuralLimit?.Value?.Text),
                MaxZeroFuelWeightOperational = Convert.ToDouble(arincPlan.WeightHeader?.ZeroFuelWeight?.OperationalLimit?.Value?.Text),
                MaxTakeOffWeightStructural = Convert.ToDouble(arincPlan.WeightHeader?.TakeoffWeight?.StructuralLimit?.Value?.Text),
                MaxTakeOffWeightOperational = Convert.ToDouble(arincPlan.WeightHeader?.TakeoffWeight?.OperationalLimit?.Value?.Text),
                MaxLandingWeightStructural = Convert.ToDouble(arincPlan.WeightHeader?.LandingWeight?.StructuralLimit?.Value?.Text),
                MaxLandingWeightOperational = Convert.ToDouble(arincPlan.WeightHeader?.LandingWeight?.OperationalLimit?.Value?.Text),
                Airports = new List<FlightAirport>
                {
                    new()
                    {
                        IATA = arincPlan.M633SupplementaryHeader?.Flight?.DepartureAirport?.AirportIATACode,
                        ICAO = arincPlan.M633SupplementaryHeader?.Flight?.DepartureAirport?.AirportICAOCode,
                        Name = arincPlan.M633SupplementaryHeader?.Flight?.DepartureAirport?.AirportName,
                        Category = arincPlan.M633SupplementaryHeader?.Flight?.DepartureAirport?.AirportCategory,
                        Type = LocationType.Departure
                    },
                    new()
                    {
                        IATA = arincPlan.M633SupplementaryHeader?.Flight?.ArrivalAirport?.AirportIATACode,
                        ICAO = arincPlan.M633SupplementaryHeader?.Flight?.ArrivalAirport?.AirportICAOCode,
                        Name = arincPlan.M633SupplementaryHeader?.Flight?.ArrivalAirport?.AirportName,
                        Category = arincPlan.M633SupplementaryHeader?.Flight?.ArrivalAirport?.AirportCategory,
                        Type = LocationType.Arrival
                    },
                    new()
                    {
                        IATA = primaryArrivalAlternateAirport?.AirportIATACode,
                        ICAO = primaryArrivalAlternateAirport?.AirportICAOCode,
                        Name = primaryArrivalAlternateAirport?.AirportName,
                        Type = LocationType.ArrivalAlternatePrimary
                    },
                    new()
                    {
                        IATA = secondaryAlternateArrivalAirport?.AirportIATACode,
                        ICAO = secondaryAlternateArrivalAirport?.AirportICAOCode,
                        Name = secondaryAlternateArrivalAirport?.AirportName,
                        Type = LocationType.ArrivalAlternateSecondary
                    },
                    new()
                    {
                        IATA = tertiaryAlternateArrivalAirport?.AirportIATACode,
                        ICAO = tertiaryAlternateArrivalAirport?.AirportICAOCode,
                        Name = tertiaryAlternateArrivalAirport?.AirportName,
                        Type = LocationType.ArrivalAlternateTertiary
                    },
                    new()
                    {
                        IATA = quaternaryAlternateArrivalAirport?.AirportIATACode,
                        ICAO = quaternaryAlternateArrivalAirport?.AirportICAOCode,
                        Name = quaternaryAlternateArrivalAirport?.AirportName,
                        Type = LocationType.ArrivalAlternateQuaternary
                    },
                    new()
                    {
                        IATA = alternateDepartureAirport?.AirportIATACode,
                        ICAO = alternateDepartureAirport?.AirportICAOCode,
                        Name = alternateDepartureAirport?.AirportName,
                        Type = LocationType.DepartureAlternate
                    },
                    new()
                    {
                        IATA = primaryContingencySavingAlternate?.AirportIATACode,
                        ICAO = primaryContingencySavingAlternate?.AirportICAOCode,
                        Name = primaryContingencySavingAlternate?.AirportName,
                        Type = LocationType.ContingencySaving
                    },
                    new()
                    {
                        IATA = contingencySavingAirport?.AirportIATACode,
                        ICAO = contingencySavingAirport?.AirportICAOCode,
                        Name = contingencySavingAirport?.AirportName,
                        Type = LocationType.ContingencySavingAlternate
                    },
                    new()
                    {
                        IATA = enrouteAlternateAirport?.AirportIATACode,
                        ICAO = enrouteAlternateAirport?.AirportICAOCode,
                        Name = enrouteAlternateAirport?.AirportName,
                        Type = LocationType.EnrouteAlternate
                    }
                }
            };
            return flightPlan;
        }

        public List<RoutePoint> TransformRoutePoints(ArincFlightBriefing fp)
        {
            var routePoints = new List<RoutePoint>();
            foreach (var locationType in RoutePoints.DefaultAirports)
            {
                var waypoints = locationType switch
                {
                    LocationType.ArrivalAlternatePrimary => fp.AlternateRoutes?.AlternateRoute?.ElementAtOrDefault(0)?.Waypoints,
                    LocationType.ArrivalAlternateSecondary => fp.AlternateRoutes?.AlternateRoute?.ElementAtOrDefault(1)?.Waypoints,
                    _ => fp.Waypoints,
                };

                if (waypoints != null)
                {
                    var hasTOC = waypoints.Waypoint?.Any(td => td.WaypointName == "TOC") ?? false;
                    var extraFuelTotal = (Airline)_user.AirlineId == Airline.EnterAir
                        ? (fp.FuelHeader?.AdditionalFuels?.AdditionalFuel?.Sum(x => Convert.ToDouble(x.EstimatedWeight?.Value?.Text))
                            + fp.FuelHeader?.ExtraFuels?.ExtraFuel?.Sum(x => Convert.ToDouble(x.EstimatedWeight?.Value?.Text))) ?? 0
                        : fp.FuelHeader?.ExtraFuels?.ExtraFuel?.Sum(x => Convert.ToDouble(x.EstimatedWeight?.Value?.Text)) ?? 0;
                    var contingencyFuel = Convert.ToInt32(fp.FuelHeader?.ContingencyFuel?.EstimatedWeight?.Value?.Text);
                    var blockFuel = Convert.ToInt32(fp.FuelHeader?.BlockFuel?.EstimatedWeight?.Value?.Text);
                    var additionalFuel = fp.FuelHeader?.AdditionalFuels?.AdditionalFuel?.Sum(x => Convert.ToDouble(x.EstimatedWeight?.Value?.Text)) ?? 0;
                    var flightTime = fp.FlightPlanSummary?.FlightTime?.EstimatedTime?.Value?.ToTimeSpan();
                    double accumulatedBurnOff = 0;
                    var accumulatedDistance = 0;
                    TimeSpan? accumulatedTime = new TimeSpan(0, 0, 0);

                    var tocReached = false;
                    var todReached = false;

                    foreach (var wp in waypoints.Waypoint)
                    {
                        var burnOff = Convert.ToDouble(wp.CumulatedBurnOff?.EstimatedWeight?.Value?.Text);
                        var legTime = wp.TimeFromPreviousWaypoint?.EstimatedTime?.Value?.ToTimeSpan();
                        accumulatedBurnOff += burnOff;
                        accumulatedDistance += Convert.ToInt32(wp.GroundDistance?.Value?.Text);
                        accumulatedTime = wp.CumulatedFlightTime?.EstimatedTime?.Value?.ToTimeSpan();
                        var remainingFlightTime = flightTime - accumulatedTime;
                        var minReqFuel = Math.Round(Convert.ToDouble(wp.MinimumFuelOnBoard?.Value?.Text) / 1000, 1);
                        var segmentWindComponent = wp.SegmentWindComponent?.Value?.Text ?? wp.SegmentWind?.Component?.Value?.Text;
                        var windDirection = Convert.ToDouble(wp.SegmentWind?.Direction?.Value?.Text);
                        var windSpeed = Convert.ToDouble(wp.SegmentWind?.Speed?.Value?.Text);
                        var windShear = 0;
                        var isa = Convert.ToDouble(wp.SegmentISADeviation?.Value?.Text);
                        var magCourse = (int)Math.Round(Convert.ToDouble(wp.Tracks?.OutboundTrack?.FirstOrDefault(x => x.Value?.Type?.ToLower() == "magnetic")?.Value?.Text), MidpointRounding.AwayFromZero);
                        var trueCourse = (int)Math.Round(Convert.ToDouble(wp.Tracks?.OutboundTrack?.FirstOrDefault(x => x.Value?.Type?.ToLower() == "true")?.Value?.Text), MidpointRounding.AwayFromZero);
                        var variation = trueCourse > magCourse
                                ? $"{trueCourse - magCourse:D2}E"
                                : magCourse > trueCourse
                                        ? $"{magCourse - trueCourse}W"
                                        : string.Empty;

                        var temperature = Convert.ToDouble(wp.SegmentTemperature?.Value?.Text);
                        var temperatureStatus = temperature.ToString();
                        var macStatus = wp.MachNumber?.EstimatedMachNumber?.Value;
                        var altitudeStatus = wp.Altitude?.EstimatedAltitude?.Value?.Text;

                        if (hasTOC)
                        {
                            tocReached |= wp.WaypointName == "TOC";
                            todReached |= wp.WaypointName == "TOD";

                            if (!tocReached)
                            {
                                temperatureStatus = "CLB";
                                macStatus = "CLB";
                                altitudeStatus = "CLB";
                            }
                            else if (todReached)
                            {
                                temperatureStatus = "DSC";
                                macStatus = "DSC";
                                altitudeStatus = "DSC";
                            }
                        }

                        var point = new RoutePoint
                        {
                            WaypointId = wp.WaypointId,
                            Latitude = (double)(wp.Coordinates?.Latitude.TransformCoordinates()),
                            EstimatedBurnOff = accumulatedBurnOff / 1000,
                            Longitude = (double)(wp.Coordinates?.Longitude.TransformCoordinates()),
                            AccumulatedBurnOff = accumulatedBurnOff,
                            Altitude = Convert.ToDouble(wp.Altitude?.EstimatedAltitude?.Value?.Text),
                            AltitudeStatus = altitudeStatus,
                            SequenceId = Convert.ToInt32(wp.SequenceId),
                            RouteType = locationType,
                            WaypointName = wp.WaypointLongName.IsNotNullOrEmpty() ? wp.WaypointLongName : wp.WaypointId,
                            WindDirection = windDirection,
                            WindSpeed = windSpeed,
                            ISADeviation = isa,
                            LegTime = legTime,
                            WindShear = windShear,
                            LegDistance = Math.Round(Convert.ToDouble(wp.GroundDistance?.Value?.Text), 0),
                            FuelUsed = accumulatedBurnOff,
                            Variation = variation,
                            AccumulatedDistance = accumulatedDistance,
                            AccumulatedTime = accumulatedTime,
                            MagneticCourse = magCourse.ToString(),
                            TrueCourse = trueCourse.ToString(),
                            TrueAirSpeed = Convert.ToDouble(wp.TrueAirSpeed?.EstimatedSpeed?.Value?.Text),
                            GroundSpeed = Convert.ToDouble(wp.GroundSpeed?.EstimatedSpeed?.Value?.Text),
                            FuelRemaining = Math.Round(Convert.ToDouble(wp.FuelOnBoard?.EstimatedWeight?.Value?.Text) / 1000, 1),
                            DistanceRemaining = Convert.ToDouble(wp.RemainingGroundDistance?.Value?.Text),
                            TimeRemaining = remainingFlightTime,
                            MinimumRequiredFuel = minReqFuel,
                            Temperature = temperature,
                            TemperatureStatus = temperatureStatus,
                            Frequency = wp.Remarks?.Remark?.RemarkType == "VOR frequency" ? wp.Remarks?.Remark?.Paragraph?.Text : wp.Airspace?.RadioFrequency,
                            Type = wp.Function,
                            CountryCode = wp.CountryICAOCode,
                            MinimumSafeAltitude = wp.MinimumSafeAltitude?.Value?.Text?.AppendZero(3),
                            Airway = wp.Airway?.Text,
                            SegmentVerticalWindChange = wp.SegmentShearRate?.SegmentVerticalWindChange?.Value?.Text?.AppendZero(2),
                            SegmentWindComponent = Convert.ToInt32(segmentWindComponent) <= 0 ? segmentWindComponent : $"+{segmentWindComponent}",
                            BurnOff = burnOff,
                            Mach = Convert.ToDouble(wp.MachNumber?.EstimatedMachNumber?.Value),
                            MachStatus = macStatus,
                            WindInfo = $"{windDirection.ToString().AppendZero(3)}/{windSpeed.ToString().AppendZero(3)}",
                            Tropopause = Math.Round(Convert.ToDouble(wp.Tropopause?.Value?.Text) / 100, 0),
                        };

                        if (point.WaypointName != "Wind data point")
                            routePoints.Add(point);
                    }
                }
            }

            return routePoints;
        }

        public List<Notam> TransformNotams(ZipEntry notamFile, ZipEntry fpFile, ZipEntry wxFile)
        {
            if (notamFile == null)
                return null;

            var notamsList = new List<Notam>();
            var fp = TransformXmlFile<ArincFlightBriefing>(fpFile, new XmlSerializer(typeof(ArincFlightBriefing)));
            var aw = TransformXmlFile<AirportWeather>(wxFile, new XmlSerializer(typeof(AirportWeather)));
            var notams = TransformXmlFile<NOTAMBriefing>(notamFile, new XmlSerializer(typeof(NOTAMBriefing)));
            if (notams == null)
                return notamsList;

            var departure = fp?.M633SupplementaryHeader?.Flight?.DepartureAirport?.AirportICAOCode;
            var arrival = fp?.M633SupplementaryHeader?.Flight?.ArrivalAirport?.AirportICAOCode;

            LocationType GetLocation(string function)
            {
                switch (function)
                {
                    case Constants.Sabre.AlternateAirport.ArrivalAirport:
                        return LocationType.Arrival;

                    case Constants.Sabre.AlternateAirport.PrimaryArrivalAlternateAirport:
                        return LocationType.ArrivalAlternatePrimary;

                    case Constants.Sabre.AlternateAirport.SecondaryArrivalAlternateAirport:
                        return LocationType.ArrivalAlternateSecondary;

                    case Constants.Sabre.AlternateAirport.ContingencySavingAirport:
                        return LocationType.ContingencySaving;

                    case Constants.Sabre.AlternateAirport.DepartureAlternateAirport:
                        return LocationType.DepartureAlternate;

                    case Constants.Sabre.AlternateAirport.FlightInformationRegion:
                        return LocationType.FlightInformationRegion;

                    case Constants.Sabre.AlternateAirport.ETOPSAirport:
                        return LocationType.EnrouteAlternate;

                    case Constants.Document.Type.Company:
                        return LocationType.Company;

                    default:
                        return LocationType.Departure;
                }
            }

            Notam CreateNotam(NOTAM item, string function, string icao)
            {
                return new Notam
                {
                    Number = $"{item.Series}{item.Serial}",
                    Series = item.Series,
                    Serial = item.Serial,
                    Year = item.Year,
                    Fir = item.ICAONOTAMInformation?.Fir,
                    LocationType = GetLocation(function),
                    FromDate = item.StartValidTime?.ToUniversalTime(DateTimeFormat.Default),
                    ToDate = item.EndValidTime?.ToUniversalTime(DateTimeFormat.Default),
                    FromLevel = item.ICAONOTAMInformation?.LowerAlt,
                    ToLevel = item.ICAONOTAMInformation?.UpperAlt,
                    Scope = item.ICAONOTAMInformation?.Scope,
                    Purpose = item.ICAONOTAMInformation?.Purpose,
                    TrafficIndicator = item.ICAONOTAMInformation?.TrafficIndicator,
                    Icao = icao,
                    ACode = item.ICAONOTAMInformation?.ItemA,
                    BCode = item.ICAONOTAMInformation?.ItemB,
                    CCode = item.ICAONOTAMInformation?.ItemC,
                    DCode = item.ICAONOTAMInformation?.ItemD,
                    QCode = item.ICAONOTAMInformation?.QCode1 ?? item.ICAONOTAMInformation?.QCode2,
                    NotamText = $"{item.Series}{item.Serial}/{item.Year} NOTAMN{Environment.NewLine}{item.NOTAMText?.Paragraph?.Text}",
                };
            }

            if (fp.AirportDataList?.AirportData != null)
            {
                Parallel.ForEach(fp.AirportDataList?.AirportData, (data) =>
                {
                    var icao = data?.Airport?.AirportICAOCode;
                    var relatedNotams = notams.NOTAMs?.NOTAM?.Where(x => x.Keys?.Airports?.Airport?.FirstOrDefault(a => a.AirportICAOCode == icao) != null && x.Source != Constants.Document.Type.Company).ToList();
                    var relatedAW = aw?.WeatherBulletins?.WeatherBulletin?.Where(x => x.Airport?.AirportICAOCode == icao).ToList();
                    var function = icao == departure
                                       ? Constants.Sabre.AlternateAirport.DepartureAirport
                                       : icao == arrival
                                            ? Constants.Sabre.AlternateAirport.ArrivalAirport
                                            : data?.Airport?.AirportFunction;

                    foreach (var item in relatedAW)
                    {
                        var weatherText = new StringBuilder();
                        if (item.Observation?.ObservationText?.Paragraph?.FirstOrDefault()?.Text.IsNotNullOrEmpty() == true)
                        {
                            weatherText.AppendLine(item.Observation?.ObservationText?.Paragraph?.FirstOrDefault()?.Text);
                        }
                        weatherText.Append(item.Forecast?.ForecastText?.Paragraph?.FirstOrDefault()?.Text);

                        var newNotam = new Notam
                        {
                            Number = $"WEATHER",
                            Icao = icao,
                            Year = item.Forecast != null ? DateTime.Parse(item.Forecast?.ForecastTime).ToString("yy") : DateTime.Parse(item.Observation?.ObservationTime).ToString("yy"),
                            FromDate = item.Forecast != null ? item.Forecast?.ForecastStartTime?.ToUniversalTime(DateTimeFormat.Default) : item.Observation?.ObservationTime?.ToUniversalTime(DateTimeFormat.Default),
                            ToDate = item.Forecast != null ? item.Forecast?.ForecastEndTime?.ToUniversalTime(DateTimeFormat.Default) : item.Observation?.ObservationTime?.ToUniversalTime(DateTimeFormat.Default),
                            NotamText = $"WEATHER{Environment.NewLine}{weatherText}",
                            LocationType = GetLocation(function)
                        };

                        notamsList.Add(newNotam);
                    }

                    foreach (var item in relatedNotams)
                    {
                        notamsList.Add(CreateNotam(item, function, icao));
                    }
                });

                var firNotams = notams.NOTAMs?.NOTAM?.Where(x => x.NOTAMSubjects?.NOTAMSubject == "Airspace").ToList();
                foreach (var item in firNotams)
                {
                    notamsList.Add(CreateNotam(item, Constants.Sabre.AlternateAirport.FlightInformationRegion, item.Keys?.Airspaces?.Airspace?.FirstOrDefault()?.AirspaceICAOCode));
                }

                var companyNotams = notams.NOTAMs?.NOTAM?.Where(x => x.Source == Constants.Document.Type.Company).ToList();
                foreach (var item in companyNotams)
                {
                    notamsList.Add(CreateNotam(item, Constants.Document.Type.Company, item.Keys?.Airports?.Airport?.FirstOrDefault()?.AirportICAOCode));
                }
            }

            return notamsList;
        }

        private FlightDocument GetFlightPlanDocument(ZipEntry file)
        {
            if (file == null)
                return null;

            using var fileStream = new MemoryStream();
            file.Extract(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);

            return new FlightDocument
            {
                Content = Convert.ToBase64String(fileStream.ToArray()),
                Filename = $"FlightPlan_{_user.AirlineId}_{DateTime.UtcNow:HHmmss}.pdf",
                ContentType = ContentType.Application.Pdf,
                JourneyPart = Sabre.AlternateAirport.DepartureAirport
            };
        }

        private FlightDocument GetFlightPlanXml(ZipEntry file)
        {
            if (file == null)
                return null;

            using var fileStream = new MemoryStream();
            file.Extract(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);

            return new FlightDocument
            {
                Content = Convert.ToBase64String(fileStream.ToArray()),
                Filename = $"FlightPlan_{_user.AirlineId}_{DateTime.UtcNow:HHmmss}.xml",
                ContentType = ContentType.Application.Xml,
                JourneyPart = Sabre.AlternateAirport.DepartureAirport
            };
        }

        private FlightDocument GetShortAtcDocument(ZipEntry file)
        {
            if (file == null)
                return null;

            var shortAtc = TransformXmlFile<FlightPlanAtcIcao>(file, new XmlSerializer(typeof(FlightPlanAtcIcao)));
            if (shortAtc == null)
                return null;

            var text = string.Empty;
            foreach (var atc in shortAtc.AtcMessageText.Paragraph)
            {
                text += atc.Text + Environment.NewLine;
            }

            return new FlightDocument
            {
                Content = _pdfService.TextToPdf("ATC", text),
                Filename = $"ATC_{_user.AirlineId}_{DateTime.UtcNow:HHmmss}.pdf",
                ContentType = ContentType.Application.Pdf,
                JourneyPart = Sabre.AlternateAirport.DepartureAirport
            };
        }

        private List<FlightDocument> GetChartDocuments(string thirdPartyId, List<ZipEntry> files)
        {
            var documents = new List<FlightDocument>();
            var sortedFiles = files
                    .OrderByDescending(o => o.FileName.Contains("SIGWX", StringComparison.InvariantCultureIgnoreCase))
                    .ThenByDescending(t => t.FileName.Contains("UAD", StringComparison.InvariantCultureIgnoreCase))
                    .ThenByDescending(b => b.FileName.EndsWith(".jpeg")).ToList();

            var chartImages = new List<byte[]>();
            foreach (var file in sortedFiles)
            {
                using var fileStream = new MemoryStream();
                file.Extract(fileStream);
                fileStream.Seek(0, SeekOrigin.Begin);
                chartImages.Add(fileStream.ToArray());
            }

            var pdf = _pdfService.ImagesToPdf(chartImages);
            if (pdf.IsNotNullOrEmpty())
            {
                documents.Add(new FlightDocument
                {
                    Content = pdf,
                    Filename = $"RSChart_{thirdPartyId}_{DateTime.UtcNow:yyMMddHHmms}.pdf",
                    ContentType = ContentType.Application.Pdf,
                    JourneyPart = Sabre.AlternateAirport.DepartureAirport
                });
            }
            return documents;
        }

        private FlightDocument GetNotamDocument(ZipEntry file)
        {
            if (file == null)
                return null;

            var notams = TransformXmlFile<NOTAMBriefing>(file, new XmlSerializer(typeof(NOTAMBriefing)));
            if (notams == null)
                return null;

            var notamList = new List<string>();
            var dict = new Dictionary<string, List<string>>();
            var sectionName = "";
            foreach (var notam in notams.NOTAMs?.NOTAM)
            {
                var newSectionName = GetSectionName(notam);
                if (sectionName != newSectionName)
                {
                    if (sectionName.IsNotNullOrEmpty())
                    {
                        if (dict.ContainsKey(sectionName))
                            dict[sectionName].AddRange(notamList);
                        else
                            dict.Add(sectionName, notamList);

                        notamList = new List<string>();
                    }

                    sectionName = newSectionName;
                }

                notamList.Add($"{notam.Series}{notam.Serial}/{notam.Year} NOTAMN{Environment.NewLine}{notam.NOTAMText?.Paragraph?.Text}{Environment.NewLine}{Environment.NewLine}");
            }
            dict.Add(sectionName, notamList);

            return new FlightDocument
            {
                Content = _pdfService.TextToPdf(dict),
                Filename = $"NOTAM_{_user.AirlineId}_{DateTime.UtcNow:HHmmss}.pdf",
                ContentType = ContentType.Application.Pdf,
                JourneyPart = Sabre.AlternateAirport.DepartureAirport
            };
        }

        private string GetSectionName(NOTAM notam)
        {
            var sectionName = notam.OtherNotamInformation?.BriefingSection?.SectionName;

            var airports = notam.Keys?.Airports?.Airport;
            if (airports?.Any() == true)
            {
                foreach (var airport in airports)
                {
                    sectionName += $"  {airport.AirportICAOCode} / {airport.AirportIATACode} {airport.AirportName}";
                }
            }

            var airspaces = notam.Keys?.Airspaces?.Airspace;
            if (airspaces?.Any() == true)
            {
                foreach (var airspace in airspaces)
                {
                    sectionName += $"\n{airspace.AirspaceICAOCode} {airspace.AirspaceName}";
                }
            }

            return sectionName;
        }

        private FlightDocument GetWeatherDocument(List<ZipEntry> files)
        {
            var weatherList = new List<(string, string, string, string)>();

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                using var fileStream = new MemoryStream();
                file.Extract(fileStream);
                fileStream.Seek(0, SeekOrigin.Begin);

                if (file.FileName.Contains(Constants.Document.Type.AirportWeather))
                {
                    var deserializer = new XmlSerializer(typeof(AirportWeather));
                    var textReader = new StreamReader(fileStream);
                    var airportWeather = (AirportWeather)deserializer.Deserialize(textReader);

                    if (airportWeather != null)
                    {
                        for (int i = 0; i < airportWeather.WeatherBulletins?.WeatherBulletin?.Count; i++)
                        {
                            (string h1, string h2, string h3, string text) weather = (string.Empty, string.Empty, string.Empty, string.Empty);
                            var previousBulletin = i > 0 ? airportWeather.WeatherBulletins?.WeatherBulletin[i - 1] : null;
                            var bulletin = airportWeather.WeatherBulletins?.WeatherBulletin[i];

                            var previousAirportFunction = previousBulletin?.Airport?.AirportFunction;
                            var previousAirportName = previousBulletin?.Airport?.AirportName;
                            var airportFunction = bulletin?.Airport?.AirportFunction;
                            var airportName = bulletin?.Airport?.AirportName;
                            var iata = bulletin.Airport?.AirportIATACode;
                            var icao = bulletin.Airport?.AirportICAOCode;
                            var br = Environment.NewLine;
                            var hasObservations = bulletin.Observation != null;
                            var hasForecasts = bulletin.Forecast != null;
                            var header = hasObservations
                                    ? $"Observations ({bulletin.Observation?.ObservationType})"
                                    : hasForecasts
                                            ? $"Forecast ({bulletin.Forecast?.ForecastType})"
                                            : string.Empty;

                            if (airportFunction == previousAirportFunction)
                            {
                                if (airportName != previousAirportName)
                                {
                                    weather.h2 = $"{br}{airportName} {iata}/{icao}{br}";
                                    weather.h3 = $"{br}{header}{br}{br}";
                                }
                                else
                                    weather.h3 = $"{br}{header}{br}{br}";
                            }
                            else
                            {
                                weather.h1 = $"{(i != 0 ? br + br + br : "")}{airportFunction.NormalizeSentence()}";
                                weather.h2 = $"{br}{airportName} {iata}/{icao}{br}";
                                weather.h3 = $"{br}{header}{br}{br}";
                            }

                            if (hasObservations)
                            {
                                foreach (var paragraph in bulletin.Observation?.ObservationText?.Paragraph)
                                    weather.text += $"{paragraph.Text}{br}";
                            }
                            else if (hasForecasts)
                            {
                                foreach (var paragraph in bulletin.Forecast?.ForecastText?.Paragraph)
                                    weather.text += $"{paragraph.Text}{br}";
                            }

                            weatherList.Add(weather);
                        }
                    }
                }
                else
                {
                    var deserializer = new XmlSerializer(typeof(RegionWeatherBriefing));
                    var textReader = new StreamReader(fileStream);
                    var regionWeather = (RegionWeatherBriefing)deserializer.Deserialize(textReader);

                    if (regionWeather != null)
                    {
                        for (int i = 0; i < regionWeather.RegionWeathers?.RegionWeather?.Count; i++)
                        {
                            (string h1, string h2, string h3, string text) weather = (string.Empty, string.Empty, string.Empty, string.Empty);
                            var previousRegion = i > 0 ? regionWeather.RegionWeathers?.RegionWeather[i - 1] : null;
                            var region = regionWeather.RegionWeathers?.RegionWeather[i];

                            var previousIssuer = previousRegion?.Location?.Airspaces?.Airspace?.FirstOrDefault()?.AirspaceICAOCode;
                            var issuer = region?.Location?.Airspaces?.Airspace?.FirstOrDefault()?.AirspaceICAOCode;
                            var type = region?.Type;
                            var br = Environment.NewLine;

                            if (issuer == previousIssuer)
                            {
                                weather.h3 = $"{br}{type}{br}";
                            }
                            else
                            {
                                weather.h2 = $"{br}Airspace name ({issuer})";
                                weather.h3 = $"{br}{type}{br}";
                            }

                            foreach (var paragraph in region.RegionWeatherText?.Paragraph)
                                weather.text += $"{paragraph.Text}{br}";

                            weatherList.Add(weather);
                        }
                    }
                }
            }

            return new FlightDocument
            {
                Content = _pdfService.TextToPdf(weatherList),
                Filename = $"WX_{_user.AirlineId}_{DateTime.UtcNow:HHmmss}.pdf",
                ContentType = ContentType.Application.Pdf,
                JourneyPart = Sabre.AlternateAirport.DepartureAirport
            };
        }

        private List<FlightDocument> GetAdditionalDocuments(IEnumerable<ZipEntry> files)
        {
            var documents = new List<FlightDocument>();
            var crewFiles = files.Where(x => x.FileName.Contains("Crew")).ToList();
            var volcanicAsh = files.FirstOrDefault(x => x.FileName.Contains(Constants.Document.Type.VolcanicAsh));
            var tropicalCyclone = files.FirstOrDefault(x => x.FileName.Contains(Constants.Document.Type.TropicalCyclone));

            FlightDocument GetAdditionalDocument(ZipEntry file)
            {
                using var fileStream = new MemoryStream();
                file.Extract(fileStream);
                fileStream.Seek(0, SeekOrigin.Begin);
                using var reader = new StreamReader(fileStream);
                var text = reader.ReadToEnd();

                return new FlightDocument
                {
                    Content = _pdfService.TextToPdf(file.FileName.Split('.')?[0]?.WithSpaces(), text),
                    Filename = $"{file.FileName.Split('.')?[0]?.WithSpaces()}.pdf",
                    ContentType = ContentType.Application.Pdf,
                    JourneyPart = Sabre.AlternateAirport.DepartureAirport
                };
            }

            foreach (var crewFile in crewFiles)
            {
                var crewPdf = GetAdditionalDocument(crewFile);
                documents.Add(crewPdf);
            }

            var volcanicAshPdf = GetAdditionalDocument(volcanicAsh);
            var tropicalCyclonePdf = GetAdditionalDocument(tropicalCyclone);

            documents.AddRange(new List<FlightDocument> { volcanicAshPdf, tropicalCyclonePdf });
            return documents;
        }

        private T TransformXmlFile<T>(ZipEntry file, XmlSerializer serializer)
        {
            using (var stream = new MemoryStream())
            {
                file.Extract(stream);
                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(stream))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
        }
    }
}
