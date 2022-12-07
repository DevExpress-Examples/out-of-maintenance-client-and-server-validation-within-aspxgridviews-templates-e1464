using System;
using System.Text;
using System.Collections.Generic;
using System.Web.UI;
using DevExpress.Web;

namespace GridViewCustomValidationInTemplates {
    public partial class _Default : System.Web.UI.Page {
        protected override void OnInit(EventArgs e) {
            base.OnInit(e);
            if(!IsPostBack && !IsCallback) {
                gvGridView1.DataSource = DataProvider.GetData();
                gvGridView1.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e) {

        }

        // Event handlers
        protected void OnSaveButtonClick(object sender, EventArgs e) {
            int minVisibleIndex = gvGridView1.VisibleStartIndex;
            int maxVisibleIndex = gvGridView1.VisibleStartIndex + gvGridView1.VisibleRowCount - 1;
            GridViewDataColumn valueColumn = (GridViewDataColumn)gvGridView1.Columns["Value"];
            for(int visibleIndex = minVisibleIndex; visibleIndex < maxVisibleIndex; visibleIndex++) {
                ASPxEdit editor = (ASPxEdit)gvGridView1.FindRowCellTemplateControl(visibleIndex, valueColumn, "tbValue");
                if(editor.IsValid) {
                    int id = (int)gvGridView1.GetRowValues(visibleIndex, "ID");
                    Record record = DataProvider.FindRecordByID(id);
                    if(editor.Value != null)
                        record.Value = int.Parse(editor.Value as string);
                    else
                        record.Value = null;
                }
            }
        }
        protected void OnValueValidation(object sender, ValidationEventArgs e) {
            ASPxEdit edit = (ASPxEdit)sender;
            if(edit.Value == null)
                return;
            
            int value;
            if(!int.TryParse(edit.Value as string, out value)) {
                e.IsValid = false;
                e.ErrorText = "Input must be a number";
                return;
            }

            GridViewDataItemTemplateContainer templateContainer = FindDataItemTemplateContainer(edit);
            object[] bounds = (object[])gvGridView1.GetRowValues(templateContainer.VisibleIndex, "Min", "Max");
            int? min = (int?)bounds[0];
            int? max = (int?)bounds[1];
            
            if(min.HasValue && value < min.Value) {
                e.IsValid = false;
                e.ErrorText = "Value should be not less than Min";
            }
            if(max.HasValue && value > max.Value) {
                e.IsValid = false;
                e.ErrorText = "Value should be not greater than Max";
            }
        }

        // Utils
        private GridViewDataItemTemplateContainer FindDataItemTemplateContainer(Control templateControl) {
            Control namingContainer = templateControl.NamingContainer;
            GridViewDataItemTemplateContainer container = null;
            while(container == null) {
                container = namingContainer as GridViewDataItemTemplateContainer;
                namingContainer = namingContainer.NamingContainer;
            }
            return container;
        }
        protected string GetValueTextBoxClientValidationHandler(object min, object max) {
            int? minValue = (int?)min;
            int? maxValue = (int?)max;
            StringBuilder sb = new StringBuilder("function(s, e) { OnValueValidation(s, e, ");
            sb.Append(minValue.HasValue ? minValue.Value.ToString() : "null");
            sb.Append(", ");
            sb.Append(maxValue.HasValue ? maxValue.Value.ToString() : "null");
            sb.Append("); }");
            return sb.ToString();
        }
    }
}