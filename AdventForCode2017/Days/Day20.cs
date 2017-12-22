using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.Days
{
    public static class Day20
    {
        private static string FilePath = Directory.GetCurrentDirectory() + @"/Input/Day20.txt";

        public static int GetPart1Result()
        {
            return GetResult(GetParticles()).ClosestToZero;
        }

        public static int GetPart2Result()
        {
            return GetResult(GetParticles(), true).LastParticleLeft;
        }

        public static (int ClosestToZero, int LastParticleLeft) GetResult(List<Particle> particles, bool removeCollisions = false)
        {
            var currentParticleCount = particles.Count;
            var lastParticleCount = currentParticleCount + 1;
            var timesTheSame = 0;
            var getOut = false;

            for (int i = 0; i < 1000; i++)
            {
                var matches = new Dictionary<string, List<Particle>>();

                foreach (var particle in particles)
                {
                    particle.Velocity.X += particle.Acceleration.X;
                    particle.Velocity.Y += particle.Acceleration.Y;
                    particle.Velocity.Z += particle.Acceleration.Z;
                    particle.Position.X += particle.Velocity.X;
                    particle.Position.Y += particle.Velocity.Y;
                    particle.Position.Z += particle.Velocity.Z;

                    particle.CurrentPosition = Math.Abs(particle.Position.X) + Math.Abs(particle.Position.Y) + Math.Abs(particle.Position.Z);

                    var particlePosition = particle.Position.ToString();
                    if (matches.ContainsKey(particlePosition))
                    {
                        matches[particlePosition].Add(particle);
                    }
                    else
                    {
                        matches.Add(particlePosition, new List<Particle> { particle });
                    }
                }

                if (removeCollisions)
                {
                    var duplicates = matches.Where(m => m.Value.Count > 1);

                    foreach (var duplicate in duplicates)
                    {
                        foreach (var duplicateItem in duplicate.Value)
                        {
                            particles.Remove(duplicateItem);
                        }

                        if (lastParticleCount == particles.Count)
                        {
                            if (timesTheSame == 5)
                            {
                                //we're done
                                getOut = true;
                            }
                            else
                            {
                                timesTheSame++;
                            }
                        }
                        else
                        {
                            lastParticleCount = particles.Count;
                        }
                    }
                }

                if (getOut)
                {
                    break;
                }
            }

            var orderedParticles = particles.OrderBy(p => p.CurrentPosition).ToList();

            return (orderedParticles.FirstOrDefault().Identifier, lastParticleCount);
        }

        private static List<Particle> GetParticles()
        {
            var particles = new List<Particle>();
            var lines = File.ReadAllLines(FilePath).ToList();

            var identifier = 0;
            foreach (var line in lines)
            {
                var particle = new Particle();

                var segments = line.Split(' ');

                var positionInfo = (segments[0].TrimStart('p', '=', '<').TrimEnd('>', ',')).Split(',');
                var velocityInfo = (segments[1].TrimStart('v', '=', '<').TrimEnd('>', ',')).Split(',');
                var accelerationInfo = (segments[2].TrimStart('a', '=', '<').TrimEnd('>')).Split(',');

                particle.Identifier = identifier;
                particle.Position = new Coordinates { X = int.Parse(positionInfo[0]), Y = int.Parse(positionInfo[1]), Z = int.Parse(positionInfo[2]) };
                particle.Velocity = new Coordinates { X = int.Parse(velocityInfo[0]), Y = int.Parse(velocityInfo[1]), Z = int.Parse(velocityInfo[2]) };
                particle.Acceleration = new Coordinates { X = int.Parse(accelerationInfo[0]), Y = int.Parse(accelerationInfo[1]), Z = int.Parse(accelerationInfo[2]) };

                particles.Add(particle);

                identifier++;
            }

            return particles;
        }
    }

    public class Particle
    {
        public int Identifier { get; set; }
        public long CurrentPosition { get; set; }
        public Coordinates Position { get; set; }
        public Coordinates Velocity { get; set; }
        public Coordinates Acceleration { get; set; }
    }

    public class Coordinates
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public override string ToString()
        {
            return "X=" + X + "Y=" + Y + "Z=" + Z;
        }
    }
}
