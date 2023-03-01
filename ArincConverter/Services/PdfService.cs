using Microsoft.Extensions.Logging;
using ArincConverter.Helpers;
using ArincConverter.Contracts;
using iText.Kernel.Pdf;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using Document = iText.Layout.Document;
using Paragraph = iText.Layout.Element.Paragraph;

namespace ArincConverter.Services
{
    public class PdfService : IPdfService
    {
        private readonly ILogger<PdfService> _logger;

        public PdfService(ILogger<PdfService> logger)
        {
            _logger = logger;
        }

        public string TextToPdf(string header, string text)
        {
            try
            {
                if (text.IsNotNullOrEmpty())
                {
                    using var ms = new MemoryStream();
                    using var pdf = new PdfDocument(new PdfWriter(ms));
                    using (var doc = new Document(pdf))
                    {
                        doc.Add(new Paragraph(header)
                            .SetFont(PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD))
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(12)
                            .SetFontColor(ColorConstants.BLACK));

                        doc.Add(new Paragraph(text)
                                .SetFont(PdfFontFactory.CreateFont(StandardFonts.COURIER))
                                .SetTextAlignment(TextAlignment.JUSTIFIED)
                                .SetFontSize(12)
                                .SetFontColor(ColorConstants.BLACK));
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while converting text to PDF");
            }

            return null;
        }

        public string TextToPdf(List<(string, string, string, string)> list)
        {
            try
            {
                using var ms = new MemoryStream();
                using var pdf = new PdfDocument(new PdfWriter(ms));
                using (var doc = new Document(pdf))
                {
                    foreach (var tpl in list)
                    {
                        if (tpl.Item1.IsNotNullOrEmpty())
                        {
                            doc.Add(new Paragraph(tpl.Item1)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFont(PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD))
                                .SetFontSize(12)
                                .SetFontColor(ColorConstants.BLACK));
                        }

                        if (tpl.Item2.IsNotNullOrEmpty())
                        {
                            doc.Add(new Paragraph(tpl.Item2)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFont(PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD))
                                .SetFontSize(12)
                                .SetFontColor(ColorConstants.BLACK));
                        }

                        if (tpl.Item3.IsNotNullOrEmpty())
                        {
                            doc.Add(new Paragraph(tpl.Item3)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFont(PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD))
                                .SetFontSize(10)
                                .SetFontColor(ColorConstants.BLACK));
                        }

                        doc.Add(new Paragraph(tpl.Item4)
                            .SetTextAlignment(TextAlignment.JUSTIFIED)
                            .SetFont(PdfFontFactory.CreateFont(StandardFonts.COURIER))
                            .SetFontSize(10)
                            .SetFontColor(ColorConstants.BLACK));
                    }
                }

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while converting text to PDF");
                return null;
            }
        }

        public string TextToPdf(Dictionary<string, List<string>> dictionary)
        {
            try
            {
                using var ms = new MemoryStream();
                using var pdf = new PdfDocument(new PdfWriter(ms));
                using (var doc = new Document(pdf))
                {
                    foreach (var item in dictionary)
                    {
                        doc.Add(new Paragraph(item.Key)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFont(PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD))
                                .SetFontSize(12)
                                .SetFontColor(ColorConstants.BLACK));

                        foreach (var text in item.Value)
                        {
                            doc.Add(new Paragraph(text)
                            .SetTextAlignment(TextAlignment.JUSTIFIED)
                            .SetFont(PdfFontFactory.CreateFont(StandardFonts.COURIER))
                            .SetFontSize(12)
                            .SetFontColor(ColorConstants.BLACK));
                        }

                        doc.Add(new Paragraph(Environment.NewLine));
                    }
                }

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while converting text to PDF");
                return null;
            }
        }

        public string ImagesToPdf(List<byte[]> files)
        {
            try
            {
                var landscapeWidth = 756;
                var landscapeHeight = 567;

                using var ms = new MemoryStream();
                using var pdf = new PdfDocument(new PdfWriter(ms));
                using (var doc = new Document(pdf, PageSize.A4.Rotate()))
                {
                    foreach (var file in files)
                    {
                        var data = ImageDataFactory.Create(file);
                        var img = new Image(data);

                        img.ScaleToFit(landscapeWidth, landscapeHeight);
                        doc.SetMargins(15, 0, 0, 20);
                        doc.Add(img);
                    }
                }

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while converting images to PDF");
                return null;
            }
        }
    }
}
