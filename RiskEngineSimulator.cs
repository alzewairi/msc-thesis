using System;
using System.IO;

namespace RiskEngineSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize variables
            double failedAuthorization = 0,
                   failedAuthentication = 0,
                   clearanceLevel = 0,
                   classificationLevel = 0,
                   timeIndicator = 0;

            // initilize const
            double maxFailedAuthorization = 3,
                   maxFailedAuthentication = 3,
                   maxClearanceLevel = 5,
                   maxClassificationLevel = 5,
                   maxTimeIndicator = 1,
                   maxFactors = 5;

            // initialize risk value
            double riskResult = 0.0;

            // initilize results file
            string outputFilePath = string.Format(@"{0}\results.csv", Environment.CurrentDirectory);

            // write result headers
            File.WriteAllText(outputFilePath, string.Format("Clearance Level,Classification Level,Failed Authorization,Failed Authentication,Time Indicator,Risk Value\n"));

            // simulate clearance level
            for (clearanceLevel = 1; clearanceLevel <= maxClearanceLevel; clearanceLevel++)
            {
                // simulate classification level
                for (classificationLevel = 1; classificationLevel <= maxClassificationLevel; classificationLevel++)
                {
                    failedAuthorization = 0;
                    // simulate 
                    for (failedAuthentication = 0; failedAuthentication <= maxFailedAuthentication; failedAuthentication++)
                    {
                        // simulate time indicator
                        for (timeIndicator = 0; timeIndicator <= maxTimeIndicator; timeIndicator++)
                        {
                            if (clearanceLevel < classificationLevel && timeIndicator < maxTimeIndicator && failedAuthentication == 0)
                                failedAuthorization = 1;
                            else if (failedAuthentication > 0 && timeIndicator < maxTimeIndicator)
                                failedAuthorization = failedAuthentication % (maxFailedAuthorization + 1);

                            riskResult =
                                   CalculateRisk(failedAuthorization, failedAuthentication, clearanceLevel, classificationLevel, timeIndicator,
                                       maxFailedAuthorization, maxFailedAuthentication, maxClearanceLevel, maxClassificationLevel, maxTimeIndicator, maxFactors);

                            File.AppendAllText(outputFilePath, string.Format("{0},{1},{2},{3},{4},{5:0.00}\n", clearanceLevel, classificationLevel, failedAuthorization, failedAuthentication, timeIndicator, riskResult));
                        }
                    }
                }
            }
        }

        static double CalculateRisk(double failedAuthorization, double failedAuthentication, double clearanceLevel, double classificationLevel, double timeIndicator,
            double maxFailedAuthorization, double maxFailedAuthentication, double maxClearanceLevel, double maxClassificationLevel, double maxTimeIndicator, double maxFactors)
        {
            double riskValue = 0;

            riskValue = CalculateSum(
                failedAuthorization / maxFailedAuthorization,
                failedAuthentication / maxFailedAuthentication,
                clearanceLevel / maxClearanceLevel,
                classificationLevel / maxClassificationLevel,
                timeIndicator / maxTimeIndicator
                ) / maxFactors;

            return riskValue * 100;
        }

        static double CalculateSum(params double[] numbers)
        {
            double sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }
    }
}
