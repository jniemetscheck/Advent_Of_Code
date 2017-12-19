using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2017.Days
{
    public static class Day18
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day18.txt";

        public static long GetPart1Result()
        {
            return GetPart1Result(GetNotes());
        }

        public static long GetPart1Result(List<Note> notes)
        {
            var registers = new Dictionary<string, long>();
            long lastSoundPlayed = 0;
            var getOut = false;

            for (var i = 0; i < notes.Count; i++)
            {
                var value = GetNumericValue((notes[i].Type == "snd" || notes[i].Type == "rcv" || notes[i].Type == "jgz") ? notes[i].Identifier : notes[i].Operand, registers);
                if (!registers.ContainsKey(notes[i].Identifier))
                {
                    registers.Add(notes[i].Identifier, 0);
                }
                switch (notes[i].Type)
                {
                    case "snd":
                        lastSoundPlayed = value;
                        break;
                    case "set":
                        registers[notes[i].Identifier] = value;
                        break;
                    case "add":
                        registers[notes[i].Identifier] += value;
                        break;
                    case "mul":
                        registers[notes[i].Identifier] *= value;
                        break;
                    case "mod":
                        registers[notes[i].Identifier] %= value;
                        break;
                    case "rcv":
                        if (value != 0)
                        {
                            getOut = true;
                        }
                        break;
                    case "jgz":
                        if (value != 0)
                        {
                            var jumps = GetNumericValue(notes[i].Operand, registers);
                            if (jumps > 0)
                            {
                                i = (int)(i + jumps);
                            }
                            else
                            {
                                i = (int)(i + jumps - 1);
                            }

                            if (i < 0 || i >= notes.Count)
                            {
                                //we're done here
                                getOut = true;
                            }
                        }
                        break;
                    default:
                        break;
                }

                if (getOut)
                {
                    break;
                }
            }

            return lastSoundPlayed;
        }

        private static long GetNumericValue(string operand, Dictionary<string, long> registers)
        {
            if (!string.IsNullOrEmpty(operand))
            {
                if (long.TryParse(operand, out long value))
                {
                    return value;
                }
                else
                {
                    if (registers.ContainsKey(operand))
                    {
                        return registers[operand];
                    }
                }
            }

            return 0;
        }

        private static List<Note> GetNotes()
        {
            var notes = new List<Note>();
            var file = new StreamReader(FilePath);
            string line;

            while ((line = file.ReadLine()) != null)
            {
                var lineSplit = line.Split(' ');

                notes.Add(new Note { Type = lineSplit[0], Identifier = lineSplit[1], Operand = lineSplit.Length == 3 ? lineSplit[2] : null });
            }

            return notes;
        }
    }

    public class Note
    {
        public string Type { get; set; }
        public string Identifier { get; set; }
        public string Operand { get; set; }
    }
}
