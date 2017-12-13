using System.Collections.Generic;

namespace AdventOfCode2017.Days
{
    public static class Day10
    {
        public static int GetPart1Result()
        {
            var input = new List<int> { 225, 171, 131, 2, 35, 5, 0, 13, 1, 246, 54, 97, 255, 98, 254, 110 };
            return GetResult(256, input);
        }

        public static int GetPart1Result(int listCount, List<int> lengths)
        {
            return GetResult(listCount, lengths);
        }

        public static int GetResult(int listCount, List<int> lengths)
        {
            var list = new List<int>();
            for (int i = 0; i < listCount; i++)
            {
                list.Add(i);
            }

            var currentPosition = 0;
            var skipSize = 0;
            for (int i = 0; i < lengths.Count; i++)
            {
                var length = lengths[i];

                if (length + currentPosition < listCount)
                {
                    //not wrapping
                    var temp = new List<int>();
                    for (int j = currentPosition; j < length; j++)
                    {
                        temp.Add(list[j]);
                    }
                    var reversed = GetReversedList(temp);
                    var reversedPosition = 0;
                    for (int k = currentPosition; k < length; k++)
                    {
                        list[k] = reversed[reversedPosition];
                        reversedPosition++;
                    }

                    if (skipSize + currentPosition < listCount)
                    {
                        currentPosition = lengths[i] + skipSize;
                    }
                    else
                    {
                        currentPosition = skipSize + currentPosition + 1 - listCount;
                    }
                }
                else
                {
                    //wrapping
                    var amountLeft = currentPosition + length - list.Count;
                    var temp = new List<int>();
                    for (int j = currentPosition; j < list.Count; j++)
                    {
                        temp.Add(list[j]);
                    }
                    for (int k = 0; k < amountLeft; k++)
                    {
                        temp.Add(list[k]);
                    }
                    var reversed = GetReversedList(temp);
                    var reversedPosition = 0;
                    for (int k = currentPosition; k < list.Count; k++)
                    {
                        list[k] = reversed[reversedPosition];
                        reversedPosition++;
                    }
                    for (int k = 0; k < amountLeft; k++)
                    {
                        list[k] = reversed[reversedPosition];
                        reversedPosition++;
                    }
                    currentPosition = amountLeft + skipSize;
                }

                skipSize++;
            }

            return list[0] * list[1];
        }

        private static List<int> GetReversedList(List<int> listToReverse)
        {
            var reversedList = new List<int>();

            for (int i = (listToReverse.Count - 1); i >= 0; i--)
            {
                reversedList.Add(listToReverse[i]);
            }

            return reversedList;
        }
    }
}
