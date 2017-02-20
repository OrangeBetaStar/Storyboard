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
    public class Loading : StoryboardObjectGenerator {

    [Configurable]
    public int startTime = 0;
    
    [Configurable]
    public int endTime = 0;

        public override void Generate()
        {
            int centreX = 440;
            int centreY = 395;
            int size = 5;
            int whiteTick = 18748 - 18357;
            int tick = whiteTick / 4;
            double angle = 0;
            double angleDiff = 0.156;
            int distance = 20;
            int endDelay = 200;

            for(int a=0; a<11; a++) {
                var loadingPixel = GetLayer("").CreateSprite("SB/twoByTwo.jpg", OsbOrigin.Centre);
                loadingPixel.Scale(startTime, startTime, size, size);
                for(int i=startTime; i<endTime; i++) {
                    loadingPixel.Move(i, i + tick, centreX + distance * Math.Sin(angle), centreY + distance * Math.Cos(angle), centreX + distance * Math.Sin(angle + angleDiff), centreY + distance * Math.Cos(angle + angleDiff));
                    loadingPixel.Rotate(i, i + tick, -angle, -(angle+angleDiff));
                    angle = angle + angleDiff;
                    i = i + (tick - 1);
                }
                loadingPixel.Move(endTime, endTime + endDelay, centreX + distance * Math.Sin(angle + angleDiff), centreY + distance * Math.Cos(angle + angleDiff), centreX + (distance + 20) * Math.Sin(angle + angleDiff), centreY + (distance + 20) * Math.Cos(angle + angleDiff));
                loadingPixel.Fade(endTime, endTime + endDelay, 1, 0);
                angle = (a * 4) * angleDiff;
            }

            int sizeO = 5;
            double angleO = 0;
            double angleDiffO = 0.156;
            int distanceO = 40;

            for(int a=0; a<11; a++) {
                var loadingPixel = GetLayer("").CreateSprite("SB/twoByTwo.jpg", OsbOrigin.Centre);
                loadingPixel.Scale(startTime, startTime, sizeO, sizeO);
                for(int i=startTime; i<endTime; i++) {
                    loadingPixel.Move(i, i + tick, centreX + distanceO * Math.Sin(angleO), centreY + distanceO * Math.Cos(angleO), centreX + distanceO * Math.Sin(angleO - angleDiffO), centreY + distanceO * Math.Cos(angleO - angleDiffO));
                    loadingPixel.Rotate(i, i + tick, (angleO+angleDiffO), angleO );
                    angleO = angleO - angleDiffO;
                    i = i + (tick - 1);
                }
                loadingPixel.Move(endTime, endTime + endDelay, centreX + distanceO * Math.Sin(angleO - angleDiffO), centreY + distanceO * Math.Cos(angleO - angleDiffO), centreX, centreY);
                loadingPixel.Fade(endTime, endTime + endDelay, 1, 0);
                angleO = (a * 4) * angleDiffO;
            }
        }
    }
}