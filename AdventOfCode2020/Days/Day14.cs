using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day14
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day14.txt";

        public static decimal GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath).ToList();
            var masks = GetMappedMasks(input);

            return GetSumOfMaskedBits(masks);
        }

        public static decimal GetSumOfMaskedBits(List<Mask> masks)
        {
            decimal sum = 0;
            var dict = new Dictionary<int, long>();

            foreach (var mask in masks)
            {
                foreach (var memory in mask.Memories)
                {
                    var binary = Convert.ToString(memory.InitialValue, 2);
                    binary = binary.PadLeft(mask.MaskString.Length, '0');
                    var result = string.Empty;

                    for (int i = binary.Length - 1; i >= 0; i--)
                    {
                        if (mask.MaskString[i] == 'X')
                        {
                            result = binary[i] + result;
                        }
                        else if (mask.MaskString[i] == '0')
                        {
                            result = '0' + result;
                        }
                        else
                        {
                            result = '1' + result;
                        }
                    }

                    memory.MaskedValue = Convert.ToInt64(result.TrimStart('0'), 2);

                    if (dict.ContainsKey(memory.Address))
                    {
                        dict[memory.Address] = memory.MaskedValue;
                    }
                    else
                    {
                        dict.Add(memory.Address, memory.MaskedValue);
                    }
                }
            }

            return dict.Sum(x => x.Value);
        }

        public static List<Mask> GetMappedMasks(List<string> input)
        {
            var masks = new List<Mask>();
            Mask mask = null;

            for (int i = 0; i < input.Count; i++)
            {
                var equalSplit = input[i].Split('=');

                if (equalSplit[0].Substring(0, 4) == "mask")
                {
                    if (i != 0)
                    {
                        masks.Add(mask);
                    }
                    mask = new Mask { Memories = new List<Memory>(), MaskString = equalSplit[1].Trim() };
                }
                else
                {
                    var memory = new Memory
                    {
                        InitialValue = long.Parse(equalSplit[1].Trim()),
                        Address = int.Parse(equalSplit[0].Split('[')[1].Split(']')[0])
                    };

                    mask.Memories.Add(memory);
                }
                //if (string.IsNullOrWhiteSpace(input[i]))
                //{
                //    masks.Add(mask);
                //    mask = new Mask { Memories = new List<Memory>() };
                //}
            }
            masks.Add(mask);

            return masks;
        }
    }

    public class Mask
    {
        public string MaskString { get; set; }
        public List<Memory> Memories { get; set; }
    }

    public class Memory
    {
        public int Address { get; set; }
        public long InitialValue { get; set; }
        public long MaskedValue { get; set; }
    }
}
