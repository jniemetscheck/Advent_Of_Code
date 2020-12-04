using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day04
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day04.txt";

        public static int GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath);

            var passports = GetMappedPassports(input.ToList());

            return GetValidPassportCount(passports);
        }

        public static int GetValidPassportCount(List<Passport> passports)
        {
            var count = 0;

            foreach (var passport in passports)
            {
                if (IsPassportValid(passport))
                {
                    count++;
                }
            }

            return count;
        }

        public static List<Passport> GetMappedPassports(List<string> input)
        {
            var passports = new List<Passport>();
            var initialPosition = 0;

            for (var i = 0; i < input.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(input[i]) || i == input.Count - 1)
                {
                    //new passport, construct object
                    var passportString = string.Empty;

                    var lastLine = i == input.Count - 1;

                    for (var j = initialPosition; j < (lastLine ? i + 1 : i); j++)
                    {
                        //reconstruct passport
                        passportString += input[j];

                        if (j != (lastLine ? i : i - 1))
                        {
                            passportString += " ";
                        }
                    }

                    var passportStringSplitSpaces = passportString.Split(" ".ToCharArray());

                    var passport = new Passport();

                    foreach (var passportStringSplitSpace in passportStringSplitSpaces)
                    {
                        var splitField = passportStringSplitSpace.Split(":".ToCharArray());

                        switch (splitField[0])
                        {
                            case "byr":
                                passport.BirthYear = splitField[1];
                                break;
                            case "iyr":
                                passport.IssueYear = splitField[1];
                                break;
                            case "eyr":
                                passport.ExpirationYear = splitField[1];
                                break;
                            case "hgt":
                                passport.Height = splitField[1];
                                break;
                            case "hcl":
                                passport.HairColor = splitField[1];
                                break;
                            case "ecl":
                                passport.EyeColor = splitField[1];
                                break;
                            case "pid":
                                passport.PasspportId = splitField[1];
                                break;
                            case "cid":
                                passport.CountryId = splitField[1];
                                break;
                        }
                    }

                    passports.Add(passport);

                    initialPosition = i + 1;
                }
            }

            return passports;
        }

        public static bool IsPassportValid(Passport passport)
        {
            if (string.IsNullOrWhiteSpace(passport.BirthYear) || string.IsNullOrWhiteSpace(passport.IssueYear) || string.IsNullOrWhiteSpace(passport.ExpirationYear) || string.IsNullOrWhiteSpace(passport.Height) || string.IsNullOrWhiteSpace(passport.HairColor) || string.IsNullOrWhiteSpace(passport.EyeColor) || string.IsNullOrWhiteSpace(passport.PasspportId))
            {
                return false;
            }

            return true;
        }
    }

    public class Passport
    {
        public string BirthYear { get; set; }
        public string IssueYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PasspportId { get; set; }
        public string CountryId { get; set; }
    }
}
