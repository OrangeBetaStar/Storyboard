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
    public class SGL : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("");
            var centreThing = layer.CreateSprite("SB/Onion0.png");
            centreThing.Move(242884,268656,404+64+50,32+55+15,404+64+50,32+55+15);
            var centreThing2 = layer.CreateSprite("SB/Onion0.png");
            centreThing2.Move(242884,268656,405+64+50,310+55+15,405+64+50,320+55+15);
        }
    }
}
