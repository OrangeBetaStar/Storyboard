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

namespace StorybrewScripts
{
    public class VisualNovelButton : StoryboardObjectGenerator
    {
        [Configurable]
        public int startTime = 0;
        
        [Configurable]
        public int endTime = 0;
        public override void Generate()
        {
            int buttonX = 500 + 65;
            int skipY = 280 + 55;
            int whiteTick = 18748 - 18357;
            int tick = whiteTick / 4;
            double size = 0.07;
            int moveThing = 10;
            int ySpacing = 17;

            // Skip

            for(int v=startTime; v<endTime; v++) {
                var skip = GetLayer("").CreateSprite("SB/skip.png", OsbOrigin.Centre);
                skip.Move(OsbEasing.Out, v + (whiteTick * 2), v + (whiteTick * 6), buttonX, skipY, buttonX + moveThing, skipY);
                if((v + whiteTick * 8) < endTime) {
                    skip.Fade(v, v + (whiteTick * 7), 1, 1);
                    skip.Fade(v + (whiteTick * 7), v + (whiteTick * 8), 1, 0);
                }
                else {
                    skip.Fade(endTime - (whiteTick * 7), endTime, 1, 0);
                }
                skip.Scale(v, v, size, size);
                v = v + ((whiteTick * 2) - 1);
            }

            // Auto

            skipY = skipY + ySpacing;
            int realBeat = 18748 - 18226;
            var auto = GetLayer("").CreateSprite("SB/auto.png", OsbOrigin.Centre);
            auto.Move(startTime, startTime, buttonX + 5, skipY, buttonX + 5, skipY);
            for(int a=startTime + ((realBeat / 4) * 2); a<endTime; a++) {
                auto.Scale(OsbEasing.In, a, a + (realBeat / 4), size, size + 0.02);
                auto.Scale(a + (realBeat / 4), a + ((realBeat / 4) * 4), size + 0.02, size);
                a = a + (realBeat - 1);
            }
            auto.Fade(startTime, endTime - (whiteTick * 2), 1, 1);
            auto.Fade(endTime - (whiteTick * 2), endTime, 1, 0);

            // Next

            skipY = skipY + ySpacing;
            var next = GetLayer("").CreateSprite("SB/prevnext.png", OsbOrigin.Centre);
            next.ScaleVec(startTime, startTime, -size, size, -size, size);
            next.Move(startTime, startTime, buttonX + 5, skipY, buttonX + 5, skipY);
            for(int a=startTime + ((realBeat / 4) * 2); a<endTime; a++) {
                next.ScaleVec(OsbEasing.In, a, a + (realBeat / 4), -size, size, -(size + 0.02), size + 0.02);
                next.ScaleVec(a + (realBeat / 4), a + ((realBeat / 4) * 4), -(size + 0.02), size + 0.02, -size, size);
                a = a + (realBeat - 1);
            }
            next.Fade(startTime, endTime - (whiteTick * 2), 1, 1);
            next.Fade(endTime - (whiteTick * 2), endTime, 1, 0);

            // Prev

            skipY = skipY + ySpacing;
            var prev = GetLayer("").CreateSprite("SB/prevnext.png", OsbOrigin.Centre);
            prev.Move(startTime, startTime, buttonX + 5, skipY, buttonX + 5, skipY);
            for(int a=startTime + ((realBeat / 4) * 2); a<endTime; a++) {
                prev.Scale(OsbEasing.In, a, a + (realBeat / 4), size, size + 0.02);
                prev.Scale(a + (realBeat / 4), a + ((realBeat / 4) * 4), size + 0.02, size);
                a = a + (realBeat - 1);
            }
            prev.Fade(startTime, endTime - (whiteTick * 2), 1, 1);
            prev.Fade(endTime - (whiteTick * 2), endTime, 1, 0);

            // Save

            skipY = skipY + ySpacing;
            var save = GetLayer("").CreateSprite("SB/save.png", OsbOrigin.Centre);
            save.Move(startTime, startTime, buttonX + 5, skipY, buttonX + 5, skipY);
            for(int a=startTime + ((realBeat / 4) * 2); a<endTime; a++) {
                save.Scale(OsbEasing.In, a, a + (realBeat / 4), size, size + 0.02);
                save.Scale(a + (realBeat / 4), a + ((realBeat / 4) * 4), size + 0.02, size);
                a = a + (realBeat - 1);
            }
            save.Fade(startTime, endTime - (whiteTick * 2), 1, 1);
            save.Fade(endTime - (whiteTick * 2), endTime, 1, 0);

            // Load

            double angle = 0;
            double angleAdd = -1.5;

            skipY = skipY + ySpacing;
            var load = GetLayer("").CreateSprite("SB/load.png", OsbOrigin.Centre);
            load.Move(startTime, startTime, buttonX + 5, skipY, buttonX + 5, skipY);
            load.Scale(startTime, startTime, size + 0.02, size + 0.02);
            for(int a=startTime + ((realBeat / 4) * 3); a<endTime; a++) {
                load.Rotate(OsbEasing.Out, a, a + ((realBeat / 4) * 4), angle, angle + angleAdd);
                a = a + (realBeat - 1);
                angle = angle + angleAdd;
            }
            load.Fade(startTime, endTime - (whiteTick * 2), 1, 1);
            load.Fade(endTime - (whiteTick * 2), endTime, 1, 0);
        }
    }
}
