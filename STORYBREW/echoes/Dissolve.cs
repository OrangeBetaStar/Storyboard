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

namespace StorybrewScripts
{
    public class Dissolve : StoryboardObjectGenerator
    {
        [Configurable]
        public int Fineness = 10;

        [Configurable]
        public Vector2 Origin = new Vector2(320, 240);

        [Configurable]
        public string PixelPath = "SB/dot.png";

        [Configurable]
        public double DotScale = 0.1;

        [Configurable]
        public double DotScaleAdd = 0.1;

        [Configurable]
        public int start = 82618;

        [Configurable]
        public int end = 85034;

        [Configurable]
        public String path = "ww.jpg";

        [Configurable]
        public float imageScale = 2.0f;

        [Configurable]
        public int pixelBrightness = 1;

        public int tick = 1075 - 873; // 1/4 tick

        public int delay = 24632 - 22618;


        public override void Generate()
        {
            start = start - delay;
            end = end - delay;
		    Bitmap imageBitmappu = GetMapsetBitmap(path);
            var scaleRatio = 480f / imageBitmappu.Height;
            var scalingThing = Fineness * scaleRatio;

            for (int x = 0; x < imageBitmappu.Width; x += Fineness)
            {
                
                for (int y = 0; y < imageBitmappu.Height; y += Fineness)
                {
                    Vector2 spritePos = new Vector2(x / Fineness * scalingThing, y / Fineness * scalingThing) - new Vector2(107,0);
                    Color pixelColor = imageBitmappu.GetPixel(x, y);

                    if (pixelColor.A > 0)
                    {
                        var sprite = GetLayer("").CreateSprite(PixelPath, OsbOrigin.Centre);
                        SpriteCommands(sprite, start+(x + y), end+(x + y), pixelColor, spritePos, path);
                    }
                    else {continue;}
                }
            }
            imageBitmappu.Dispose();
            
        }
        public void SpriteCommands(OsbSprite sprite, int StartTime, int EndTime, Color pixelColor, Vector2 location, string path)
        {
            int OffsetX = Random(-20, 20);
            int OffsetY = Random(-20, 20);
            int OffsetXAfterEffect = Random(300, 400);
            int OffsetYAfterEffect = Random(-40, 40);

            // sprite.Move(OsbEasing.Out, StartTime, StartTime + (tick * 2), OffsetX + location.X, OffsetY + location.Y, location.X, location.Y);
            sprite.Move(OsbEasing.In, EndTime - (tick * 2), EndTime, OffsetX + location.X, OffsetY + location.Y, location.X + OffsetXAfterEffect, location.Y + OffsetYAfterEffect);
            
            //sprite.Fade(OsbEasing.Out, StartTime, StartTime + (tick * 2), 0, pixelBrightness);
            //sprite.Fade(StartTime + (tick * 2), EndTime - (tick * 2), pixelBrightness , pixelBrightness);
            sprite.Fade(EndTime - (tick * 2), EndTime, pixelBrightness, 0);
            sprite.Scale(EndTime - (tick * 2), EndTime, DotScale, DotScale + DotScaleAdd);
            //sprite.Scale(StartTime + (tick * 2), EndTime - (tick * 4), DotScale, DotScale + 0.3);

            sprite.Color(EndTime - (tick * 2), pixelColor);
            // sprite.Color(EndTime - (tick * 2), EndTime, pixelColor, new Color4(255, 255, 255, 255));
            //sprite.Scale(EndTime - (tick * 2), DotScale);
        }
    }
}
