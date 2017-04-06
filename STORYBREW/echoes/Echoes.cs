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
    public class Echoes : StoryboardObjectGenerator
    {
        [Configurable]
        public string white = "SB/twoByTwo.jpg";

        [Configurable]
        public int start = 85034;

        [Configurable]
        public int end = 95101;

        [Configurable]
        public Color4 startColour = new Color4(0, 255, 128, 255);

        [Configurable]
        public Color4 endColour = new Color4(0, 191, 255, 255);
        

        public int tick = 1075 - 873; // 1/4 tick

        public override void Generate()
        {
		    var sprite = GetLayer("").CreateSprite(white, OsbOrigin.Centre);
            sprite.Color(start, end, startColour, endColour);
            sprite.Fade(start, start + tick, 0, 1);
            sprite.Fade(start + tick, end - tick, 1, 0.5);
            sprite.Fade(end - tick, end, 0.5, 0);
            sprite.ScaleVec(start, 430, 240);


            var bar = GetLayer("").CreateSprite(white, OsbOrigin.Centre);
            bar.Color(start, end, endColour, startColour);
            bar.ScaleVec(OsbEasing.Out, start, end, 0, 3, 300, 3);
            bar.Fade(start, start + tick, 0, 1);
            bar.Fade(start + tick, end - tick, 1, 0.5);
            bar.Fade(end - tick, end, 0.5, 0);
            
        }
    }
}
