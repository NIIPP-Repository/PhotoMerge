using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace PhotoMerge
{
    public partial class PhotoMerge : Form
    {
        public PhotoMerge()
        {
            InitializeComponent();
        }

        public class MicrographMerge
        {
            // ********************************************************
            // Переменные класса
            int stepX = 0;
            int stepY = 0;
            int width = 0;
            int height = 0;
            int alignAreaX = 0;
            int alignAreaY = 0;
            Queue<String> queue = new Queue<string>();
            bool needAlign;
            // ********************************************************



            private int DeltaColor(Color col2, Color col1)
            {
                return Math.Abs((col2.R - col1.R) + (col2.B - col1.B) + (col2.G - col1.G));
            }

            private int FindStep(Bitmap bmpLeft, Bitmap bmpRight)
            {
                int w = bmpLeft.Width,
                    h = bmpLeft.Height,
                    delta = 20;
                int colMin = 3 * 256 * h,
                    ans = 0;
                for (int res = 20; res <= w; res++)
                {
                    int curr = 0;
                    for (int i = 0; i < h; i++)
                    {
                        Color colLeft = bmpLeft.GetPixel(w - res, i),
                                  colRight = bmpRight.GetPixel(delta, i);
                        curr += DeltaColor(colLeft, colRight);
                    }
                    if (curr < colMin)
                    {
                        colMin = curr;
                        ans = res;
                    }
                }
                ans += delta;

                return ans;
            }

            private void AlignEdge(Bitmap prevImage, Bitmap newImage, int delta, int halfStep)
            {
                for (int l = 0; l < newImage.Height; l++)
                    for (int c = 0; c < delta; c++)
                    {
                        double v = (c + 0.5) / (delta),
                            w = 1 - v;
                        Color col1 = newImage.GetPixel(c, l);
                        Color col2 = prevImage.GetPixel(prevImage.Width - halfStep + c, l);
                        Color colRes = Color.FromArgb((byte)(col1.A * v + col2.A * w), (byte)(col1.R * v + col2.R * w), (byte)(col1.G * v + col2.G * w), (byte)(col1.B * v + col2.B * w));
                        newImage.SetPixel(c, l, colRes);
                    }
            }

            private Bitmap CombineBitmapQuads(string[] files)
            {
                List<Bitmap> images = new List<Bitmap>();
                Bitmap finalImage = null;

                try
                {
                    int width = 0;
                    int height = 0;
                    foreach (string image in files)
                    {
                        Bitmap bitmap = new Bitmap(image);
                        width += bitmap.Width;
                        height = bitmap.Height > height ? bitmap.Height : height;

                        images.Add(bitmap);
                    }
                    width -= stepX * (images.Count - 1);

                    finalImage = new Bitmap(width, height);
                    using (Graphics g = Graphics.FromImage(finalImage))
                    {
                        g.Clear(Color.Black);

                        int offset = 0;
                        int i = 0;
                        Bitmap prev = null;
                        foreach (Bitmap image in images)
                        {
                            i++;
                            Bitmap newImage;
                            if (i == 1)
                            {
                                newImage = (Bitmap)image.Clone(new RectangleF(0, 0, image.Width - stepX / 4, image.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                                g.DrawImage(newImage, new Rectangle(offset, 0, newImage.Width, newImage.Height));
                                offset += newImage.Width - stepX / 2;
                                prev = newImage;
                            }
                            if (i > 1)
                            {
                                if (i < images.Count)
                                    newImage = (Bitmap)image.Clone(new RectangleF(stepX / 4, 0, image.Width - stepX / 2, image.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                                else
                                    newImage = (Bitmap)image.Clone(new RectangleF(stepX / 4, 0, image.Width - stepX / 4, image.Height), System.Drawing.Imaging.PixelFormat.DontCare);

                                // сглаживание
                                if (needAlign)
                                    AlignEdge(prev, newImage, alignAreaX, stepX / 2);

                                g.DrawImage(newImage, new Rectangle(offset, 0, newImage.Width, newImage.Height));
                                offset += newImage.Width - stepX / 2;
                                prev = newImage;
                            }
                        }
                    }

                    return finalImage;
                }
                catch (Exception ex)
                {
                    if (finalImage != null)
                        finalImage.Dispose();

                    throw ex;
                }
                finally
                {
                    //clean up memory
                    foreach (Bitmap image in images)
                    {
                        image.Dispose();
                    }
                }
            }

            private Bitmap CombineBitmapColums(List<Bitmap> images)
            {
                Bitmap finalImage = null;
                try
                {
                    int width = 0;
                    int height = 0;
                    foreach (Bitmap bitmap in images)
                    {
                        width += bitmap.Width;
                        height = bitmap.Height > height ? bitmap.Height : height;
                    }
                    width -= stepY * (images.Count - 1);

                    finalImage = new Bitmap(width, height);
                    using (Graphics g = Graphics.FromImage(finalImage))
                    {
                        g.Clear(Color.Black);

                        int offset = 0;
                        int i = 0;
                        Bitmap prev = null;
                        foreach (Bitmap image in images)
                        {
                            i++;
                            Bitmap newImage;
                            if (i == 1)
                            {
                                newImage = (Bitmap)image.Clone(new RectangleF(0, 0, image.Width - stepY / 4, image.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                                g.DrawImage(newImage, new Rectangle(offset, 0, newImage.Width, newImage.Height));
                                offset += newImage.Width - stepY / 2;
                                prev = newImage;
                            }
                            if (i > 1)
                            {
                                if (i < images.Count)
                                    newImage = (Bitmap)image.Clone(new RectangleF(stepY / 4, 0, image.Width - stepY / 2, image.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                                else
                                    newImage = (Bitmap)image.Clone(new RectangleF(stepY / 4, 0, image.Width - stepY / 4, image.Height), System.Drawing.Imaging.PixelFormat.DontCare);

                                // сглаживание
                                if (needAlign)
                                    AlignEdge(prev, newImage, alignAreaY, stepY / 2);

                                g.DrawImage(newImage, new Rectangle(offset, 0, newImage.Width, newImage.Height));
                                offset += newImage.Width - stepY / 2;
                                prev = newImage;
                            }
                        }
                    }

                    return finalImage;
                }
                catch (Exception ex)
                {
                    if (finalImage != null)
                        finalImage.Dispose();

                    throw ex;
                }
                finally
                {
                   // clean up memory
                    foreach (Bitmap image in images)
                    {
                        image.Dispose();
                    }
                }
            }

            private int GetYCoord(String str)
            {
                int len = str.IndexOf('_');
                return Int32.Parse(str.Substring(0, len));
            }

            private int GetXCoord(String str)
            {
                int len = str.IndexOf('.') - str.IndexOf('_') - 1;
                return Int32.Parse(str.Substring(str.IndexOf('_') + 1, len));
            }

            public Bitmap GetWholePicture()
            {
                List<Bitmap> bmpStrings = new List<Bitmap>();
                Queue<String> bmpString = new Queue<string>();
                int y = 0;
                int countOfBmpStrings = 0;
                foreach (String path in queue)
                {
                    String fileName = Path.GetFileName(path);
                    if (y != GetYCoord(fileName))
                    {
                        bmpStrings.Add(CombineBitmapQuads(bmpString.ToArray()));
                        bmpString.Clear();
                        countOfBmpStrings++;
                        y = GetYCoord(fileName);
                    }
                    bmpString.Enqueue(path);
                }
                if (bmpString.Count > 0)
                {
                    bmpStrings.Add(CombineBitmapQuads(bmpString.ToArray()));
                    bmpString.Clear();
                    countOfBmpStrings++;
                }

                for (int i = 0; i < countOfBmpStrings; i++)
                {
                    bmpStrings[i].RotateFlip(RotateFlipType.Rotate270FlipNone);
                }

                Bitmap bmp = CombineBitmapColums(bmpStrings);
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);

                return bmp;
            }

            public Bitmap GetCalibrPicture(int stepX, int stepY, int alignAreaX, int alignAreaY)
            {
                this.stepX = stepX;
                this.stepY = stepY;
                this.alignAreaX = alignAreaX;
                this.alignAreaY = alignAreaY;

                List<Bitmap> bmpStrings = new List<Bitmap>();
                Queue<String> bmpString = new Queue<string>();
                int y = 0;
                int countOfRowElem = 0;
                int countOfBmpStrings = 0;
                int i = 0;
                foreach (String path in queue)
                {
                    String fileName = Path.GetFileName(path);
                    if (y != GetYCoord(fileName))
                    {
                        countOfRowElem = bmpString.Count;
                        countOfBmpStrings++;
                        i = 0;
                        if (countOfBmpStrings > 1 && countOfBmpStrings < 4)
                        {
                            bmpStrings.Add(CombineBitmapQuads(bmpString.ToArray()));
                        }
                        bmpString.Clear();
                        y = GetYCoord(fileName);
                    }
                    i++;
                    if (i > 1 && i < 4)
                        bmpString.Enqueue(path);
                }
                if (bmpString.Count > 0)
                {
                    countOfBmpStrings++;
                    i = 0;
                    if (countOfBmpStrings > 1 && countOfBmpStrings < 4)
                    {
                        bmpStrings.Add(CombineBitmapQuads(bmpString.ToArray()));
                    }
                    bmpString.Clear();
                }

                for (int k = 0; k < Math.Min(2, bmpStrings.Count); k++)
                {
                    bmpStrings[k].RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                Bitmap bmp = CombineBitmapColums(bmpStrings);
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);

                return bmp;
            }

            private void FindingOfShift(Queue<String> queue)
            {
                String[] mas = queue.ToArray();
                Bitmap first = new Bitmap(mas[0]),
                       second = new Bitmap(mas[1]),
                       third = null;
                stepX = FindStep(first, second);

                int y = GetYCoord(Path.GetFileName(mas[0]));
                foreach (String path in queue)
                {
                    String fileName = Path.GetFileName(path);
                    if (y != GetYCoord(fileName))
                    {
                        third = new Bitmap(path);
                    }
                }

                if (third != null)
                {
                    first.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    third.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    stepY = FindStep(first, third);
                }

                // MessageBox.Show("X = " + stepX.ToString() + "; Y = " + stepY.ToString());
            }

            private void FindFiles(String pathToFolder)
            {
                DirectoryInfo di = new DirectoryInfo(pathToFolder);
                foreach (FileInfo fi in di.GetFiles())
                {
                    String ext = Path.GetExtension(fi.Name);
                    if (!(ext == ".bmp" || ext == ".jpeg" || ext == ".jpg" || ext == ".png"))
                        continue;
                    queue.Enqueue(fi.FullName);
                }
                if (queue.Count > 0)
                {
                    Bitmap bmp = new Bitmap( queue.Peek() );
                    width = bmp.Width;
                    height = bmp.Height;
                }
            }

            public int getWidth()
            {
                return width;
            }

            public int getHeight()
            {
                return height;
            }

            public MicrographMerge(String pathToFolder, int stepX, int stepY, bool needAlign, int alignAreaX, int alignAreaY)
            {
                FindFiles(pathToFolder);
                this.stepX = stepX;
                this.stepY = stepY;
                this.needAlign = needAlign;
                this.alignAreaX = alignAreaX;
                this.alignAreaY = alignAreaY;
            }

            public MicrographMerge(String pathToFolder, int stepX, int stepY)
            {
                FindFiles(pathToFolder);
                this.stepX = stepX;
                this.stepY = stepY;

                // настройки по умолчанию
                this.needAlign = true;
                this.alignAreaX = stepX / 4;
                this.alignAreaY = stepY / 4;
            }
        }

        String folderPath = "",
               sourcePath = "";
        private void btnPeek_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo di = new DirectoryInfo(fbd.SelectedPath);
                sourcePath = fbd.SelectedPath;
                lblPath.Text = fbd.SelectedPath;
                folderPath = Path.GetDirectoryName(sourcePath);
            }
        }

        private void btnGlue_Click(object sender, EventArgs e)
        {
            MicrographMerge micMerge = new MicrographMerge(sourcePath, trkBrStep.Value + deltaX, trkBrStep.Value + deltaY, true, trkBrAlign.Value, trkBrAlign.Value);
            Bitmap bmp = micMerge.GetWholePicture();
            bmp.Save(folderPath + "\\picFromClass.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        }

        MicrographMerge micMergeCalibr;
        int width = 0,
            height = 0,
            deltaX = 0,
            deltaY = 0;
        private void btnCalibr_Click(object sender, EventArgs e)
        {
            micMergeCalibr = new MicrographMerge(sourcePath, 0, 0, true, 0, 0);
            pictureBoxCalibr.Image = micMergeCalibr.GetCalibrPicture(0, 0, 0, 0);

            width = micMergeCalibr.getWidth();
            height = micMergeCalibr.getHeight();

            trkBrStep.Maximum = Math.Min(width, height);
            trkBrAlign.Maximum = (Math.Min(width, height) / 2);
            trkBrAlign.Maximum = 1;

            if (width > height)
            {
                deltaX = width - height;
                deltaY = 0;
            }
            else
            {
                deltaX = 0;
                deltaY = height - width;
            }
        }

        private void PhotoMerge_Load(object sender, EventArgs e)
        {
            
        }

        private void trkBrAlign_Scroll(object sender, EventArgs e)
        {
            lblAlign.Text = "Ширина сглаживания : " + trkBrAlign.Value.ToString() + " пикс.";
            pictureBoxCalibr.Image = micMergeCalibr.GetCalibrPicture(trkBrStep.Value + deltaX, trkBrStep.Value + deltaY, trkBrAlign.Value, trkBrAlign.Value);
            GC.Collect();
            pictureBoxCalibr.Refresh();
        }

        private void trkBrStep_Scroll(object sender, EventArgs e)
        {
            GC.Collect();
            int min = Math.Min(width, height);
            lblStep.Text = "Сдвиг по X: " + trkBrStep.Value.ToString() + " пикс.";
            pictureBoxCalibr.Image = micMergeCalibr.GetCalibrPicture(trkBrStep.Value + deltaX, trkBrStep.Value + deltaY, trkBrAlign.Value, trkBrAlign.Value);

            trkBrAlign.Maximum = trkBrStep.Value / 2;
        }
    }
}
