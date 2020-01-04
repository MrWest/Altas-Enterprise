using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Presentation.Features
{
    public class ConvinientReportPaginator : DocumentPaginator
    {
        private Typeface typeface;
        private double fontSize;
        private double margin;
        private Size pageSize;
        private int rowsPerPage;
        private int pageCount;
        private BlockCollection blockCollection;
        private string reportName;
        private double totalCount;
        private double usableWidth;
        private double pixelsFator;


        private FormattedText GetFormattedText(string text)
        {
            return GetFormattedText(text, typeface);
        }

        private FormattedText GetFormattedText(string text, Typeface typeface)
        {
            return new FormattedText(
                text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                typeface, fontSize, Brushes.Black);
        }
        private FormattedText GetFormattedText(string text, Typeface typeface, double fontSize2, Brush brush)
        {
            return new FormattedText(
                text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                typeface, fontSize2, brush);
        }
        private FormattedText GetFormattedText(string text, Typeface typeface, double fontSize2, Brush brush, FontWeight fontWeight, TextAlignment textAlignment)
        {
            var formatted = new FormattedText(
                text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                typeface, fontSize2, brush);

            formatted.SetFontWeight( fontWeight);
            formatted.TextAlignment = textAlignment;

            return formatted;
            
        }
        private FormattedText GetHeaderFormattedText(string text, Typeface typeface)
        {
            return new FormattedText(
                text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                typeface, 18, Brushes.LightGray);
        }
        private FormattedText GetHeaderFormattedText2(string text, Typeface typeface)
        {
            return new FormattedText(
                text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                typeface, 12, Brushes.DarkGray);
        }
        public ConvinientReportPaginator(BlockCollection blockCollection, Typeface typeface, double fontSize, double margin, Size pageSize, double pixelsWidth, string reportName)
        {
            this.blockCollection = blockCollection;
            this.typeface = typeface;
            this.fontSize = fontSize;
            this.margin = margin;
            this.pageSize = pageSize;
            this.reportName = reportName ?? "";
            usableWidth = pageSize.Width - margin * 2;
            pixelsFator = pageSize.Width / pixelsWidth;
            PaginateData();
        }
       
      

        public override Size PageSize
        {
            get { return pageSize; }
            set {
                pageSize = value;
                PaginateData();
            }
        }
        // and synchronously, when the page size changes.
        // It's never left in an indeterminate state.
        public override bool IsPageCountValid
        {
            get { return true; }
        }
        public override int PageCount
        {
            get { return pageCount; }
        }
        public override IDocumentPaginatorSource Source
        {
            get { return null; }
        }

        private void PaginateData()
        {   
            // Create a test string for the purposes of measurement.  
            FormattedText text = GetFormattedText("A");
            // Count the lines that fit on a page.     
            rowsPerPage = (int)((pageSize.Height - margin * 2) / text.Height);
            // Leave a row for the headings    and footer
            rowsPerPage -= 2;  
            IList<Table> tables = blockCollection.Cast<Table>().ToList();

           
             totalCount = tables.Sum(x=>x.RowGroups.Sum(rg=> rg.Rows.Count));

              pageCount = (int)Math.Ceiling(totalCount / rowsPerPage);
        }

       

        public override DocumentPage GetPage(int pageNumber)
        {
            // Create a test string for the purposes of measurement.
            FormattedText text = GetFormattedText("A");
            double col1_X = margin;
            double col2_X = col1_X + text.Width * 15;

            double text_Height = text.Height; // make it a constant for avoid blank cases

            

            // Calculate the range of rows that fits on this page.
            int minRow = pageNumber * rowsPerPage;
            int maxRow = minRow + rowsPerPage;
            // Create the visual for the page.
            DrawingVisual visual = new DrawingVisual();
            // Set the position to the top-left corner of the printable area.
            Point point = new Point(margin, margin- text_Height*2);
            Point pointcol2 = new Point(col2_X, point.Y+5);
            using (DrawingContext dc = visual.RenderOpen())
            {
                // Draw the column headers.
                Typeface columnHeaderTypeface = new Typeface(
                typeface.FontFamily, FontStyles.Normal, FontWeights.Bold,
                FontStretches.Normal);
                point.X = col1_X;
                text = GetHeaderFormattedText(Resources.Atlas, columnHeaderTypeface);
                dc.DrawText(text, new Point(margin, point.Y-4));
                text = GetHeaderFormattedText2(reportName, columnHeaderTypeface);
                point.X = col2_X;
                dc.DrawText(text, pointcol2);
                text = GetHeaderFormattedText2("reportName", columnHeaderTypeface); // in case is blank
                // Draw the line underneath.
                point.Y += text_Height+4;
                dc.DrawLine(new Pen(Brushes.Gray, 2),
                new Point(margin, point.Y),
                new Point(pageSize.Width - margin, point.Y));

                point.Y += text_Height;

                // Draw the column values.

                point.X = col1_X;
                for (int i = minRow; i < maxRow; i++)
                {
                    // Check for the end of the last (half-filled) page.
                    if (i > (totalCount - 1)) break;
                   

                    var currentRow = GetRow(i);

                   
                    foreach (TableCell currentRowCell in currentRow.Cells)
                    {
                        TextRange currentRowCellTextRange = new TextRange(currentRowCell.ContentStart, currentRowCell.ContentEnd);

                        Paragraph paragraph = ((Paragraph) (currentRowCell.Blocks.FirstBlock));

                        if (paragraph != null&&paragraph.Inlines.Count>0)
                        {

                            text = GetFormattedText(currentRowCellTextRange.Text, new Typeface(currentRowCell.FontFamily.Source), currentRowCell.FontSize, currentRowCell.Foreground, paragraph.Inlines.FirstInline.FontWeight, paragraph.TextAlignment);

                            if (paragraph.TextAlignment == TextAlignment.Left || paragraph.TextAlignment == TextAlignment.Justify)
                                point.X = GetXPosition(GetTable(currentRow), currentRow.Cells.IndexOf(currentRowCell), currentRow);

                            if (paragraph.TextAlignment == TextAlignment.Right)
                            {
                                if (currentRow.Cells.IndexOf(currentRowCell) < currentRow.Cells.Count - 1)
                                    point.X = GetXPosition(GetTable(currentRow), currentRow.Cells.IndexOf(currentRowCell) + 1, currentRow) - text.Width - 2;
                                else
                                {
                                    point.X = pageSize.Width - text.Width - margin - 3;
                                }
                            }




                            text.MaxTextWidth = GetXPosition(GetTable(currentRow), currentRow.Cells.IndexOf(currentRowCell) + 1, currentRow) - point.X;//GetMaxTextWidth(GetTable(currentRow), currentRow.Cells.IndexOf(currentRowCell), currentRowCell.ColumnSpan);
                            text.TextAlignment = currentRowCell.TextAlignment;
                            text.MaxLineCount = 1;
                            dc.DrawText(text, point);
                        }

                        
                    }

                   
                    point.Y += text_Height;
                }

                point.Y = pageSize.Height - text_Height * 2;
               
                columnHeaderTypeface = new Typeface(
                typeface.FontFamily, FontStyles.Normal, FontWeights.Bold,
                FontStretches.Normal);
               
                text = GetFormattedText((pageNumber  + 1).ToString(), columnHeaderTypeface);
                
                dc.DrawText(text, new Point((pageSize.Width-margin), point.Y));
            }
            return new DocumentPage(visual, pageSize, new Rect(pageSize),
            new Rect(pageSize));
        }

        private TableRow GetRow(int row)
        {
            IList<Table> tables = blockCollection.Cast<Table>().ToList();

            int index = 0;
           
            foreach (Table table in tables)
            {
                int tableRows = table.RowGroups.Sum(rg => rg.Rows.Count);
                int subindex = index;
                foreach (TableRowGroup tableRowGroup in table.RowGroups)
                {
                    int subRows = tableRowGroup.Rows.Count;
                    if (subRows + subindex > row)
                        return tableRowGroup.Rows[row - subindex];

                    subindex += subRows;
                }


                index += tableRows;
            }

            return new TableRow();
        }

        private Table GetTable(TableRow tableRow)
        {
            return ((TableRowGroup) tableRow.Parent).Parent as Table;
        }

        private double GetXPosition(Table table, int pindex, TableRow tableRow)
        {
            

            if (pindex == 0)
                return margin;
            //get my real index

            int index = GetRealIndex(pindex,tableRow);

         
            return GetXPositionII(table, index ) + 3;

            //if (table.Columns[index-1].Width.GridUnitType == GridUnitType.Auto)
            //{
            //    double restColumnWidth = table.Columns.Where(c=> c.Width.GridUnitType == GridUnitType.Pixel).Sum(c => c.Width.Value * pixelsFator);
               
            //    //if(tableRow.Cells.Any(tc=> tc.ColumnSpan>1)&& tableRow.Cells.Where(tc => tc.ColumnSpan > 1).All(tc => tableRow.Cells.IndexOf(tc)>index))
            //     return (usableWidth - restColumnWidth + GetXPosition(table, index - 1,tableRow) + 2) ;
            //}

            
            //return (table.Columns[index - 1].Width.Value * pixelsFator + GetXPosition(table, index - 1,tableRow) + 2);




        }

        private int GetRealIndex( int pindex, TableRow tableRow)
        {
            if (pindex == 0)
                return 0;
            return tableRow.Cells[pindex - 1].ColumnSpan + GetRealIndex(pindex - 1, tableRow);
        }


        private double GetXPositionII(Table table, int index)
        {

            if (index == 0)
                return margin;



            if (table.Columns[index - 1].Width.GridUnitType == GridUnitType.Auto)
            {
                double restColumnWidth = table.Columns.Where(c => c.Width.GridUnitType == GridUnitType.Pixel).Sum(c => c.Width.Value * pixelsFator);

                return (usableWidth - restColumnWidth + GetXPositionII(table, index - 1) + 2);
            }


            return (table.Columns[index - 1].Width.Value * pixelsFator + GetXPositionII(table, index - 1) + 2);




        }
        private double GetMaxTextWidth(Table table, int index, int columnSpan)
        {

            


            if (table.Columns[index].Width.GridUnitType == GridUnitType.Auto)
            {
                double restColumnWidth = table.Columns.Where(c => c.Width.GridUnitType == GridUnitType.Pixel).Sum(c => c.Width.Value * pixelsFator);

                return (usableWidth - restColumnWidth)*pixelsFator;
            }

            double rslt = table.Columns[index].Width.Value * pixelsFator;

            for (int i = 1; i < columnSpan; i++)
                rslt += GetMaxTextWidth(table, index + i, 0);


            return rslt;




        }

    }
}
