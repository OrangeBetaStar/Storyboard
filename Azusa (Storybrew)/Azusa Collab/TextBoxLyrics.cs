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
    public class TextBoxLyrics : StoryboardObjectGenerator
    {
        [Configurable]
        public int startPoint = 17704;

        [Configurable]
        public int endPoint = 24487;

        [Configurable]
        public Color4 textBoxColour = new Color4(255, 255, 255, 100);

        public override void Generate()
        {
		    int whiteTick = 18748 - 18357;
            double size = 0.55;
            double fadeValue = 0.7;

		    var textBox = GetLayer("").CreateSprite("SB/textBox.png", OsbOrigin.Centre);
            textBox.Color(startPoint, startPoint, textBoxColour, textBoxColour);
            textBox.Scale(startPoint, startPoint, size, size);
            textBox.Move(startPoint, startPoint + (whiteTick * 2), 260, 380, 260, 380);
            textBox.Fade(startPoint, startPoint + (whiteTick * 1), 0, fadeValue);
            textBox.Fade(startPoint + (whiteTick * 1), endPoint - (whiteTick * 2), fadeValue, fadeValue);
            textBox.Fade(endPoint - (whiteTick * 2), endPoint, fadeValue, 0);
            
            
        }
    }
}
