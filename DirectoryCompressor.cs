using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace RescaleAllImages
{
    class DirectoryCompressor
    {

        public int resolutionIndex=0;
        public int qualityIndex = 0;
        public string selectedPath = "";
        public bool processSubfolders = false;
        public bool createBackups = true;
        private MainForm app;

        public DirectoryCompressor(MainForm aApp)
        {
            app = aApp;
        }

        private void setPreview(String path)
        {
            app.Invoke(app.setPreview,new object[]{(string)path});
        }

        private void log(string message)
        {
            app.Invoke(app.log, new object[] { (string)message });
        }

        private void done()
        {
            app.Invoke(app.onDone);
        }

        private void registerTempFile(string path)
        {
            app.Invoke(app.registerTempFile, new object[] { (string)path });
        }

        private void writeImage(string srcPath, string dstPath)
        {
            var image = Image.FromFile(srcPath, false);

            int orientation = 0;

            try
            {
                orientation = (int)image.GetPropertyItem(274).Value[0];
            }
            catch (Exception) { }

            switch (orientation)
            {
                case 1:
                    // No rotation required.
                    break;
                case 2:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case 3:
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 4:
                    image.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case 5:
                    image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 6:
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 7:
                    image.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                case 8:
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }


            var imgAr = (float)image.Width / (float)image.Height;

            float dstW = 320;
            float dstH = 240;
            switch (resolutionIndex)
            {
                case 0: dstW = 320; dstH = 240; break;
                case 1: dstW = 640; dstH = 480; break;
                case 2: dstW = 1280; dstH = 960; break;
                case 3: dstW = 1920; dstH = 1440; break;
                case 4: dstW = 3840; dstH = 2880; break;
                case 5: dstW = 7680; dstH = 5760; break;
            }


            long dstQ = 25;
            switch (qualityIndex)
            {
                case 0: dstQ = 15; break;
                case 1: dstQ = 30; break;
                case 2: dstQ = 50; break;
                case 3: dstQ = 70; break;
                case 4: dstQ = 90; break;
                case 5: dstQ = 99; break;
            }


            var dstAr = dstW / dstH;

            int w, h;
            if (image.Width < dstW && image.Height < dstH)
            {
                w = image.Width;
                h = image.Height;
            }
            else if (imgAr < dstAr)
            {
                w = (int)(dstH * imgAr);
                h = (int)dstH;
            }
            else
            {
                w = (int)dstW;
                h = (int)(dstW / imgAr);
            }

            var destRect = new Rectangle(0, 0, w, h);
            var bmp = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            var graphics = Graphics.FromImage(bmp);

            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            image.Dispose();

            var quality = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, dstQ);
            var settings = new EncoderParameters(1);
            var codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo encoder = null;
            for (var j = 0; j < codecs.Length; j++)
            {
                if (codecs[j].MimeType == "image/jpeg")
                {
                    encoder = codecs[j];
                }
            }

            settings.Param[0] = quality;

            bmp.Save(dstPath, encoder, settings);
            Thread.Sleep(100);
        }





        public void genPreview()
        {
            var samplePath = getFirstImage(selectedPath);

            if (samplePath != "")
            {
                var path = Path.GetTempFileName();
                try
                {
                    writeImage(samplePath, path);

                    registerTempFile(path);

                    setPreview(path);
                }
                catch (Exception)
                {
                    log(String.Format(RescaleAllImagesStrings.preview_error, samplePath));
                }

            }
            else
            {
                setPreview("");
            }

        }

        public string getFirstImage(string path)
        {
            if (path == "" || !Directory.Exists(path))
                return "";
            var files = System.IO.Directory.GetFiles(path);
            for (var i = 0; i < files.Length; i++)
            {
                var ext = Path.GetExtension(files[i]).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                {
                    return files[i];
                }
            }

            if (processSubfolders)
            {
                var subDirs = Directory.GetDirectories(path);

                for (var i = 0; i < subDirs.Length; i++)
                {
                    var dirName = Path.GetFileName(subDirs[i]).ToLower();
                    if (dirName != "originale")
                    {
                        return getFirstImage(subDirs[i]);
                    }
                }
            }

            return "";
        }

        public void compressFolder()
        {
            try
            {
                compressFolder(selectedPath);
                log("Fertig.\r\n\r\n");
            }
            catch (Exception e)
            {
                log(e.Message+"\r\n\r\n");
            }
            done();
        }

        public void compressFolder(string path)
        {
            var files = System.IO.Directory.GetFiles(path);
            var logged = false;
            var subDirCreated = false;
            for (var i = 0; i < files.Length; i++)
            {
                var ext = Path.GetExtension(files[i]).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                {

                    if (!logged)
                    {
                        log(String.Format(RescaleAllImagesStrings.process_start, path));
                    }
                    log(".");

                    logged = true;

                    if (createBackups)
                    {

                        try
                        {
                            if (createBackups && !subDirCreated && !Directory.Exists(path + "\\Originale"))
                            {
                                Directory.CreateDirectory(path + "\\Originale");
                                subDirCreated = true;

                            }
                        }
                        catch (Exception)
                        {
                            log(RescaleAllImagesStrings.process_backup_dir_error);
                        }
                        var bakPath = Path.GetDirectoryName(files[i]) + "\\Originale\\" + Path.GetFileName(files[i]);

                        var bakBase = Path.GetDirectoryName(files[i]) + "\\Originale\\" + Path.GetFileNameWithoutExtension(files[i]);
                        var bakExt = Path.GetExtension(files[i]);
                        var bakIndex = 0;

                        while (File.Exists(bakPath))
                        {
                            bakPath = bakBase + " (" + bakIndex.ToString() + ")" + bakExt;
                            bakIndex++;
                        }

                        var fileMoved = false;
                        for (var j = 0; j < 16 && !fileMoved; j++)
                        {
                            try
                            {
                                File.Move(files[i], bakPath);
                                fileMoved = true;
                            }
                            catch (Exception)
                            {
                                Thread.Sleep(100);
                            }
                        }

                        if (!fileMoved)
                        {
                            
                            log(String.Format(RescaleAllImagesStrings.process_backup_move_original_error, files[i]));
                            try
                            {
                                File.Copy(files[i], bakPath);
                                log(RescaleAllImagesStrings.process_backup_move_original_fallback_success);
                            }
                            catch (Exception)
                            {
                                throw new Exception(RescaleAllImagesStrings.process_backup_move_original_fatal);
                            }
                        }
                        else
                        {
                            var dstPath = Path.GetDirectoryName(files[i]) + "\\" + Path.GetFileNameWithoutExtension(files[i]) + ".jpg";
                            try
                            {
                                writeImage(bakPath, dstPath);
                            }
                            catch (Exception)
                            {
                                log(String.Format(RescaleAllImagesStrings.process_compress_error, files[i]));
                                try
                                {
                                    File.Copy(bakPath, dstPath);
                                    log(RescaleAllImagesStrings.process_compress_fallback_success);
                                }
                                catch (Exception)
                                {
                                    throw new Exception(RescaleAllImagesStrings.process_compress_error_fatal);

                                }
                            }
                        }
                    }
                    else
                    {
                        var dstPath = Path.GetDirectoryName(files[i]) + "\\" + Path.GetFileNameWithoutExtension(files[i]) + ".jpg";
                        if (Path.GetExtension(files[i]).ToLower() == ".jpg")
                        {
                            try
                            {
                                writeImage(files[i], files[i]);
                            }
                            catch (Exception)
                            {
                                log(RescaleAllImagesStrings.process_nobackup_error);
                            }
                        }
                        else
                        {
                            try
                            {
                                writeImage(files[i], dstPath);
                                try
                                {
                                    File.Delete(files[i]);
                                }
                                catch (Exception)
                                {
                                    log(String.Format(RescaleAllImagesStrings.process_nobackup_error_delete_source, files[i]));
                                }
                            }
                            catch (Exception)
                            {
                                log(String.Format(RescaleAllImagesStrings.process_nobackup_error, files[i]));
                            }


                        }

                    }
                }
            }

            if (logged)
            {

                log("\r\n\r\n");
            }

            if (processSubfolders)
            {
                var subDirs = Directory.GetDirectories(path);

                for (var i = 0; i < subDirs.Length; i++)
                {
                    var dirName = Path.GetFileName(subDirs[i]).ToLower();
                    if (dirName != "originale")
                    {
                        compressFolder(subDirs[i]);
                    }
                }
            }

        }
    }

    public class DirectorCompressorSettings{
            public int selectedQuality;
            public int selectedResolution;
            public string selectedPath;
            public bool processSubfolders;
            public bool createBackups;
    }
}
