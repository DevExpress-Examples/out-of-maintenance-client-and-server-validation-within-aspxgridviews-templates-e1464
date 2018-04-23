Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.SessionState

Namespace GridViewCustomValidationInTemplates

	Public NotInheritable Class DataProvider
		Private Const SessionKey As String = "MySampleData"

		Private Sub New()
		End Sub
		Private Shared ReadOnly Property Session() As HttpSessionState
			Get
				Return HttpContext.Current.Session
			End Get
		End Property

		Public Shared Function GetData() As IList(Of Record)
			Dim data As IList(Of Record) = TryCast(Session(SessionKey), IList(Of Record))
			If data Is Nothing Then
				data = CreateData()
				Session(SessionKey) = data
			End If
			Return data
		End Function
		Public Shared Function FindRecordByID(ByVal id As Integer) As Record
			Dim data As IList(Of Record) = GetData()
			For Each record As Record In data
				If record.ID = id Then
					Return record
				End If
			Next record
			Return Nothing
		End Function

		Private Shared Function CreateData() As IList(Of Record)
			Return New List(Of Record)(New Record() { New Record(1), New Record(2, Nothing), New Record(3, 1), New Record(4, 2, 1, 3), New Record(5, 0, 1, 3), New Record(6, 4, 1, 3), New Record(7, 4, 1, Nothing), New Record(8, 4, 1, Nothing), New Record(9, Nothing, 1, 2), New Record(10, -2, -1, 1) })
		End Function
	End Class

End Namespace