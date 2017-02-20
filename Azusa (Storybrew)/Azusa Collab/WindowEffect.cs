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
    public class WindowEffect : StoryboardObjectGenerator
    {
        [Configurable]
        public int startPoint = 0;
        public override void Generate()
        {
        //    int startPoint = 17704;
		    int whiteTick = 18748 - 18357;
            int degree = 45;
            int tick = whiteTick / 4;
            double xWindow = 0.8;
            double yWindow = 1.4;
            double lightness = 1.0;

            var whiteBlow = GetLayer("").CreateSprite("SB/twoByTwo.jpg", OsbOrigin.Centre);
            whiteBlow.Fade(startPoint - whiteTick, startPoint, 0, 1);
            whiteBlow.Fade(startPoint, startPoint + (tick * 8), 1, 0);
            whiteBlow.ScaleVec(startPoint - whiteTick, startPoint - whiteTick, 427, 240, 427, 240);
            
            for(int i=0; i<8; i++) {
                if(i % 2 == 0) {
                    // Flipped image
                    if(i % 4 == 0) {
                        var windowPanel = GetLayer("").CreateSprite("SB/panel.png", OsbOrigin.TopRight);
                        windowPanel.Rotate(startPoint, startPoint, MathHelper.DegreesToRadians((i * degree) + 180), MathHelper.DegreesToRadians((i * degree) + 180));
                        windowPanel.Fade(startPoint, startPoint + tick * (i), lightness ,lightness);
                        windowPanel.Fade(startPoint + tick * (i), startPoint + tick * (i + 1), lightness, 0);
                        windowPanel.ScaleVec(startPoint, startPoint, -xWindow, xWindow, -xWindow, xWindow);
                    }
                    else if(i % 4 == 2) {
                        var windowPanel = GetLayer("").CreateSprite("SB/panel.png", OsbOrigin.TopRight);
                        windowPanel.Rotate(startPoint, startPoint, MathHelper.DegreesToRadians((i * degree) + 180), MathHelper.DegreesToRadians((i * degree) + 180));
                        windowPanel.Fade(startPoint, startPoint + tick * (i), lightness ,lightness);
                        windowPanel.Fade(startPoint + tick * (i), startPoint + tick * (i + 1), lightness, 0);
                        windowPanel.ScaleVec(startPoint, startPoint, -yWindow, yWindow, -yWindow, yWindow);
                    }
                }
                else if(i % 2 == 1) {
                    // Non-Flipped image
                    if(i % 4 == 1) {
                        var windowPanel = GetLayer("").CreateSprite("SB/panel.png", OsbOrigin.TopLeft);
                        windowPanel.Rotate(startPoint, startPoint, MathHelper.DegreesToRadians((i * degree) + 180 + 45), MathHelper.DegreesToRadians((i * degree) + 180 + 45));
                        windowPanel.Fade(startPoint, startPoint + tick * (i), lightness ,lightness);
                        windowPanel.Fade(startPoint + tick * (i), startPoint + tick * (i + 1), lightness, 0);
                        windowPanel.ScaleVec(startPoint, startPoint, yWindow, yWindow, yWindow, yWindow);
                    }
                    else if(i % 4 == 3) {
                        var windowPanel = GetLayer("").CreateSprite("SB/panel.png", OsbOrigin.TopLeft);
                        windowPanel.Rotate(startPoint, startPoint, MathHelper.DegreesToRadians((i * degree) + 180 + 45), MathHelper.DegreesToRadians((i * degree) + 180 + 45));
                        windowPanel.Fade(startPoint, startPoint + tick * (i), lightness ,lightness);
                        windowPanel.Fade(startPoint + tick * (i), startPoint + tick * (i + 1), lightness, 0);
                        windowPanel.ScaleVec(startPoint, startPoint, xWindow, xWindow, xWindow, xWindow);
                    }
                }
            }
        }

        /*
        public double decToRad (int dec) {
            
            return (dec * (Math.PI / 180));
        }
        */
    }
}
