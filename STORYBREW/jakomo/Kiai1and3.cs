using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Storyboarding.Display;
using StorybrewCommon.Storyboarding.CommandValues;
using StorybrewCommon.Storyboarding.Commands;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using StorybrewScripts.ExtraFunctions;

namespace StorybrewScripts
{
    public class Kiai1and3 : StoryboardObjectGenerator
    {
        public Library glowLibrary = new Library("sb/glow.png");
        public List<int> starTimes = new List<int>() {
            //kiai1
            96764, 97107, 97278, 97450, 
            102078, 102421, 102592, 102935,
            107735, 107907, 108078, 108250, 108421, 
            //kiai3
            279164, 279507, 279678, 279850,
            284478, 284821, 284992, 285335,
            290135, 290307, 290478, 290650, 290821
        };
        public Library sqLibrary = new Library("sb/p.png");
        public List<int> lineTimes = new List<int>() {
            //kiai1
            95221, 95392, 95564,
            96592,
            102250, 102764, 103107, 103278,
            107564,
            110307, 110478,
            //kiai3
            277621, 277792, 277964,
            278992,
            284649, 285164, 285507, 285678,
            289964, 
            292707, 292878,
        };
        public void CreateWaifu(int startTime, Vector2 origin)
        {
            var fineness = 2;
            var fadeTime = 200;
            var grillSize = 3;
            var moveAmt = 100;

            Bitmap grillBitmapFirst = GetMapsetBitmap(@"sb/girl.png");
            Bitmap grillBitmap = new Bitmap(grillBitmapFirst, new Size((int)(grillBitmapFirst.Width / grillSize), (int)(grillBitmapFirst.Height / grillSize)));

            var endTime = startTime + 115449 - 111164;

            List<Vector2> locations = new List<Vector2>();
            List<Color> colors = new List<Color>();

            for (int x = 0; x < grillBitmap.Width; x += fineness)
            {
                for (int y = 0; y < grillBitmap.Height; y += fineness)
                {
                    var color = grillBitmap.GetPixel(x, y);
                    if (!color.A.Equals(0))
                    {
                        locations.Add(new Vector2(x + origin.X, y + origin.Y));
                        colors.Add(color);
                    }
                }
            }

            List<int> miniTimes = new List<int>();
            for (int i = startTime - 10286; i < startTime - 9600; i += (int)(HelperClass.WhiteTick / 6))
            {
                miniTimes.Add(i);
            }
            for (int z = 0; z < locations.Count; z++)
            {
                var randNum = Random(0, miniTimes.Count * 6);
                if (randNum < miniTimes.Count)
                {
                    var spritePos = locations[z];

                    OsbSprite sprite = sqLibrary.GetSprite(miniTimes[randNum] - fadeTime, (int)(startTime - 9600 + HelperClass.WhiteTick * 4));

                    sprite.Fade(miniTimes[randNum] - fadeTime, miniTimes[randNum], 0, 1);
                    sprite.Scale(miniTimes[randNum] - fadeTime, fineness / 2f);
                    sprite.Color(miniTimes[randNum] - fadeTime, colors[z]);
                    sprite.Fade(startTime - 9600, startTime - 9600 + HelperClass.WhiteTick * 4, 1, 0);
                    sprite.Rotate(miniTimes[randNum] - fadeTime, 0);

                    sprite.Move(miniTimes[randNum] - fadeTime, spritePos);
                }
            }

            List<int> times = new List<int>();
            for (int i = startTime; i < endTime; i += (int)(HelperClass.WhiteTick / 6))
            {
                times.Add(i);
            }

            for (int z = 0; z < locations.Count; z++)
            {
                var randNum = Random(0, times.Count);
                var spritePos = locations[z];

                OsbSprite sprite = sqLibrary.GetSprite(times[randNum] - 100, (int)(endTime + HelperClass.WhiteTick * 4));

                sprite.Fade(times[randNum] - fadeTime, times[randNum], 0, 1);
                sprite.Scale(times[randNum] - fadeTime, fineness / 2f);
                sprite.Color(times[randNum] - fadeTime, colors[z]);
                sprite.Fade(endTime, endTime + HelperClass.WhiteTick, 1, 0);
                sprite.Scale(endTime, endTime + HelperClass.WhiteTick, fineness / 2f, 0);
                sprite.Rotate(times[randNum] - fadeTime, 0);

                sprite.Move(times[randNum] - fadeTime, spritePos);
                sprite.Move(endTime, endTime + HelperClass.WhiteTick, spritePos, new Vector2(spritePos.X + Random(-1 * moveAmt, moveAmt), spritePos.Y + Random(-1 * moveAmt, moveAmt)));
            }
        }
        public void Moan()
        {
            var layer = GetLayer("");
            var glow = layer.CreateSprite("sb/sideglow.png", OsbOrigin.Centre);
            var glow2 = layer.CreateSprite("sb/sideglow.png", OsbOrigin.Centre);

            int WhiteTick = 104821 - 104478;

            var glowBitmap = GetMapsetBitmap("sb/sideglow.png");

            double ScaleAmount = 1.3;

            glow.Fade(0, 0);
            glow.ScaleVec(0, ScaleAmount, 480f / glowBitmap.Height);
            glow.Fade(104135, 104135 + WhiteTick * 2, 1, 0);
            glow.Move(104135, 320 - (854 / 2f) + (glowBitmap.Width * ScaleAmount / 2), 240);
            glow.Fade(108935, 108935 + WhiteTick * 2, 0.08, 0);

            glow2.Fade(0, 0);
            glow2.ScaleVec(0, ScaleAmount, 480f / glowBitmap.Height);
            glow2.Fade(104135, 104135 + WhiteTick * 2, 1, 0);
            glow2.FlipH(104135, 109621);
            glow2.Move(104135, 320 + (854 / 2f) - (glowBitmap.Width * ScaleAmount / 2), 240);

            double LoopNumber = 0;
            for (int i = 104821; i <= 108935; i += 4 * WhiteTick)
            {
                double FadeStep = 1 / 8f;

                if (1 - FadeStep * LoopNumber <= 0) {break;}
                else
                {
                    LoopNumber++;
                    glow.Fade(i, i + WhiteTick * 2, 1 - FadeStep * LoopNumber, 0);

                    LoopNumber++;
                    glow2.Fade(i + WhiteTick * 2, i + WhiteTick * 4, 1 - (FadeStep * LoopNumber), 0);
                }
            }
        }
        public void shootingStar(int startTime, bool side)
        {
            var endTime = startTime + HelperClass.WhiteTick;
            
            var easingX = Random(0, 10);
            var easingY = Random(0, 10);

            var xStart = side ? HelperClass.LowerBoundX : HelperClass.UpperBoundX;
            var xEnd = !side ? HelperClass.LowerBoundX : HelperClass.UpperBoundX;
            
            var xVal = new AnimatedValue<CommandDecimal>();
            xVal.Add(new MoveXCommand((OsbEasing)easingX, startTime, endTime, xStart, xEnd));

            var yStart = Random(80, 400);
            var yEnd = yStart + Random(-120, 120);

            var yVal = new AnimatedValue<CommandDecimal>();
            yVal.Add(new MoveXCommand((OsbEasing)easingY, startTime, endTime, yStart, yEnd));

            for (int i = startTime; i < endTime; i++)
            {
                var position = new Vector2(xVal.ValueAtTime(i), yVal.ValueAtTime(i));

                OsbSprite sprite = glowLibrary.GetSprite(i, (int)(i + HelperClass.WhiteTick));
                
                sprite.Move(i, position);
                sprite.Fade(i, i + HelperClass.WhiteTick, 1, 0);
                sprite.Scale(i, i + HelperClass.WhiteTick, 0.1, 0);
                sprite.Color(i, i + HelperClass.WhiteTick, Color4.BlueViolet, Color4.Red);
            }
        }
        public void squareLines(int startTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime == startTime)
                {
                    double angle = 0;
                    if (hitobject is OsuSlider)
                        angle = HelperClass.Angle(hitobject.Position, hitobject.EndPosition);
                    else
                        angle = Random(0, Math.PI * 2);

                    for (int i = 0; i < 2; i++)
                    {
                        var space = i == 1 ? 10 : -10;

                        var circ = 2 * Math.PI * 36.49 * 1.5;

                        var x = hitobject.Position.X;
                        var y = hitobject.Position.Y;

                        var thing = space < 0 ? 0 : circ / space;

                        var xtraTime = 200;

                        for (double z = 0; z < thing; z++)
                        {
                            var anglezz = Math.PI * 2 / (circ / space) * z;

                            var time = startTime;
                            OsbSprite sprite = sqLibrary.GetSprite(time, (int)(time + HelperClass.WhiteTick * 2 + xtraTime + 1));

                            sprite.ScaleVec(time, time + xtraTime, 0, 0, space / 2f, space / 2f);
                            sprite.Fade(time, 1);
                            sprite.Fade(time + HelperClass.WhiteTick * 2, time + HelperClass.WhiteTick * 2 + xtraTime, 1, 0);
                            sprite.ScaleVec(time + HelperClass.WhiteTick * 2, time + HelperClass.WhiteTick * 2 + xtraTime, space / 2f, space / 2f, 0, 0);
                            sprite.Rotate(time, time + xtraTime, anglezz - Math.PI / 2, anglezz);
                            sprite.Rotate(time + HelperClass.WhiteTick * 2, time + HelperClass.WhiteTick * 2 + xtraTime, anglezz, anglezz + Math.PI / 2);
                            sprite.Color(time, hitobject.Color);
                            sprite.Move(time, HelperClass.CirclePos(anglezz, 36.49 * 1.5, new Vector2(x, y)));
                        }
                        var LoopNumber = 0;

                        while (HelperClass.InField(new Vector2(x, y)))
                        {
                            var offset = new Vector2((float)Math.Cos(angle) * space, (float)Math.Sin(angle) * space);

                            var qqq = space >= 0 ? space / -2 : space / 2;

                            if (HelperClass.Distance(hitobject.Position, new Vector2(x, y)) >= (36.49 * 1.5) + qqq)
                            {
                                var time = startTime + LoopNumber * 5;
                                OsbSprite sprite = GetLayer("").CreateSprite("sb\\p.png", OsbOrigin.Centre);//sqLibrary.GetSprite(time, (int)(time + HelperClass.WhiteTick * 2 + xtraTime));

                                sprite.ScaleVec(time, time + xtraTime, 0, 0, space / 2f, space / 2f);
                                sprite.Fade(time, 1);
                                sprite.Fade(time + HelperClass.WhiteTick * 2, time + HelperClass.WhiteTick * 2 + xtraTime, 1, 0);
                                sprite.ScaleVec(time + HelperClass.WhiteTick * 2, time + HelperClass.WhiteTick * 2 + xtraTime, space / 2f, space / 2f, 0, 0);
                                sprite.Rotate(time, time + xtraTime, angle - Math.PI / 2, angle);
                                sprite.Rotate(time + HelperClass.WhiteTick * 2, time + HelperClass.WhiteTick * 2 + xtraTime, angle, angle + Math.PI / 2);
                                sprite.Color(time, hitobject.Color);
                                sprite.Move(time, x + offset.X, y + offset.Y);
                            }
                            LoopNumber++;
                            x += offset.X;
                            y += offset.Y;
                        }
                    }
                }
                else
                {continue;}
            }
        }
        public void CreateBackground(int startTime)
        {
            
        }
        public override void Generate()
        {
            CreateBackground(93507);
            CreateBackground(275907);
            CreateWaifu(111164, new Vector2(40, 40));
            CreateWaifu(293564, new Vector2(40, 40));
            Moan();
            
            for (int i = 0; i < lineTimes.Count; i++)
            {
                squareLines(lineTimes[i]);
            }
            
            for (int i = 0; i < starTimes.Count; i++)
            {
                bool side = i % 2 == 0 ? true : false;
                shootingStar(starTimes[i], side);
            }
        }
    }
}
