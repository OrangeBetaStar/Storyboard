using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using StorybrewScripts.ExtraFunctions;

namespace StorybrewScripts
{
    public class HelperClass
    {
        public static double SbWidth = (640 * 4) / 3;
        public static double WhiteTick = 60000d / StoryboardObjectGenerator.Current.Beatmap.GetTimingPointAt(0).Bpm;
        public static double LowerBoundX = 320 - SbWidth / 2;
        public static double UpperBoundX = 320 + SbWidth / 2;
        public static Vector2 Middle = new Vector2(320, 240);
        public static Vector2 CirclePos(double angle, double radius, Vector2 origin)
        {
            double posX = origin.X + (radius * Math.Cos(angle));
            double posY = origin.Y + (radius * Math.Sin(angle));

            return new Vector2((float)posX, (float)posY);
        }
        public static Vector2 EllipsePos(double angle, double width, double height, Vector2 origin)
        {
            double posX = origin.X + ((width / 2) * Math.Cos(angle));
            double posY = origin.Y + ((height / 2) * Math.Sin(angle));

            return new Vector2((float)posX, (float)posY);
        }
        public static bool InCircle(double radius, Vector2 origin, Vector2 point)
        {
            if (HelperClass.Distance(origin, point) > radius)
                return false;
            return true;
        }
        public static List<Vector2> LocationsInCircle(double radius, Vector2 origin, double fineness)
        {
            List<Vector2> returnlist = new List<Vector2>();
            for (double x = origin.X - radius; x < origin.X + radius; x += fineness)
                for (double y = origin.Y - radius; y < origin.Y + radius; y += fineness)
                    if (InCircle(radius, origin, new Vector2((float)x, (float)y)))
                        returnlist.Add(new Vector2((float)x, (float)y));
                        
            return returnlist;
        }
        public static double Distance(Vector2 p1, Vector2 p2)
        {
            double xVal = Math.Pow(p1.X - p2.X, 2);
            double yVal = Math.Pow(p1.Y - p2.Y, 2);

            return Math.Sqrt(xVal + yVal);
        }
        public static double Angle(Vector2 p1, Vector2 p2)
        {
            var height = p2.Y - p1.Y;
            var length = p2.X - p1.X;

            var ratio = height / length;

            return Math.Atan(ratio);
        }
        public static double AngleSpecific(Vector2 point, Vector2 centre)
        {
            var posY = point.Y >= centre.Y ? true : false;
            var posX = point.X >= centre.X ? true : false;

            var height = point.Y - centre.Y;
            var hyp = Distance(point, centre);

            if (posY && posX)           //quadrant 1
                return Math.Asin(height / hyp);
            else if (posY && !posX)     //quadrant 2
                return (Math.PI - Math.Asin(height / hyp));
            else if (!posY && !posX)    //quadrant 3
                return (Math.PI - Math.Asin(height / hyp));
            else                        //quadrant 4
                return (Math.PI * 1.5 - Math.Asin(height / hyp));
        }
        public static float Lerp(float v0, float v1, float t) => (1-t) * v0 + t * v1; //From starrodkirby86 <3
        public static Color4 ColorLerp(Color4 a, Color4 b, float t)
        {
            var R = Lerp(a.R, b.R, t);
            var G = Lerp(a.G, b.G, t);
            var B = Lerp(a.B, b.B, t);
            var A = Lerp(a.A, b.A, t);

            return new Color4(R, G, B, A);
        }
        public static Vector2 Midpoint(Vector2 a, Vector2 b) => Vector2.Lerp(a, b, 0.5f);
        public static bool InField(Vector2 pos) => (pos.X > LowerBoundX && pos.X < UpperBoundX && pos.Y < 480 && pos.Y > 0);
        public static Color4 RandomColor(float minR, float minG, float minB)
        {
            var R = StoryboardObjectGenerator.Current.Random(minR, 1);
            var G = StoryboardObjectGenerator.Current.Random(minG, 1);
            var B = StoryboardObjectGenerator.Current.Random(minB, 1);

            return new Color4((float)R, (float)G, (float)B, 1);
        }
        public static double Circumference(int radius) => 2 * Math.PI * radius;
        public static int RemoveRemainder(int numb, int modulus) => numb % modulus == 0 ? numb : numb - numb % modulus;
        public static int AddRemainder(int numb, int modulus) => numb % modulus == 0 ? numb : numb + numb % modulus;
    }
}