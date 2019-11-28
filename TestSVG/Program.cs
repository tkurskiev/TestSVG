using System;
using System.IO;
using Aspose.Pdf;

namespace TestSVG
{
    class Program
    {
        private static readonly string _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private static readonly string _testPath = $"{_desktopPath}\\test";

        private static readonly string _path = $"{_testPath}\\test.pdf";

        private static readonly string _cleanPath = $"{_testPath}\\clean.pdf";

        private static readonly string _output = $"{_testPath}\\aspTest.pdf";

        private static readonly string _svg = $"{_testPath}\\svg.svg";

        private static readonly string _svgHerb = $"{_testPath}\\test_gerb_2.svg";

        static void Main(string[] args)
        {
            // Instantiate License class and call its SetLicense method to use the license
            //Aspose.Pdf.License license = new Aspose.Pdf.License();
            //license.SetLicense("Aspose.Pdf.lic");

            #region Чушня

            // Set coordinates
            int lowerLeftX = 100;
            int lowerLeftY = 100;
            int upperRightX = 200;
            int upperRightY = 200;

            #endregion

            var document = new Document(new FileStream(_cleanPath, FileMode.Open, FileAccess.Read, FileShare.None));

            //var imageStream = new FileStream($"{_testPath}\\1.jpeg", FileMode.Open, FileAccess.Read, FileShare.None);
            var imageStream = new FileStream(Path.Combine(_testPath, _svgHerb), FileMode.Open, FileAccess.Read, FileShare.None);

            document.Pages.Add();

            // последняя страница
            var page = document.Pages[document.Pages.Count - 1];

            page.Resources.Images.Add(imageStream);
            page.Contents.Add(new Aspose.Pdf.Operators.GSave());

            #region Чушня

            // Create Rectangle and Matrix objects
            Aspose.Pdf.Rectangle rectangle = new Aspose.Pdf.Rectangle(lowerLeftX, lowerLeftY, upperRightX, upperRightY);

            Matrix matrix = new Matrix(new double[] { rectangle.URX - rectangle.LLX, 0, 0, rectangle.URY - rectangle.LLY, rectangle.LLX, rectangle.LLY });

            // Using ConcatenateMatrix (concatenate matrix) operator: defines how image must be placed
            page.Contents.Add(new Aspose.Pdf.Operators.ConcatenateMatrix(matrix));
            XImage ximage = page.Resources.Images[page.Resources.Images.Count];

            // Using Do operator: this operator draws image
            page.Contents.Add(new Aspose.Pdf.Operators.Do(ximage.Name));

            // Using GRestore operator: this operator restores graphics state
            page.Contents.Add(new Aspose.Pdf.Operators.GRestore());

            #endregion


            document.Save(_output);
        }
    }
}
