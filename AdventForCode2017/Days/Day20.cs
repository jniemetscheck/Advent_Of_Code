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
            return GetPart1Result(GetParticles());
        }

        public static int GetPart1Result(List<Particle> particles)
        {
            for (int i = 0; i < 10000; i++)
            {
                foreach (var particle in particles)
                {
                    particle.Velocity.X += particle.Acceleration.X;
                    particle.Velocity.Y += particle.Acceleration.Y;
                    particle.Velocity.Z += particle.Acceleration.Z;
                    particle.Position.X += particle.Velocity.X;
                    particle.Position.Y += particle.Velocity.Y;
                    particle.Position.Z += particle.Velocity.Z;

                    particle.CurrentPosition = Math.Abs(particle.Position.X) + Math.Abs(particle.Position.Y) + Math.Abs(particle.Position.Z);
                }
            }

            var orderedParticles = particles.OrderBy(p => p.CurrentPosition).ToList();
            return orderedParticles.FirstOrDefault().Identifier;
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
    }
}
