using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Ecm.Rex.Reports
{
    /// <summary>
    /// nhanvuong 
    /// display report details about Nhansu
    /// </summary>
    public partial class rptRex_Nhansu : DevExpress.XtraReports.UI.XtraReport
    {
        string label;
        public rptRex_Nhansu()
        {
            InitializeComponent();
        }


        public XRTable CreateXR_HeaderTable()
        {
            XRTable table = new XRTable();
            table.BeginInit();

            table.Borders = DevExpress.XtraPrinting.BorderSide.All;
            table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            int tableHeight = 0;
            int tableWidth = 0;
            XRTableRow row = new XRTableRow();
            row.Height = 40;
            tableHeight += row.Height;
            for (int j = 0; j < 5; j++)
            {
                XRTableCell cell = new XRTableCell();
                switch (j)
                {
                    case 0:
                        cell.Text = "Bộ phận";
                        cell.Width = 350;
                        tableWidth += cell.Width;
                        row.Cells.Add(cell);
                        cell.BackColor = Color.Gray;
                        break;

                    case 1:
                        cell.Text = "Mã Nhân sự";
                        cell.Width = 200;
                        tableWidth += cell.Width;
                        cell.BackColor = Color.Gray;
                        row.Cells.Add(cell);
                        break;

                    case 2:
                        cell.Text = "Họ tên Nhân sự";
                        cell.Width = 450;
                        tableWidth += cell.Width;
                        cell.BackColor = Color.Gray;
                        row.Cells.Add(cell);
                        break;

                    case 3:
                        cell.Text = " Giới tính";
                        cell.Width = 100;
                        tableWidth += cell.Width;
                        cell.BackColor = Color.Gray;
                        row.Cells.Add(cell);
                        break;

                    case 4:
                        cell.Text = label;
                        cell.Width = 300;
                        tableWidth += cell.Width;
                        cell.BackColor = Color.Gray;
                        row.Cells.Add(cell);
                        break;
                }
                table.Rows.Add(row);
            }
            tableWidth = 1050;
            table.Size = new Size(tableWidth, tableHeight);

            table.EndInit();
            table.EndInit();
            return table;
        }


        public XRTable CreateXRTable(DataSet ds_collection, string value, string column)
        {
            XRTable table = new XRTable();
            table.BeginInit();

            table.Borders = DevExpress.XtraPrinting.BorderSide.All;
            table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            int tableHeight = 0;
            int tableWidth = 0;
            try
            {
                DataRow[] dtr = ds_collection.Tables[0].Select(column + " = '" + value + "'");

                for (int i = 0; i < dtr.Length; i++)
                {
                    XRTableRow row = new XRTableRow();
                    row.Height = 40;
                    tableHeight += row.Height;


                    for (int j = 0; j < 5; j++)
                    {
                        XRTableCell cell = new XRTableCell();
                        switch (j)
                        {
                            case 0:
                                cell.Text = dtr[i]["Ten_Bophan"].ToString();
                                cell.Width = 350;
                                tableWidth += cell.Width;
                                row.Cells.Add(cell);
                                break;

                            case 1:
                                cell.Text = dtr[i]["Ma_Nhansu"].ToString();
                                cell.Width = 200;
                                tableWidth += cell.Width;
                                row.Cells.Add(cell);
                                break;

                            case 2:
                                cell.Text = dtr[i]["Hoten_Nhansu"].ToString();
                                cell.Width = 450;
                                tableWidth += cell.Width;
                                row.Cells.Add(cell);
                                break;

                            case 3:
                                cell.Text = dtr[i]["Gioitinh"].ToString();
                                cell.Width = 100;
                                tableWidth += cell.Width;
                                row.Cells.Add(cell);
                                break;

                            case 4:
                                cell.Text = dtr[i][column].ToString();
                                cell.Width = 300;
                                tableWidth += cell.Width;
                                row.Cells.Add(cell);
                                break;
                        }
                        //}
                        tableWidth += cell.Width;
                        row.Cells.Add(cell);
                    }
                    table.Rows.Add(row);
                }
                tableWidth = 1050;
                table.Size = new Size(tableWidth, tableHeight);

                table.EndInit();
                return table;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        public void setInfo(DataSet dsCollection, string value, string column, string Label)
        {
            this.label = Label;
            try
            {
                this.xrLb_Tieuchi.Text = label.ToUpper();
                this.PageHeader.Controls.Add(CreateXR_HeaderTable());
                this.Detail.Controls.Add(CreateXRTable(dsCollection, value, column));
                this.lblNgay.Text = DateTime.Today.Day.ToString();
                this.lblThang.Text = DateTime.Today.Month.ToString();
                this.lblNam.Text = DateTime.Today.Year.ToString();
            }
            catch (Exception ex)
            { ex.ToString(); }
        }
    }
}
