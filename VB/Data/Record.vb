Imports Microsoft.VisualBasic
Imports System

Namespace GridViewCustomValidationInTemplates

	Public Class Record
		Private _id As Integer
		Private _min As Nullable(Of Integer)
		Private _max As Nullable(Of Integer)
		Private _value As Nullable(Of Integer)

		Public Sub New(ByVal id As Integer)
			Me.New(id, Nothing)
		End Sub
		Public Sub New(ByVal id As Integer, ByVal value As Nullable(Of Integer))
			Me.New(id, value, Nothing, Nothing)
		End Sub
		Public Sub New(ByVal id As Integer, ByVal value As Nullable(Of Integer), ByVal min As Nullable(Of Integer), ByVal max As Nullable(Of Integer))
			Me._id = id
			Me._value = value
			Me._min = min
			Me._max = max
		End Sub

		Public ReadOnly Property ID() As Integer
			Get
				Return _id
			End Get
		End Property
		Public ReadOnly Property Min() As Nullable(Of Integer)
			Get
				Return _min
			End Get
		End Property
		Public ReadOnly Property Max() As Nullable(Of Integer)
			Get
				Return _max
			End Get
		End Property
		Public Property Value() As Nullable(Of Integer)
			Get
				Return _value
			End Get
			Set(ByVal value As Nullable(Of Integer))
				_value = value
			End Set
		End Property
	End Class

End Namespace