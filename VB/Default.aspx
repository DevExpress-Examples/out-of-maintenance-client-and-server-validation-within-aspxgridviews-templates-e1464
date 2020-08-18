<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="GridViewCustomValidationInTemplates._Default" %>
<%@ Register Assembly="DevExpress.Web.v14.1" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v14.1" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Untitled Page</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<script type="text/javascript">
			function OnValueValidation(editor, args, min, max) {
				var value = editor.GetValue();
				if(IsDefined(value)) {
					if(IsDefined(min)) {
						var validateMin = function(value, min, max) { return value >= min; };
						ValidateValue(args, value, validateMin, min, max, "Value should be not less than Min");
					}
					if(IsDefined(max)) {
						var validateMax = function(value, min, max) { return value <= max; };
						ValidateValue(args, value, validateMax, min, max, "Value should be not greater than Max");
					}
				}
			}
			function ValidateValue(args, value, validationProc, min, max, errorText) {
				var isValid = validationProc(value, min, max);
				if(!isValid) {
					args.isValid = false;
					args.errorText = errorText;
				}
			}
			function IsDefined(value) {
				return typeof(value) != "undefined" && value != null;
			}
		</script>
		<dxwgv:ASPxGridView ID="gvGridView1" runat="server" AutoGenerateColumns="False">
			<Settings ShowFooter="true" />
			<Templates>
				<FooterRow>
					<dxe:ASPxButton ID="btnSave" runat="server" Text="Save" ValidationGroup="ValueValidationGroup" OnClick="OnSaveButtonClick" />
				</FooterRow>
			</Templates>
			<Columns>
				<dxwgv:GridViewDataTextColumn FieldName="ID" VisibleIndex="0" />
				<dxwgv:GridViewDataTextColumn FieldName="Min" VisibleIndex="1" />
				<dxwgv:GridViewDataTextColumn FieldName="Max" VisibleIndex="2" />
				<dxwgv:GridViewDataTextColumn Caption="Value" VisibleIndex="3">
					<DataItemTemplate>
						<dxe:ASPxTextBox ID="tbValue" runat="server" Width="70px" Text='<%#Eval("Value")%>'
							OnValidation="OnValueValidation"
							ClientSideEvents-Validation='<%#GetValueTextBoxClientValidationHandler(Eval("Min"), Eval("Max"))%>'>
							<ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic">
								<RegularExpression ErrorText="Input must be a number" ValidationExpression="^[+-]?[\d]*$" />
							</ValidationSettings>
						</dxe:ASPxTextBox>
					</DataItemTemplate>
				</dxwgv:GridViewDataTextColumn>
			</Columns>
		</dxwgv:ASPxGridView>
	</div>
	</form>
</body>
</html>