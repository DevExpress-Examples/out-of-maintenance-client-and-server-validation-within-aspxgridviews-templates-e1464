Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Collections.Generic
Imports System.Web.UI
Imports DevExpress.Web

Namespace GridViewCustomValidationInTemplates
	Partial Public Class _Default
		Inherits System.Web.UI.Page
		Protected Overrides Sub OnInit(ByVal e As EventArgs)
			MyBase.OnInit(e)
			If (Not IsPostBack) AndAlso (Not IsCallback) Then
				gvGridView1.DataSource = DataProvider.GetData()
				gvGridView1.DataBind()
			End If
		End Sub
		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

		End Sub

		' Event handlers
		Protected Sub OnSaveButtonClick(ByVal sender As Object, ByVal e As EventArgs)
			Dim minVisibleIndex As Integer = gvGridView1.VisibleStartIndex
			Dim maxVisibleIndex As Integer = gvGridView1.VisibleStartIndex + gvGridView1.VisibleRowCount - 1
			Dim valueColumn As GridViewDataColumn = CType(gvGridView1.Columns("Value"), GridViewDataColumn)
			For visibleIndex As Integer = minVisibleIndex To maxVisibleIndex - 1
				Dim editor As ASPxEdit = CType(gvGridView1.FindRowCellTemplateControl(visibleIndex, valueColumn, "tbValue"), ASPxEdit)
				If editor.IsValid Then
					Dim id As Integer = CInt(Fix(gvGridView1.GetRowValues(visibleIndex, "ID")))
					Dim record As Record = DataProvider.FindRecordByID(id)
					If editor.Value IsNot Nothing Then
						record.Value = Integer.Parse(TryCast(editor.Value, String))
					Else
						record.Value = Nothing
					End If
				End If
			Next visibleIndex
		End Sub
		Protected Sub OnValueValidation(ByVal sender As Object, ByVal e As ValidationEventArgs)
			Dim edit As ASPxEdit = CType(sender, ASPxEdit)
			If edit.Value Is Nothing Then
				Return
			End If

			Dim value As Integer
			If (Not Integer.TryParse(TryCast(edit.Value, String), value)) Then
				e.IsValid = False
				e.ErrorText = "Input must be a number"
				Return
			End If

			Dim templateContainer As GridViewDataItemTemplateContainer = FindDataItemTemplateContainer(edit)
			Dim bounds() As Object = CType(gvGridView1.GetRowValues(templateContainer.VisibleIndex, "Min", "Max"), Object())
			Dim min As Nullable(Of Integer) = CType(bounds(0), Nullable(Of Integer))
			Dim max As Nullable(Of Integer) = CType(bounds(1), Nullable(Of Integer))

			If min.HasValue AndAlso value < min.Value Then
				e.IsValid = False
				e.ErrorText = "Value should be not less than Min"
			End If
			If max.HasValue AndAlso value > max.Value Then
				e.IsValid = False
				e.ErrorText = "Value should be not greater than Max"
			End If
		End Sub

		' Utils
		Private Function FindDataItemTemplateContainer(ByVal templateControl As Control) As GridViewDataItemTemplateContainer
			Dim namingContainer As Control = templateControl.NamingContainer
			Dim container As GridViewDataItemTemplateContainer = Nothing
			Do While container Is Nothing
				container = TryCast(namingContainer, GridViewDataItemTemplateContainer)
				namingContainer = namingContainer.NamingContainer
			Loop
			Return container
		End Function
		Protected Function GetValueTextBoxClientValidationHandler(ByVal min As Object, ByVal max As Object) As String
			Dim minValue As Nullable(Of Integer) = CType(min, Nullable(Of Integer))
			Dim maxValue As Nullable(Of Integer) = CType(max, Nullable(Of Integer))
			Dim sb As New StringBuilder("function(s, e) { OnValueValidation(s, e, ")
			If minValue.HasValue Then
				sb.Append(minValue.Value.ToString())
			Else
				sb.Append("null")
			End If
			sb.Append(", ")
			If maxValue.HasValue Then
				sb.Append(maxValue.Value.ToString())
			Else
				sb.Append("null")
			End If
			sb.Append("); }")
			Return sb.ToString()
		End Function
	End Class
End Namespace