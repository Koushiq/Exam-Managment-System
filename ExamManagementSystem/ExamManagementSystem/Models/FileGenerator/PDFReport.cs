using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models.FileGenerator
{
    public class PDFReport
    {
        private PdfDocument document;
        private PdfPage page;
        private XGraphics gfx;
        private XFont font;
        private int lineSpace;
        private string filename;
        private double x=125;
        private double y=80;
        public PDFReport(string filename,string fontName,int fontSize,int lineSpace)
        {
            this.document = new PdfDocument();
            this.page = this.document.AddPage();
            this.gfx = XGraphics.FromPdfPage(page);
            this.font = new XFont(fontName,fontSize);
            this.lineSpace = lineSpace;
            this.filename = filename;
        }

        [Obsolete]
        public void Generate(string[] data)
        {
            for(int i=0;i<data.Length;i++)
            {
                this.gfx.DrawString(data[i], this.font, XBrushes.Black, new XRect(125, 80 + this.lineSpace, page.Width, page.Height), XStringFormat.TopLeft);
            }
            this.document.Save(this.filename);
            
        }

       



    }
}