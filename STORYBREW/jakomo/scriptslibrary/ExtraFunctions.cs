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

namespace StorybrewScripts
{
    namespace ExtraFunctions
    {
        public class ObjFace
        {
            public int Point1;
            public int Point2;
            public int Point3;

            public ObjFace(int p1, int p2, int p3)
            {
                Point1 = p1;
                Point2 = p2;
                Point3 = p3;
            }
            public bool Contains(int number)
            {
                if (Point1 == number || Point2 == number || Point3 == number)
                {
                    return true;
                }
                
                return false;
            }
        }
        public class Range
        {
            public double Minimum;
            public double Maximum;
            public double Random;
            public double Length;

            public Range(float min, float max)
            {
                double minLocal = min;
                double maxLocal = max;

                if (min > max)
                {
                     minLocal = max;
                     maxLocal = min;
                }

                Minimum = minLocal;
                Maximum = maxLocal;
                Random = StoryboardObjectGenerator.Current.Random(min, max);
                Length = maxLocal - minLocal;
            }
            public bool Overlaps(Range a)
            {
                if (this.Maximum > a.Minimum && this.Minimum < a.Maximum)
                    return true;
                return false;
            }
        }
        public class Library
        {
            public List<OsbSprite> spriteListBig = new List<OsbSprite>();
            public List<Range> timeListBig = new List<Range>();
            public string path;
            OsbOrigin origin;
            public Library(string spritePath, OsbOrigin spriteOrigin)
            {
                path = spritePath;
                origin = spriteOrigin;
            }
            public Library(string spritePath)
            {
                path = spritePath;
                origin = OsbOrigin.Centre;
            }
            public OsbSprite GetSprite() => FindSprite(-1000000, 1000000);
            public OsbSprite GetSprite(int startTime, int endTime) => FindSprite(startTime, endTime);
            public OsbSprite GetSprite(double startTime, double endTime) => FindSprite((int)Math.Round(startTime), (int)Math.Round(endTime));
            public OsbSprite GetSprite(float startTime, float endTime) => FindSprite((int)Math.Round(startTime), (int)Math.Round(endTime));
            public OsbSprite GetSprite(decimal startTime, decimal endTime) => FindSprite((int)Math.Round(startTime), (int)Math.Round(endTime));
            public OsbSprite GetSprite(Vector2 time) => FindSprite((int)Math.Round(time.X), (int)Math.Round(time.Y));
            public OsbSprite GetSprite(Range time) => FindSprite((int)Math.Round(time.Minimum), (int)Math.Round(time.Maximum));
            public List<OsbSprite> SpriteList() => spriteListBig;
            public List<Range> TimeList() => timeListBig;
            public string GetPath() => path;
            public OsbOrigin GetOrigin() => origin;
            private OsbSprite FindSprite(int startTime, int endTime)
            {
                var sprite = StoryboardObjectGenerator.Current.GetLayer("").CreateSprite(path, origin);

                var spriteList = spriteListBig;
                var timeList = timeListBig;

                var timeRange = new Range(startTime, endTime);

                if (spriteList.Count == 0)
                {
                    spriteList.Add(sprite);
                    timeList.Add(timeRange);

                    spriteListBig = spriteList;
                    timeListBig = timeList;

                    return sprite;
                }
                else
                {
                    for (int i = 0; i < spriteList.Count; i++)
                    {
                        if (!timeList[i].Overlaps(timeRange))
                        {
                            sprite = spriteList[i];
                            timeList[i] = timeRange;

                            spriteListBig = spriteList;
                            timeListBig = timeList;

                            return sprite;
                        }
                    }
                }
                spriteList.Add(sprite);
                timeList.Add(timeRange);

                spriteListBig = spriteList;
                timeListBig = timeList;
                
                return sprite;
            }
        }
    }
}