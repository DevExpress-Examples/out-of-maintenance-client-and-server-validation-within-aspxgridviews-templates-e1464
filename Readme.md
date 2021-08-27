<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128537056/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1464)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [DataProvider.cs](./CS/Data/DataProvider.cs) (VB: [DataProvider.vb](./VB/Data/DataProvider.vb))
* [Record.cs](./CS/Data/Record.cs) (VB: [Record.vb](./VB/Data/Record.vb))
* [Default.aspx](./CS/Default.aspx) (VB: [Default.aspx](./VB/Default.aspx))
* [Default.aspx.cs](./CS/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/Default.aspx.vb))
<!-- default file list end -->
# Client and server validation within ASPxGridView's templates
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e1464/)**
<!-- run online end -->


<p>This example demonstrates how to perform validation of editors placed within ASPxGridView's templates. Here both the integrated validation via ValidationSettings.RegularExpression and a custom validation on Validation events are used.</p><p>Another approach used here is the multi-row editing, which means that the grid allows you to instantly edit several row values. Thereby, the grid's edit form is not used in the example at all. (The multi-row editing approach is also described in example E324.)</p><p>It's important that validation in this example is performed both on the client and server, since we cannot guarantee that the client sends us valid data. All validation rules are implemented both on the client and server, but the client validation serves only as an input helper. The final word always rests with the server.</p><p>The end user inputs some values into text boxes and clicks the Save button. Then, the client validation is performed. In a regular case, if some editors contain invalid values, the end user will be informed about the mistakes he/she made, and a postback won't be sent to the server. However, even if the client validation passes despite that some of the editors contain invalid values, the server will validate all the editors, and only valid editor data will be sent to the database (in this example, a simple List<Record> stored in the Session is used as a data storage).</p>

<br/>


