using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public static class Day12
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day12.txt";

        public static int GetResultPartOne()
        {
            var input = File.ReadAllLines(FilePath).ToList();
            var instructions = GetShipEvasiveInstructions(input);

            return ProcessInstructions(instructions, Direction.East, 0, 0);
        }

        public static int GetResultPartTwo()
        {
            var input = File.ReadAllLines(FilePath).ToList();
            var instructions = GetShipEvasiveInstructions(input);

            return ProcessWaypointInstructions(instructions, 0, 0, 10, 1);
        }

        public static List<EvasiveInstruction> GetShipEvasiveInstructions(List<string> input)
        {
            var instructions = new List<EvasiveInstruction>();

            foreach (var item in input)
            {
                instructions.Add(new EvasiveInstruction { Manuever = GetManeuver(item.Substring(0, 1)), Distance = int.Parse(item.Substring(1, item.Length - 1)) });
            }

            return instructions;
        }

        public static Maneuver GetManeuver(string maneuverString)
        {
            switch (maneuverString)
            {
                case "F":
                    return Maneuver.Forward;
                case "N":
                    return Maneuver.MoveNorth;
                case "E":
                    return Maneuver.MoveEast;
                case "S":
                    return Maneuver.MoveSouth;
                case "W":
                    return Maneuver.MoveWest;
                case "L":
                    return Maneuver.TurnLeft;
                case "R":
                    return Maneuver.TurnRight;
            }

            return Maneuver.Unknown;
        }

        public static int ProcessInstructions(List<EvasiveInstruction> instructions, Direction currentlyFacing, int currentX, int currentY)
        {
            foreach (var instruction in instructions)
            {
                switch (instruction.Manuever)
                {
                    case Maneuver.Forward:
                        switch (currentlyFacing)
                        {
                            case Direction.North:
                                currentY += instruction.Distance;
                                break;
                            case Direction.South:
                                currentY -= instruction.Distance;
                                break;
                            case Direction.East:
                                currentX += instruction.Distance;
                                break;
                            case Direction.West:
                                currentX -= instruction.Distance;
                                break;
                        }
                        break;
                    case Maneuver.TurnLeft:
                        switch (currentlyFacing)
                        {
                            case Direction.North:
                                currentlyFacing = instruction.Distance == 90 ? Direction.West : instruction.Distance == 180 ? Direction.South : Direction.East;
                                break;
                            case Direction.South:
                                currentlyFacing = instruction.Distance == 90 ? Direction.East : instruction.Distance == 180 ? Direction.North : Direction.West;
                                break;
                            case Direction.East:
                                currentlyFacing = instruction.Distance == 90 ? Direction.North : instruction.Distance == 180 ? Direction.West : Direction.South;
                                break;
                            case Direction.West:
                                currentlyFacing = instruction.Distance == 90 ? Direction.South : instruction.Distance == 180 ? Direction.East : Direction.North;
                                break;
                        }
                        break;
                    case Maneuver.TurnRight:
                        switch (currentlyFacing)
                        {
                            case Direction.North:
                                currentlyFacing = instruction.Distance == 90 ? Direction.East : instruction.Distance == 180 ? Direction.South : Direction.West;
                                break;
                            case Direction.South:
                                currentlyFacing = instruction.Distance == 90 ? Direction.West : instruction.Distance == 180 ? Direction.North : Direction.East;
                                break;
                            case Direction.East:
                                currentlyFacing = instruction.Distance == 90 ? Direction.South : instruction.Distance == 180 ? Direction.West : Direction.North;
                                break;
                            case Direction.West:
                                currentlyFacing = instruction.Distance == 90 ? Direction.North : instruction.Distance == 180 ? Direction.East : Direction.South;
                                break;
                        }
                        break;
                    case Maneuver.MoveNorth:
                        currentY += instruction.Distance;
                        break;
                    case Maneuver.MoveSouth:
                        currentY -= instruction.Distance;
                        break;
                    case Maneuver.MoveEast:
                        currentX += instruction.Distance;
                        break;
                    case Maneuver.MoveWest:
                        currentX -= instruction.Distance;
                        break;
                }
            }

            return Math.Abs(currentY) + Math.Abs(currentX);
        }

        public static int ProcessWaypointInstructions(List<EvasiveInstruction> instructions, int currentX, int currentY, int relativeWaypointX, int relativeWaypointY)
        {
            var currentWaypointX = relativeWaypointX;
            var currentWaypointY = relativeWaypointY;

            foreach (var instruction in instructions)
            {
                switch (instruction.Manuever)
                {
                    case Maneuver.Forward:
                        currentY += instruction.Distance * (currentWaypointY - currentY);
                        currentX += instruction.Distance * (currentWaypointX - currentX);

                        currentWaypointY = currentY + relativeWaypointY;
                        currentWaypointX = currentX + relativeWaypointX;
                        break;
                    case Maneuver.TurnLeft:
                        switch (instruction.Distance)
                        {
                            case 90:
                                break;
                            case 180:
                                break;
                            default:
                                break;
                        }
                        break;
                    case Maneuver.TurnRight:
                        switch (instruction.Distance)
                        {
                            case 90:
                                var tempRelativeCurrentWaypointX = relativeWaypointX;
                                var tempRelativeCurrentWaypointY = relativeWaypointY;
                                if (relativeWaypointY > 0)
                                {
                                    //north of ship, make it east of ship
                                    relativeWaypointY = tempRelativeCurrentWaypointX;
                                    currentWaypointY = currentY - relativeWaypointY;
                                }
                                else
                                {
                                    //south of ship, make it west of ship
                                    relativeWaypointX = tempRelativeCurrentWaypointY;
                                    currentWaypointX = currentX - relativeWaypointX;
                                }

                                if (relativeWaypointX > 0)
                                {
                                    //east of ship, make it south of ship
                                    relativeWaypointX = relativeWaypointY;
                                    currentWaypointX = currentX + relativeWaypointX;
                                }
                                else
                                {
                                    //west of ship, make it north of ship
                                    relativeWaypointX = relativeWaypointY;
                                    currentWaypointX = currentX + relativeWaypointX;
                                }
                                break;
                            case 180:
                                break;
                            default:
                                break;
                        }
                        break;
                    case Maneuver.MoveNorth:
                        currentWaypointY += instruction.Distance;
                        relativeWaypointY = currentWaypointY - currentY;
                        break;
                    case Maneuver.MoveSouth:
                        currentWaypointY -= instruction.Distance;
                        relativeWaypointY = currentWaypointY - currentY;
                        break;
                    case Maneuver.MoveEast:
                        currentWaypointX += instruction.Distance;
                        relativeWaypointX = currentWaypointX - currentX;
                        break;
                    case Maneuver.MoveWest:
                        currentWaypointX -= instruction.Distance;
                        relativeWaypointX = currentWaypointX - currentX;
                        break;
                }
            }

            return Math.Abs(currentY) + Math.Abs(currentX);
        }
    }

    public class EvasiveInstruction
    {
        public Maneuver Manuever { get; set; }
        public int Distance { get; set; }
    }

    public enum Maneuver
    {
        Forward,
        MoveNorth,
        MoveEast,
        MoveSouth,
        MoveWest,
        TurnLeft,
        TurnRight,
        Unknown
    }

    public enum Direction
    {
        North,
        South,
        East,
        West
    }
}
