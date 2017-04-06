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
using System.Drawing;
using System.Text;

namespace StorybrewScripts
{
    public class LyricCollabNamesXDddd : StoryboardObjectGenerator
    {
        [Configurable]
        public string SubtitlesPath = "echoes sub test.ass";

        [Configurable]
        public string FontName = "02うつくし明朝体";

        [Configurable]
        public string OutputPath = "SB/wow";

        [Configurable]
        public string PixelPath = "SB/dot.png";

        [Configurable]
        public char EffectLetter = 'x';

        [Configurable]
        public int FontSize = 30;

        [Configurable]
        public float FontScale = 0.5f;

        [Configurable]
        public double DotScale = 0.1;

        [Configurable]
        public Color4 FontColor = Color4.White;

        [Configurable]
        public FontStyle FontStyle = FontStyle.Regular;

        [Configurable]
        public int OutlineThickness = 3;

        [Configurable]
        public Color4 OutlineColor = new Color4(50, 50, 50, 200);

        [Configurable]
        public int ShadowThickness = 0;

        [Configurable]
        public Color4 ShadowColor = new Color4(0, 0, 0, 100);

        [Configurable]
        public float PaddingX = 0;

        [Configurable]
        public float PaddingY = 0;

        [Configurable]
        public Vector2 Origin = new Vector2(320, 240);

        [Configurable]
        public bool Debug = false;

        [Configurable]
        public int Fineness = 2;
        [Configurable]
        public bool GenerateText = true;

        List<OsbSprite> spriteStorage = new List<OsbSprite>();
        List<string> textPath = new List<string>();
        List<int> startTime = new List<int>();
        List<int> endTime = new List<int>();

        public int tick = 1075 - 873; // 1/4 tick
        public bool toggle = true;

        public override void Generate()
        {
            var subtitles = LoadSubtitles(SubtitlesPath);

            if (GenerateText)
            {
                CharacterGeneration(subtitles);
            }
            else
            {
                AddThings(subtitles);
                for (int i = 0; i < textPath.Count; i++)
                {
                    Pixelize(textPath[i], startTime[i], endTime[i]);
                }
            }
        }
        public FontGenerator texturething(string path)
        {
            var font = LoadFont(path, new FontDescription()
            {
                FontPath = FontName,
                FontSize = FontSize,
                Color = FontColor,
                Padding = new Vector2(PaddingX, PaddingY),
                FontStyle = FontStyle,
                Debug = Debug,
            },
            new FontOutline()
            {
                Thickness = OutlineThickness,
                Color = OutlineColor,
            },
            new FontShadow()
            {
                Thickness = ShadowThickness,
                Color = ShadowColor,
            });

            var subtitles = LoadSubtitles(SubtitlesPath);
            return font;
        }
        public void CharacterGeneration(SubtitleSet subtitles)
        {
            string RealOutput = OutputPath + "\\";
            int lineNum = 0;

            if (Directory.Exists(MapsetPath + "\\" + OutputPath))
            {
                foreach (var file in Directory.GetFiles(MapsetPath + "\\" + OutputPath))
                {
                    Directory.Delete(file, true);
                }
            }

            foreach (var line in subtitles.Lines)
            {
                if (!line.Equals(null))
                {
                    var texture = texturething(RealOutput + lineNum + EffectLetter).GetTexture(line.Text);
                    lineNum++;
                }
                else {continue;}
            }
        }
        public void AddThings(SubtitleSet subtitles)
        {
            string RealOutput = OutputPath + "\\";
            int lineNum = 0;
            foreach (var line in subtitles.Lines)
            {
                if (!line.Equals(null))
                {
                    textPath.Add(OutputPath + "\\" + lineNum + EffectLetter + "\\_000.png");
                    startTime.Add((int)line.StartTime);
                    endTime.Add((int)line.EndTime);
                    lineNum++;
                }
                else {continue;}
            }
        }
        public void Pixelize(string path, int start, int end)
        {
            Bitmap textBitmap = GetMapsetBitmap(path);
            int spriteNum = 0;
            for (int x = 0; x < textBitmap.Width; x += Fineness)
            {
                for (int y = 0; y < textBitmap.Height; y += Fineness)
                {
                    Vector2 spritePos = new Vector2(Origin.X - (textBitmap.Width / 2) + x, Origin.Y - (textBitmap.Height / 2) + y);
                    Color pixelColor = textBitmap.GetPixel(x, y);

                    if (pixelColor.A > 0)
                    {
                        if (spriteNum < spriteStorage.Count)
                        {
                            SpriteCommands(spriteStorage[spriteNum], start, end, pixelColor, new Vector2(spritePos.X, spritePos.Y), path);
                            spriteNum++;
                        }
                        else
                        {
                            var sprite = GetLayer("").CreateSprite(PixelPath, OsbOrigin.Centre);
                            spriteStorage.Add(sprite);
                            SpriteCommands(sprite, start, end, pixelColor, new Vector2(spritePos.X, spritePos.Y), path);
                            spriteNum++;
                        }
                    }
                    else {continue;}
                }
            }
            textBitmap.Dispose();
            toggle = true;
        }
        public void SpriteCommands(OsbSprite sprite, int StartTime, int EndTime, Color pixelColor, Vector2 location, string path)
        {
            int OffsetX = 0; //Random(-50, 50);
            int OffsetY = 0; //Random(-50, 50);
            int OffsetXAfterEffect = Random(-50, 50);
            int OffsetYAfterEffect = Random(-50, 50);
            // sprite.Move(0 , StartTime, StartTime + (tick * 2), OffsetX + location.X, OffsetY + location.Y, location.X, location.Y);
            sprite.Move(OsbEasing.In, EndTime - (tick * 4), EndTime, location.X, location.Y, location.X + OffsetXAfterEffect, location.Y+OffsetYAfterEffect);
            // sprite.Fade(OsbEasing.Out, StartTime, StartTime + (tick * 2), 0, 0.5);
            sprite.Fade(StartTime + (tick * 2), EndTime - (tick * 4), 0 , 0);
            sprite.Fade(EndTime - (tick * 4), EndTime, 1, 0);
            sprite.Color(StartTime, pixelColor);
            sprite.Scale(StartTime, DotScale);

            if(toggle) {
            var mainSprite = GetLayer("").CreateSprite(path, OsbOrigin.Centre);
            mainSprite.Move(StartTime + (tick * 2), Origin);
            mainSprite.Fade(OsbEasing.Out, StartTime,  StartTime + (tick * 2), 0, 1);
            mainSprite.Fade(StartTime + (tick * 2), EndTime - (tick * 4), 1, 1  );
            toggle = false; // toggle so that main lyric .png is ran once.
            }
        }
    }
}
