using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace YUI.YUtil
{
    /// <summary>
    /// DataGrid辅助类
    /// </summary>
    public static class YDataGridHelper
    {
        /// <summary>  
        /// 获取DataGrid控件单元格  
        /// </summary>  
        /// <param name="dataGrid">DataGrid控件</param>  
        /// <param name="rowIndex">单元格所在的行号</param>  
        /// <param name="columnIndex">单元格所在的列号</param>  
        /// <returns>指定的单元格</returns>  
        public static DataGridCell GetCell(this DataGrid dataGrid, int rowIndex, int columnIndex)
        {
            var rowContainer = dataGrid.GetRow(rowIndex);
            if (rowContainer == null) return null;

            var presenter = YControlHelper.GetVisualChild<DataGridCellsPresenter>(rowContainer);
            if (presenter == null) return null;

            var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
            if (cell != null) return cell;

            dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[columnIndex]);
            cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
            return cell;
        }

        /// <summary>  
        /// 获取DataGrid的行  
        /// </summary>  
        /// <param name="dataGrid">DataGrid控件</param>  
        /// <param name="rowIndex">DataGrid行号</param>  
        /// <returns>指定的行号</returns>  
        public static DataGridRow GetRow(this DataGrid dataGrid, int rowIndex)
        {
            var rowContainer = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (rowContainer != null) return rowContainer;

            dataGrid.UpdateLayout();
            dataGrid.ScrollIntoView(dataGrid.Items[rowIndex]);
            rowContainer = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            return rowContainer;
        }

        /// <summary>
        /// 获取Cell
        /// </summary>
        /// <param name="dataGridCellInfo"></param>
        /// <returns></returns>
        public static DataGridCell GetCell(DataGridCellInfo dataGridCellInfo)
        {
            if (!dataGridCellInfo.IsValid)
                return null;

            var cellContent = dataGridCellInfo.Column.GetCellContent(dataGridCellInfo.Item);
            return (DataGridCell)cellContent?.Parent;
        }

        /// <summary>
        /// 获取单元格内容
        /// </summary>
        /// <param name="dataGridCellInfo"></param>
        /// <returns></returns>
        public static string GetCellString(DataGridCellInfo dataGridCellInfo)
        {
            var row = dataGridCellInfo.Item as DataRowView;

            return row?[dataGridCellInfo.Column.DisplayIndex].ToString().Trim();
        }

        /// <summary>
        /// 获取单元格所在行
        /// </summary>
        /// <param name="dataGridCell"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static int GetRowIndex(DataGridCell dataGridCell, DataGrid dataGrid = null)
        {
            if (dataGridCell == null) return -1;

            var rowDataItemProperty = dataGridCell.GetType().GetProperty("RowDataItem", BindingFlags.Instance | BindingFlags.NonPublic);

            if (rowDataItemProperty == null) return -1;

            try
            {
                if (dataGrid == null)
                    dataGrid = GetDataGridFromChild(dataGridCell);
            }
            catch
            {
                return -1;
            }

            return dataGrid.Items.IndexOf(rowDataItemProperty.GetValue(dataGridCell, null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGridPart"></param>
        /// <returns></returns>
        public static DataGrid GetDataGridFromChild(DependencyObject dataGridPart)
        {
            if (VisualTreeHelper.GetParent(dataGridPart) == null)
            {
                throw new NullReferenceException("Control is null.");
            }
            if (VisualTreeHelper.GetParent(dataGridPart) is DataGrid)
            {
                return (DataGrid)VisualTreeHelper.GetParent(dataGridPart);
            }

            return GetDataGridFromChild(VisualTreeHelper.GetParent(dataGridPart));
        }

        /// <summary>
        /// 判断是否点击在DataGrid的行上
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool IsPointOnRow(DataGrid dataGrid, Point point)
        {
            var element = dataGrid.InputHitTest(point);
            var target = element as DependencyObject;

            while (target != null)
            {
                if (target is DataGridRow)
                    break;

                target = VisualTreeHelper.GetParent(target);
            }

            return target != null;
        }

        /// <summary>
        /// 获取正在编辑的单元格
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static List<DataGridCell> GetEditingCells(this DataGrid dataGrid)
        {
            var result = new List<DataGridCell>();

            if (dataGrid == null)
                return result;

            var columns = dataGrid.Columns.Count;
            var rows = dataGrid.Items.Count;

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    var cell = dataGrid.GetCell(i, j);
                    if (cell != null && cell.IsEditing)
                        result.Add(cell);
                }
            }

            return result;
        }

        /// <summary>
        /// 设置指定单元格的背景色
        /// </summary>
        public static void SetCellBackground(this DataGrid dataGrid, int rowIndex, int columnIndex, Brush brush, string toolTip = null)
        {
            try
            {
                var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
                var presenter = YControlHelper.GetVisualChild<DataGridCellsPresenter>(row);
                var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                var baseStyle = dataGrid.FindResource("DataGridCellStyle") as Style;
                var celStyle = new Style(typeof(DataGridCell), baseStyle);
                celStyle.Setters.Add(new Setter { Property = Control.BackgroundProperty, Value = brush });
                cell.Style = celStyle;
                cell.ToolTip = toolTip;
            }
            catch (Exception)
            {
                //
            }
        }
    }
}
