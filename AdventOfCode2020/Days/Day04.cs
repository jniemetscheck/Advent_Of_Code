using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

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
                                passport.PassportId = splitField[1];
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
            if (string.IsNullOrWhiteSpace(passport.BirthYear) || string.IsNullOrWhiteSpace(passport.IssueYear) || string.IsNullOrWhiteSpace(passport.ExpirationYear) || string.IsNullOrWhiteSpace(passport.Height) || string.IsNullOrWhiteSpace(passport.HairColor) || string.IsNullOrWhiteSpace(passport.EyeColor) || string.IsNullOrWhiteSpace(passport.PassportId))
            {
                return false;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(passport.BirthYear))
                {
                    if (passport.BirthYear.Length != 4 || int.Parse(passport.BirthYear) < 1920 || int.Parse(passport.BirthYear) > 2002)
                    {
                        return false;
                    }
                }
                if (!string.IsNullOrWhiteSpace(passport.IssueYear))
                {
                    if (passport.IssueYear.Length != 4 || int.Parse(passport.IssueYear) < 2010 || int.Parse(passport.IssueYear) > 2020)
                    {
                        return false;
                    }
                }
                if (!string.IsNullOrWhiteSpace(passport.ExpirationYear))
                {
                    if (passport.ExpirationYear.Length != 4 || int.Parse(passport.ExpirationYear) < 2020 || int.Parse(passport.ExpirationYear) > 2030)
                    {
                        return false;
                    }
                }
                if (!string.IsNullOrWhiteSpace(passport.Height))
                {
                    if (passport.Height.EndsWith("cm"))
                    {
                        var value = passport.Height.Substring(0, passport.Height.Length - 2);

                        if (int.TryParse(value, out var numericValue))
                        {
                            if (numericValue < 150 || numericValue > 193)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (passport.Height.EndsWith("in"))
                    {
                        var value = passport.Height.Substring(0, passport.Height.Length - 2);

                        if (int.TryParse(value, out var numericValue))
                        {
                            if (numericValue < 59 || numericValue > 76)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                if (!string.IsNullOrWhiteSpace(passport.HairColor))
                {
                    if (passport.HairColor.StartsWith("#") && passport.HairColor.Substring(1, passport.HairColor.Length - 1).Length == 6)
                    {
                        string pattern = @"^[a-z0-9]+$";
                        var regex = new Regex(pattern);

                        if (!regex.IsMatch(passport.HairColor.Substring(1, passport.HairColor.Length - 1)))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                if (!string.IsNullOrWhiteSpace(passport.EyeColor))
                {
                    var count = 0;

                    count += Regex.Matches(passport.EyeColor, "amb", RegexOptions.IgnoreCase).Count;
                    count += Regex.Matches(passport.EyeColor, "blu", RegexOptions.IgnoreCase).Count;
                    count += Regex.Matches(passport.EyeColor, "brn", RegexOptions.IgnoreCase).Count;
                    count += Regex.Matches(passport.EyeColor, "gry", RegexOptions.IgnoreCase).Count;
                    count += Regex.Matches(passport.EyeColor, "grn", RegexOptions.IgnoreCase).Count;
                    count += Regex.Matches(passport.EyeColor, "hzl", RegexOptions.IgnoreCase).Count;
                    count += Regex.Matches(passport.EyeColor, "oth", RegexOptions.IgnoreCase).Count;

                    if (count != 1)
                    {
                        return false;
                    }
                }
                if (!string.IsNullOrWhiteSpace(passport.PassportId))
                {
                    if (passport.PassportId.Length == 9)
                    {
                        foreach (var c in passport.PassportId.ToCharArray())
                        {
                            if (!int.TryParse(c.ToString(), out var digit))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
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
        public string PassportId { get; set; }
        public string CountryId { get; set; }
    }
}
