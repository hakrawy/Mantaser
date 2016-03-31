﻿using System.Collections.Generic;
using System.IO;

namespace MediaPoint.Subtitles.Logic.Mp4.Boxes
{
    public class Moov : Box
    {
        public readonly Mvhd Mvhd;
        public readonly List<Trak> Tracks;

        public Moov(FileStream fs, ulong maximumLength)
        {
            Tracks = new List<Trak>();
            pos = (ulong) fs.Position;
            while (fs.Position < (long)maximumLength)
            {
                if (!InitializeSizeAndName(fs))
                    return;

                if (name == "trak")
                    Tracks.Add(new Trak(fs, pos));
                else if (name == "mvhd")
                    Mvhd = new Mvhd(fs, pos);

                fs.Seek((long)pos, SeekOrigin.Begin);
            }
        }
    }
}
