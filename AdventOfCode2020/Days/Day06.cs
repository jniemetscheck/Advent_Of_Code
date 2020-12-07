using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day06
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day06.txt";

        public static int GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath);

            var groups = GetMappedGroups(input.ToList());

            return GetAnyoneYesAnswerCount(groups);
        }

        public static int GetResultPartTwo()
        {
            var input = File.ReadAllLines(FilePath);

            var groups = GetMappedGroups(input.ToList());

            return GetEveryoneYesAnswerCount(groups);
        }

        public static int GetAnyoneYesAnswerCount(List<Group> groups)
        {
            var count = 0;

            foreach (var group in groups)
            {
                var yesAnswers = string.Empty;

                foreach (var person in group.People)
                {
                    foreach (var answer in person.YesAnswers)
                    {
                        if (!yesAnswers.Contains(answer))
                        {
                            yesAnswers += answer;
                        }
                    }
                }

                count += yesAnswers.Length;
            }

            return count;
        }

        public static int GetEveryoneYesAnswerCount(List<Group> groups)
        {
            var count = 0;

            foreach (var group in groups)
            {
                var everyoneAnsweredYes = string.Empty;
                var yesAnswers = string.Empty;

                foreach (var person in group.People)
                {
                    foreach (var answer in person.YesAnswers)
                    {
                        if (!yesAnswers.Contains(answer))
                        {
                            yesAnswers += answer;
                        }
                    }
                }

                foreach (var yesAnswer in yesAnswers)
                {
                    var found = true;

                    foreach (var person in group.People)
                    {
                        if (!person.YesAnswers.Contains(yesAnswer.ToString()))
                        {
                            found = false;
                            break;
                        }
                    }

                    if (found)
                    {
                        everyoneAnsweredYes += yesAnswer;
                    }
                }

                count += everyoneAnsweredYes.Length;
            }

            return count;
        }

        public static List<Group> GetMappedGroups(List<string> input)
        {
            var groups = new List<Group>();
            var group = new Group();
            group.People = new List<Person>();
            
            foreach (var groupLine in input)
            {
                if (!string.IsNullOrWhiteSpace(groupLine))
                {
                    var person = new Person();
                    person.YesAnswers = groupLine.Select(a => a.ToString()).ToList();
                    group.People.Add(person);
                }
                else
                {
                    groups.Add(group);
                    group = new Group();
                    group.People = new List<Person>();
                }
            }

            groups.Add(group);

            return groups;
        }
    }

    public class Group
    {
        public List<Person> People { get; set; }
    }

    public class Person
    {
        public List<string> YesAnswers { get; set; }
    }
}
