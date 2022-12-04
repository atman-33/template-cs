using System.Windows;
using System.Windows.Controls;

namespace Template.WPF.Views
{
    /// <summary>
    /// Interaction logic for TestDataGridView
    /// </summary>
    public partial class SampleTableEdit1View : UserControl
    {
        public SampleTableEdit1View()
        {
            InitializeComponent();
        }

        //private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{

        //}

        //// DataGridのカラム名を変更、もしくは非表示の制御を行う処理
        //private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        //{
        //    this.SampleTableDataGrid.SelectedIndex = -1;

        //    switch (e.PropertyName)
        //    {
        //        case "SampleId":
        //            e.Column.Visibility = Visibility.Collapsed;
        //            break;

        //        case "SampleText":
        //            e.Column.Header = "サンプルテキスト";
        //            break;

        //        case "SampleValue":
        //            e.Column.Header = "サンプル値";
        //            break;

        //        case "SampleDate":
        //            e.Column.Header = "サンプル日付";
        //            break;

        //        default:
        //            break;

        //    }
        //}
    }
}
