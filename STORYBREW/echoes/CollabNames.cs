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
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Drawing;

namespace StorybrewScripts
{
    public class CollabNames : StoryboardObjectGenerator
    {
        [Configurable]
        public string CollabName = "CollabNames.txt";

        [Configurable]
        public string OutputPath = "SB/Collab";

        [Configurable]
        public string PixelPath = "SB/pixel.png";

        [Configurable]
        public float SpriteScale = 0.3f;

        [Configurable]
        public float SpriteFade = 0.4f;

        [Configurable]
        public string FontName = "Times New Roman";

        [Configurable]
        public int FontSize = 16;

        [Configurable]
        public Color4 FontColor = new Color4(0, 0, 0, 255);

        [Configurable]
        public int ShadowSize = 3;

        [Configurable]
        public Color4 ShadowColor = new Color4(0, 0, 0, 255);

        [Configurable]
        public int OutlineSize = 2;

        [Configurable]
        public Color4 OutlineColor = new Color4(0, 0, 0, 255);

        [Configurable]
        public int XOrigin = 320;

        [Configurable]
        public int YOrigin = 240;

        [Configurable]
        public bool ChangeFontPicture = true;

        private int o = 2457;
        private int WhiteTick = 169639 - 169296;

        public void CreateName()
        {
            var font = LoadFont(OutputPath, new FontDescription()
                {
                    FontPath = FontName,
                    FontSize = FontSize * 2,
                    Color = FontColor,
                    Padding = new Vector2(0, 0),
                },
                new FontOutline()
                {
                    Thickness = OutlineSize,
                    Color = OutlineColor,
                },
                new FontShadow()
                {
                    Thickness = ShadowSize,
                    Color = ShadowColor,
                });
            string ActualPath = "projects/his see/" + CollabName;

            string text = File.ReadAllText(ActualPath);

            using (StringReader reader = new StringReader(text))
            {
                string name;
                while ((name = reader.ReadLine()) != null)
                {  
                    var texture = font.GetTexture(name);
                }
            }
        }
        public void xChose(int NameInt)
        {
            var layer = GetLayer("");
            var NamePath = OutputPath + "/_00" + NameInt + ".png";

            Bitmap image = GetMapsetBitmap(NamePath);
            var sprite2 = layer.CreateSprite(NamePath, OsbOrigin.Centre);
            
            
            int[] NameTimes = {
                /*
                16039, 37982,
                59925, 80496,
                103810, 125753,
                151810, 158668,
                169639, 180268,
                191582, 202553,
                224496, 248839,
                260153, 270782,
                293068, 315010
                */
                }; //start, end, start, end...
            for(int PositionY = 0; PositionY < image.Height; PositionY += 4)
            {
                for(int PositionX = 0; PositionX < image.Width; PositionX += 4)
                {
                    var color = image.GetPixel(PositionX, PositionY);

                    if(color.A > 0) 
                    {
                        var sprite = layer.CreateSprite(PixelPath, OsbOrigin.Centre);
                        int ActualXPosition = (XOrigin - (image.Width / 4)) + PositionX / 2;
                        int ActualYPosition = (YOrigin - (image.Height / 4)) + PositionY / 2;
                        int OffsetX = Random(-40, 40);
                        int OffsetY = Random(-40, 40);

                        var FirstArrayNumber = NameTimes[0];
                        sprite.Scale(FirstArrayNumber, SpriteScale);

                        for (int i = 0; i < NameTimes.Length; i += 2)
                        {
                            var PositionInArray = NameTimes[i];
                            if (i + 1 < NameTimes.Length)
                            {
                                var PositionInArrayNext = NameTimes[i + 1];

                                sprite.Fade(PositionInArray, PositionInArray + 1000, 0, SpriteFade);
                                sprite.Fade(PositionInArray + 1000, 0);
                                sprite.Fade(PositionInArrayNext, PositionInArrayNext + 500, 0, SpriteFade / 2);
                                sprite.Fade(PositionInArrayNext + 500, PositionInArrayNext + 1000, SpriteFade / 2, 0);

                                sprite2.Move(PositionInArray + 1000, XOrigin, YOrigin);
                                sprite2.Fade(PositionInArray + 1000, SpriteFade);
                                sprite2.Fade(PositionInArrayNext, PositionInArrayNext + 500, SpriteFade, 0);
                                sprite2.Scale(PositionInArray + 1000, 0.5);
                                
                                sprite.Move(PositionInArray, PositionInArray + 1000, ActualXPosition + OffsetX, ActualYPosition + OffsetY, ActualXPosition, ActualYPosition);

                                sprite.Color(PositionInArray, PositionInArray + 1000, Color.White, color);
                                sprite.Color(PositionInArrayNext + 400, PositionInArrayNext + 1000, color, Color.White);
                            }
                            else {continue;}
                        }
                    }
                    else {continue;}
                }
            }
            image.Dispose();
        }
        public void Kise(int NameInt)
        {
            var layer = GetLayer("");
            var NamePath = OutputPath + "/_00" + NameInt + ".png";
            Bitmap image = GetMapsetBitmap(NamePath);
            var sprite2 = layer.CreateSprite(NamePath, OsbOrigin.Centre);
            
            int[] NameTimes = {
                5067, 16039
                /*
                37982, 59925,
                81525, 103810,
                125753, 147696 - 1000,
                158668, 169639,
                180610, 191582,
                249182, 260153,
                271125, 293068,
                315010, 336953
                */
                }; //start, end, start, end...
            for(int PositionY = 0; PositionY < image.Height; PositionY += 4)
            {
                for(int PositionX = 0; PositionX < image.Width; PositionX += 4)
                {
                    var color = image.GetPixel(PositionX, PositionY);

                    if(color.A > 0) 
                    {
                        var sprite = layer.CreateSprite(PixelPath, OsbOrigin.Centre);
                        int ActualXPosition = (XOrigin - (image.Width / 4)) + (PositionX / 2);
                        int ActualYPosition = (YOrigin - (image.Height / 4)) + (PositionY / 2);
                        int OffsetX = Random(-40, 40);
                        int OffsetY = Random(-40, 40);

                        var FirstArrayNumber = NameTimes[0];
                        sprite.Scale(FirstArrayNumber, SpriteScale);

                        for (int i = 0; i < NameTimes.Length; i += 2)
                        {
                            var PositionInArray = NameTimes[i];
                            if (i + 1 < NameTimes.Length)
                            {
                                var PositionInArrayNext = NameTimes[i + 1];

                                sprite.Fade(PositionInArray, PositionInArray + 1000, 0, SpriteFade);
                                sprite.Fade(PositionInArray + 1000, 0);
                                sprite.Fade(PositionInArrayNext, PositionInArrayNext + 500, 0, SpriteFade / 2);
                                sprite.Fade(PositionInArrayNext + 500, PositionInArrayNext + 1000, SpriteFade / 2, 0);

                                sprite2.Move(PositionInArray + 1000, XOrigin, YOrigin);
                                sprite2.Fade(PositionInArray + 1000, SpriteFade);
                                sprite2.Fade(PositionInArrayNext, PositionInArrayNext + 500, SpriteFade, 0);
                                sprite2.Scale(PositionInArray + 1000, 0.5);
                                
                                sprite.Move(PositionInArray, PositionInArray + 1000, ActualXPosition + OffsetX, ActualYPosition + OffsetY, ActualXPosition, ActualYPosition);

                                sprite.Color(PositionInArray, PositionInArray + 1000, Color.White, color);
                                sprite.Color(PositionInArrayNext + 400, PositionInArrayNext + 1000, color, Color.White);
                            }
                            else {continue;}
                        }
                    }
                    else {continue;}
                }
            }
            image.Dispose();
        }
        public override void Generate()
        {
            if (ChangeFontPicture)
            {
                CreateName();
                Log("Make sure to turn this off to get the sprites back!");
            }

            if (!ChangeFontPicture)
            {
                Kise(0);
                xChose(1);
            }
        }
    }
}
