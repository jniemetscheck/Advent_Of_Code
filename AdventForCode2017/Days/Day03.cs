using System;
using System.Collections.Generic;

namespace AdventOfCode2017.Days
{
    public static class Day03
    {
        #region Day Part 1

        public static int GetPart1Result()
        {
            const int input = 312051;

            return GetPositionsAway(input);
        }

        private static int GetPositionsAway(int input)
        {
            var stepsAway = 0;
            var lowSquareRoot = Math.Floor(Math.Sqrt(input));
            var highSquareRoot = lowSquareRoot == Math.Sqrt(input) ? lowSquareRoot : lowSquareRoot + 1;
            var corner = lowSquareRoot == highSquareRoot ? (int)input / 2 : (int)((highSquareRoot * highSquareRoot) - highSquareRoot + 1);
            var additional = corner <= input ? input - corner : 0;
            var less = corner > input ? corner - input : 0;

            var portCoordinate = (int)Math.Ceiling(highSquareRoot / 2);

            var xCoordinateStepsAway = 0;
            if (additional > 0)
            {
                if (input - corner < portCoordinate)
                {
                    //along x - before middle
                    xCoordinateStepsAway = portCoordinate - (input - corner) - 2; // -2 to not count the initial x and y
                }
                else if (input - corner > portCoordinate)
                {
                    //along x - after middle
                    xCoordinateStepsAway = (input - corner) - portCoordinate - 2; // -2 to not count the initial x and y
                }
                else
                {
                    //middle - do nothing because it's the same x coordinate as the port
                }

                stepsAway = xCoordinateStepsAway + portCoordinate;
            }

            return stepsAway;
        }

        #endregion

        #region Part 2

        public static int GetPart2Result()
        {
            const int input = 312051;

            var largerNumber = 0;

            //build spiral;
            var outer = new List<List<int>>();

            outer.Add(new List<int> { 1 });
            var currentOuterArrayIndex = 0;
            var currentInnerArrayIndex = 0;
            var direction = Direction.Right;

            while (true)
            {
                var sum = 0;
                var left = 0;
                var right = 0;
                var above = 0;
                var below = 0;
                var aboveLeft = 0;
                var aboveRight = 0;
                var belowLeft = 0;
                var belowRight = 0;
                switch (direction)
                {
                    case Direction.Left:
                        right = GetSafeValue(outer, currentOuterArrayIndex, currentInnerArrayIndex + 1);
                        below = GetSafeValue(outer, currentOuterArrayIndex + 1, currentInnerArrayIndex);
                        belowLeft = GetSafeValue(outer, currentOuterArrayIndex + 1, currentInnerArrayIndex - 1);
                        belowRight = GetSafeValue(outer, currentOuterArrayIndex + 1, currentInnerArrayIndex + 1);

                        sum = right + below + belowLeft + belowRight;
                        outer[currentOuterArrayIndex][currentInnerArrayIndex] = sum;

                        if (below == 0)
                        {
                            direction = Direction.Down;
                            currentOuterArrayIndex++;
                        }
                        else
                        {
                            if (belowLeft == 0)
                            {
                                if (currentInnerArrayIndex == 0)
                                {
                                    //need to shift all arrays over one
                                    foreach (var item in outer)
                                    {
                                        item.Insert(0, 0);
                                    }
                                }
                                else
                                {
                                    currentInnerArrayIndex--;
                                }
                            }
                            else
                            {
                                currentInnerArrayIndex--;
                            }
                        }
                        break;
                    case Direction.Right:
                        var isFirst = false;
                        if (currentOuterArrayIndex == 0 && currentInnerArrayIndex == 0)
                        {
                            //we're at the very beginning
                            left = GetSafeValue(outer, currentOuterArrayIndex, currentInnerArrayIndex);
                            isFirst = true;
                        }
                        else
                        {
                            left = GetSafeValue(outer, currentOuterArrayIndex, currentInnerArrayIndex - 1);
                        }
                        above = GetSafeValue(outer, currentOuterArrayIndex - 1, currentInnerArrayIndex);
                        aboveLeft = GetSafeValue(outer, currentOuterArrayIndex - 1, currentInnerArrayIndex - 1);
                        aboveRight = GetSafeValue(outer, currentOuterArrayIndex - 1, currentInnerArrayIndex + 1);

                        sum = left + above + aboveLeft + aboveRight;

                        if (isFirst || currentInnerArrayIndex == outer[currentOuterArrayIndex].Count)
                        {
                            outer[currentOuterArrayIndex].Add(sum);

                            if (!isFirst)
                            {
                                //add element to rest of rows
                                for (int i = 0; i < outer.Count - 1; i++)
                                {
                                    outer[i].Add(0);
                                }
                            }
                        }
                        else
                        {
                            outer[currentOuterArrayIndex][currentInnerArrayIndex] = sum;
                        }

                        if (above == 0)
                        {
                            direction = Direction.Up;
                            if (aboveLeft > 0)
                            {
                                currentOuterArrayIndex--;
                            }
                            else
                            {
                                //need new array - allocate as same size as current row
                                var newArray = new List<int>();
                                for (int i = 0; i < outer[currentOuterArrayIndex].Count; i++)
                                {
                                    newArray.Add(0);
                                }
                                outer.Insert(0, newArray);
                                currentInnerArrayIndex++;
                            }
                        }
                        else
                        {
                            currentInnerArrayIndex++;
                        }
                        break;
                    case Direction.Up:
                        left = GetSafeValue(outer, currentOuterArrayIndex, currentInnerArrayIndex - 1);
                        below = GetSafeValue(outer, currentOuterArrayIndex + 1, currentInnerArrayIndex);
                        aboveLeft = GetSafeValue(outer, currentOuterArrayIndex - 1, currentInnerArrayIndex - 1);
                        belowLeft = GetSafeValue(outer, currentOuterArrayIndex + 1, currentInnerArrayIndex - 1);

                        sum = left + below + aboveLeft + belowLeft;
                        outer[currentOuterArrayIndex][currentInnerArrayIndex] = sum;

                        if (left == 0)
                        {
                            direction = Direction.Left;
                            currentInnerArrayIndex--;
                        }
                        else if (aboveLeft == 0)
                        {
                            //need to add a new item
                            outer.Insert(0, new List<int>());
                            for (int i = 0; i < outer[1].Count; i++)
                            {
                                outer[0].Add(0);
                            }
                        }
                        else
                        {
                            currentOuterArrayIndex--;
                        }
                        break;
                    case Direction.Down:
                        above = GetSafeValue(outer, currentOuterArrayIndex - 1, currentInnerArrayIndex);
                        aboveRight = GetSafeValue(outer, currentOuterArrayIndex - 1, currentInnerArrayIndex + 1);
                        right = GetSafeValue(outer, currentOuterArrayIndex, currentInnerArrayIndex + 1);
                        belowRight = GetSafeValue(outer, currentOuterArrayIndex + 1, currentInnerArrayIndex + 1);

                        sum = above + aboveRight + right + belowRight;
                        outer[currentOuterArrayIndex][currentInnerArrayIndex] = sum;

                        if (belowRight == 0)
                        {
                            if (right == 0)
                            {
                                direction = Direction.Right;
                                currentInnerArrayIndex++;
                            }
                            else
                            {
                                //need to add an array
                                outer.Add(new List<int>());

                                for (int i = 0; i < outer[0].Count; i++)
                                {
                                    outer[outer.Count - 1].Add(0);
                                }
                                currentOuterArrayIndex++;
                            }
                        }
                        else
                        {
                            currentOuterArrayIndex++;
                        }
                        break;
                    default:
                        break;
                }

                if (sum > input)
                {
                    largerNumber = sum;
                    break;
                }
            }

            return largerNumber;
        }

        private static int GetSafeValue(List<List<int>> outer, int outerIndex, int innerIndex)
        {
            if (outerIndex < 0 || innerIndex < 0 || outer.Count <= outerIndex || outer[outerIndex].Count <= innerIndex)
            {
                return 0;
            }
            else
            {
                return outer[outerIndex][innerIndex];
            }
        }

        #endregion
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}
