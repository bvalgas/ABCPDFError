using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSupergoo.ABCpdf10;
using WebSupergoo.ABCpdf10.Objects;

namespace TestRotate
{
    class Program
    {
        static void Main(string[] args)
        {
            CanvasToEntity.Generate();
        }
    }

    public static class CanvasToEntity
    {
        private static void InstallLicence()
        {
            if (!XSettings.LicenseValid)
            {
                XSettings.InstallLicense(@"XeJREBodo/8GtyBWbP6Hadj3KuVJPdtypp278bXcOQATynTAfDv6dOMqIlo18kIFBw==");
            }
        }
        public static void Generate()
        {
            InstallLicence();
            Doc theDoc = new Doc();
            theDoc.FontSize = 12;
            theDoc.Width = 1000;
            theDoc.TopDown = true; //if you change the direction of the build the rotate function doesn't work properly
            theDoc.TextStyle.Indent = 48;
            for (int i2 = 1; i2 <= 8; i2++)
            {
                int theAngle = i2 * 45;
                theDoc.Pos.String = "436 500";
                theDoc.Transform.Reset();
                theDoc.Transform.Rotate(theAngle, 436, 500);
                theDoc.AddText("Rotated " + theAngle.ToString());
            }
            theDoc.Transform.Reset();
            theDoc.Rect.Left = 100;
            theDoc.Rect.Bottom = 100;
            theDoc.Rect.Width = 100;
            theDoc.Rect.Height = 100;
            theDoc.Transform.Rotate(265, 100, 200);
            var xImage = new XImage();
            xImage.SetFile(Environment.CurrentDirectory + "/teste01.png");
            //inserting an image png with transparent background, ABCPDF turn the background in black before set the alpha channel
            int i = theDoc.AddImageObject(xImage, false);
            ImageLayer im = (ImageLayer)theDoc.ObjectSoup[i];
            im.PixMap.AutoFix = true;

            im.PixMap.SetAlpha(100);

            theDoc.Save(Environment.CurrentDirectory + "/arquivodetestes.pdf");
            theDoc.Clear();
        }
    }
}
