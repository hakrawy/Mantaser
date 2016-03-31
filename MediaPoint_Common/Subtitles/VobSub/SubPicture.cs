﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace MediaPoint.Subtitles.Logic.VobSub
{
    /// <summary>
    /// Subtitle Picture - see http://www.mpucoder.com/DVD/spu.html for more info
    /// </summary>
    public class SubPicture
    {
        private enum DisplayControlCommand
        {
            ForcedStartDisplay = 0,
            StartDisplay = 1,
            StopDisplay = 2,
            SetColor = 3,
            SetContrast = 4,
            SetDisplayArea = 5,
            SetPixelDataAddress = 6,
            ChangeColorAndContrast = 7,
            End = 0xFF,
        }

        public readonly int SubPictureDateSize;
        public TimeSpan Delay;
        public int BufferSize { get { return _data.Length; } }
        private readonly byte[] _data;
        public Rectangle ImageDisplayArea;
        public bool Forced { get; private set; }
        private int _pixelDataAddressOffset = 0;
        private int _startDisplayControlSequenceTableAddress;

        public SubPicture(byte[] data)
        {
            _data = data;
            SubPictureDateSize = Helper.GetEndianWord(_data, 0);
            _startDisplayControlSequenceTableAddress = Helper.GetEndianWord(_data, 2);
            ParseDisplayControlCommands(false, null, null, false);
        }

        /// <summary>
        /// For SP packet with dvd subpictures
        /// </summary>
        /// <param name="data">Byte data buffer</param>
        /// <param name="startDisplayControlSequenceTableAddress">Address of first control sequence in data</param>
        /// <param name="pixelDataAddressOffset">Bitmap pixel data address offset</param>
        public SubPicture(byte[] data, int startDisplayControlSequenceTableAddress, int pixelDataAddressOffset)
        {
            _data = data;
            SubPictureDateSize = _data.Length;
            _startDisplayControlSequenceTableAddress = startDisplayControlSequenceTableAddress;
            _pixelDataAddressOffset = pixelDataAddressOffset;
            ParseDisplayControlCommands(false, null, null, false);
        }

        /// <summary>
        /// Generates the current subtitle image
        /// </summary>
        /// <param name="colorLookupTable">The Color LookUp Table (CLUT), if null then only the four colors are used (should contain 16 elements if not null)</param>
        /// <param name="background">Background color</param>
        /// <param name="pattern">Color</param>
        /// <param name="emphasis1">Color</param>
        /// <param name="emphasis2">Color</param>
        /// <returns>Subtitle image</returns>
        public Bitmap GetBitmap(List<Color> colorLookupTable, Color background, Color pattern, Color emphasis1, Color emphasis2, bool useCustomColors)
        {
            var fourColors = new List<Color> { background, pattern, emphasis1, emphasis2 };
            return ParseDisplayControlCommands(true, colorLookupTable, fourColors, useCustomColors);
        }

        private Bitmap ParseDisplayControlCommands(bool createBitmap, List<Color> colorLookUpTable, List<Color> fourColors, bool useCustomColors)
        {
            ImageDisplayArea = new Rectangle();
            Bitmap bmp = null;
            var displayControlSequenceTableAddresses = new List<int>();
            byte[] imageContrast = null;
            int imageTopFieldDataAddress = 0;
            int imageBottomFieldDataAddress = 0;
            bool bitmapGenerated = false;
            double largestDelay = -999999;
            int displayControlSequenceTableAddress = _startDisplayControlSequenceTableAddress - _pixelDataAddressOffset;
            int lastDisplayControlSequenceTableAddress = 0;
            displayControlSequenceTableAddresses.Add(displayControlSequenceTableAddress);
            int commandIndex = 0;
            while (displayControlSequenceTableAddress > lastDisplayControlSequenceTableAddress && displayControlSequenceTableAddress + 1 < _data.Length && commandIndex < _data.Length)
            {
                int delayBeforeExecute = Helper.GetEndianWord(_data, displayControlSequenceTableAddress + _pixelDataAddressOffset);
                commandIndex = displayControlSequenceTableAddress + 4 + _pixelDataAddressOffset;
                int command = _data[commandIndex];
                int numberOfCommands = 0;
                while (command != 0xFF && numberOfCommands < 1000 && commandIndex < _data.Length)
                {
                    numberOfCommands++;
                    switch (command)
                    {
                        case (int)DisplayControlCommand.ForcedStartDisplay: // 0
                            Forced = true;
                            commandIndex++;
                            break;
                        case (int)DisplayControlCommand.StartDisplay: // 1
                            commandIndex++;
                            break;
                        case (int)DisplayControlCommand.StopDisplay: // 2
                            Delay = TimeSpan.FromMilliseconds(((delayBeforeExecute << 10) + 1023) / 90.0);
                            if (createBitmap && Delay.TotalMilliseconds > largestDelay) // in case of more than one images, just use the one with the largest display time
                            {
                                largestDelay = Delay.TotalMilliseconds;
                                bmp = GenerateBitmap(ImageDisplayArea, imageTopFieldDataAddress, imageBottomFieldDataAddress, fourColors);
                                bitmapGenerated = true;
                            }
                            commandIndex++;
                            break;
                        case (int)DisplayControlCommand.SetColor: // 3
                            if (colorLookUpTable != null && fourColors.Count == 4)
                            {
                                byte[] imageColor = new[] { _data[commandIndex + 1], _data[commandIndex + 2] };
                                if (!useCustomColors)
                                {
                                    SetColor(fourColors, 3, imageColor[0] >> 4, colorLookUpTable);
                                    SetColor(fourColors, 2, imageColor[0] & Helper.B00001111, colorLookUpTable);
                                    SetColor(fourColors, 1, imageColor[1] >> 4, colorLookUpTable);
                                    SetColor(fourColors, 0, imageColor[1] & Helper.B00001111, colorLookUpTable);
                                }
                            }
                            commandIndex += 3;
                            break;
                        case (int)DisplayControlCommand.SetContrast: // 4
                            if (colorLookUpTable != null && fourColors.Count == 4)
                            {
                                imageContrast = new[] { _data[commandIndex + 1], _data[commandIndex + 2] };
                                if (imageContrast[0] + imageContrast[1] > 0)
                                {
                                    SetTransparency(fourColors, 3, (imageContrast[0] & 0xF0) >> 4);
                                    SetTransparency(fourColors, 2, imageContrast[0] & Helper.B00001111);
                                    SetTransparency(fourColors, 1, (imageContrast[1] & 0xF0) >> 4);
                                    SetTransparency(fourColors, 0, imageContrast[1] & Helper.B00001111);
                                }
                            }
                            commandIndex += 3;
                            break;
                        case (int)DisplayControlCommand.SetDisplayArea: // 5
                            if (_data.Length > commandIndex + 6)
                            {
                                string binary = Helper.GetBinaryString(_data, commandIndex + 1, 6);
                                int startingX = (int)Helper.GetUInt32FromBinaryString(binary.Substring(0, 12));
                                int endingX = (int)Helper.GetUInt32FromBinaryString(binary.Substring(12, 12));
                                int startingY = (int)Helper.GetUInt32FromBinaryString(binary.Substring(24, 12));
                                int endingY = (int)Helper.GetUInt32FromBinaryString(binary.Substring(36, 12));
                                ImageDisplayArea = new Rectangle(startingX, startingY, endingX - startingX, endingY - startingY);
                            }
                            commandIndex += 7;
                            break;
                        case (int)DisplayControlCommand.SetPixelDataAddress: // 6
                            imageTopFieldDataAddress = Helper.GetEndianWord(_data, commandIndex + 1) + _pixelDataAddressOffset;
                            imageBottomFieldDataAddress = Helper.GetEndianWord(_data, commandIndex + 3) + _pixelDataAddressOffset;
                            commandIndex += 5;
                            break;
                        case (int)DisplayControlCommand.ChangeColorAndContrast: // 7
                            commandIndex++;
                            //int parameterAreaSize = (int)Helper.GetEndian(_data, commandIndex, 2);
                            int parameterAreaSize = _data[commandIndex + 1]; // this should be enough??? (no larger than 255 bytes)
                            if (colorLookUpTable != null)
                            {
                                //TODO: set fourColors
                            }
                            commandIndex += parameterAreaSize;
                            break;
                        case (int)DisplayControlCommand.End: // FF (255) - Stop looping of Display Control Commands
                            break;
                        default:
                            commandIndex++;
                            break;
                    }
                    if (commandIndex >= _data.Length) // in case of bad files...
                        break;
                    command = _data[commandIndex];
                }

                lastDisplayControlSequenceTableAddress = displayControlSequenceTableAddress;
                if (_pixelDataAddressOffset == -4)
                    displayControlSequenceTableAddress = Helper.GetEndianWord(_data, commandIndex+3);
                else
                    displayControlSequenceTableAddress = Helper.GetEndianWord(_data, displayControlSequenceTableAddress + 2);
            }
            if (createBitmap && !bitmapGenerated) // StopDisplay not needed (delay will be zero - should be just before start of next subtitle)
                bmp = GenerateBitmap(ImageDisplayArea, imageTopFieldDataAddress, imageBottomFieldDataAddress, fourColors);

            return bmp;
        }

        private static void SetColor(List<Color> fourColors, int fourColorIndex, int clutIndex, List<Color> colorLookUpTable)
        {
            if (clutIndex >= 0 && clutIndex < colorLookUpTable.Count && fourColorIndex >= 0)
                fourColors[fourColorIndex] = colorLookUpTable[clutIndex];
        }

        private static void SetTransparency(List<Color> fourColors, int fourColorIndex, int alpha)
        {
            // alpha: 0x0 = transparent, 0xF = opaque (in C# 0 is fully transparent, and 255 is fully opaque so we have to multiply by 17)

            if (fourColorIndex >= 0)
                fourColors[fourColorIndex] = Color.FromArgb(alpha * 17, fourColors[fourColorIndex].R, fourColors[fourColorIndex].G, fourColors[fourColorIndex].B);
        }

        private Bitmap GenerateBitmap(Rectangle imageDisplayArea, int imageTopFieldDataAddress, int imageBottomFieldDataAddress, List<Color> fourColors)
        {
            if (imageDisplayArea.Width <= 0 || imageDisplayArea.Height <= 0)
                return new Bitmap(1,1);

            var bmp = new Bitmap(imageDisplayArea.Width + 1, imageDisplayArea.Height + 1);
            if (fourColors[0] != Color.Transparent)
            {
                Graphics gr = Graphics.FromImage(bmp);
                gr.FillRectangle(new SolidBrush(fourColors[0]), new Rectangle(0, 0, bmp.Width, bmp.Height));
            }
            var fastBmp = new FastBitmap(bmp);
            fastBmp.LockImage();
            GenerateBitmap(_data, fastBmp, 0, imageTopFieldDataAddress, fourColors);
            GenerateBitmap(_data, fastBmp, 1, imageBottomFieldDataAddress, fourColors);
            Bitmap cropped = CropBitmapAndUnlok(fastBmp, fourColors[0]);
            bmp.Dispose();
            return cropped;
        }

        private static Bitmap CropBitmapAndUnlok(FastBitmap bmp, Color backgroundColor)
        {
            int y = 0;
            int x;
            Color c = backgroundColor;

            // Crop top
            while (y < bmp.Height && IsBackgroundColor(c, backgroundColor))
            {
                c = bmp.GetPixel(0, y);
                if (IsBackgroundColor(c, backgroundColor))
                {
                    for (x = 1; x < bmp.Width; x++)
                    {
                        c = bmp.GetPixelNext();
                        if (!IsBackgroundColor(c, backgroundColor))
                            break;
                    }
                }
                if (IsBackgroundColor(c, backgroundColor))
                    y++;
            }
            int minY = y;
            if (minY > 3)
                minY -= 3;
            else
                minY = 0;

            // Crop left
            x = 0;
            c = backgroundColor;
            while (x < bmp.Width && IsBackgroundColor(c, backgroundColor))
            {
                for (y = minY; y < bmp.Height; y++)
                {
                    c = bmp.GetPixel(x, y);
                    if (!IsBackgroundColor(c, backgroundColor))
                        break;
                }
                if (IsBackgroundColor(c, backgroundColor))
                    x++;
            }
            int minX = x;
            if (minX > 3)
                minX -= 3;
            else
                minX -= 0;

            // Crop bottom
            y = bmp.Height-1;
            c = backgroundColor;
            while (y > minY && IsBackgroundColor(c, backgroundColor))
            {
                c = bmp.GetPixel(0, y);
                if (IsBackgroundColor(c, backgroundColor))
                {
                    for (x = 1; x < bmp.Width; x++)
                    {
                        c = bmp.GetPixelNext();
                        if (!IsBackgroundColor(c, backgroundColor))
                            break;
                    }
                }
                if (IsBackgroundColor(c, backgroundColor))
                    y--;
            }
            int maxY = y + 7;
            if (maxY >= bmp.Height)
                maxY = bmp.Height - 1;

            // Crop right
            x = bmp.Width - 1;
            c = backgroundColor;
            while (x > minX && IsBackgroundColor(c, backgroundColor))
            {
                for (y = minY; y < bmp.Height; y++)
                {
                    c = bmp.GetPixel(x, y);
                    if (!IsBackgroundColor(c, backgroundColor))
                        break;
                }
                if (IsBackgroundColor(c, backgroundColor))
                    x--;
            }
            int maxX = x + 7;
            if (maxX >= bmp.Width)
                maxX = bmp.Width - 1;

            bmp.UnlockImage();
            Bitmap bmpImage = bmp.GetBitmap();
            if (bmpImage.Width > 1 && bmpImage.Height > 1 && maxX - minX > 0 && maxY - minY > 0)
            {
                Bitmap bmpCrop = bmpImage.Clone(new Rectangle(minX, minY, maxX - minX, maxY - minY), bmpImage.PixelFormat);
                return bmpCrop;
            }
            return (Bitmap)bmpImage.Clone();
        }

        private static bool IsBackgroundColor(Color c, Color backgroundColor)
        {
            return c.ToArgb() == backgroundColor.ToArgb() || c.A == 0;
        }

        private static void GenerateBitmap(byte[] data, FastBitmap bmp, int startY, int dataAddress, List<Color> fourColors)
        {
            int index = 0;
            bool onlyHalf = false;
            int y = startY;
            int x = 0;
            while (y < bmp.Height && dataAddress + index + 2 < data.Length)
            {
                int runLength;
                int color;
                bool restOfLine;
                index += DecodeRle(dataAddress + index, data, out color, out runLength, ref onlyHalf, out restOfLine);
                if (restOfLine)
                    runLength = bmp.Width - x;

                Color c = fourColors[color]; // set color via the four colors
                for (int i = 0; i < runLength; i++, x++)
                {
                    if (x >= bmp.Width-1)
                    {
                        if (y < bmp.Height && x < bmp.Width && c != fourColors[0])
                            bmp.SetPixel(x, y, c);

                        if (onlyHalf)
                        {
                            onlyHalf = false;
                            index++;
                        }
                        x = 0;
                        y += 2;
                        break;
                    }
                    if (y < bmp.Height && c != fourColors[0])
                       bmp.SetPixel(x, y, c);
                }
            }
        }

        private static int DecodeRle(int index, byte[] data, out int color, out int runLength, ref bool onlyHalf, out bool restOfLine)
        {
            //Value      Bits   n=length, c=color
            //1-3        4      n n c c                           (half a byte)
            //4-15       8      0 0 n n n n c c                   (one byte)
            //16-63     12      0 0 0 0 n n n n n n c c           (one and a half byte)
            //64-255    16      0 0 0 0 0 0 n n n n n n n n c c   (two bytes)
            // When reaching EndOfLine, index is byte aligned (skip 4 bits if necessary)
            restOfLine = false;
            string binary2 = Helper.GetBinaryString(data, index, 3);
            if (onlyHalf)
                binary2 = binary2.Substring(4);

            if (binary2.StartsWith("000000"))
            {
                runLength = (int)Helper.GetUInt32FromBinaryString(binary2.Substring(6, 8));
                color = (int)Helper.GetUInt32FromBinaryString(binary2.Substring(14, 2));
                if (runLength == 0)
                {
                    // rest of line + skip 4 bits if Only half
                    restOfLine = true;
                    if (onlyHalf)
                    {
                        onlyHalf = false;
                        return 3;
                    }
                }
                return 2;
            }

            if (binary2.StartsWith("0000"))
            {
                runLength = (int)Helper.GetUInt32FromBinaryString(binary2.Substring(4, 6));
                color = (int)Helper.GetUInt32FromBinaryString(binary2.Substring(10, 2));
                if (onlyHalf)
                {
                    onlyHalf = false;
                    return 2;
                }
                onlyHalf = true;
                return 1;
            }

            if (binary2.StartsWith("00"))
            {
                runLength = (int)Helper.GetUInt32FromBinaryString(binary2.Substring(2, 4));
                color = (int)Helper.GetUInt32FromBinaryString(binary2.Substring(6, 2));
                return 1;
            }

            runLength = (int)Helper.GetUInt32FromBinaryString(binary2.Substring(0, 2));
            color = (int)Helper.GetUInt32FromBinaryString(binary2.Substring(2, 2));
            if (onlyHalf)
            {
                onlyHalf = false;
                return 1;
            }
            onlyHalf = true;
            return 0;
        }

    }
}
