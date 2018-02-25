using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace PasteEx.Util
{
    /// <summary>
    /// Reference:
    /// https://stackoverflow.com/questions/14333336/how-can-i-convert-a-cf-dibv5-from-clipboard-format17-to-a-transparent-bitmap
    /// https://stackoverflow.com/questions/44177115/copying-from-and-to-clipboard-loses-image-transparency/46424800
    /// 
    /// C++ like
    /// </summary>
    public class ImageHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPV5HEADER
        {
            public uint bV5Size;
            public int bV5Width;
            public int bV5Height;
            public UInt16 bV5Planes;
            public UInt16 bV5BitCount;
            public uint bV5Compression;
            public uint bV5SizeImage;
            public int bV5XPelsPerMeter;
            public int bV5YPelsPerMeter;
            public UInt16 bV5ClrUsed;
            public UInt16 bV5ClrImportant;
            public UInt16 bV5RedMask;
            public UInt16 bV5GreenMask;
            public UInt16 bV5BlueMask;
            public UInt16 bV5AlphaMask;
            public UInt16 bV5CSType;
            public IntPtr bV5Endpoints;
            public UInt16 bV5GammaRed;
            public UInt16 bV5GammaGreen;
            public UInt16 bV5GammaBlue;
            public UInt16 bV5Intent;
            public UInt16 bV5ProfileData;
            public UInt16 bV5ProfileSize;
            public UInt16 bV5Reserved;
        }
        /// <summary>
        /// Code written by RamonEeza from
        /// https://stackoverflow.com/questions/14333336/how-can-i-convert-a-cf-dibv5-from-clipboard-format17-to-a-transparent-bitmap
        /// Use PixelFormat.Format32bppPArgb instead of PixelFormat.Format32bppArgb
        /// It's has a problem of the right shift of the picture
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Bitmap</returns>
        public static Bitmap CF_DIBV5ToBitmap(byte[] data)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var bmi = (BITMAPV5HEADER)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(BITMAPV5HEADER));
            Bitmap bitmap = new Bitmap((int)bmi.bV5Width, (int)bmi.bV5Height, -
                                       (int)(bmi.bV5SizeImage / bmi.bV5Height), PixelFormat.Format32bppPArgb,
                                       new IntPtr(handle.AddrOfPinnedObject().ToInt32()
                                       + bmi.bV5Size + (bmi.bV5Height - 1)
                                       * (int)(bmi.bV5SizeImage / bmi.bV5Height)));
            handle.Free();
            return bitmap;
        }

        public static Bitmap CloneImage(Bitmap bm)
        {
            int stride;
            return BuildImage(GetImageData(bm, out stride), bm.Width, bm.Height, stride, bm.PixelFormat, null, null);
        }

        /// <summary>
        /// Converts the image to Device Independent Bitmap format of type BITFIELDS.
        /// This is (wrongly) accepted by many applications as containing transparency,
        /// so I'm abusing it for that.
        /// </summary>
        /// <param name="image">Image to convert to DIB</param>
        /// <returns>The image converted to DIB, in bytes.</returns>
        public static Byte[] ConvertToDib(Image image)
        {
            Byte[] bm32bData;
            Int32 width = image.Width;
            Int32 height = image.Height;
            // Ensure image is 32bppARGB by painting it on a new 32bppARGB image.
            using (Bitmap bm32b = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics gr = Graphics.FromImage(bm32b))
                    gr.DrawImage(image, new Rectangle(0, 0, bm32b.Width, bm32b.Height));
                // Bitmap format has its lines reversed.
                bm32b.RotateFlip(RotateFlipType.Rotate180FlipX);
                Int32 stride;
                bm32bData = GetImageData(bm32b, out stride);
            }
            // BITMAPINFOHEADER struct for DIB.
            Int32 hdrSize = 0x28;
            Byte[] fullImage = new Byte[hdrSize + 12 + bm32bData.Length];
            //Int32 biSize;
            WriteIntToByteArray(fullImage, 0x00, 4, true, (UInt32)hdrSize);
            //Int32 biWidth;
            WriteIntToByteArray(fullImage, 0x04, 4, true, (UInt32)width);
            //Int32 biHeight;
            WriteIntToByteArray(fullImage, 0x08, 4, true, (UInt32)height);
            //Int16 biPlanes;
            WriteIntToByteArray(fullImage, 0x0C, 2, true, 1);
            //Int16 biBitCount;
            WriteIntToByteArray(fullImage, 0x0E, 2, true, 32);
            //BITMAPCOMPRESSION biCompression = BITMAPCOMPRESSION.BITFIELDS;
            WriteIntToByteArray(fullImage, 0x10, 4, true, 3);
            //Int32 biSizeImage;
            WriteIntToByteArray(fullImage, 0x14, 4, true, (UInt32)bm32bData.Length);
            // These are all 0. Since .net clears new arrays, don't bother writing them.
            //Int32 biXPelsPerMeter = 0;
            //Int32 biYPelsPerMeter = 0;
            //Int32 biClrUsed = 0;
            //Int32 biClrImportant = 0;

            // The aforementioned "BITFIELDS": colour masks applied to the Int32 pixel value to get the R, G and B values.
            WriteIntToByteArray(fullImage, hdrSize + 0, 4, true, 0x00FF0000);
            WriteIntToByteArray(fullImage, hdrSize + 4, 4, true, 0x0000FF00);
            WriteIntToByteArray(fullImage, hdrSize + 8, 4, true, 0x000000FF);
            Array.Copy(bm32bData, 0, fullImage, hdrSize + 12, bm32bData.Length);
            return fullImage;
        }

        public static Bitmap ImageFromClipboardDib(Byte[] dibBytes)
        {
            if (dibBytes == null || dibBytes.Length < 4)
                return null;
            try
            {
                Int32 headerSize = (Int32)ReadIntFromByteArray(dibBytes, 0, 4, true);
                // Only supporting 40-byte DIB from clipboard
                if (headerSize != 40)
                    return null;
                Byte[] header = new Byte[40];
                Array.Copy(dibBytes, header, 40);
                Int32 imageIndex = headerSize;
                Int32 width = (Int32)ReadIntFromByteArray(header, 0x04, 4, true);
                Int32 height = (Int32)ReadIntFromByteArray(header, 0x08, 4, true);
                Int16 planes = (Int16)ReadIntFromByteArray(header, 0x0C, 2, true);
                Int16 bitCount = (Int16)ReadIntFromByteArray(header, 0x0E, 2, true);
                //Compression: 0 = RGB; 3 = BITFIELDS.
                Int32 compression = (Int32)ReadIntFromByteArray(header, 0x10, 4, true);
                // Not dealing with non-standard formats.
                if (planes != 1 || (compression != 0 && compression != 3))
                    return null;
                PixelFormat fmt;
                switch (bitCount)
                {
                    case 32:
                        fmt = PixelFormat.Format32bppRgb;
                        break;
                    case 24:
                        fmt = PixelFormat.Format24bppRgb;
                        break;
                    case 16:
                        fmt = PixelFormat.Format16bppRgb555;
                        break;
                    default:
                        return null;
                }
                if (compression == 3)
                    imageIndex += 12;
                if (dibBytes.Length < imageIndex)
                    return null;
                Byte[] image = new Byte[dibBytes.Length - imageIndex];
                Array.Copy(dibBytes, imageIndex, image, 0, image.Length);
                // Classic stride: fit within blocks of 4 bytes.
                Int32 stride = (((((bitCount * width) + 7) / 8) + 3) / 4) * 4;
                if (compression == 3)
                {
                    UInt32 redMask = ReadIntFromByteArray(dibBytes, headerSize + 0, 4, true);
                    UInt32 greenMask = ReadIntFromByteArray(dibBytes, headerSize + 4, 4, true);
                    UInt32 blueMask = ReadIntFromByteArray(dibBytes, headerSize + 8, 4, true);
                    // Fix for the undocumented use of 32bppARGB disguised as BITFIELDS. Despite lacking an alpha bit field,
                    // the alpha bytes are still filled in, without any header indication of alpha usage.
                    // Pure 32-bit RGB: check if a switch to ARGB can be made by checking for non-zero alpha.
                    // Admitted, this may give a mess if the alpha bits simply aren't cleared, but why the hell wouldn't it use 24bpp then?
                    if (bitCount == 32 && redMask == 0xFF0000 && greenMask == 0x00FF00 && blueMask == 0x0000FF)
                    {
                        // Stride is always a multiple of 4; no need to take it into account for 32bpp.
                        for (Int32 pix = 3; pix < image.Length; pix += 4)
                        {
                            // 0 can mean transparent, but can also mean the alpha isn't filled in, so only check for non-zero alpha,
                            // which would indicate there is actual data in the alpha bytes.
                            if (image[pix] == 0)
                                continue;
                            fmt = PixelFormat.Format32bppPArgb;
                            break;
                        }
                    }
                    else
                        // Could be supported with a system that parses the colour masks,
                        // but I don't think the clipboard ever uses these anyway.
                        return null;
                }
                Bitmap bitmap = BuildImage(image, width, height, stride, fmt, null, null);
                // This is bmp; reverse image lines.
                bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the raw bytes from an image.
        /// </summary>
        /// <param name="sourceImage">The image to get the bytes from.</param>
        /// <param name="stride">Stride of the retrieved image data.</param>
        /// <returns>The raw bytes of the image</returns>
        public static Byte[] GetImageData(Bitmap sourceImage, out Int32 stride)
        {
            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), ImageLockMode.ReadOnly, sourceImage.PixelFormat);
            stride = sourceData.Stride;
            Byte[] data = new Byte[stride * sourceImage.Height];
            Marshal.Copy(sourceData.Scan0, data, 0, data.Length);
            sourceImage.UnlockBits(sourceData);
            return data;
        }

        /// <summary>
        /// Creates a bitmap based on data, width, height, stride and pixel format.
        /// </summary>
        /// <param name="sourceData">Byte array of raw source data</param>
        /// <param name="width">Width of the image</param>
        /// <param name="height">Height of the image</param>
        /// <param name="stride">Scanline length inside the data</param>
        /// <param name="pixelFormat">Pixel format</param>
        /// <param name="palette">Color palette</param>
        /// <param name="defaultColor">Default color to fill in on the palette if the given colors don't fully fill it.</param>
        /// <returns>The new image</returns>
        public static Bitmap BuildImage(Byte[] sourceData, Int32 width, Int32 height, Int32 stride, PixelFormat pixelFormat, Color[] palette, Color? defaultColor)
        {
            Bitmap newImage = new Bitmap(width, height, pixelFormat);
            BitmapData targetData = newImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, newImage.PixelFormat);
            CopyToMemory(targetData.Scan0, sourceData, 0, sourceData.Length, stride, targetData.Stride);
            newImage.UnlockBits(targetData);
            // For indexed images, set the palette.
            if ((pixelFormat & PixelFormat.Indexed) != 0 && palette != null)
            {
                ColorPalette pal = newImage.Palette;
                for (Int32 i = 0; i < pal.Entries.Length; i++)
                {
                    if (i < palette.Length)
                        pal.Entries[i] = palette[i];
                    else if (defaultColor.HasValue)
                        pal.Entries[i] = defaultColor.Value;
                    else
                        break;
                }
                newImage.Palette = pal;
            }
            return newImage;
        }
        public static void CopyToMemory(IntPtr target, Byte[] bytes, Int32 startPos, Int32 length, Int32 origStride, Int32 targetStride)
        {
            Int32 sourcePos = startPos;
            IntPtr destPos = target;
            Int32 minStride = Math.Min(origStride, targetStride);
            while (length >= targetStride)
            {
                Marshal.Copy(bytes, sourcePos, destPos, minStride);
                length -= origStride;
                sourcePos += origStride;
                destPos = new IntPtr(destPos.ToInt64() + targetStride);
            }
            if (length > 0)
            {
                Marshal.Copy(bytes, sourcePos, destPos, length);
            }
        }

        public static void WriteIntToByteArray(Byte[] data, Int32 startIndex, Int32 bytes, Boolean littleEndian, UInt32 value)
        {
            Int32 lastByte = bytes - 1;
            if (data.Length < startIndex + bytes)
                throw new ArgumentOutOfRangeException("startIndex", "Data array is too small to write a " + bytes + "-byte value at offset " + startIndex + ".");
            for (Int32 index = 0; index < bytes; index++)
            {
                Int32 offs = startIndex + (littleEndian ? index : lastByte - index);
                data[offs] = (Byte)(value >> (8 * index) & 0xFF);
            }
        }

        public static UInt32 ReadIntFromByteArray(Byte[] data, Int32 startIndex, Int32 bytes, Boolean littleEndian)
        {
            Int32 lastByte = bytes - 1;
            if (data.Length < startIndex + bytes)
                throw new ArgumentOutOfRangeException("startIndex", "Data array is too small to read a " + bytes + "-byte value at offset " + startIndex + ".");
            UInt32 value = 0;
            for (Int32 index = 0; index < bytes; index++)
            {
                Int32 offs = startIndex + (littleEndian ? index : lastByte - index);
                value += (UInt32)(data[offs] << (8 * index));
            }
            return value;
        }
    }
}
